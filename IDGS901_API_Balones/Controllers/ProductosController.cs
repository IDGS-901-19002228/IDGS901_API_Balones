using Microsoft.AspNetCore.Mvc;
using IDGS901_API_Balones.Context;
using IDGS901_API_Balones.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;

namespace IDGS901_API_Balones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : Controller
    {
        private readonly AppDbContext _context;
        public ProductosController(AppDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                List<Productos> listProductos = new List<Productos>();

                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_GetAllProductos";
                SqlDataReader read = comando.ExecuteReader();
                while (read.Read())
                {
                    Productos product = new Productos();

                    product.Id = (int)read["Id"];
                    product.Nombre = (string)read["Nombre"];
                    product.Imagen = (string)read["Imagen"];
                    product.Descripcion = (string)read["Descripcion"];
                    product.Precio = (int)read["Precio"];
                    product.Stock = (int)read["Stock"];
                    product.Estatus = (string)read["Estatus"];


                    listProductos.Add(product);
                }
                conexion.Close();
                return Json(listProductos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<Productos> Post([FromBody] Productos product)
        {
            try
            {
                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_InsertarProducto";

                comando.Parameters.Add("@Nombre", System.Data.SqlDbType.NVarChar).Value = product.Nombre;
                comando.Parameters.Add("@Imagen", System.Data.SqlDbType.NVarChar).Value = product.Imagen;
                comando.Parameters.Add("@Descripcion", System.Data.SqlDbType.NVarChar).Value = product.Descripcion;
                comando.Parameters.Add("@Precio", System.Data.SqlDbType.Int).Value = product.Precio;
                comando.Parameters.Add("@Stock", System.Data.SqlDbType.NVarChar).Value = product.Stock;

                comando.ExecuteNonQuery();
                conexion.Close();

                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Productos product)
        {
            try
            {
                if (product.Id == id)
                {
                    SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                    SqlCommand comando = conexion.CreateCommand();
                    conexion.Open();
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.CommandText = "sp_ActualizarProducto";

                    comando.Parameters.Add("@Id", System.Data.SqlDbType.VarChar).Value = product.Id;
                    comando.Parameters.Add("@Nombre", System.Data.SqlDbType.VarChar).Value = product.Nombre;
                    comando.Parameters.Add("@Descripcion", System.Data.SqlDbType.VarChar).Value = product.Descripcion;
                    comando.Parameters.Add("@Precio", System.Data.SqlDbType.Int).Value = product.Precio;
                    comando.Parameters.Add("@Stock", System.Data.SqlDbType.Int).Value = product.Stock;

                    comando.ExecuteReader();


                    conexion.Close();

                    return Ok(product);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_EliminarProducto";
                comando.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = id;
                comando.ExecuteReader();
                conexion.Close();

                return Ok(id);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //[HttpPost("fabricar{idProducto}")]
        //public IActionResult FabricarProducto(int idProducto)
        //{
        //    try
        //    {
        //        using (SqlConnection connection = (SqlConnection)_context.Database.GetDbConnection())
        //        {
        //            connection.Open();

        //            using (SqlCommand command = connection.CreateCommand())
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandText = "fabricar";

        //                command.Parameters.Add(new SqlParameter("@idProducto", idProducto));

        //                command.ExecuteNonQuery();
        //            }
        //        }

        //        return Ok(new { message = "Producto fabricado exitosamente" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}







    }
}

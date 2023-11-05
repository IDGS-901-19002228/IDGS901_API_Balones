using Microsoft.AspNetCore.Mvc;
using IDGS901_API_Balones.Context;
using IDGS901_API_Balones.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace IDGS901_API_Balones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasController : Controller
    {

        private readonly AppDbContext _context;
        public ComprasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("realizar-compra")]
        public IActionResult RealizarCompra([FromBody] Compras compra)
        {
            try
            {
                using (SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection())
                {
                    using (SqlCommand comando = conexion.CreateCommand())
                    {
                        conexion.Open();
                        comando.CommandType = System.Data.CommandType.StoredProcedure;
                        comando.CommandText = "sp_insertarCompras";

                        comando.Parameters.Add("@IdProveedor", System.Data.SqlDbType.Int).Value = compra.IdProveedor;
                        comando.Parameters.Add("@Fecha", System.Data.SqlDbType.Date).Value = compra.Fecha;
                        comando.Parameters.Add("@id_materia_prima", System.Data.SqlDbType.Int).Value = compra.Id_materia_prima;
                        comando.Parameters.Add("@cantidad", System.Data.SqlDbType.Int).Value = compra.Cantidad;
                        comando.Parameters.Add("@costo_compra", System.Data.SqlDbType.Decimal).Value = compra.Costo_compra;
                        
                        comando.ExecuteNonQuery();
                        conexion.Close();

                        return Ok("Compra realizada con éxito");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al realizar la compra: {ex.Message}");
            }
        }




        [HttpGet("ver-compras")]
        public ActionResult VerCompras()
        {
           

            try
            {
                List<Compras> listCompras = new List<Compras>();

                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_verCompras2";
                SqlDataReader read = comando.ExecuteReader();
                while (read.Read())
                {
                    Compras compra = new Compras();

                    compra.Id_compra_materia = (int)read["Id_compra_materia"];
                    //Id_compra_materia = Convert.ToInt32(reader["id_compra_materia"]),
                    compra.IdProveedor = (int)read["IdProveedor"];
                    compra.Fecha = (DateTime)read["Fecha"];
                    compra.Id_materia_prima = (int)read["Id_materia_prima"];
                    compra.Cantidad = (int)read["Cantidad"];
                    compra.Costo_compra = (int)read["Costo_compra"];
                    compra.Estatus = (string)read["Estatus"];


                    listCompras.Add(compra);
                }
                conexion.Close();
                return Json(listCompras);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }







    }
}

using Microsoft.AspNetCore.Mvc;
using IDGS901_API_Balones.Context;
using IDGS901_API_Balones.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Xml.Serialization;

namespace IDGS901_API_Balones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecetaController : Controller
    {
        private readonly AppDbContext _context;
        public RecetaController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet("ver-recetas")]
        public ActionResult VerRecetas()
        {
            try
            {
                List<Receta> listRecetas = new List<Receta>();

                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_MostrarRecetas";
                SqlDataReader read = comando.ExecuteReader();
                while (read.Read())
                {
                    Receta receta = new Receta();

                    receta.Id = (int)read["id"];
                    receta.Nombre = (string)read["nombre"];
                    receta.ingredientesMateriaP = (string)read["ingredientesMateriaP"];
                    //receta.Cantidad = (int)read["cantidad"];
                    receta.CantidadProducto = (int)read["cantidad_producto"];
                    receta.Estatus = (string)read["estatus"];
                    receta.IdProducto = (int)read["idProducto"];
                    /*receta.NombreProducto = (string)read["nombreProducto"]*/;

                    listRecetas.Add(receta);
                }
                conexion.Close();
                return Json(listRecetas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //[HttpGet("ver-recetas")]
        //public ActionResult VerCompras()
        //{





        //}


        //[HttpPost("agregarReceta")]
        //public ActionResult AgregarReceta([FromBody] Receta receta)
        //{

        //}

        [HttpPost("agregarReceta")]
        public ActionResult<Receta> Post([FromBody] Receta receta)
        {
            try
            {
                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "InsertarReceta";

                comando.Parameters.Add("@nombre", System.Data.SqlDbType.NVarChar).Value = receta.Nombre;
                comando.Parameters.Add("@ingredientesMateriaP", System.Data.SqlDbType.NVarChar).Value = receta.ingredientesMateriaP;
                comando.Parameters.Add("@cantidad", System.Data.SqlDbType.Int).Value = receta.Cantidad;
                comando.Parameters.Add("@cantidad_producto", System.Data.SqlDbType.Int).Value = receta.CantidadProducto;
                comando.Parameters.Add("@idProducto", System.Data.SqlDbType.Int).Value = receta.IdProducto;

                comando.ExecuteNonQuery();
                conexion.Close();

                return Ok(receta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }








    }
}

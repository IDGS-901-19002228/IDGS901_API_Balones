using IDGS901_API_Balones.Context;
using IDGS901_API_Balones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace IDGS901_API_Balones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetasController : Controller
    {

        private readonly AppDbContext _context;
        public TarjetasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("{usuario}")]
        public ActionResult<Tarjetas> Post([FromBody] Tarjetas tarjetas)
        {
            try
            {
                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_InsertarTarjeta";

                comando.Parameters.Add("@nombreTarjeta", System.Data.SqlDbType.NVarChar).Value = tarjetas.NombreTarjeta;
                comando.Parameters.Add("@numTarjeta", System.Data.SqlDbType.NVarChar).Value = tarjetas.NumTarjeta;
                comando.Parameters.Add("@fechaVencimiento", System.Data.SqlDbType.NVarChar).Value = tarjetas.FechaVencimiento;
                comando.Parameters.Add("@ccv", System.Data.SqlDbType.VarChar).Value = tarjetas.CCV;
                //comando.Parameters.Add("@estatus", System.Data.SqlDbType.VarChar).Value = tarjetas.Estatus;
                comando.Parameters.Add("@usuario", System.Data.SqlDbType.VarChar).Value = tarjetas.Usuario.Usuario;

                comando.ExecuteNonQuery();
                conexion.Close();

                return Ok(tarjetas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{usuario}")]
        public ActionResult VerTarjetas(string usuario)
        {

            try
            {
                List<Tarjetas> listTarjetas = new List<Tarjetas>();

                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_MostrarTarjetas";
                comando.Parameters.Add("@usuario", System.Data.SqlDbType.VarChar).Value = usuario;
                SqlDataReader read = comando.ExecuteReader();
                while (read.Read())
                {
                    Clientes clientes = new Clientes();
                    Tarjetas tarjetas = new Tarjetas();

                    tarjetas.Id = (int)read["idTarjeta"];
                    tarjetas.NombreTarjeta = (string)read["nombreTarjeta"];
                    tarjetas.NumTarjeta = (string)read["numTarjeta"];
                    tarjetas.FechaVencimiento = (string)read["fechaVencimiento"];
                    tarjetas.CCV = (string)read["ccv"];
                    //tarjetas.Estatus = (string)read["estatus"];
                    clientes.Id = (int)read["id"];
                    clientes.Usuario = (string)read["usuario"];
                    tarjetas.Usuario = clientes;

                    listTarjetas.Add(tarjetas);
                }
                conexion.Close();
                return Json(listTarjetas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{usuario}/{numTarjeta}")]
        public ActionResult Delete(string usuario, string numTarjeta)
        {
            try
            {
                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_EliminarTarjeta";

                comando.Parameters.Add("@usuario", System.Data.SqlDbType.VarChar).Value = usuario;
                comando.Parameters.Add("@numTarjeta", System.Data.SqlDbType.VarChar).Value = numTarjeta;

                comando.ExecuteNonQuery();
                conexion.Close();

                return Ok($"Tarjeta {numTarjeta} eliminada correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}

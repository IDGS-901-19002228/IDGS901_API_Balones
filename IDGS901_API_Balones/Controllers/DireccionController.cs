using IDGS901_API_Balones.Context;
using IDGS901_API_Balones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace IDGS901_API_Balones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DireccionController : Controller
    {


        private readonly AppDbContext _context;
        public DireccionController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("{usuario}")]
        public ActionResult<Direccion> Post([FromBody] Direccion direccion)
        {
            try
            {
                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_InsertarDireccion2";

                comando.Parameters.Add("@nombreCompleto", System.Data.SqlDbType.NVarChar).Value = direccion.NombreCompleto;
                comando.Parameters.Add("@calleNumero", System.Data.SqlDbType.NVarChar).Value = direccion.CalleNumero;
                comando.Parameters.Add("@codigoPostal", System.Data.SqlDbType.NVarChar).Value = direccion.CodigoPostal;
                comando.Parameters.Add("@telefono", System.Data.SqlDbType.VarChar).Value = direccion.Telefono;
                comando.Parameters.Add("@usuario", System.Data.SqlDbType.VarChar).Value = direccion.Usuario.Usuario;

                comando.ExecuteNonQuery();
                conexion.Close();

                return Ok(direccion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{usuario}")]
        public ActionResult VerDireccion(string usuario)
        {

            try
            {
                List<Direccion> listDireccion = new List<Direccion>();

                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_MostrarDirecciones";
                comando.Parameters.Add("@usuario", System.Data.SqlDbType.VarChar).Value = usuario;
                SqlDataReader read = comando.ExecuteReader();
                while (read.Read())
                {
                    Clientes clientes = new Clientes();
                    Direccion direccion = new Direccion();

                    direccion.Id = (int)read["idDireccion"];
                    direccion.NombreCompleto = (string)read["nombreCompleto"];
                    direccion.CodigoPostal = (string)read["codigoPostal"];
                    direccion.Telefono = (string)read["telefono"];
                    direccion.CalleNumero = (string)read["direccion"];
                    clientes.Id = (int)read["id"];
                    clientes.Usuario = (string)read["usuario"];
                    direccion.Usuario = clientes;

                    listDireccion.Add(direccion);
                }
                conexion.Close();
                return Json(listDireccion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

    }
}
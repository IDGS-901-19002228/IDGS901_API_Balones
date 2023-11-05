using Microsoft.AspNetCore.Mvc;
using IDGS901_API_Balones.Context;
using IDGS901_API_Balones.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

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

        [HttpPost]
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
                comando.Parameters.Add("@telefono", System.Data.SqlDbType.Int).Value = direccion.Telefono;
                comando.Parameters.Add("@idcliente", System.Data.SqlDbType.NVarChar).Value = direccion.IdCliente;

                comando.ExecuteNonQuery();
                conexion.Close();

                return Ok(direccion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}

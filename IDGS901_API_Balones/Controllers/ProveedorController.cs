using IDGS901_API_Balones.Context;
using IDGS901_API_Balones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net.NetworkInformation;

namespace IDGS901_API_Balones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : Controller
    {
        private readonly AppDbContext _context;

        public ProveedorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                List<Proveedor> listProveedor = new List<Proveedor>();

                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_GetAllProveedores";
                SqlDataReader read = comando.ExecuteReader();
                while (read.Read())
                {
                    Proveedor proveedor = new Proveedor();

                    proveedor.id = (int)read["id"];
                    proveedor.nombre = (string)read["nombre"];
                    proveedor.empresa = (string)read["empresa"];
                    proveedor.rfc = (string)read["rfc"];
                    proveedor.telefono = (string)read["telefono"];
                    proveedor.correo = (string)read["correo"];
                    proveedor.estatus = (string)read["estatus"];


                    listProveedor.Add(proveedor);
                }
                conexion.Close();
                return Json(listProveedor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<Proveedor> Post([FromBody] Proveedor proveedor)
        {
            try
            {
                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_InsertarProveedor";

                comando.Parameters.Add("@nombre", System.Data.SqlDbType.NVarChar).Value = proveedor.nombre;
                comando.Parameters.Add("@empresa", System.Data.SqlDbType.NVarChar).Value = proveedor.empresa;
                comando.Parameters.Add("@rfc", System.Data.SqlDbType.NVarChar).Value = proveedor.rfc;
                comando.Parameters.Add("@telefono", System.Data.SqlDbType.NVarChar).Value = proveedor.telefono;
                comando.Parameters.Add("@correo", System.Data.SqlDbType.NVarChar).Value = proveedor.correo;

                comando.ExecuteNonQuery();
                conexion.Close();

                return Ok(proveedor);
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
                comando.CommandText = "sp_EliminarProveedor";
                comando.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
                comando.ExecuteReader();
                conexion.Close();

                return Ok(id);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}

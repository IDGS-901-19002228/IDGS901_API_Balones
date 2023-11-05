using Microsoft.AspNetCore.Mvc;
using IDGS901_API_Balones.Context;
using IDGS901_API_Balones.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace IDGS901_API_Balones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriaPrimaController : Controller
    {

        private readonly AppDbContext _context;
        public MateriaPrimaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                List<MateriaPrima> listMateriaP = new List<MateriaPrima>();

                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_MostrarMateriasPrimasActivas";
                SqlDataReader read = comando.ExecuteReader();
                while (read.Read())
                {
                    MateriaPrima mp = new MateriaPrima();

                    mp.Id = (int)read["Id"];
                    mp.Nombre = (string)read["Nombre"];
                    mp.Descripcion = (string)read["Descripcion"];
                    mp.Cantidad = (int)read["Cantidad"];
                    mp.Unidad_Medida = (string)read["Unidad_Medida"];
                    mp.Stock = (int)read["Stock"];
                    mp.Estatus = (string)read["Estatus"];


                    listMateriaP.Add(mp);
                }
                conexion.Close();
                return Json(listMateriaP);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<MateriaPrima> Post([FromBody] MateriaPrima mp)
        {
            try
            {
                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_InsertarMateriaPrima";

                comando.Parameters.Add("@Nombre", System.Data.SqlDbType.NVarChar).Value = mp.Nombre;
                comando.Parameters.Add("@Descripcion", System.Data.SqlDbType.NVarChar).Value = mp.Descripcion;
                comando.Parameters.Add("@Cantidad", System.Data.SqlDbType.Int).Value = mp.Cantidad;
                comando.Parameters.Add("@Unidad_Medida", System.Data.SqlDbType.NVarChar).Value = mp.Unidad_Medida;
                comando.Parameters.Add("@Stock", System.Data.SqlDbType.Int).Value = mp.Stock;

                comando.ExecuteNonQuery();
                conexion.Close();

                return Ok(mp);
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
                comando.CommandText = "sp_EliminarMateriaPrima";
                comando.Parameters.Add("@p_Id", System.Data.SqlDbType.Int).Value = id;
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

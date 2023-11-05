using IDGS901_API_Balones.Context;
using IDGS901_API_Balones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace IDGS901_API_Balones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : Controller
    {
        private readonly AppDbContext _context;
        public VentaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                List<Venta> listventa = new List<Venta>();

                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_GetAllVentas";
                SqlDataReader read = comando.ExecuteReader();
                while (read.Read())
                {
                    Venta v = new Venta();

                    v.id = (int)read["id"];
                    v.fecha = (DateTime)read["fecha"];
                    v.estatus = (string)read["estatus"];
                    v.cliente = (string)read["nombre"];

                    listventa.Add(v);
                }
                conexion.Close();
                return Json(listventa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{id}", Name = "Ventas")]
        public ActionResult Get(int id)
        {
            try
            {
                List<Venta> listventas = new List<Venta>();

                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_GetDetailsVentas";
                comando.Parameters.Add("@id", System.Data.SqlDbType.NVarChar).Value = id;

                SqlDataReader read = comando.ExecuteReader();
                while (read.Read())
                {
                    Venta v = new Venta();

                    v.id = (int)read["id"];
                    v.fecha = (DateTime)read["fecha"];
                    v.estatus = (string)read["estatus"];
                    v.cliente = (string)read["nombre"];
                    v.cantidad = (int)read["cantidad"];
                    v.nombreproducto = (string)read["nombreproducto"];
                    v.precioUnitario = (int)read["precioUnitario"];

                    listventas.Add(v);
                }
                conexion.Close();
                return Json(listventas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
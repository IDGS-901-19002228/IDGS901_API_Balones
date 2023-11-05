//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Data.SqlClient;
//using Microsoft.EntityFrameworkCore;
////using NaturaLife.Context;
////using NaturaLife.Model;
//using IDGS901_API_Balones.Context;
//using IDGS901_API_Balones.Models;

//namespace NaturaLife.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CompraMateriaController : Controller
//    {
//        private readonly AppDbContext _context;
//        public CompraMateriaController(AppDbContext context)
//        {
//            _context = context;
//        }

//        [HttpGet]
//        public ActionResult Get()

//        {
//            try
//            {
//                List<CompraMateriaP> listCompras = new List<CompraMateriaP>();

//                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
//                SqlCommand comando = conexion.CreateCommand();
//                conexion.Open();
//                comando.CommandType = System.Data.CommandType.StoredProcedure;
//                comando.CommandText = "sp_MostrarCompras";
//                SqlDataReader read = comando.ExecuteReader();
//                while (read.Read())
//                {
//                    Usuarios emp = new Usuarios();
//                    Proveedor pro = new Proveedor();
//                    CompraMateriaP comp = new CompraMateriaP(); 
                    
                    
//                    emp.Id = (int)read["id"];
//                    emp.Nombre = (string)read["Nombre"];
//                    emp.ApellidoMaterno = (string)read["ApellidoMaterno"];

//                    pro.id = (int)read["id_proveedor"];
//                    pro.nombre = (string)read["nombre_proveedor"];
//                    pro.telefono = (string)read["telefono_proveedor"];
//                    pro.correo = (string)read["correo"];

//                    comp.id_compra_materia = (int)read["id_compra_materia"];
//                    comp.fecha = (DateTime)read["fecha_compra"];
//                    comp.proveedor = pro;
//                    comp.empleado = emp;

//                    listCompras.Add(comp);
//                }
//                conexion.Close();
//                return Json(listCompras);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }


//        [HttpPost]
//        public ActionResult Post([FromBody] CompraMateriaP cm)
//        {
//            try
//            {

//                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
//                SqlCommand comando = conexion.CreateCommand();
//                conexion.Open();
//                comando.CommandType = System.Data.CommandType.StoredProcedure;
//                comando.CommandText = "sp_CompraMateriaPrima";

//                comando.Parameters.Add("@id_proveedor", System.Data.SqlDbType.Int).Value = cm.proveedor.id;
//                comando.Parameters.Add("@id_empleado", System.Data.SqlDbType.Int).Value = cm.empleado.Id;

//                comando.ExecuteReader();
//                conexion.Close();

//                return Ok("Compra realizada");
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }

//    }
//}

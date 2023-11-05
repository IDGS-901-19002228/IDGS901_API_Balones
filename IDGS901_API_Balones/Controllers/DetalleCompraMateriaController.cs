//using IDGS901_API_Balones.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Data.SqlClient;
//using Microsoft.EntityFrameworkCore;
////using NaturaLife.Context;
////using NaturaLife.Model;
//using IDGS901_API_Balones.Context;

//namespace NaturaLife.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class DetalleCompraMateriaController : Controller
//    {
//        private readonly AppDbContext _context;
//        public DetalleCompraMateriaController(AppDbContext context)
//        {
//            _context = context;
//        }

//        [HttpGet]
//        public ActionResult Get(int id_compra)

//        {
//            try
//            {
//                List<DetalleCompraMateria> listDetalleCompra = new List<DetalleCompraMateria>();

//                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
//                SqlCommand comando = conexion.CreateCommand();
//                conexion.Open();
//                comando.CommandType = System.Data.CommandType.StoredProcedure;
//                comando.CommandText = "sp_MostrarDetalleCompra";
//                comando.Parameters.Add("@id_compra", System.Data.SqlDbType.VarChar).Value = id_compra;
//                SqlDataReader read = comando.ExecuteReader();
//                while (read.Read())
//                {
//                    Usuarios emp = new Usuarios();
//                    Proveedor pro = new Proveedor();
//                    MateriaPrima materiaPrima = new MateriaPrima();
//                    CompraMateriaP comp = new CompraMateriaP();
//                    DetalleCompraMateria detalleCompra = new DetalleCompraMateria();


//                    emp.Id = (int)read["Id"];
//                    emp.Nombre = (string)read["Nombre"];

//                    pro.id = (int)read["id"];
//                    pro.nombre = (string)read["nombre_proveedor"];
//                    pro.telefono = (string)read["telefono_proveedor"];
//                    pro.correo = (string)read["correo"];

//                    comp.id_compra_materia = (int)read["id_compra_materia"];
//                    comp.fecha = (DateTime)read["fecha_compra"];
//                    comp.proveedor = pro;
//                    comp.empleado = emp;

//                    materiaPrima.Nombre = (string)read["nombre_materia_prima"];
//                    //materiaPrima.desc = (string)read["descripcion_materia_prima"];
//                    materiaPrima.Cantidad = (int)read["cantidad_materia_prima"];
//                    materiaPrima.Unidad_Medida = (string)read["unidad_medida_materia_prima"];

//                    detalleCompra.compra_materia = comp;
//                    detalleCompra.materia = materiaPrima;
//                    detalleCompra.cantidad = (int)read["cantidad_comprada"];
//                    detalleCompra.costo_compra = (decimal)read["costo_de_compra"];

//                    listDetalleCompra.Add(detalleCompra);
//                }
//                conexion.Close();
//                return Json(listDetalleCompra);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }



//        [HttpPost]
//        public ActionResult Post([FromBody] DetalleCompraMateria dcm)
//        {
//            try
//            {

//                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
//                SqlCommand comando = conexion.CreateCommand();
//                conexion.Open();
//                comando.CommandType = System.Data.CommandType.StoredProcedure;
//                comando.CommandText = "sp_DetalleCompraMateriaPrima";

//                comando.Parameters.Add("@cantidad", System.Data.SqlDbType.Int).Value = dcm.cantidad;
//                comando.Parameters.Add("@costo_compra", System.Data.SqlDbType.Int).Value = dcm.costo_compra;
//                comando.Parameters.Add("@id_materia_prima", System.Data.SqlDbType.Int).Value = dcm.materia.Id;

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

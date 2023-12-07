using IDGS901_API_Balones.Context;
using IDGS901_API_Balones.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace IDGS901_API_Balones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : Controller
    {
        private readonly AppDbContext _context;
        public PedidosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost()]
        public IActionResult RealizarPedido([FromBody] Pedidos pedidos)
        {
            try
            {
                using (SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection())
                {
                    using (SqlCommand comando = conexion.CreateCommand())
                    {
                        conexion.Open();
                        comando.CommandType = System.Data.CommandType.StoredProcedure;
                        comando.CommandText = "sp_insertarPedido";

                        comando.Parameters.Add("@Folio", System.Data.SqlDbType.VarChar).Value = pedidos.folio;
                        comando.Parameters.Add("@Fecha", System.Data.SqlDbType.Date).Value = pedidos.fecha;
                        comando.Parameters.Add("@usuario", System.Data.SqlDbType.VarChar).Value = pedidos.cliente.Usuario;
                        comando.Parameters.Add("@id_direccion", System.Data.SqlDbType.Decimal).Value = pedidos.direccion.Id;
                        comando.Parameters.Add("@id_tarjeta", System.Data.SqlDbType.Int).Value = pedidos.tarjeta.Id;



                        comando.ExecuteNonQuery();


                        conexion.Close();

                        return Ok("Pedido en proceso con éxito. ");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al realizar el pedido: {ex.Message}");
            }
        }

        [HttpPost("insertar_detalle")]
        public IActionResult InsertarDetalle([FromBody] DetallePedido detalle)
        {
            try
            {
                using (SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection())
                {
                    using (SqlCommand comando = conexion.CreateCommand())
                    {
                        conexion.Open();
                        comando.CommandType = System.Data.CommandType.StoredProcedure;
                        comando.CommandText = "sp_insertarDetallePedido";

                        comando.Parameters.Add("@idProducto", System.Data.SqlDbType.Int).Value = detalle.productos.Id;
                        comando.Parameters.Add("@cantidad", System.Data.SqlDbType.Int).Value = detalle.cantidad;

                        comando.ExecuteNonQuery();
                        conexion.Close();

                        return Ok("Pedido en proceso con éxito");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al realizar el pedido: {ex.Message}");
            }
        }

        [HttpGet("{usuario}")]
        public ActionResult VerPedidos(string usuario)
        {
            try
            {
                List<Pedidos> listCompras = new List<Pedidos>();

                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_GetPedidoClienteByID";
                comando.Parameters.Add("@nombre", System.Data.SqlDbType.VarChar).Value = usuario;
                SqlDataReader read = comando.ExecuteReader();
                while (read.Read())
                {
                    Pedidos pedido = new Pedidos();
                    Direccion direccion = new Direccion();
                    Clientes clientes = new Clientes();
                    Usuarios usuarios = new Usuarios();
                    Tarjetas tarjeta = new Tarjetas();

                    tarjeta.NumTarjeta = (string)read["numTarjeta"];
                    tarjeta.FechaVencimiento = (string)read["fechaVencimiento"];


                    direccion.CalleNumero = (string)read["calleNumero"];
                    direccion.CodigoPostal = (string)read["codigoPostal"];
                    direccion.NombreCompleto = (string)read["nombreCompleto"];

                    clientes.Id = (int)read["id"];
                    clientes.Nombre = (string)read["nombre"];
                    clientes.Usuario = (string)read["usuario"];
                    clientes.Correo = (string)read["correo"];

                    pedido.idPedido = (int)read["id"];
                    pedido.folio = (string)read["folio"];
                    pedido.fecha = (DateTime)read["fecha"];
                    pedido.estatus = (string)read["estatus"];
                    pedido.cliente = clientes;
                    pedido.direccion = direccion;
                    pedido.tarjeta = tarjeta;



                    listCompras.Add(pedido);
                }
                conexion.Close();
                return Json(listCompras);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("verDetallePedido")]
        public ActionResult VerDetallePedido(int id)
        {


            try
            {
                List<DetallePedido> listDetallePedido = new List<DetallePedido>();

                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_GetDetallePedidoByID";
                comando.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
                SqlDataReader read = comando.ExecuteReader();
                while (read.Read())
                {
                    Pedidos pedido = new Pedidos();
                    Productos producto = new Productos();
                    DetallePedido detalle_p = new DetallePedido();


                    pedido.idPedido = (int)read["idPedido"];

                    producto.Nombre = (string)read["nombre"];
                    producto.Descripcion = (string)read["descripcion"];
                    producto.Precio = (int)read["precio"];
                    producto.Imagen = (string)read["imagen"];

                    detalle_p.id = (int)read["id"];
                    detalle_p.cantidad = (int)read["cantidad"];
                    detalle_p.pedidos = pedido;
                    detalle_p.productos = producto;


                    listDetallePedido.Add(detalle_p);
                }
                conexion.Close();
                return Json(listDetallePedido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("mostrarPedidos")]
        public IActionResult MostrarPedidos()
        {
            try
            {
                List<Pedidos> listPedidos = new List<Pedidos>();

                using (SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection())
                {
                    using (SqlCommand comando = conexion.CreateCommand())
                    {
                        conexion.Open();
                        comando.CommandType = System.Data.CommandType.StoredProcedure;
                        comando.CommandText = "MostrarPedidos";
                        SqlDataReader read = comando.ExecuteReader();

                        while (read.Read())
                        {
                            // Aquí deberías construir objetos Pedidos a partir de los resultados de la consulta y agregarlos a listPedidos
                            // ...

                            // Ejemplo:
                            // Pedidos pedido = new Pedidos();
                            // pedido.idPedido = (int)read["id"];
                            // pedido.folio = (string)read["folio"];
                            // pedido.fecha = (DateTime)read["fecha"];
                            // pedido.estatus = (string)read["estatus"];
                            // listPedidos.Add(pedido);
                            Pedidos pedido = new Pedidos();
                            pedido.idPedido = (int)read["id"];
                            pedido.folio = (string)read["folio"];
                            pedido.fecha = (DateTime)read["fecha"];
                            pedido.estatus = (string)read["estatus"];
                            //pedido.cliente = clientes;
                            //pedido.direccion = direccion;
                            //pedido.tarjeta = tarjeta;
                            listPedidos.Add(pedido);
                        }

                        conexion.Close();
                    }
                }

                return Json(listPedidos);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener los pedidos: {ex.Message}");
            }
        }

        [HttpPut("actualizarEnProcesoAEnCamino/{idPedido}")]
        public ActionResult ActualizarEnProcesoAEnCamino(int idPedido)
        {
            try
            {
                using (SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection())
                {
                    using (SqlCommand comando = conexion.CreateCommand())
                    {
                        conexion.Open();
                        comando.CommandType = System.Data.CommandType.StoredProcedure;
                        comando.CommandText = "ActualizarEnProcesoAEnCaminoPorID";

                        comando.Parameters.Add("@idPedido", System.Data.SqlDbType.Int).Value = idPedido;

                        comando.ExecuteNonQuery();
                        conexion.Close();

                        return Ok($"Estatus actualizado de 'En proceso' a 'En camino' para el pedido con ID {idPedido}.");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el estatus: {ex.Message}");
            }
        }


        [HttpPut("actualizarEnCaminoAEntregado/{idPedido}")] 
        public ActionResult ActualizarEnCaminoAEntregado(int idPedido)
        {
            try
            {
                using (SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection())
                {
                    using (SqlCommand comando = conexion.CreateCommand())
                    {
                        conexion.Open();
                        comando.CommandType = System.Data.CommandType.StoredProcedure;
                        comando.CommandText = "ActualizarEnCaminoAEntregadoPorID";

                        comando.Parameters.Add("@idPedido", System.Data.SqlDbType.Int).Value = idPedido;

                        comando.ExecuteNonQuery();
                        conexion.Close();

                        return Ok($"Estatus actualizado de 'En camino' a 'Entregado' para el pedido con ID {idPedido}.");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el estatus: {ex.Message}");
            }
        }




    }

}

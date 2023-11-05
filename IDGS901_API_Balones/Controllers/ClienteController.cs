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
    public class ClienteController : Controller
    {
        private readonly AppDbContext _context;

        public ClienteController(AppDbContext context)
        {
            _context = context;
        }


        //[HttpGet]//api/<Proyecto>
        //public ActionResult Get()
        //{
        //    try
        //    {
        //        List<Clientes> listClientes = new List<Clientes>();

        //        SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
        //        SqlCommand comando = conexion.CreateCommand();
        //        conexion.Open();
        //        comando.CommandType = System.Data.CommandType.StoredProcedure;
        //        comando.CommandText = "sp_GetAllClientes";
        //        SqlDataReader read = comando.ExecuteReader();
        //        while (read.Read())
        //        {
        //            Clientes cliente = new Clientes();

        //            cliente.Id = (int)read["Id"];
        //            cliente.Nombre = (string)read["Nombre"];
        //            cliente.Usuario = (string)read["Usuario"];
        //            cliente.Correo = (string)read["Correo"];
        //            cliente.Rol = (string)read["Rol"];
        //            cliente.Estatus = (string)read["Estatus"];

        //            listClientes.Add(cliente);
        //        }
        //        conexion.Close();
        //        return Json(listClientes);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


        //[HttpGet("{usuario}", Name = "ClientesPorUsuario")]
        //public ActionResult GetPorUsuario(string usuario)
        //{
        //    try
        //    {
        //        var cliente = _context.Clientes.FirstOrDefault(x => x.Correo == usuario);
        //        if (cliente == null)
        //        {
        //            return NotFound(); // Retorna 404 si el cliente no se encuentra
        //        }
        //        return Ok(cliente);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


        //[HttpGet("{usuario}", Name = "ClientesPorUsuario")]
        //public ActionResult GetPorUsuario(string usuario)
        //{
        //    try
        //    {
        //        var cliente = _context.Clientes.FirstOrDefault(x => x.Usuario == usuario);
        //        if (cliente == null)
        //        {
        //            return NotFound(); // Retorna 404 si el cliente no se encuentra
        //        }
        //        return Ok(cliente);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}



        //[HttpPost]
        //public ActionResult<Clientes> Post([FromBody] Clientes clientes)
        //{
        //    try
        //    {
        //        SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
        //        SqlCommand comando = conexion.CreateCommand();
        //        conexion.Open();
        //        comando.CommandType = System.Data.CommandType.StoredProcedure;
        //        comando.CommandText = "sp_InsertarCliente";

        //        comando.Parameters.Add("@Nombre", System.Data.SqlDbType.NVarChar).Value = clientes.Nombre;
        //        comando.Parameters.Add("@Usuario", System.Data.SqlDbType.NVarChar).Value = clientes.Usuario;
        //        comando.Parameters.Add("@Correo", System.Data.SqlDbType.NVarChar).Value = clientes.Correo;
        //        comando.Parameters.Add("@Contrasenia", System.Data.SqlDbType.NVarChar).Value = clientes.Contrasenia;



        //        comando.ExecuteNonQuery();
        //        conexion.Close();

        //        return Ok(clientes);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpPut("{id}")]
        //public ActionResult Put([FromBody] Clientes alum, int id)
        //{
        //    try
        //    {
        //        if (alum.Id == id)
        //        {
        //            _context.Entry(alum).State = EntityState.Modified;
        //            _context.SaveChanges();
        //            return Ok(alum); // Cambiado de CreatedAtRoute a Ok
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


        //[HttpDelete("{id}")]
        //public ActionResult Delete(int id)
        //{
        //    try
        //    {
        //        var alum = _context.Clientes.FirstOrDefault(x => x.Id == id);
        //        if (alum != null)
        //        {
        //            _context.Remove(alum);
        //            _context.SaveChanges();
        //            return Ok(id);
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}




        [HttpGet]//api/<Proyecto>
        public ActionResult Get()
        {
            try
            {
                return Ok(_context.Clientes2.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{usuario}", Name = "ClientesPorUsuario")]
        public ActionResult GetPorUsuario(string usuario)
        {
            try
            {
                var cliente = _context.Clientes2.FirstOrDefault(x => x.Usuario == usuario);
                if (cliente == null)
                {
                    return NotFound(); // Retorna 404 si el cliente no se encuentra
                }
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public ActionResult<Clientes> Post([FromBody] Clientes alumn)
        {
            try
            {


                _context.Clientes2.Add(alumn);
                _context.SaveChanges();
                return CreatedAtRoute("Clientes", new { id = alumn.Id }, alumn);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromBody] Clientes alum, int id)
        {
            try
            {
                if (alum.Id == id)
                {
                    _context.Entry(alum).State = EntityState.Modified;
                    _context.SaveChanges();
                    return Ok(alum); // Cambiado de CreatedAtRoute a Ok
                }
                else
                {
                    return BadRequest();
                }
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
                var alum = _context.Clientes2.FirstOrDefault(x => x.Id == id);
                if (alum != null)
                {
                    _context.Remove(alum);
                    _context.SaveChanges();
                    return Ok(id);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }

}



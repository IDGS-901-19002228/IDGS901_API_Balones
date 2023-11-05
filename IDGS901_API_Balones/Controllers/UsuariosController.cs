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
    public class UsuariosController : Controller
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        //[HttpGet]
        //public ActionResult Get()
        //{
        //    try
        //    {
        //        List<Usuarios> listUsuarios = new List<Usuarios>();

        //        SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
        //        SqlCommand comando = conexion.CreateCommand();
        //        conexion.Open();
        //        comando.CommandType = System.Data.CommandType.StoredProcedure;
        //        comando.CommandText = "GetAllUsuarios";
        //        SqlDataReader read = comando.ExecuteReader();
        //        while (read.Read())
        //        {
        //            Usuarios user = new Usuarios();

        //            user.Id = (int)read["Id"];
        //            user.Nombre = (string)read["Nombre"];
        //            user.ApellidoPaterno = (string)read["ApellidoPaterno"];
        //            user.ApellidoMaterno = (string)read["ApellidoMaterno"];
        //            user.Edad = (int)read["Edad"];
        //            user.Sexo = (string)read["Sexo"];
        //            user.Telefono = (string)read["Telefono"];
        //            user.Direccion = (string)read["Direccion"];
        //            user.Correo = (string)read["Correo"];
        //            user.Contrasenia = (string)read["Contrasenia"];
        //            user.FechaRegistro = (DateTime)read["FechaRegistro"];
        //            user.Rol = (string)read["Rol"];
        //            user.Estatus = (string)read["Estatus"];


        //            listUsuarios.Add(user);
        //        }
        //        conexion.Close();
        //        return Json(listUsuarios);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpGet("{id}", Name = "Usuarios")]
        //public ActionResult Get(int id)
        //{
        //    try
        //    {
        //        var alum = _context.Usuarios.FirstOrDefault(x => x.Id == id);
        //        return Ok(alum);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}



        //[HttpPost]
        //public ActionResult<Usuarios> Post([FromBody] Usuarios user)
        //{
        //    try
        //    {
        //        SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
        //        SqlCommand comando = conexion.CreateCommand();
        //        conexion.Open();
        //        comando.CommandType = System.Data.CommandType.StoredProcedure;
        //        comando.CommandText = "RegistrarUsuarioConRol";

        //        comando.Parameters.Add("@p_Nombre", System.Data.SqlDbType.NVarChar).Value = user.Nombre;
        //        comando.Parameters.Add("@p_ApellidoPaterno", System.Data.SqlDbType.NVarChar).Value = user.ApellidoPaterno;
        //        comando.Parameters.Add("@p_ApellidoMaterno", System.Data.SqlDbType.NVarChar).Value = user.ApellidoMaterno;
        //        comando.Parameters.Add("@p_Edad", System.Data.SqlDbType.Int).Value = user.Edad;
        //        comando.Parameters.Add("@p_Sexo", System.Data.SqlDbType.NVarChar).Value = user.Sexo;
        //        comando.Parameters.Add("@p_Telefono", System.Data.SqlDbType.NVarChar).Value = user.Telefono;
        //        comando.Parameters.Add("@p_Direccion", System.Data.SqlDbType.NVarChar).Value = user.Direccion;
        //        comando.Parameters.Add("@p_Correo", System.Data.SqlDbType.NVarChar).Value = user.Correo;
        //        comando.Parameters.Add("@p_Contrasenia", System.Data.SqlDbType.NVarChar).Value = user.Contrasenia;
        //        comando.Parameters.Add("@p_FechaRegistro", System.Data.SqlDbType.DateTime).Value = user.FechaRegistro;
        //comando.Parameters.Add("@p_Estatus", System.Data.SqlDbType.NVarChar).Value = user.Estatus;
        //        comando.Parameters.Add("@p_RoleId", System.Data.SqlDbType.Int).Value = user.RolId; // Asegúrate de tener el campo RolId en tu modelo Usuarios

        //        comando.ExecuteNonQuery();
        //        conexion.Close();

        //        return Ok(user);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}




        //[HttpPut("{id}")]
        //public ActionResult Put([FromBody] Usuarios alum, int id)
        //{
        //    try
        //    {
        //        if (alum.Id == id)
        //        {
        //            _context.Entry(alum).State = EntityState.Modified;
        //            _context.SaveChanges();
        //            return CreatedAtRoute("Usuarios", new { id = alum.Id }, alum);
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
        //        SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
        //        SqlCommand comando = conexion.CreateCommand();
        //        conexion.Open();
        //        comando.CommandType = System.Data.CommandType.StoredProcedure;
        //        comando.CommandText = "DesactivarUsuario";
        //        comando.Parameters.Add("@p_Id", System.Data.SqlDbType.Int).Value = id;
        //        comando.ExecuteReader();
        //        conexion.Close();

        //        return Ok(id);

        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


        //    [HttpPut("CambiarEstado/{id}")]
        //    public ActionResult CambiarEstado(int id)
        //    {
        //        try
        //        {
        //            var usuario = _context.Usuarios.FirstOrDefault(x => x.Id == id);

        //            if (usuario != null)
        //            {
        //                // Cambia el estado a 'Inactivo' si actualmente es 'Activo' y viceversa
        //                usuario.Estatus = usuario.Estatus == "Activo" ? "Inactivo" : "Activo";
        //                _context.SaveChanges();

        //                return Ok("Estado cambiado correctamente");
        //            }
        //            else
        //            {
        //                return BadRequest();
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex.Message);
        //        }
        //    }
        //}



        [HttpGet]//api/<Proyecto>
        public ActionResult Get()
        {
            try
            {
                return Ok(_context.Usuarios2.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}", Name = "Usuarios")]
        public ActionResult Get(int id)
        {
            try
            {
                var us = _context.Usuarios2.FirstOrDefault(x => x.Id == id);
                return Ok(us);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<Usuarios> Post([FromBody] Usuarios usu)
        {
            try
            {


                _context.Usuarios2.Add(usu);
                _context.SaveChanges();
                return CreatedAtRoute("Usuarios", new { id = usu.Id }, usu);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromBody] Usuarios us, int id)
        {
            try
            {
                if (us.Id == id)
                {
                    _context.Entry(us).State = EntityState.Modified;
                    _context.SaveChanges();
                    return CreatedAtRoute("Usuarios", new { id = us.Id }, us);
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
                var us = _context.Usuarios2.FirstOrDefault(x => x.Id == id);
                if (us != null)
                {
                    _context.Remove(us);
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

        [HttpPut("CambiarEstado/{id}")]
        public ActionResult CambiarEstado(int id)
        {
            try
            {
                var usuario = _context.Usuarios2.FirstOrDefault(x => x.Id == id);

                if (usuario != null)
                {
                    // Cambia el estado a 'Inactivo' si actualmente es 'Activo' y viceversa
                    usuario.Estatus = usuario.Estatus == "Activo" ? "Inactivo" : "Activo";
                    _context.SaveChanges();

                    return Ok("Estado cambiado correctamente");
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
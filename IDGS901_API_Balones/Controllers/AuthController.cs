using IDGS901_API_Balones.Context;
using IDGS901_API_Balones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace IDGS901_API_Balones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        //[HttpPost]
        //[Route("login")]
        //public ActionResult Login(Login model)
        //{
        //    try
        //    {
        //        var user = _context.Usuarios
        //            .FirstOrDefault(u => u.Correo == model.Correo && u.Contrasenia == model.Contrasenia);

        //        var client = _context.Clientes
        //            .FirstOrDefault(c => c.Correo == model.Correo && c.Contrasenia == model.Contrasenia);

        //        if (user != null)
        //        {
        //            return Ok(new { Rol = user.Rol }); // User found, return rol
        //        }
        //        else if (client != null)
        //        {
        //            return Ok(new { Rol = "Cliente" }); // Client found, return rol
        //        }
        //        else
        //        {
        //            return BadRequest("Credenciales inválidas");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpGet]
        //[Route("usuario")]
        //public ActionResult ObtenerEstatusPorCorreo(string correo)
        //{
        //    try
        //    {
        //        var user = _context.Usuarios
        //            .FirstOrDefault(u => u.Correo == correo);

        //        var client = _context.Clientes
        //            .FirstOrDefault(c => c.Correo == correo);

        //        if (user != null)
        //        {
        //            return Ok(new { Estatus = user.Estatus });
        //        }
        //        else if (client != null)
        //        {
        //            return Ok(new { Estatus = client.Estatus });
        //        }
        //        else
        //        {
        //            return NotFound("Usuario no encontrado");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpGet]
        //[Route("verificar")]
        //public ActionResult VerificarUsuarioRegistrado(string correo)
        //{
        //    try
        //    {
        //        var userExists = _context.Usuarios.Any(u => u.Correo == correo);
        //        var clientExists = _context.Clientes.Any(c => c.Correo == correo);

        //        if (userExists || clientExists)
        //        {
        //            return Ok(new { resp = "Si" });
        //        }
        //        else
        //        {
        //            return Ok(new { resp = "No" });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}



        [HttpPost]
        [Route("login")]
        public ActionResult Login(Login model)
        {
            try
            {
                var user = _context.Usuarios2.FirstOrDefault(u => u.Usuario == model.Usuario && u.Contrasenia == model.Contrasenia);
                var client = _context.Clientes2.FirstOrDefault(c => c.Usuario == model.Usuario && c.Contrasenia == model.Contrasenia);

                if (user != null)
                {
                    return Ok(new { Rol = user.Rol }); // User found, return rol
                }
                else if (client != null)
                {
                    return Ok(new { Rol = "Cliente" }); // Client found, return rol
                }

                else
                {
                    return BadRequest("Credenciales inválidas");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("usuario")]
        public ActionResult<string> ObtenerEstatusPorUsuario(string usuario)
        {
            try
            {
                var user = _context.Usuarios2.FirstOrDefault(u => u.Usuario == usuario);
                var client = _context.Clientes2.FirstOrDefault(c => c.Usuario == usuario);

                if (user != null)
                {
                    return Ok(new { Estatus = user.Estatus });
                }
                else if (client != null)
                {
                    return Ok(new { Estatus = client.Estatus });
                }
                else
                {
                    return NotFound("Usuario no encontrado");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("verificar")]
        public ActionResult<string> VerificarUsuarioRegistrado(string usuario)
        {
            try
            {
                var user = _context.Usuarios2.Any(u => u.Usuario == usuario);
                var client = _context.Clientes2.Any(c => c.Usuario == usuario);

                if (user || client)
                {
                    return Ok(new { resp = "Si" });
                }
                else
                {
                    return Ok(new { resp = "No" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}

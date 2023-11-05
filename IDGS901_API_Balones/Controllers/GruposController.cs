using IDGS901_API_Balones.Context;
using IDGS901_API_Balones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace IDGS901_API_Balones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GruposController : Controller
    {
        private readonly AppDbContext _context;
        public GruposController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                List<Alumnos> listAlumnos = new List<Alumnos>();

                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "getAll_Usuario";
                SqlDataReader read = comando.ExecuteReader();
                while (read.Read())
                {
                    Alumnos alum = new Alumnos();

                    alum.Nombre = (string)read["Nombre"];
                    alum.Correo = (string)read["Correo"];
                    alum.Edad = (int)read["Edad"];

                    listAlumnos.Add(alum);
                }
                conexion.Close();
                return Json(listAlumnos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //[HttpGet]//api/<Grupos>
        //public ActionResult Get()
        //{
        //    try
        //    {
        //        return Ok(_context.Alumnos.ToList());
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpGet("{id}", Name = "Alumnos")]
        public ActionResult Get(int id)
        {
            try
            {
                var alum = _context.Alumnos.FirstOrDefault(x => x.Id == id);
                return Ok(alum);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<Alumnos> Post([FromBody] Alumnos alumn)
        {
            try
            {


                _context.Alumnos.Add(alumn);
                _context.SaveChanges();
                return CreatedAtRoute("Alumnos", new { id = alumn.Id }, alumn);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Alumnos alum)
        {
            try
            {
                if (alum.Id == id)
                {
                    _context.Entry(alum).State = EntityState.Modified;
                    _context.SaveChanges();
                    return CreatedAtRoute("Alumnos", new { id = alum.Id }, alum);
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
                var alum = _context.Alumnos.FirstOrDefault(x => x.Id == id);
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

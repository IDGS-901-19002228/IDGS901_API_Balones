using Microsoft.AspNetCore.Mvc;
using IDGS901_API_Balones.Context;
using IDGS901_API_Balones.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;


namespace IDGS901_API_Balones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FabricarController : Controller
    {
        private readonly AppDbContext _context;
        public FabricarController(AppDbContext context)
        {
            _context = context;
        }


        [HttpPost("{idProducto}")]
        public ActionResult FabricarProducto(int idProducto)
        {
            try
            {
                using (SqlConnection connection = (SqlConnection)_context.Database.GetDbConnection())
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "fabricar";

                        command.Parameters.Add(new SqlParameter("@idProducto", idProducto));

                        command.ExecuteNonQuery();
                    }
                }

                return Ok(new { message = "Producto fabricado exitosamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}

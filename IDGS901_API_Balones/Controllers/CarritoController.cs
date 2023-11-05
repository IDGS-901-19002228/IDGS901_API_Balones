using IDGS901_API_Balones.Context;
using IDGS901_API_Balones.Models;
using Microsoft.AspNetCore.Mvc;

namespace IDGS901_API_Balones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoController : Controller
    {
        private readonly AppDbContext _context;
        public CarritoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("agregar")]
        public ActionResult AgregarProductoAlCarrito([FromBody] Carrito carritoItem)
        {
            try
            {
                _context.Carrito2.Add(carritoItem);
                _context.SaveChanges();

                return Ok("Producto agregado al carrito exitosamente.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al agregar el producto al carrito: {ex.Message}");
            }
        }



    }
}

using Microsoft.AspNetCore.Mvc;

namespace IDGS901_API_Balones.Controllers
{
    public class PedidosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

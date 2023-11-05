using Microsoft.AspNetCore.Mvc;

namespace IDGS901_API_Balones.Controllers
{
    public class ProductClientesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

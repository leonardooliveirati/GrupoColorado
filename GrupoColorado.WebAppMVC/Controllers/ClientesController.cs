using Microsoft.AspNetCore.Mvc;

namespace GrupoColorado.WebAppMVC.Controllers
{
    public class ClientesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

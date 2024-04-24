using Microsoft.AspNetCore.Mvc;

namespace MvcCorePersonajesSeriesAzure.Controllers
{
    public class PersonajesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

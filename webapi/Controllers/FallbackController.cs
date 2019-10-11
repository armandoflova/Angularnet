using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    public class FallbackController : Controller
    {
        public IActionResult Index()
        {
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(),
            "wwwroot" , "index.html"), "text/HTML");
        }
    }
}
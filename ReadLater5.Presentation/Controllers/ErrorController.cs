using Microsoft.AspNetCore.Mvc;

namespace ReadLater5.Presentation.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/Error/Index")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index() => View();

        [Route("/Error/Index/{code}")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index(int code)
        {
            switch (code)
            {
                case 500:
                    return View("500ErrorPage");
                default:
                    return View("404ErrorPage");
            }
        }
    }
}

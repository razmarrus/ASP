using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PointSystem.Controllers
{
    public class ErrorController : Controller
    {
        // GET: /<controller>/
        [Route("Error/404")]
        public IActionResult ErrorNotFound()
        {
            return View();
        }

        //[Route("Error/308")]
        public IActionResult ErrorNoPower()
        {
            return View();
        }

        [Route("Error/401")]
        public IActionResult ErrorUnauthorized()
        {
            return View();
        }
    }
}

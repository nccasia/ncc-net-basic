using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinQDemo.Web.Controllers
{
    public class HomeController : ControllerBase
    {
        public IActionResult Index()
        {
            return Redirect("/swagger");
        }
    }
}
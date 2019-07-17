using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store_Rating_System_Dev.Models;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Store_Rating_System_Dev.Controllers
{
    public class HomeController : Controller
    {

        public ViewResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(IFormFile uploadedFile)
        {
            return RedirectToAction("Index");
        }
    }
}
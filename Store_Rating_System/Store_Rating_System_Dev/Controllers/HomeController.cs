using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store_Rating_System_Dev.Models;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Identity;

namespace Store_Rating_System_Dev.Controllers
{
    public class HomeController : Controller
    {
        private IRepository repository;

        public HomeController(IRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View(repository.Stores);
        }

        [Route("Home/Error/{code:int}")]
        public ViewResult Error(int code) => View(code);

    }
}
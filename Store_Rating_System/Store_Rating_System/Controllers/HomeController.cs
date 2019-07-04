using Microsoft.AspNetCore.Mvc;
using Store_Rating_System.Models;
using System.Linq;

namespace Store_Rating_System.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepository repository;

        public HomeController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View("ListResponses", repository.Stores);
        }

        [HttpGet]
        public ViewResult RsvpForm()
        {
            return View();
        }

        [HttpPost]
        public ViewResult RsvpForm(Store guestResponse)
        {
            if (ModelState.IsValid)
            {
                Repository.AddResponse(guestResponse);
                return View("Thanks", guestResponse);
            }
            else
            {
                // there is a validation error
                return View();
            }
        }

        public ViewResult ListResponses()
        {
            return View(Repository.Responses.Where(r => r.ID != 100));
        }

        public ViewResult ListStores()
        {
            return View();
        }
    }
}
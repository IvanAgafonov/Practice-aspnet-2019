using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store_Rating_System_Dev.Models;

namespace Store_Rating_System_Dev.Controllers
{
    public class StoreController : Controller
    {

        private IRepository repository;
        private UserManager<User> userManager;

        public StoreController(IRepository repo, UserManager<User> usrMgr)
        {
            userManager = usrMgr;
            repository = repo;
        }

        // def category -1 is for selected Any Category in View
        public ViewResult Index(string name, int category = -1, string error_mes = null)
        {      
            var res = repository.Stores;

            // Is category from GET request belong to enum values
            if (((int)Categories.Electronics <= category) &&
                (category <= (int)Categories.Clothing))
            {
                res = res.Where(x => x.Category == (Categories)category);
                ViewBag.Category = category;
            }

            if (name != null)
            {
                res = res.Where(x => x.Name.Contains(name));
                ViewBag.Category = category;
            }

            if (error_mes != null)
            {
                ViewBag.Error_mes = error_mes;
                error_mes = null;
            }

            res = res.OrderByDescending(x => x.Avarange_rating);
            return View(res);
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            Store store = repository.Stores.Where(x => x.ID == id).FirstOrDefault();
            var ratings = repository.Ratings.Where(x => x.Store_ID == id);

            
            if (store != null)
            {
                Store_with_Ratings model = new Store_with_Ratings { store = store, ratings = ratings };
                return View(model);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }



        [Authorize]
        [HttpGet]
        public ActionResult Rate(int id)
        {
            Store store = repository.Stores.Where(x => x.ID == id).FirstOrDefault();

            var Name = HttpContext.User.Identity.Name;
            User Current_user = repository.Users.Where(x => x.UserName == Name).FirstOrDefault();
            bool IsRatingAlready = repository.Ratings.Where(x => x.User_ID == Current_user.Id &&
                x.Store_ID == id).Any();

            if (IsRatingAlready)
            {
                ViewBag.Error_mes = "Rating already has";
                var res = repository.Stores;
                return View("Index", res);
            }
            else
            {
                if (store != null)
                {
                    ViewBag.storeID = id;
                    ViewBag.storeName = store.Name;
                    return View();
                }
                else
                {
                    ViewBag.Error_mes = "Store not found";
                    var res = repository.Stores;
                    return View("Index", res);
                }
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Rate(int store_id, string comment, bool rate_value)
        {
            if (ModelState.IsValid)
            {
                var Name = HttpContext.User.Identity.Name;
                User Current_user = repository.Users.Where(x => x.UserName == Name).FirstOrDefault();
                Rating rating = new Rating
                {
                    User_ID = Current_user.Id,
                    Store_ID = store_id,
                    Comment = comment,
                    Rate_value = rate_value,
                    Rate_Status = Statuses.Accepted,
                    Date_of_publication = DateTime.Now,
                };


                repository.SaveRating(rating);

            }
            return RedirectToAction("Detail", store_id);
        }
    }
}
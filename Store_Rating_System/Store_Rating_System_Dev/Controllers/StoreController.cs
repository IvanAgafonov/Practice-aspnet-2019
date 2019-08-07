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

        public ViewResult Index(Store_search model)
        {      
            var res = repository.Stores;

            // Is category from GET request belong to enum values
            if (((int)Categories.Electronics <= model.category) &&
                (model.category <= (int)Categories.Clothing))
            {
                res = res.Where(x => x.Category == (Categories)model.category);
                ViewBag.Category = model.category;
            }

            if (model.name != null)
            {
                res = res.Where(x => x.Name.Contains(model.name));
                ViewBag.NameStore = model.name;
            }

            if (model.error_mes != null)
            {
                ViewBag.Error_mes = model.error_mes;
            }

            res = res.OrderByDescending(x => x.Avarange_rating);
            return View(res);
        }

        [HttpGet]
        public ActionResult Detail(string id)
        {
            Store store = repository.Stores.Where(x => x.ID == id).FirstOrDefault();
            var ratings = repository.Ratings.Where(x => x.Store_ID == id);
            var dict_user_rating = new Dictionary<User, Rating>();

            foreach (var rating in ratings)
            {
                User user = repository.Users.Where(x => x.Id == rating.User_ID).FirstOrDefault();
                dict_user_rating.Add(user, rating);
            }

            
            if (store != null)
            {
                Store_with_Ratings_Users model = new Store_with_Ratings_Users { store = store, dict_user_rating = dict_user_rating };
                return View(model);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }



        [Authorize]
        [HttpGet]
        public ActionResult Rate(string id)
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
        public ActionResult Rate(Rate_data model)
        {
            if (ModelState.IsValid)
            {
                var Name = HttpContext.User.Identity.Name;
                User Current_user = repository.Users.Where(x => x.UserName == Name).FirstOrDefault();
                Rating rating = new Rating
                {
                    User_ID = Current_user.Id,
                    Store_ID = model.store_id,
                    Comment = model.comment,
                    Rate_value = model.rate_value,
                    Rate_Status = Statuses.Accepted,
                    Date_of_publication = DateTime.Now,
                };


                repository.SaveRating(rating);

            }
            return RedirectToAction("Detail", routeValues: model.store_id);
        }
    }
}
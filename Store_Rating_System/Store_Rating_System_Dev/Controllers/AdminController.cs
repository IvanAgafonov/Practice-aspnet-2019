using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Store_Rating_System_Dev.Models;

namespace Store_Rating_System_Dev.Controllers
{
    [Authorize(Roles ="Admins")]
    public class AdminController : Controller
    {
        private UserManager<User> userManager;
        private IRepository repository;
        private IPasswordValidator<User> passwordValidator;
        private IPasswordHasher<User> passwordHasher;
        private IUserValidator<User> userValidator;

        public AdminController(IRepository repo, UserManager<User> usrMgr,
                    IPasswordValidator<User> passValid, IPasswordHasher<User> passwordHash, IUserValidator<User> userValid)
        {
            userManager = usrMgr;
            userValidator = userValid;
            passwordValidator = passValid;
            passwordHasher = passwordHash;
            repository = repo;
        }

        public ViewResult Index()
        {
            return View();
        }

        public ViewResult Ratings()
        {
            return View();
        }

        public ViewResult Stores()
        {
            return View();
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        #region Ratings
        public ViewResult RatingsRead()
        {
            return View(repository.Ratings.OrderBy(rating => rating.Store_ID));
        }

        [HttpGet]
        public ViewResult RatingsCreate()
        {
            return View();
        }


        [HttpPost]
        public ActionResult RatingsCreate(Rating model)
        {
            if (ModelState.IsValid)
            {
                Rating rating = new Rating
                {
                    User_ID = model.User_ID,
                    Store_ID = model.Store_ID,
                    Comment = model.Comment,
                    Rate_value = model.Rate_value,
                    Rate_Status = model.Rate_Status,
                    Date_of_publication = model.Date_of_publication,
                };


                repository.SaveRating(rating);

            }
            return RedirectToAction("RatingsRead");
        }

        [HttpGet]
        public ActionResult RatingsUpdate(int id)
        {
            Rating rating = repository.Ratings.Where(x => x.ID == id).FirstOrDefault();
            if (rating != null)
            {
                return View(rating);
            }
            else
            {
                return RedirectToAction("RatingsRead");
            }
        }


        [HttpPost]
        public ActionResult RatingsUpdate(Rating model)
        {
            Rating rating = repository.Ratings.Where(x => x.ID == model.ID).FirstOrDefault();
            if (ModelState.IsValid)
            {
                rating.User_ID = model.User_ID;
                rating.Store_ID = model.Store_ID;
                rating.Comment = model.Comment;
                rating.Rate_value = model.Rate_value;
                rating.Rate_Status = model.Rate_Status;
                rating.Date_of_publication = model.Date_of_publication;

                repository.SaveRating(rating);

            }
            return RedirectToAction("RatingsRead");
        }

        [HttpPost]
        public ActionResult RatingsDelete(int id)
        {
            Rating rating = repository.Ratings.Where(x => x.ID == id).FirstOrDefault();

            if (rating != null)
            {
                repository.DeleteRating(rating);
            }

            return RedirectToAction("RatingsRead");

        }
        #endregion

        #region Stores
        public ViewResult StoresRead()
        {
            return View(repository.Stores.OrderBy(store => store.Name));
        }

        [HttpGet]
        public ViewResult StoresCreate()
        {
            return View();
        }


        [HttpPost]
        public ActionResult StoresCreate(Store model, IFormFile NewImage)
        {
            if (ModelState.IsValid)
            {
                Store store = new Store
                {
                    Name = model.Name,
                    Url = model.Url,
                    Category = model.Category,
                    Number_of_ratings = model.Number_of_ratings,
                    Number_of_pos_ratings = model.Number_of_pos_ratings,
                    Description = model.Description,
                    City = model.City,
                    Country = model.Country
                };

                if (NewImage != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        NewImage.CopyTo(ms);
                        store.Image = ms.ToArray();
                    }
                }

                repository.SaveStore(store);

            }
            return RedirectToAction("StoresRead");
        }

        [HttpGet]
        public ActionResult StoresUpdate(int id)
        {
            Store store = repository.Stores.Where(x => x.ID == id).FirstOrDefault();
            if (store != null)
            {
                return View(store);
            }
            else
            {
                return RedirectToAction("UsersRead");
            }
        }


        [HttpPost]
        public ActionResult StoresUpdate(Store model, IFormFile NewImage)
        {
            Store store = repository.Stores.Where(x => x.ID == model.ID).FirstOrDefault();
            if (ModelState.IsValid)
            {
                store.Name = model.Name;
                store.Url = model.Url;
                store.Category = model.Category;
                store.Number_of_ratings = model.Number_of_ratings;
                store.Number_of_pos_ratings = model.Number_of_pos_ratings;
                store.Description = model.Description;
                store.City = model.City;
                store.Country = model.Country;
            

                if (NewImage != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        NewImage.CopyTo(ms);
                        store.Image = ms.ToArray();
                    }
                }

                repository.SaveStore(store);

            }
            return RedirectToAction("StoresRead");
        }

        [HttpPost]
        public ActionResult StoresDelete(int id)
        {
            Store store = repository.Stores.Where(x => x.ID == id).FirstOrDefault();

            if (store != null)
            {
                repository.DeleteStore(store);
            }

            return RedirectToAction("StoresRead");

        }

        #endregion

        #region Users
        public ViewResult UsersRead()
        {
            return View(repository.Users.OrderBy(store => store.UserName));
        }

        [HttpGet]
        public ViewResult UsersCreate()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UsersCreate(UserCreateModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    UserName = model.Name,
                    Email = model.Email,
                    City = model.City,
                    Country = model.Country
                };

                if (model.Image != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        model.Image.CopyTo(ms);
                        user.Image = ms.ToArray();
                    }
               
                }

                IdentityResult result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("UsersRead");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UsersDelete(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("UsersRead");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            return View("UsersRead", userManager.Users);
        }

        [HttpGet]
        public async Task<IActionResult> UsersUpdate(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("UsersRead");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UsersUpdate(User model, IFormFile NewImage, string NewPassword)
        {
            User user = await userManager.FindByIdAsync(model.Id);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(NewPassword))
                {
                    user.PasswordHash = passwordHasher.HashPassword(user, NewPassword);
                }
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.City = model.City;
                user.Country = model.Country;
                 
                if (NewImage != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        NewImage.CopyTo(ms);
                        user.Image = ms.ToArray();
                    }

                }

                IdentityResult result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("UsersRead");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            return View(user);
        }


        #endregion

    }
}
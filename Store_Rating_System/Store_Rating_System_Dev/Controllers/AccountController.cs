using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store_Rating_System_Dev.Models;

namespace Store_Rating_System_Dev.Controllers
{

    public class AccountController : Controller
    {
        private IRepository repository;
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        public AccountController(IRepository rep ,UserManager<User> userMgr,
        SignInManager<User> signinMgr)
        {
            repository = rep;
            userManager = userMgr;
            signInManager = signinMgr;
        }

        [Authorize]
        [HttpGet]
        public ActionResult Ratings()
        {

            var Name = HttpContext.User.Identity.Name;
            User Current_user = repository.Users.Where(x => x.UserName == Name).FirstOrDefault();
            IEnumerable<Rating> ratings = repository.Ratings.Where(x => x.User_ID == Current_user.Id);

            Dictionary<string, Rating> Dict = new Dictionary<string, Rating>();
            string Store_name = null;
            foreach(var rating in ratings)
            {
                Store_name = repository.Stores.Where(x => x.ID == rating.Store_ID).FirstOrDefault().Name;
                Dict.Add(Store_name, rating);
            }

            return View(Dict);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginModel details,
        string returnUrl)
        {
            
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByNameAsync(details.Name);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result =
                    await signInManager.PasswordSignInAsync(
                    user, details.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                }
                ModelState.AddModelError(nameof(UserLoginModel.Name),
                "Invalid user or password");
                
            }
            return View(details);
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult Registration()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Registration(UserCreateModel model)
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
                    return RedirectToAction("Index", "Home");
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

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Profile(string Name)
        {
            User user = null;
            string CurrentName = HttpContext.User.Identity.Name;
            ViewBag.UserName = CurrentName;

            if (Name != null)
            {
                user = await userManager.FindByNameAsync(Name);
            }
            else if (CurrentName != null)
            {      
                user = await userManager.FindByNameAsync(CurrentName);
            }
            else
            {
                return new RedirectResult(Url.Action("Login") + "?returnUrl=" + Url.Action(nameof(Profile)));
            }

            return View(user);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ProfileEdit()
        { 
            string CurrentName = HttpContext.User.Identity.Name;
            User user = await userManager.FindByNameAsync(CurrentName);

            return View(user);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProfileEdit(User model, IFormFile NewImage)
        {
            User user = await userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            if (user != null)
            {
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
                    return RedirectToAction("Profile");
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

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

    }
}
using ELearningSystem.Models;
using ELearningSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ELearningSystem.Controllers
{
    public class UserRegistrationController : Controller
    {
        public User User
        {
            get
            {
                return Session["USER"] as User;
            }

            set
            {
                Session["USER"] = value;
            }
        }


        public UserService userService = new UserService();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (!String.IsNullOrEmpty(user.UserName))
                {
                    var authenticatedUser = userService.GetUserByNameAndPass(user);
                    if (authenticatedUser != null)
                    {
                        return HandleSuccessfulLogin(authenticatedUser, returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Потребителското име и/или паролата са невалидни. Опитайте отново.");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Login data is incorrect");
            }
            return View(user);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(User user)
        {
            if (ModelState.IsValid)
            {
                if (user.Email == null)
                    user.Email = string.Empty;
                userService.InsertUser(user);
                return RedirectToAction("Login", "UserRegistration");
            }
            return View();
        }

        [HttpGet]
        public ActionResult AddRole()
        {
            var users = userService.GetAllUsers();
            return View(users);
        }

        [HttpPost]
        public ActionResult AddRole(List<User> users)
        {
            foreach (var user in users)
            {
                userService.UpdateUser(user);
            }
            return RedirectToAction("Index", "Home");
        }

        private ActionResult HandleSuccessfulLogin(User user, string returnUrl)
        {
            user.Role = userService.GetRole(user.RoleId);
            User = user;

            //FormsAuthentication.SetAuthCookie
            FormsAuthentication.SetAuthCookie(user.UserName, false);

            if (!string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = returnUrl.Trim('\'');

                var decoded = Server.UrlDecode(returnUrl);
                if (Url.IsLocalUrl(decoded))
                {
                    return Redirect(returnUrl);
                }
            }

            return RedirectToAction("Index", "Home", null);
        }

        public ActionResult EditUserRoles(int id = 0)
        {
            if (id != 0)
            {
                var user = userService.GetUserById(id);
                user.IsAdmin = true;
                userService.UpdateUser(user);
            }

            var users = userService.GetAllUsers();
            return View(users);
        }

        public ActionResult EditUserRoles1(int id = 0)
        {
            if (id != 0)
            {
                var user = userService.GetUserById(id);
                user.IsAdmin = false;
                userService.UpdateUser(user);
            }
            var users = userService.GetAllUsers();
            return RedirectToAction("EditUserRoles");
        }

    }
}

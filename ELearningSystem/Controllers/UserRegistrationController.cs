using ELearningSystem.Models;
using ELearningSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
            //if (ModelState.IsValid)
            //{
            if (!String.IsNullOrEmpty(user.UserName))
            {
                user.Password = Encrypt(user.Password);
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
            //}
            //else
            //{
            //    ModelState.AddModelError("", "Login data is incorrect");
            //}
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
            //if (ModelState.IsValid)
            //{
                if (user.Email == null)
                    user.Email = string.Empty;

                user.Password = Encrypt(user.Password);
                userService.InsertUser(user);
                return RedirectToAction("Login", "UserRegistration");
            //}
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
            foreach (var user in users)
            {
                user.Role = userService.GetRole(user.RoleId);
            }
            return View(users);
        }

        public ActionResult EditUserRoles1(int id = 0)
        {
            if (id != 0)
            {
                var user = userService.GetUserById(id);
                user.IsAdmin = !user.IsAdmin;
                userService.UpdateUser(user);
            }
            var users = userService.GetAllUsers();
            return RedirectToAction("EditUserRoles");
        }

        static string key { get; set; } = "A!9HHhi%XjjYY4YP2@Nob009X";

        public static string Encrypt(string text)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateEncryptor())
                    {
                        byte[] textBytes = UTF8Encoding.UTF8.GetBytes(text);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }
                }
            }
        }

        public static string Decrypt(string cipher)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateDecryptor())
                    {
                        byte[] cipherBytes = Convert.FromBase64String(cipher);
                        byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                        return UTF8Encoding.UTF8.GetString(bytes);
                    }
                }
            }
        }
    }
}

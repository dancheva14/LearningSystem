using ELearningSystem.Models;
using ELearningSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELearningSystem.Controllers
{
    public class StudentResultController : Controller
    {
        public TestService testService = new TestService();
        public ActionResult Index()
        {

            if (User.Identity.IsAuthenticated)
            {
                var user = Session["User"] as User;
                string sortColumn = "date";
                if (user != null)
                {
                    var testResult = testService.GetStudentResults(user.Id, sortColumn);
                    foreach (var result in testResult)
                    {
                        result.Test = testService.GetTest(result.TestId);
                    }

                    if (testResult.Count() == 0)
                    {
                        string noResult = "Ученикът още няма оценки в историята си.";
                        ViewBag.Message = noResult;
                    }
                    return View(testResult);
                }
            }
            return RedirectToAction("Login", "UserRegistration");
        }

        [HttpGet]
        public ActionResult IndexDate()
        {
            var user = Session["User"] as User;
            var sortedTests = testService.GetStudentResults(user.Id, "date");
            foreach (var result in sortedTests)
            {
                result.Test = testService.GetTest(result.TestId);
            }

            return View("Index", sortedTests);
        }

        [HttpGet]
        public ActionResult IndexName()
        {
            var user = Session["User"] as User;
            var sortedTests = testService.GetStudentResults(user.Id, "title");
            foreach (var result in sortedTests)
            {
                result.Test = testService.GetTest(result.TestId);
            }

            return View("Index", sortedTests);
        }
    }
}
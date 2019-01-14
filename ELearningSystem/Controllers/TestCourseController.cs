using ELearningSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELearningSystem.Controllers
{
    public class TestCourseController : Controller
    {
        private CourseService courseService = new CourseService();
        public ActionResult Index()
        {
            var courses = courseService.GetAllCourses();
            return View(courses);
        }
    }
}
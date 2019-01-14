using ELearningSystem.Hubs;
using ELearningSystem.Models;
using ELearningSystem.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELearningSystem.Controllers
{
    public class CoursesController : Controller
    {

        private CourseService courseService = new CourseService();

        //[Authorize]
        public ActionResult Index()
        {
            var model = courseService.GetAllCourses();
            return View();
        }

        [HttpGet]
        public ActionResult AddCourse()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCourse(Course course)
        {
            courseService.InsertCourse(course);
            return RedirectToAction("Index", "Courses");
        }


        [HttpGet]
        public ActionResult Edit(Course course, int number = 0)
        {
            var model = courseService.GetCourse(course.Id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Course course)
        {
            courseService.UpdateCourse(course);
            return RedirectToAction("Index", "Courses");
        }

        [HttpGet]
        public ActionResult Delete(Course course)
        {
            courseService.DeleteCourse(course);
            return RedirectToAction("Index", "Courses");
        }

        public ActionResult Details(int courseId)
        {
            var model = courseService.GetCourse(courseId);
            return PartialView("_Information", model);
        }

        public JsonResult Get()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CustomerConnection"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"Select [Id],[Name],[Summary] FROM [dbo].[Courses]", connection))
                {
                    command.Notification = null;
                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    var listCourse = reader.Cast<IDataRecord>()
                        .Select(x => new
                        {
                            Id = (int)x["Id"],
                            Name = (string)x["Name"],
                            Summary = (string)x["Summary"],
                        }).ToList();

                    return Json(new { listCourse = listCourse }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            CusHub.Show();
        }
    }
}

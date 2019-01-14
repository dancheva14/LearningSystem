using ELearningSystem.Models;
using ELearningSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELearningSystem.Controllers
{
    public class EditDeleteController : Controller
    {
        private LectureService lectureService = new LectureService();
        private TestService testService = new TestService();
        private CourseService courseService = new CourseService();
        private ImageService imageService = new ImageService();

        [HttpGet]
        public ActionResult DeleteLecture(int id = 0)
        {
            var user = Session["User"] as User;
            if (user == null)
                return RedirectToAction("Login", "UserRegistration");
            else
            {
                if (id != 0)
                {
                    var lecture = lectureService.GetLecture(id);
                    if (lecture != null)
                        lectureService.DeleteLecture(lecture);
                    id = 0;
                }
                var lectures = lectureService.GetAllLecture();
                return View(lectures);
            }
        }

        [HttpGet]
        public ActionResult DeleteCourse(int id = 0)
        {
            var user = Session["User"] as User;
            if (user == null)
                return RedirectToAction("Login", "UserRegistration");
            else
            {
                if (id != 0)
                {
                    var course = courseService.GetCourse(id);
                    if (course != null)
                        courseService.DeleteCourse(course);
                    id = 0;
                }
                var courses = courseService.GetAllCourses();
                return View(courses);
            }
        }

        [HttpGet]
        public ActionResult DeleteTest(int id = 0)
        {
            var user = Session["User"] as User;
            if (user == null)
                return RedirectToAction("Login", "UserRegistration");
            else
            {
                if (id != 0)
                {
                    var test = testService.GetTest(id);
                    if (test != null)
                        testService.DeleteTest(test);
                    id = 0;
                }
                var tests = testService.GetAllTests(0);
                return View(tests);
            }
        }
    }
}
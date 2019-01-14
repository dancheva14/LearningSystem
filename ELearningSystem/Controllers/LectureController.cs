using ELearningSystem.Hubs;
using ELearningSystem.Models;
using ELearningSystem.Services;
using ELearningSystem.ViewModels;
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
    public class LectureController : Controller
    {
        private LectureService lectureService = new LectureService();
        private CourseService courseService = new CourseService();
        private ImageService imageService = new ImageService();

        List<Course> courseModel = new List<Course>();
        private const string COURSEIDKEY = "COURSEIDKEY";
        private string noResult = string.Empty;

        public string CourseId
        {
            get
            {
                return Session[COURSEIDKEY] as string;
            }
            set
            {
                Session[COURSEIDKEY] = value;
            }
        }


        public ActionResult Index(int id = 0)
        {
            if (id > 0)
            {
                CourseId = id.ToString();
                courseModel.Add(courseService.GetCourse(id));
                courseModel[0].LectureList = lectureService.GetCourseLectures(id);
                courseModel[0].LectureList.ForEach(l => l.CourseId = id);
                if (courseModel[0].LectureList.Count() == 0)
                {
                    noResult = "За този курс още няма въведени лекции.";
                    ViewBag.Message = noResult;
                }
                id = 0;
                return View(courseModel);
            }
            else
            {
                courseModel = courseService.GetAllCourses();
                for (int i = 0; i < courseModel.Count(); i++)
                {
                    var lectures = lectureService.GetCourseLectures(courseModel[i].Id);
                    lectures.ForEach(l => l.CourseId = id);
                    courseModel[i].LectureList = lectures;
                }
                id = 0;
                return View(courseModel);
            }
        }

        [HttpGet]
        public ActionResult LectureList()
        {
            courseModel = courseService.GetAllCourses();
            for (int i = 0; i < courseModel.Count(); i++)
            {
                var lectures = lectureService.GetCourseLectures(courseModel[i].Id);
                foreach (var lecture in lectures)
                {
                    lecture.Images = imageService.GetItemImages(lecture.Id);
                    lecture.Image = imageService.GetItemImage(lecture.Id);
                }

                courseModel[i].LectureList = lectures;
            }
            return View(courseModel);
        }


        [HttpPost]
        public ActionResult Index(Course model)
        {
            var courseModel = courseService.GetAllCourses();
            return View(courseModel);
        }

        [HttpGet]
        public ActionResult AddLecture1(int courseId = 1, bool isPartial = true)
        {
            //if (isPartial)
            //{
            //    return PartialView(new Lecture { CourseId = courseId });
            //}

            //return View(new Lecture { CourseId = courseId });
            return View();
        }

        [HttpPost]
        public ActionResult AddLecture1(Lecture lecture)
        {

            lectureService.InsertLecture(lecture);
            var lectureId = lectureService.GetLectureByNameAndCourseId(lecture.Title, lecture.CourseId).Id;

            if (Session["file"] as IEnumerable<HttpPostedFileBase> != null)
            {
                foreach (var image in Session["file"] as IEnumerable<HttpPostedFileBase>)
                {
                    imageService.UploadImageToDB(image, lectureId);
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult CoursesDropdown(AddLectureViewModel vm)
        {
            vm.Lecture.CourseId = vm.CourseId;
            lectureService.InsertLecture(vm.Lecture);
            var lectureId = lectureService.GetLectureByNameAndCourseId(vm.Lecture.Title, vm.CourseId).Id;

            if (Session["file"] as IEnumerable<HttpPostedFileBase> != null)
            {
                foreach (var image in Session["file"] as IEnumerable<HttpPostedFileBase>)
                {
                    imageService.UploadImageToDB(image, lectureId);
                }
            }

            return RedirectToAction("LectureList", "Lecture");
        }

        [HttpGet]
        public ActionResult Edit(Lecture lecture)
        {
            var model = lectureService.GetLecture(lecture.Id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Lecture lecture, int number = 0)
        {
            lectureService.UpdateLecture(lecture);
            return RedirectToAction("LectureList", "Lecture");
        }

        [HttpGet]
        public ActionResult Delete(Lecture lecture)
        {
            lectureService.DeleteLecture(lecture);
            return RedirectToAction("LectureList", "Lecture");
        }

        public ActionResult GetCoursesDropdown()
        {
            return View("CoursesDropdown");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void UploadImage(IEnumerable<HttpPostedFileBase> files)
        {
            Session["file"] = files;
        }

        public JsonResult Get(int Id)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CustomerConnection"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"Select [Id],[Title] FROM [dbo].[Lectures] WHERE [CourseId] = " + Id, connection))
                {
                    command.Notification = null;
                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    var listLecture = reader.Cast<IDataRecord>()
                        .Select(x => new
                        {
                            Id = (int)x["Id"],
                            Name = (string)x["Title"]
                        }).ToList();

                    return Json(new { listLecture = listLecture }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            CusHub.Show();
        }
    }
}
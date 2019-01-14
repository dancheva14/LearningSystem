using ELearningSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELearningSystem.Controllers
{
    public class HomeController : Controller
    {


        private ImageService imageService = new ImageService();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Status()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Електронна система за обучение и провеждане на електронни тестове";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //[HttpGet]
        //public ActionResult UploadVideo(int lectureId = 6)
        //{
        //    List<VideoFile> videolist = imageService.GetAllVideoFile(lectureId);
        //    return View(videolist);
        //}

        //[HttpPost]
        //public ActionResult UploadVideo(HttpPostedFileBase fileupload)
        //{
        //    if (fileupload != null)
        //    {
        //        string fileName = Path.GetFileName(fileupload.FileName);
        //        int fileSize = fileupload.ContentLength;
        //        int Size = fileSize / 1000;
        //        fileupload.SaveAs(Server.MapPath("~/VideoFileUpload/" + fileName));

        //        imageService.AddNewVideoFile("goshu", fileSize, fileName, 6);
        //    }
        //    return RedirectToAction("UploadVideo");
        //}

        public JsonResult GetNotificationContacts()
        {
            var notificationRegisterTime = Session["LastUpdated"] != null ? Convert.ToDateTime(Session["LastUpdated"]) : DateTime.Now;
            NotificationComponent NC = new NotificationComponent();
            var list = NC.GetContacts(notificationRegisterTime);
            //update session here for get only new added contacts (notification)
            Session["LastUpdate"] = DateTime.Now;
            return new JsonResult { Data = list, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
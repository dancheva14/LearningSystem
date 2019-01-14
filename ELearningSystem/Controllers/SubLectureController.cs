using ELearningSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELearningSystem.Controllers
{
    public class SubLectureController : Controller
    {
        private LectureService lectureService = new LectureService();
        private ImageService imageService = new ImageService();

        [Authorize]
        public ActionResult Index(int id)
        {
            var model = lectureService.GetLecture(id);
            model.Images = imageService.GetItemImages(model.Id);
            model.Image = imageService.GetItemImage(model.Id);

            if (model == null)
                return View();
            else
                return View(model);
        }
    }
}
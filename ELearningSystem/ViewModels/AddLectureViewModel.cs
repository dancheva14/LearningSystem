using ELearningSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELearningSystem.ViewModels
{
    public class AddLectureViewModel
    {
        public Lecture Lecture { get; set; }

        public int CourseId { get; set; }
    }
}
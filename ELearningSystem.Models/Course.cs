using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearningSystem.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Display(Name = "strCourseName", ResourceType = typeof(Resources.Resource))]
        public string Name { get; set; }

        [Display(Name = "strCourseContent", ResourceType = typeof(Resources.Resource))]
        public string Summary { get; set; }

        public List<Lecture> LectureList { get; set; }
       
    }
}

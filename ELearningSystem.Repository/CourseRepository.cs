using ELearningSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearningSystem.Repository
{
    public class CourseRepository : BaseRepository
    {

        public Course GetCourse(int id)
        {
            return QueryFirst<Course>("GetCourse", new { pId = id });
        }

        public List<Course> GetAllCourses()
        {
            return QueryMultiple<Course>("GetAllCourses");
        }

        public Course InsertCourse(Course course)
        {
            return QueryFirst<Course>("InsertCourse", new { name = course.Name, summary = course.Summary });
        }

        public Course UpdateCourse(Course c)
        {
            return QueryFirst<Course>("UpdateCourse", new { Id = c.Id, name = c.Name, summary = c.Summary });
        }

        public Course DeleteCourse(int courseid)
        {
            return QueryFirst<Course>("DeleteCourse", new { Id = courseid });
        }
    }
}

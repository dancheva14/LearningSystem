using ELearningSystem.Models;
using ELearningSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearningSystem.Services
{
    public class CourseService
    {
        private CourseRepository courseRepository;

        public CourseService()
        {
            courseRepository = new CourseRepository();
        }

        public Course GetCourse(int id)
        {
            return courseRepository.GetCourse(id);
        }

        public List<Course> GetAllCourses()
        {
            return courseRepository.GetAllCourses();
        }

        public Course InsertCourse(Course course)
        {
            return courseRepository.InsertCourse
                    (course);
        }

        public Course UpdateCourse(Course course)
        {
            return courseRepository.UpdateCourse
                    (course);
        }
        public Course DeleteCourse(Course course)
        {
            return courseRepository.DeleteCourse
                    (course.Id);
        }
    }
}
using ELearningSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearningSystem.Repository
{
    public class LectureRepository : BaseRepository
    {
        public Lecture GetLecture(int lectureid)
        {
            return QueryFirst<Lecture>("GetLecture", new { pId = lectureid });
        }

        public List<Lecture> GetAllLecture()
        {
            return QueryMultiple<Lecture>("GetAllLecture");
        }

        public List<Lecture> GetCourseLectures(int courseid)
        {
            return QueryMultiple<Lecture>("GetCourseLectures", new { pId = courseid });
        }

        public Lecture InsertLecture(Lecture lecture)
        {
            return QueryFirst<Lecture>("InsertLecture", new { courseid = lecture.CourseId, title = lecture.Title, content = lecture.Content });
        }

        public Lecture GetLectureByNameAndCourseId(string lectureName, int courseId)
        {
            return QueryFirst<Lecture>("GetLectureByNameAndCourseId", new { @plectureName = lectureName, @pcourseId = courseId });
        }

        public Lecture UpdateLecture(Lecture lecture)
        {
            return QueryFirst<Lecture>("UpdateLecture",
                                      new { Id = lecture.Id, courseid = lecture.CourseId, title = lecture.Title, content = lecture.Content });
        }

        public Lecture DeleteLecture(int id)
        {
            return QueryFirst<Lecture>("DeleteLecture",
                                      new { Id = id });
        }
    }
}
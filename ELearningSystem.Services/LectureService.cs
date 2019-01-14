using ELearningSystem.Models;
using ELearningSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearningSystem.Services
{
    public class LectureService
    {
        private LectureRepository lectureRepository;

        public LectureService()
        {
            lectureRepository = new LectureRepository();
        }

        public Lecture GetLecture(int id)
        {
            return lectureRepository.GetLecture(id);
        }

        public List<Lecture> GetAllLecture()
        {
            return lectureRepository.GetAllLecture();
        }

        public List<Lecture> GetCourseLectures(int courseid)
        {
            return lectureRepository.GetCourseLectures(courseid);
        }
        public Lecture InsertLecture(Lecture lecture)
        {
            return lectureRepository.InsertLecture
                    (lecture);
        }

        public Lecture GetLectureByNameAndCourseId(string lectureName, int courseId)
        {
            return lectureRepository.GetLectureByNameAndCourseId
                    (lectureName, courseId);
        }

        public Lecture UpdateLecture(Lecture lecture)
        {
            return lectureRepository.UpdateLecture
                    (lecture);
        }

        public Lecture DeleteLecture(Lecture lecture)
        {
            return lectureRepository.DeleteLecture
                    (lecture.Id);
        }

    }
}
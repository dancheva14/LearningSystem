using ELearningSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearningSystem.Repository
{
    public class TestRepository : BaseRepository
    {
        public Test GetTest(int id)
        {
            return QueryFirst<Test>("GetTest", new { pId = id });
        }

        public List<Test> GetAllTests(int courseid)
        {
            return QueryMultiple<Test>("GetAllTests", new { courseid = courseid });
        }

        public List<Question> GetTestQuestions(int testid)
        {
            return QueryMultiple<Question>("GetTestQuestions", new { pId = testid });
        }

        public List<Answer> GetQuestionAnswers(int questionid)
        {
            return QueryMultiple<Answer>("GetQuestionAnswers", new { pId = questionid });
        }

        public TestResult InsertResult(TestResult result)
        {
            return QueryFirst<TestResult>("InsertResult", new
            {
                correctAnswers = result.CorrectAnswers,
                userId = result.UserId,
                date = result.Date,
                grade = result.Grade,
                testId = result.TestId,
                wrongAnswers = result.WrongAnswers,
                emptyAnswers = result.EmptyAnswers
            });
        }

        public List<TestResult> GetStudentResults(int userId, string sortcolumn)
        {
            return QueryMultiple<TestResult>("GetStudentResults", new { userId = userId, sortcolumn = sortcolumn });
        }

        public Test InsertTest(Test test)
        {
            return QueryFirst<Test>("InsertTest", new
            {
                @ptitle = test.Title,
                @ppoints = test.Points,
                @pcourseId = test.CourseId
            });
        }

        public Question InsertTestQuestion(Question question, int testId)
        {
            return QueryFirst<Question>("InsertTestQuestion", new
            {
                @pcontent = question.Content,
                @pcomment = question.Comment,
                @panswer = question.Answer,
                @prightAnswerIndex = question.RightAnswerIndex,
                @panswersCount = question.AnswersCount,
                @ptestId = testId
            });
        }

        public Answer InsertTestAnswer(Answer answer, int questionId)
        {
            return QueryFirst<Answer>("InsertTestAnswer", new
            {
                @pcontent = answer.Content,
                @pisChecked = answer.IsChecked,
                @pquestionId = questionId
            });
        }

        public Test DeleteTest(Test test)
        {
            return QueryFirst<Test>("DeleteTest", new
            {
                @pId = test.Id
            });
        }

        public List<Question> GetAllQuestions()
        {
            return QueryMultiple<Question>("GetAllQuestions");
        }
    }
}

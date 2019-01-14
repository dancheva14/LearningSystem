using ELearningSystem.Models;
using ELearningSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearningSystem.Services
{
    public class TestService
    {
        private TestRepository testRepository;

        public TestService()
        {
            testRepository = new TestRepository();
        }

        public Test GetTest(int testid)
        {
            return testRepository.GetTest(testid);
        }

        public List<Test> GetAllTests(int courseid)
        {
            return testRepository.GetAllTests(courseid).ToList();
        }
        public List<Question> GetTestQuestions(int testid)
        {
            return testRepository.GetTestQuestions(testid);
        }

        public List<Answer> GetQuestionAnswers(int questionid)
        {
            return testRepository.GetQuestionAnswers(questionid);
        }
        public TestResult InsertResult(TestResult result)
        {
            return testRepository.InsertResult(result);
        }

        public List<TestResult> GetStudentResults(int studentid, string sortColumn)
        {
            return testRepository.GetStudentResults(studentid, sortColumn);
        }

        public List<AnswerType> GetAnswerTypes()
        {
            List<AnswerType> list = new List<AnswerType>();
            list.Add(new AnswerType()
            {
                Id = 1,
                Name = "Да/Не"
            });
            list.Add(new AnswerType()
            {
                Id = 2,
                Name = "Въпрос с избираем отговор"
            });
            //list.Add(new AnswerType()
            //{
            //    Id = 3,
            //    Name = "Комбобокс"
            //});
            list.Add(new AnswerType()
            {
                Id = 3,
                Name = "Попълване"
            });

            return list;
        }

        public Test InsertTest(Test test)
        {
            return testRepository.InsertTest(test);
        }

        public Question InsertTestQuestion(Question question, int testId)
        {
            return testRepository.InsertTestQuestion(question, testId);
        }

        public Answer InsertTestAnswer(Answer answer, int questionId)
        {
            return testRepository.InsertTestAnswer(answer, questionId);
        }

        public Test DeleteTest(Test test)
        {
            return testRepository.DeleteTest(test);
        }

        public List<Question> GetAllQuestions()
        {
            return testRepository.GetAllQuestions();
        }
    }
}

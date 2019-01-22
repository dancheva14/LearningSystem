using ELearningSystem.Models;
using ELearningSystem.Services;
using ELearningSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELearningSystem.Controllers
{
    public class TestsController : Controller
    {
        private const string TESTKEY = "TESTKEY";
        private const string COURSEKEY = "COURSEKEY";
        List<Test> testModel = new List<Test>();
        private const int TESTQUESTIONSCOUNT = 10;
        private TestService testService = new TestService();
        private CourseService courseService = new CourseService();
        public Test CurrentTest
        {
            get
            {
                return Session[TESTKEY] as Test ?? new Test();
            }
            set
            {
                Session[TESTKEY] = value;
            }
        }

        public User User
        {
            get
            {
                return Session["USER"] as User;
            }

            set
            {
                Session["USER"] = value;
            }
        }

        public ActionResult Index(int id = 0)
        {
            if (id != 0)
            {
                var testModel = testService.GetTest(id);
                List<Test> tests = testService.GetAllTests(id);
                if (tests.Count() > 0)
                {
                    return AllTests(tests);
                }
                else
                {
                    if (testModel != null)
                    {
                        testModel.Questions = testService.GetTestQuestions(testModel.Id);
                        foreach (var question in testModel.Questions)
                        {
                            question.Answers = testService.GetQuestionAnswers(question.Id);

                            foreach (var answer in question.Answers)
                            {
                                answer.Color = "Black";
                            }
                        }

                        CurrentTest = testModel;

                        return View(testModel);
                    }
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult AllTests(List<Test> tests)
        {
            return View("AllTests", tests);
        }

        [HttpPost]
        public ActionResult SubmitTest(Test test)
        {

            for (int i = 0; i < test.Questions.Count(); i++)
            {
                test.Questions[i].RightAnswerIndex = CurrentTest.Questions[i].RightAnswerIndex;
                test.Questions[i].Content = CurrentTest.Questions[i].Content;
                test.Questions[i].Id = CurrentTest.Questions[i].Id;
            }

            var user = Session["USER"] as User;
            int correctAnswers = GradeTest(test);
            var realTest = testService.GetTest(test.Id);
            test.CourseId = realTest.CourseId;
            test.Points = realTest.Points;
            test.Title = realTest.Title;
            test.QuestionCount = test.Questions.Count();
            Session["CurrentTest"] = test;
            var testR = new TestResult();

            var testResult = new TestResultViewModel();

            DateTime date = DateTime.Now; // will give the date for today
            string dateWithFormat = date.ToLongDateString();
            testResult.Date = DateTime.Now;
            testResult.TestId = test.Id;
            testResult.UserId = user.Id;
            testResult.CorrectAnswers = correctAnswers;
            testResult.EmptyAnswers = EmptyAnswers(test);
            testResult.WrongAnswers = test.Questions.Select(q => q.Answers.Where(a => a.IsSelected).Count()).Sum() - (testResult.EmptyAnswers + correctAnswers);
            testResult.Procent = ((test.QuestionCount - correctAnswers) / test.QuestionCount) * 100;

            double studentGrade = ((correctAnswers * 10) / (test.Points)) * 6;

            if (studentGrade <= 2.5)
            {
                testResult.Status = "Bad";
                testResult.Grade = "Слаб(2)";
            }
            else if (studentGrade <= 3.5)
            {
                testResult.Status = "Bad";
                testResult.Grade = "Среден(3)";
            }
            else if (studentGrade <= 4.5)
            {
                testResult.Status = "Good";
                testResult.Grade = "Добър(4)";
            }
            else if (studentGrade <= 5.5)
            {
                testResult.Status = "Excellent";
                testResult.Grade = "Много добър(5)";
            }
            else
            {
                testResult.Status = "Excellent";
                testResult.Grade = "Отличен(6)";
            }

            testR = new TestResult()
            {
                TestId = testResult.TestId,
                Grade = testResult.Grade,
                Status = testResult.Status,
                Date = testResult.Date,
                CorrectAnswers = testResult.CorrectAnswers,
                UserId = testResult.UserId,
                Procent = testResult.Procent,
                WrongAnswers = testResult.WrongAnswers,
                EmptyAnswers = testResult.EmptyAnswers
            };

            Session["TestResult"] = testR;
            testService.InsertResult(testR);
            return View("TestResult", testR);
        }

        [HttpGet]
        public ActionResult CheckCorrectAnswers(Test t)
        {
            var test = Session["CurrentTest"];

            Session.Remove("CurrentTest");
            return View(test);
        }

        [HttpPost]
        public ActionResult CheckCorrectAnswers()
        {
            var currentTest = Session["CurrentTest"] as Test;
            var realTest = GetTest(currentTest.Id);
            var test = new Test();

            test = currentTest;


            //foreach (var question in test.Questions)
            //{
            //    question.Answers = testService.GetQuestionAnswers(question.Id);
            //}

            for (int i = 0; i < test.Questions.Count(); i++)
            {
                for (int j = 0; j < test.Questions[i].Answers.Count(); j++)
                {
                    if (test.Questions[i].Answers[j].IsSelected && realTest.Questions[i].Answers[j].IsChecked)
                        test.Questions[i].Answers[j].Color = "Green";

                    if (test.Questions[i].Answers[j].IsSelected && !realTest.Questions[i].Answers[j].IsChecked)
                        test.Questions[i].Answers[j].Color = "Red";

                    if(!test.Questions[i].Answers[j].IsSelected && realTest.Questions[i].Answers[j].IsChecked)
                        test.Questions[i].Answers[j].Color = "Green";

                }
            }

            Session["CurrentTest"] = test;

            return View("CheckCorrectAnswers", test);
        }

        private Test GetTest(int testId)
        {
            var realTest = testService.GetTest(testId);
            realTest.Questions = testService.GetTestQuestions(testId);
            foreach (var q in realTest.Questions)
            {
                q.Answers = testService.GetQuestionAnswers(q.Id);
            }

            return realTest;
        }

        private int GradeTest(Test test)
        {
            var realTest = GetTest(test.Id);

            int countRightAnswers = 0;
            int countWrongAnswers = 0;
            if (test.Questions == null)
                return 0;

            for (int i = 0; i < test.Questions.Count(); i++)
            {
                for (int j = 0; j < test.Questions[i].Answers.Count(); j++)
                {
                    if (test.Questions[i].Answers[j].IsSelected && realTest.Questions[i].Answers[j].IsChecked)
                    {
                        countRightAnswers++;
                    }
                    if (test.Questions[i].Answers[j].IsSelected && !realTest.Questions[i].Answers[j].IsChecked)
                        countWrongAnswers++;
                }
            }

            return countRightAnswers;
        }

        private int EmptyAnswers(Test test)
        {
            int countEmptyAnswers = 0;
            if (test.Questions == null)
                return 0;
            for (int i = 0; i < test.Questions.Count(); i++)
            {
                if (test.Questions[i].Answers.Select(a => a.IsSelected).Count() == 0)
                    countEmptyAnswers++;
            }

            return countEmptyAnswers;
        }

        [HttpGet]
        public ActionResult InsertTest()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertTest(Test test)
        {
            Session["Test"] = test;
            Session["QCount"] = test.QuestionCount;
            return RedirectToAction("FillTable", "Tests");
        }

        [HttpGet]
        public ActionResult FillTable(string userAction)
        {
            if (userAction == "save")
                ViewBag.SubmitValue = "Запази";
            else
                ViewBag.SubmitValue = "Продължи";
            return View();
        }

        [HttpPost]
        public ActionResult FillTable(Question question, FormCollection f)
        {

            //int index = Convert.ToInt32(Request.Form["isChecked"]);
            var test = Session["Test"] as Test;
            if (test.Questions == null)
                test.Questions = new List<Question>();
            //question.RightAnswerIndex = index + 1;

            test.Questions.Add(question);
            Session["Test"] = test;


            string userAction;

            if (test.QuestionCount == test.Questions.Count() + 1)
                userAction = "save";
            else
                userAction = "continue";


            if (test.QuestionCount == test.Questions.Count())
            {
                SaveTest(test);
                return RedirectToAction("Index", "Home");
            }
            else
                return RedirectToAction("FillTable", "Tests", new { userAction = userAction });
        }

        public void SaveTest(Test test)
        {
            test.Points = test.Questions.Select(q => q.Answers.Where(a => a.IsChecked).Count()).Sum() * 10;

            testService.InsertTest(test);

            int testId = testService.GetAllTests(0).OrderBy(m => m.Id).LastOrDefault().Id;
            foreach (var q in test.Questions)
            {
                testService.InsertTestQuestion(q, testId);
                foreach (var a in q.Answers)
                {
                    int qId = testService.GetAllQuestions().OrderBy(m => m.Id).LastOrDefault().Id;
                    testService.InsertTestAnswer(a, qId);
                }
            }
            Session.Remove("Test");
            Session.Remove("QCount");
        }
    }
}
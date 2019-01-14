using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELearningSystem.ViewModels
{
    public class TestResultViewModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int CorrectAnswers { get; set; }

        public int EmptyAnswers { get; set; }

        public int WrongAnswers { get; set; }

        public decimal Procent { get; set; }

        public string Status { get; set; }

        public int UserId { get; set; }

        public int TestId { get; set; }

        public string Grade { get; set; }

    }
}
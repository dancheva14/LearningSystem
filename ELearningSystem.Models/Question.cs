using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearningSystem.Models
{
    public class Question
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string Comment { get; set; }

        public int Answer { get; set; }

        public List<Answer> Answers { get; set; }

        public int RightAnswerIndex { get; set; }

        public int AnswersCount { get; set; }

        public int QuestionTypeId { get; set; }

        public string SelectedAnswer { get; set; }
    }
}

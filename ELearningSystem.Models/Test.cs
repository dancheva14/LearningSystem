using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearningSystem.Models
{
    public class Test
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public double Points { get; set; }

        public int CourseId { get; set; }

        public List<Question> Questions { get; set; }
        public int QuestionCount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearningSystem.Models
{
    public class Answer
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public bool IsChecked { get; set; }

        public string Color { get; set; }
    }
}
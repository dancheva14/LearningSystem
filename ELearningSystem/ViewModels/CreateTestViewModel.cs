using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELearningSystem.ViewModels
{
    public class CreateTestViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int AnswersCount { get; set; }

        public int EnumId { get; set; }
    }
}
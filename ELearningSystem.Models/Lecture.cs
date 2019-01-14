using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearningSystem.Models
{
    public class Lecture
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int CourseId { get; set; }

        public Image Image { get; set; }

        public List<Image> Images { get; set; }

        public DateTime AddedOn { get; set; }

    }
}

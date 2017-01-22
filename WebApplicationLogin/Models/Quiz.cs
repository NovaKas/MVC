using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationLogin.Models
{
    public class Quiz
    {
        public int QuizID { get; set; }
        public string Name { get; set; }
        public int Timer { get; set; }

        //public int QuestionID { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationLogin.Models.ViewModels
{
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public string Content { get; set; }
        public string GoodAnswer { get; set; }
        public string BadAnswer { get; set; }
        public int Points { get; set; }
    }
}
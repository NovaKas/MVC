using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationLogin.Models.ViewModels
{
    public class QuestionViewModel
    {
        public QuestionViewModel()
        {
            QuizIds = new List<int>();
        }

        public int Id { get; set; }
        [Display(Name = "Quizy")]
        public List<int> QuizIds { get; set; }
        public string Content { get; set; }
        public string GoodAnswer { get; set; }
        public string BadAnswer { get; set; }
        public int Points { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationLogin.Models
{
    public class Grade
    {
        
        public int GradeID { get; set; }

        [Display(Name = "Uczeń")]
        public int StudentID { get; set; }

        [Display(Name = "Ocena")]   
        public int GradeListID { get; set; }

        [Display(Name = "Waga")]
        public float Weight { get; set; }

        [Display(Name = "Opis oceny")]
        public string Description { get; set; }

        [Display(Name = "Data wystawienia")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy.MM.dd}")]
        public DateTime DateGrade { get; set; }

        [Display(Name = "Przedmiot")]
        public int SubjectID { get; set; }

        public virtual Subject Subject { get; set; }
        public virtual Student Student { get; set; }
        public virtual GradeList GradeList { get; set; }
    }
}
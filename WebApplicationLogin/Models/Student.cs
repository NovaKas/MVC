using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationLogin.Models
{
    public class Student
    {
        [Display(Name = "Uczeń")]
        public int StudentID { get; set; }
        [Display(Name = "Imię i Nazwisko")]
        public string Name { get; set; }

        public int SClassID { get; set; }

        public virtual SClass SClass { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
    }
}
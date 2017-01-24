using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationLogin.Models
{
    public class Teacher
    {
        public int TeacherID { get; set; }

        [Display(Name ="Imię i Nazwisko")]
        public string Name { get; set; }

        //[Display(Name = "Wychowawca klasy")]
        //public int? SClassID { get; set; }

        public virtual Subject Subject { get; set; }
        public virtual ICollection<SClass> SClass { get; set; }

    }
}
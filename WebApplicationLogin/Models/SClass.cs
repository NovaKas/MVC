using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplicationLogin.Models
{
    public class SClass
    {

        public int SClassID { get; set; }
        [Display(Name = "Klasa")]
        public string Name { get; set; }

        [Display(Name = "Wychowawca")]
        public int TeacherID { get; set; }

        [Display(Name = "Lista przedmiotów:")]
        public int PlanID { get; set; }

        public virtual Plan Plan { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<Student> Students { get; set; }



    }
}
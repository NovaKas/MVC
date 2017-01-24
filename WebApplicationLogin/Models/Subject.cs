using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationLogin.Models
{
    public class Subject
    {
        [Display(Name = "Przedmiot")]
        public int SubjectID { get; set; }
        [Display(Name="Nazwa przedmiotu")]
        public string Name { get; set; }
        [Display(Name = "Sylabus (ścieżka)")]
        public string FilePath { get; set; }


        public virtual ICollection<Teacher> Teachers { get; set; }
        //public virtual ICollection<SClass> SClasses { get; set; }
        public virtual ICollection<PlanSubject> PlansSubjects { get; set; }
    }
}
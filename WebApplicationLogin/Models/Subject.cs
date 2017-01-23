using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationLogin.Models
{
    public class Subject
    {


        public int SubjectID { get; set; }
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Display(Name = "Sylabus (ścieżka)")]
        public string FilePath { get; set; }

        //public int MySubjectID { get; set; }
        //public int PlanID { get; set; }

        public virtual ICollection<MySubject> MySubjects { get; set; }
        //public virtual ICollection<SClass> SClasses { get; set; }
        public virtual ICollection<PlanSubject> PlansSubjects { get; set; }
    }
}
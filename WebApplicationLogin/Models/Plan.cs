using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationLogin.Models
{
    public class Plan
    {
        public int PlanID { get; set; }
        public string Nazwa { get; set; }
        [Display(Name="Przedmiot: ")]
        public int SubjectID { get; set; }

        public virtual ICollection<PlanSubject> PlanSubjects { get; set; }
        public virtual ICollection<SClass> SClasses { get; set; }
    }
}
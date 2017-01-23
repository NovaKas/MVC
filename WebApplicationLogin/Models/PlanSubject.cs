using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationLogin.Models
{
    public class PlanSubject
    {
        public int PlanSubjectID { get; set; }

        public int PlanID { get; set; }
        public int SubjectID { get; set; }

        public virtual Plan Plan { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
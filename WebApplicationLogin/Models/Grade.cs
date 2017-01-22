using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationLogin.Models
{
    public class Grade
    {
        public int GradeID { get; set; }
        public string Description { get; set; }
        public int Weight { get; set; }
        public DateTime DateGrade { get; set; }

        public int MySubjectID { get; set; }
        //public int StudentID { get; set; }
        public string userID { get; set; }
        public int GradeListID { get; set; }

        //public virtual Teacher Teacher { get; set; }
        //public virtual Student Student { get; set; }
        public virtual ApplicationUser user { get; set; }
        public virtual GradeList GradeList { get; set; }
    }
}
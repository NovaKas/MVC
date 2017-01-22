using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplicationLogin.Models
{
    public class MySubject
    {
        public int MySubjectID { get; set; }
        //public int TeacherID { get; set; }
        public string userID { get; set; }
        public int SubjectID { get; set; }

        [ForeignKey("userID")]
        [InverseProperty("MySubjects")]
        public virtual ApplicationUser user { get; set; }
//public virtual Teacher Teacher { get; set; }
        [ForeignKey("SubjectID")]
        [InverseProperty("MySubjects")]
        public virtual Subject Subject { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationLogin.Models
{
    public class GradeList
    {
        public int GradeListID { get; set; }
        public int Value { get; set; }
        public string Name { get; set; }

        //public int GradeID { get; set; }

        //public virtual ICollection<Grade> Grade { get; set; }
    }
}
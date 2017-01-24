using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationLogin.Models
{
    public class GradeList
    {
        public int GradeListID { get; set; }
        [Display(Name = "Wartość")]
        public int Value { get; set; }
        [Display(Name = "Nazwa oceny")]
        public string Name { get; set; }

        public virtual ICollection<Grade> Grade { get; set; }
    }
}
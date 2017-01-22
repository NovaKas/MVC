using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationLogin.Models
{
    public class Subject
    {
        public int SubjectID { get; set; }
        public string Name { get; set; }

        public int MySubjectID { get; set; }
        public int SClassID { get; set; }

        public string FilePath { get; set; }

        public virtual ICollection<MySubject> MySubjects { get; set; }
        public virtual ICollection<SClass> SClasses { get; set; }
    }
}
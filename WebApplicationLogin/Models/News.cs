using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationLogin.Models
{
    public class News
    {
        public int NewsID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public string userID { get; set; }

        public virtual ApplicationUser user { get; set; }
    }
}
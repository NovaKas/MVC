using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationLogin.Models;

namespace WebApplicationLogin.Helpers
{
    public static class StartupHelper
    {
        private static ApplicationDbContext context = new ApplicationDbContext();

        public static bool SetDefaultRolesIfNotExists()
        {
            if(!context.Roles.Any())
            {
                var defaultRoles = new List<string>() { "Teacher" , "Student", "Parent" };
                for(int i=0; i< defaultRoles.Count(); ++i)
                {
                    context.Roles.Add(new IdentityRole
                    {
                        Name = defaultRoles[i]
                    });
                }

                context.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
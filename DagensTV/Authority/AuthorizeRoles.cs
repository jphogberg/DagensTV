using DagensTV.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DagensTV.Authority
{
    public class AuthorizeRoles : AuthorizeAttribute
    {
        private readonly string[] userAssignedRole;
        private DbOperations db = new DbOperations();

        public AuthorizeRoles(params string[] roles)
        {
            this.userAssignedRole = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            foreach(var roles in userAssignedRole)
            {
                authorize = db.IsInRole(httpContext.User.Identity.Name, roles);
                if (authorize)
                {
                    return authorize;
                }
            }
            return authorize;
        }
    }
}
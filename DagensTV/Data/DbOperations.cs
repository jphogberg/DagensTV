using DagensTV.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DagensTV.Data
{
    public class DbOperations
    {
        private DagensTVEntities db = new DagensTVEntities();

        public bool CheckUser(string username, string password)
        {
            var user = db.Person.Where(
                x => x.Username.Equals(username) && x.Password.Equals(password));
            if (user.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsInRole(string userName, string roleName)
        {
            var role = db.Person
                .Where(x => x.Username.Equals(userName))
                .Include(x => x.Role).Where(x => x.Role.Name.Equals(roleName));
            return role.Any();
        }

    }
}
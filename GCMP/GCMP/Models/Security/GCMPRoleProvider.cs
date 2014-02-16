using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using GCMP.Models.Entities;

namespace GCMP.Models.Security
{
    public class GCMPRoleProvider:RoleProvider
    {
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            using (var db = new GCMPDBEntities())
            {
                var user =
                    db.Users.FirstOrDefault(
                        u => u.UserName.Equals(username, StringComparison.CurrentCultureIgnoreCase));
                if (user != null)
                {
                    return new string[] { user.Role.RoleName };
                }
                return new string[] { };
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            using (var db = new GCMPDBEntities())
            {
                var user =
                    db.Users.FirstOrDefault(
                        u => u.UserName.Equals(username, StringComparison.CurrentCultureIgnoreCase));
                if (user != null && user.Role.RoleName.Equals(roleName))
                    return true;
                return false;
            }
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
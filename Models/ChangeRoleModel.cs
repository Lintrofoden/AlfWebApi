using Microsoft.AspNetCore.Identity;

namespace WebApiWeb.Models
{
    public class UserRoleModel
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public List<IdentityRole> AllRoles { get; set; }
        public List<string> UserRoles { get; set; }

        public UserRoleModel()
        {
            AllRoles = new List<IdentityRole>();
            UserRoles = new List<string>();
        }
    }
}
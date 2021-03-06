using TutoringAppProject.Models.Enums;

namespace TutoringAppProject.Models.Users
{
    public class Admin
    {
        public string Key { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public RoleType Role { get; set; }
    }
}
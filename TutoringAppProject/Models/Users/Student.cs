using TutoringAppProject.Models.Enums;

namespace TutoringAppProject.Models.Users
{
    public class Student 
    {
        public string Key { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public RoleType Role { get; set; }
        public bool IsVerified { get; set; }
        public string[] Courses { get; set; }
    }
}

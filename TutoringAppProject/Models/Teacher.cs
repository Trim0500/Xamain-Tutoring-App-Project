using TutoringAppProject.Enums;

namespace TutoringAppProject.Models
{
    public class Teacher : SystemUser
    {
        public string[] courses { get; set; }
        public Teacher()
        {
            
        }

        public Teacher(string firstName, string lastName, string userName, string[] courses, bool isVerified)
        {
            this.firstName = firstName;
            this.lastName= lastName;
            this.userName = userName;
            this.password = password;
            this.courses = courses;
            this.isVerified = isVerified;
            role = RoleType.Student;
        }
    }
}

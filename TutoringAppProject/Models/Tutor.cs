using TutoringAppProject.Enums;

namespace TutoringAppProject.Models
{
    public class Tutor : SystemUser
    {
        public Tutor()
        {
            
        }

        public Tutor(string firstName, string lastName, string userName, string password, string[] courses, bool isVerified)
        {
            this.firstName = firstName;
            this.lastName= lastName;
            this.userName = userName;
            this.password = password;
            this.courses = courses;
            this.isVerified = isVerified;
            role = RoleType.Tutor;
        }
    }
}

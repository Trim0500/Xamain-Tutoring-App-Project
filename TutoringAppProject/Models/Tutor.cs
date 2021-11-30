using TutoringAppProject.Enums;

namespace TutoringAppProject.Models
{
    public class Tutor : SystemUser
    {
        public Tutor()
        {
            
        }

        public Tutor(string firstName, string lastName, string userName, bool isVerified)
        {
            this.firstName = firstName;
            this.lastName= lastName;
            this.userName = userName;
            this.password = password;
            this.isVerified = isVerified;
            role = RoleType.Tutor;
        }
    }
}

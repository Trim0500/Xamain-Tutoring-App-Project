using TutoringAppProject.Enums;

namespace TutoringAppProject.Models
{
    public class Admin : User
    {
        public Admin() {
            
        }
        
        public Admin(string firstName, string lastName, string userName, string password)
        {
            this.firstName = firstName;
            this.lastName= lastName;
            this.userName = userName;
            this.password = password;
            role = RoleType.Admin;
        }
    }
}
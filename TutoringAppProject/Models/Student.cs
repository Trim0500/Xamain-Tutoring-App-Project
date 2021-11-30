using TutoringAppProject.Enums;

namespace TutoringAppProject.Models
{
    public class Student : SystemUser
    {
        public Student() {
            
        }
        
        public Student(string firstName, string lastName, string userName, string password, bool isVerified) 
        {
            this.firstName = firstName;
            this.lastName= lastName;
            this.userName = userName;
            this.password = password;
            this.isVerified = isVerified;
            role = RoleType.Student;
        }
    }
}

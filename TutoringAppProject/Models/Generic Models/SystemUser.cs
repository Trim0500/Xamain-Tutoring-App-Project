namespace TutoringAppProject.Models
{
    public class SystemUser : User
    {
        public bool isVerified { get; set; }
        
        public SystemUser()
        {
        }
        
        public SystemUser(bool isVerified)
        {
            this.isVerified = isVerified;
        }


    }
}
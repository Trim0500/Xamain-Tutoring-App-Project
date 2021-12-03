using System;

namespace TutoringAppProject.Models
{
    public class SystemUser : User
    {
        public bool isVerified { get; set; }
        public string[] courses { get; set; }
        public SystemUser()
        {
        }
        
        public SystemUser(bool isVerified)
        {
            this.isVerified = isVerified;
            this.courses = Array.Empty<string>();
        }


    }
}
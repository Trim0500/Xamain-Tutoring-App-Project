﻿using TutoringAppProject.Enums;

namespace TutoringAppProject.Models
{
    public class Teacher : SystemUser
    {
        public Teacher()
        {
            
        }

        public Teacher(string firstName, string lastName, string userName, bool isVerified)
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

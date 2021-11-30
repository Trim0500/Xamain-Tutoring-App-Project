using TutoringAppProject.Enums;

namespace TutoringAppProject.Models
{
    public class User
    {
        public string key { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public RoleType role { get; set; }
    }
}

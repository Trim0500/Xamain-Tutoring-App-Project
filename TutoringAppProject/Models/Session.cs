using System;
using TutoringAppProject.Enums;

namespace TutoringAppProject.Models
{
    public class Session
    {
        public string Key { get; set; }
        public string TutorKey { get; set; }
        public TutoringType SessionType { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
using System;
using TutoringAppProject.Models.Enums;

namespace TutoringAppProject.Models.System
{
    public class Session
    {
        public string Key { get; set; }
        public string TutorKey { get; set; }
        public TutoringType SessionType { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string CourseName { get; set; }
        public string[] AttendingStudents { get; set; }
        
        public int TutorGrade { get; set; }
    }
}
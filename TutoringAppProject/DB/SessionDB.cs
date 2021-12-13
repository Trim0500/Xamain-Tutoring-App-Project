using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Newtonsoft.Json;
using TutoringAppProject.Models;
using TutoringAppProject.Models.System;
using TutoringAppProject.Models.Users;

namespace TutoringAppProject.DB
{
    public class SessionDB
    {
        private readonly FirebaseClient _client = new FirebaseClient(
            "https://xamarin-tutoring-students-default-rtdb.firebaseio.com/"
        );

        public async Task<bool> Create(Session session)
        {
            var data = await _client.Child(nameof(Session)).PostAsync(JsonConvert.SerializeObject(session));

            return !string.IsNullOrEmpty(data.Key);
        }
        
        //select all sessions from a tutor
        public async Task<List<Session>> GetAllSessions(string tutorId)
        {
            var sessions = await _client.Child(nameof(Session)).OnceAsync<Session>();

            var sessionList = sessions.Where(s => s.Object.TutorKey == tutorId).Select(s => s.Object).ToList();

            return sessionList;
        }

        public async Task<List<Session>> ReadAll()
        {
            return (await _client.Child(nameof(Session)).OnceAsync<Session>()).Select(item => new Session
            {
                Key = item.Key,
                TutorKey = item.Object.TutorKey,
                SessionType = item.Object.SessionType,
                Date = item.Object.Date,
                StartTime = item.Object.StartTime,
                EndTime = item.Object.EndTime,
                CourseName = item.Object.CourseName,
                AttendingStudents = item.Object.AttendingStudents,
                TutorGrade = item.Object.TutorGrade
            }).ToList();
        }

        public async Task<List<Session>> ReadAllByCourseName(List<string> courseNames)
        {
            return (await _client.Child(nameof(Session)).OnceAsync<Session>()).Select(item => new Session
            {
                Key = item.Key,
                TutorKey = item.Object.TutorKey,
                SessionType = item.Object.SessionType,
                Date = item.Object.Date,
                StartTime = item.Object.StartTime,
                EndTime = item.Object.EndTime,
                CourseName = item.Object.CourseName,
                AttendingStudents = item.Object.AttendingStudents,
                TutorGrade = item.Object.TutorGrade
            }).Where(s => courseNames.Contains(s.CourseName)).ToList();
        }
        
        //select all sessions where a tutor is part of the class
        public async Task<List<Session>> GetAllSessionsByTutor(string tutorId)
        {
            var sessions = await _client.Child(nameof(Session)).OnceAsync<Session>();

            var sessionList = sessions.Select(item => new Session
            {
                Key = item.Key,
                TutorKey = item.Object.TutorKey,
                SessionType = item.Object.SessionType,
                Date = item.Object.Date,
                StartTime = item.Object.StartTime,
                EndTime = item.Object.EndTime,
                CourseName = item.Object.CourseName,
                AttendingStudents = item.Object.AttendingStudents,
                TutorGrade = item.Object.TutorGrade
            }).Where(s => s.TutorKey == tutorId).ToList();

            return sessionList;
        }

        public async Task<Session> ReadById(string key)
        {
            return (await _client.Child(nameof(Session)).OnceAsync<Session>()).Select(item => new Session()
            {
                Key = item.Key,
                TutorKey = item.Object.TutorKey,
                SessionType = item.Object.SessionType,
                Date = item.Object.Date,
                StartTime = item.Object.StartTime,
                EndTime = item.Object.EndTime,
                CourseName = item.Object.CourseName,
                AttendingStudents = item.Object.AttendingStudents,
                TutorGrade = item.Object.TutorGrade
            }).FirstOrDefault(i => i.Key == key);
        }

        public async Task<bool> Update(Session student)
        {

            await _client.Child(nameof(Session) + "/" + student.Key).PutAsync(JsonConvert.SerializeObject(student));

            return true;

        }

        public async Task<bool> Delete(string key)
        {
            await _client.Child(nameof(Session) + "/" + key).DeleteAsync();
            return true;
        }
    }
}
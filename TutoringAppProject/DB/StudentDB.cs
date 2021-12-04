using Firebase.Database;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutoringAppProject.Models;
using TutoringAppProject.Models.Users;

namespace TutoringAppProject.DB
{
    public class StudentDb
    {
        private readonly FirebaseClient _client = new FirebaseClient(
            "https://xamarin-tutoring-students-default-rtdb.firebaseio.com/"
        );
        
        public async Task<bool> Create(Student student)
        {
            var data = await _client.Child(nameof(Student)).PostAsync(JsonConvert.SerializeObject(student));

            return !string.IsNullOrEmpty(data.Key);
        }

        public async Task<List<Student>> ReadAll()
        {
            return (await _client.Child(nameof(Student)).OnceAsync<Student>()).Select(item => new Student
            {
                Key = item.Key,
                FirstName = item.Object.FirstName,
                LastName = item.Object.LastName,
                Username = item.Object.Username,
                Password = item.Object.Password,
                Role = item.Object.Role,
                Courses = item.Object.Courses,
                IsVerified = item.Object.IsVerified
            }).ToList();
        }

        public async Task<List<Student>> ReadAllByCourse(string courseName)
        {
            return (await _client.Child(nameof(Student)).OnceAsync<Student>()).Select(item => new Student
            {
                Key = item.Key,
                FirstName = item.Object.FirstName,
                LastName = item.Object.LastName,
                Username = item.Object.Username,
                Password = item.Object.Password,
                Role = item.Object.Role,
                Courses = item.Object.Courses,
                IsVerified = item.Object.IsVerified
            }).Where(s => s.Courses.Contains<string>(courseName)).ToList();
        }

        public async Task<Student> ReadById(string key)
        {
            return (await _client.Child(nameof(Student)).OnceAsync<Student>()).Select(item => new Student()
            {
                Key = item.Key,
                FirstName = item.Object.FirstName,
                LastName = item.Object.LastName,
                Username = item.Object.Username,
                Password = item.Object.Password,
                Role = item.Object.Role,
                Courses = item.Object.Courses,
                IsVerified = item.Object.IsVerified

            }).FirstOrDefault(i => i.Key == key);
        }

        public async Task<bool> Update(Student student)
        {

            await _client.Child(nameof(Student) + "/" + student.Key).PutAsync(JsonConvert.SerializeObject(student));

            return true;

        }

        public async Task<bool> Delete(string key)
        {
            await _client.Child(nameof(Student) + "/" + key).DeleteAsync();
            return true;
        }
    }
}

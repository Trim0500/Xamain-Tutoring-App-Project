using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Newtonsoft.Json;
using TutoringAppProject.Models;
using TutoringAppProject.Models.Users;

namespace TutoringAppProject.DB
{
    public class TeacherDb
    {
        private readonly FirebaseClient _client = new FirebaseClient(
            "https://xamarin-tutoring-teachers-default-rtdb.firebaseio.com/"
        );

        public async Task<bool> Create(Teacher teacher)
        {
            var data = await _client.Child(nameof(Teacher)).PostAsync(JsonConvert.SerializeObject(teacher));

            return !string.IsNullOrEmpty(data.Key);
        }

        public async Task<List<Teacher>> ReadAll()
        {
            return (await _client.Child(nameof(Teacher)).OnceAsync<Teacher>()).Select(item => new Teacher
            {
                Key = item.Key,
                FirstName = item.Object.FirstName,
                LastName = item.Object.LastName,
                Username = item.Object.Username,
                Password = item.Object.Password,
                Role = item.Object.Role,
                IsVerified = item.Object.IsVerified,
                Courses = item.Object.Courses,
            }).ToList();
        }

        public async Task<Teacher> ReadById(string key)
        {
            return (await _client.Child(nameof(Teacher)).OnceAsync<Teacher>()).Select(item => new Teacher()
            {
                Key = item.Key,
                FirstName = item.Object.FirstName,
                LastName = item.Object.LastName,
                Username = item.Object.Username,
                Password = item.Object.Password,
                Role = item.Object.Role,
                IsVerified = item.Object.IsVerified,
                Courses = item.Object.Courses
            }).FirstOrDefault(i => i.Key == key);
        }

        public async Task<bool> Update(Teacher teacher)
        {

            await _client.Child(nameof(Teacher) + "/" + teacher.Key).PutAsync(JsonConvert.SerializeObject(teacher));

            return true;

        }

        public async Task<bool> Delete(string key)
        {
            await _client.Child(nameof(Teacher) + "/" + key).DeleteAsync();
            return true;
        }
    }
}

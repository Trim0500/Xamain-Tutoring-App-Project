using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Newtonsoft.Json;
using TutoringAppProject.Models;

namespace TutoringAppProject.DB
{
    public class TeacherDb
    {
        readonly FirebaseClient _client = new FirebaseClient(
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
                key = item.Key,
                firstName = item.Object.firstName,
                lastName = item.Object.lastName,
                userName = item.Object.userName,
                password = item.Object.password,
                role = item.Object.role,
                isVerified = item.Object.isVerified
            }).ToList();
        }

        public async Task<Teacher> ReadById(string key)
        {
            return (await _client.Child(nameof(Teacher)).OnceAsync<Teacher>()).Select(item => new Teacher()
            {
                key = item.Key,
                firstName = item.Object.firstName,
                lastName = item.Object.lastName,
                userName = item.Object.userName,
                password = item.Object.password,
                role = item.Object.role,
                isVerified = item.Object.isVerified
                

            }).FirstOrDefault(i => i.key == key);
        }

        public async Task<bool> Update(Teacher teacher)
        {

            await _client.Child(nameof(Teacher) + "/" + teacher.key).PutAsync(JsonConvert.SerializeObject(teacher));

            return true;

        }

        public async Task<bool> Delete(string key)
        {
            await _client.Child(nameof(Teacher) + "/" + key).DeleteAsync();
            return true;
        }
    }
}

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
    public class AdminDb
    {
        private readonly FirebaseClient _client = new FirebaseClient(
            "https://xamarin-tutoring-semester-default-rtdb.firebaseio.com/"
        );

        public async Task<bool> Create(Admin admin)
        {
            var data = await _client.Child(nameof(Admin)).PostAsync(JsonConvert.SerializeObject(admin));

            return !string.IsNullOrEmpty(data.Key);
        }

        public async Task<List<Admin>> ReadAll()
        {
            return (await _client.Child(nameof(Admin)).OnceAsync<Admin>()).Select(item => new Admin()
            {
                Key = item.Key,
                FirstName = item.Object.FirstName,
                LastName = item.Object.LastName,
                Username = item.Object.Username,
                Password = item.Object.Password,
                Role = item.Object.Role
            }).ToList();
        }

        public async Task<Admin> ReadById(string key)
        {
            return (await _client.Child(nameof(Admin)).OnceAsync<Admin>()).Select(item => new Admin()
            {
                Key = item.Key,
                FirstName = item.Object.FirstName,
                LastName = item.Object.LastName,
                Username = item.Object.Username,
                Password = item.Object.Password,
                Role = item.Object.Role
            }).FirstOrDefault(i => i.Key == key);
        }

        public async Task<bool> Update(Admin course)
        {

            await _client.Child(nameof(Admin) + "/" + course.Key).PutAsync(JsonConvert.SerializeObject(course));
            return true;

        }

        public async Task<bool> Delete(string key)
        {
            await _client.Child(nameof(Admin) + "/" + key).DeleteAsync();
            return true;
        }
    }
}
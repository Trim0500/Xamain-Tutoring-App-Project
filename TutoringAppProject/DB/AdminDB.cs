using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Newtonsoft.Json;
using TutoringAppProject.Models;

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
                key = item.Key,
                firstName = item.Object.firstName,
                lastName = item.Object.lastName,
                userName = item.Object.userName,
                password = item.Object.password,
                role = item.Object.role
            }).ToList();
        }

        public async Task<Admin> ReadById(string key)
        {
            return (await _client.Child(nameof(Admin)).OnceAsync<Admin>()).Select(item => new Admin()
            {
                key = item.Key,
                firstName = item.Object.firstName,
                lastName = item.Object.lastName,
                userName = item.Object.userName,
                password = item.Object.password,
                role = item.Object.role
            }).FirstOrDefault(i => i.key == key);
        }

        public async Task<bool> Update(Admin course)
        {

            await _client.Child(nameof(Admin) + "/" + course.key).PutAsync(JsonConvert.SerializeObject(course));
            return true;

        }

        public async Task<bool> Delete(string key)
        {
            await _client.Child(nameof(Admin) + "/" + key).DeleteAsync();
            return true;
        }
    }
}
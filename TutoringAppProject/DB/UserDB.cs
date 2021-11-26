using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutoringAppProject.Models;

namespace TutoringAppProject.DB
{
    public class UserDB
    {
        FirebaseClient client = new FirebaseClient(
            "https://xamarin-tutoring-user-default-rtdb.firebaseio.com/"
        );

        public async Task<bool> Create(User user)
        {
            var data = await client.Child(nameof(User)).PostAsync(JsonConvert.SerializeObject(user));

            if (!String.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<User>> ReadAll()
        {
            return (await client.Child(nameof(User)).OnceAsync<User>()).Select(item => new User
            {
                key = item.Key,
                firstName = item.Object.firstName,
                lastName = item.Object.lastName,
                userName = item.Object.userName,
                password = item.Object.password,
                role = item.Object.role
            }).ToList();
        }

        public async Task<User> ReadById(string key)
        {
            return await client.Child(nameof(User) + "/" + key).OnceSingleAsync<User>();
        }

        public async Task<bool> Update(User user)
        {

            await client.Child(nameof(User) + "/" + user.key).PutAsync(JsonConvert.SerializeObject(user));

            return true;

        }

        public async Task<bool> Delete(string key)
        {
            await client.Child(nameof(User) + "/" + key).DeleteAsync();
            return true;
        }
    }
}

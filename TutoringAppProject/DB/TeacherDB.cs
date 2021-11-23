using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutoringAppProject.Models;

namespace TeacheringAppProject.DB
{
    public class TeacherDB
    {
        FirebaseClient client = new FirebaseClient(
            "https://xamarin-tutoring-teachers-default-rtdb.firebaseio.com/"
        );

        public async Task<bool> Create(Teacher teacher)
        {
            var data = await client.Child(nameof(Teacher)).PostAsync(JsonConvert.SerializeObject(teacher));

            if (!String.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Teacher>> ReadAll()
        {
            return (await client.Child(nameof(Teacher)).OnceAsync<Teacher>()).Select(item => new Teacher
            {
                key = item.Key,
                firstName = item.Object.firstName,
                lastName = item.Object.lastName,
                studentId = item.Object.studentId,
                classGiven = item.Object.classGiven,
                semester = item.Object.semester
            }).ToList();
        }

        public async Task<Teacher> ReadById(string key)
        {
            return await client.Child(nameof(Teacher) + "/" + key).OnceSingleAsync<Teacher>();
        }

        public async Task<bool> Update(Teacher teacher)
        {

            await client.Child(nameof(Teacher) + "/" + teacher.key).PutAsync(JsonConvert.SerializeObject(teacher));

            return true;

        }

        public async Task<bool> Delete(string key)
        {
            await client.Child(nameof(Teacher) + "/" + key).DeleteAsync();
            return true;
        }
    }
}

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
    public class StudentDB
    {
        FirebaseClient client = new FirebaseClient(
            "https://xamarin-tutoring-students-default-rtdb.firebaseio.com/"
        );

        public async Task<bool> Create(Student student)
        {
            var data = await client.Child(nameof(Student)).PostAsync(JsonConvert.SerializeObject(student));

            if (!String.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Student>> ReadAll()
        {
            return (await client.Child(nameof(Student)).OnceAsync<Student>()).Select(item => new Student
            {
                key = item.Key,
                firstName = item.Object.firstName,
                lastName = item.Object.lastName
            }).ToList();
        }

        public async Task<Student> ReadById(string key)
        {
            return await client.Child(nameof(Student) + "/" + key).OnceSingleAsync<Student>();
        }

        public async Task<bool> Update(Student student)
        {

            await client.Child(nameof(Student) + "/" + student.key).PutAsync(JsonConvert.SerializeObject(student));

            return true;

        }

        public async Task<bool> Delete(string key)
        {
            await client.Child(nameof(Student) + "/" + key).DeleteAsync();
            return true;
        }
    }
}

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
    public class TutorDB
    {
        FirebaseClient client = new FirebaseClient(
            "https://xamarin-tutoring-tutors-default-rtdb.firebaseio.com/"
        );

        public async Task<bool> Create(Tutor tutor)
        {
            var data = await client.Child(nameof(Tutor)).PostAsync(JsonConvert.SerializeObject(tutor));

            if (!String.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Tutor>> ReadAll()
        {
            return (await client.Child(nameof(Tutor)).OnceAsync<Tutor>()).Select(item => new Tutor
            {
                key = item.Key,
                firstName = item.Object.firstName,
                lastName = item.Object.lastName,
                studentId = item.Object.studentId,
                gradeGiven = item.Object.gradeGiven,
                hours = item.Object.hours
            }).ToList();
        }

        public async Task<Tutor> ReadById(string key)
        {
            return await client.Child(nameof(Tutor) + "/" + key).OnceSingleAsync<Tutor>();
        }

        public async Task<bool> Update(Tutor tutor)
        {

            await client.Child(nameof(Tutor) + "/" + tutor.key).PutAsync(JsonConvert.SerializeObject(tutor));

            return true;

        }

        public async Task<bool> Delete(string key)
        {
            await client.Child(nameof(Tutor) + "/" + key).DeleteAsync();
            return true;
        }
    }
}

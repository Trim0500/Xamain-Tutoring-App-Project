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
    public class SemesterDB
    {
        FirebaseClient client = new FirebaseClient(
            "https://xamarin-tutoring-semester-default-rtdb.firebaseio.com/"
        );

        public async Task<bool> Create(Semester semester)
        {
            var data = await client.Child(nameof(Semester)).PostAsync(JsonConvert.SerializeObject(semester));

            if (!String.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Semester>> ReadAll()
        {
            return (await client.Child(nameof(Semester)).OnceAsync<Semester>()).Select(item => new Semester
            {
                key = item.Key,
                semesterCode = item.Object.semesterCode,
                semesterSeason = item.Object.semesterSeason,
                semesterYear = item.Object.semesterYear
            }).ToList();
        }

        public async Task<Semester> ReadById(string key)
        {
            return await client.Child(nameof(Semester) + "/" + key).OnceSingleAsync<Semester>();
        }

        public async Task<bool> Update(Semester semester)
        {

            await client.Child(nameof(Semester) + "/" + semester.key).PutAsync(JsonConvert.SerializeObject(semester));

            return true;

        }

        public async Task<bool> Delete(string key)
        {
            await client.Child(nameof(Semester) + "/" + key).DeleteAsync();
            return true;
        }
    }
}

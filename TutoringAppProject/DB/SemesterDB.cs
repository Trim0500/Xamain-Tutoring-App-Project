using Firebase.Database;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutoringAppProject.Models;

namespace TutoringAppProject.DB
{
    public class SemesterDb
    {
        private readonly FirebaseClient _client = new FirebaseClient(
            "https://xamarin-tutoring-semester-default-rtdb.firebaseio.com/"
        );

        public async Task<bool> Create(Semester semester)
        {
            var data = await _client.Child(nameof(Semester)).PostAsync(JsonConvert.SerializeObject(semester));

            return !string.IsNullOrEmpty(data.Key);
        }

        public async Task<List<Semester>> ReadAll()
        {
            return (await _client.Child(nameof(Semester)).OnceAsync<Semester>()).Select(item => new Semester
            {
                key = item.Key,
                semesterCode = item.Object.semesterCode,
                semesterSeason = item.Object.semesterSeason,
                semesterYear = item.Object.semesterYear
            }).ToList();
        }

        public async Task<Semester> ReadById(string key)
        {
            return (await _client.Child(nameof(Semester)).OnceAsync<Semester>()).Select(item => new Semester()
            {
                key = item.Key,
                semesterCode = item.Object.semesterCode,
                semesterSeason = item.Object.semesterSeason,
                semesterYear = item.Object.semesterYear
            }).FirstOrDefault(i => i.key == key);
        }

        public async Task<bool> Update(Semester semester)
        {

            await _client.Child(nameof(Semester) + "/" + semester.key).PutAsync(JsonConvert.SerializeObject(semester));

            return true;

        }

        public async Task<bool> Delete(string key)
        {
            await _client.Child(nameof(Semester) + "/" + key).DeleteAsync();
            return true;
        }
    }
}

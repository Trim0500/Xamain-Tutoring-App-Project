using Firebase.Database;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutoringAppProject.Models;
using TutoringAppProject.Models.System;

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
                Key = item.Key,
                SemesterCode = item.Object.SemesterCode,
                SemesterSeason = item.Object.SemesterSeason,
                SemesterYear = item.Object.SemesterYear
            }).ToList();
        }

        public async Task<Semester> ReadById(string key)
        {
            return (await _client.Child(nameof(Semester)).OnceAsync<Semester>()).Select(item => new Semester()
            {
                Key = item.Key,
                SemesterCode = item.Object.SemesterCode,
                SemesterSeason = item.Object.SemesterSeason,
                SemesterYear = item.Object.SemesterYear
            }).FirstOrDefault(i => i.Key == key);
        }

        public async Task<bool> Update(Semester semester)
        {

            await _client.Child(nameof(Semester) + "/" + semester.Key).PutAsync(JsonConvert.SerializeObject(semester));

            return true;

        }

        public async Task<bool> Delete(string key)
        {
            await _client.Child(nameof(Semester) + "/" + key).DeleteAsync();
            return true;
        }
    }
}

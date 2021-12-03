﻿using System.Collections;
using Firebase.Database;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database.Query;
using TutoringAppProject.Models;

namespace TutoringAppProject.DB
{
    public class TutorDb
    {
        private readonly FirebaseClient _client = new FirebaseClient(
            "https://xamarin-tutoring-tutors-default-rtdb.firebaseio.com/"
        );

        public async Task<bool> Create(Tutor tutor)
        {
            var data = await _client.Child(nameof(Tutor)).PostAsync(JsonConvert.SerializeObject(tutor));

            return !string.IsNullOrEmpty(data.Key);
        }
        
        // get tutor courses by tutor id
        public async Task<List<string>> GetTutorCourses(string tutorId)
        {
            var data = await _client.Child(nameof(Tutor)).Child(tutorId).Child(nameof(Tutor.courses)).OnceAsync<string>();

            return data.Select(x => x.Object).ToList();
        }

        public async Task<List<Tutor>> ReadAll()
        {
            return (await _client.Child(nameof(Tutor)).OnceAsync<Tutor>()).Select(item => new Tutor
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

        public async Task<Tutor> ReadById(string key)
        {
            return (await _client.Child(nameof(Tutor)).OnceAsync<Tutor>()).Select(item => new Tutor()
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

        public async Task<bool> Update(Tutor tutor)
        {

            await _client.Child(nameof(Tutor) + "/" + tutor.key).PutAsync(JsonConvert.SerializeObject(tutor));

            return true;

        }

        public async Task<bool> Delete(string key)
        {
            await _client.Child(nameof(Tutor) + "/" + key).DeleteAsync();
            return true;
        }
    }
}

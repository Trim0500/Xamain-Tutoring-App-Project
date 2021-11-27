using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Newtonsoft.Json;
using TutoringAppProject.Models;

namespace TutoringAppProject.DB
{
    public class CourseDB
    {
        FirebaseClient client = new FirebaseClient(
            "https://xamarin-tutoring-semester-default-rtdb.firebaseio.com/"
        );

        public async Task<bool> Create(Course course)
        {
            var data = await client.Child(nameof(Course)).PostAsync(JsonConvert.SerializeObject(course));

            if (!String.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Course>> ReadAll()
        {
            return (await client.Child(nameof(Course)).OnceAsync<Course>()).Select(item => new Course()
            {
                key = item.Key,
                courseCode = item.Object.courseCode,
                courseName = item.Object.courseName
            }).ToList();
        }

        public async Task<Course> ReadById(string key)
        {
            return (await client.Child(nameof(Course)).OnceAsync<Course>()).Select(item => new Course()
            {
                key = item.Key,
                courseCode = item.Object.courseCode,
                courseName = item.Object.courseName
            }).FirstOrDefault(i => i.key == key);
        }

        public async Task<bool> Update(Course course)
        {

            await client.Child(nameof(Course) + "/" + course.key).PutAsync(JsonConvert.SerializeObject(course));
            return true;

        }

        public async Task<bool> Delete(string key)
        {
            await client.Child(nameof(Course) + "/" + key).DeleteAsync();
            return true;
        }
    }
}
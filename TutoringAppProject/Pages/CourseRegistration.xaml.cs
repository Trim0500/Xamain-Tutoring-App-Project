using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutoringAppProject.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseRegistration : ContentPage
    {
        Course _course;
        public CourseRegistration()
        {
            InitializeComponent();
            CourseUpdateButton.IsEnabled = false;
        }

        public CourseRegistration(Course course)
        {
            InitializeComponent();
            _course = course;
            CourseAddButton.IsEnabled = false;
            CourseCode.Text = course.courseCode;
            CourseName.Text = course.courseName;
        }

        private async void AddCourse(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CourseCode.Text))
            {
                await DisplayAlert("Error", "Please enter a course code", "OK");
                return;
            }

            if (string.IsNullOrEmpty(CourseName.Text))
            {
                await DisplayAlert("Error", "Please enter a valid course name", "OK");
                return;
            }

            Course course = new Course()
            {
                courseCode = CourseCode.Text,
                courseName = CourseName.Text,

            };

            if (await App._courseDB.Create(course))
            {
                await DisplayAlert("Success", "Course added", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Course not added", "OK");
            }
        }

        private async void UpdateCourse(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CourseCode.Text))
            {
                await DisplayAlert("Error", "Please enter a course code", "OK");
                return;
            }

            if (string.IsNullOrEmpty(CourseName.Text))
            {
                await DisplayAlert("Error", "Please enter a valid course name", "OK");
                return;
            }

            Course course = new Course()
            {
                key = _course.key,
                courseCode = CourseCode.Text,
                courseName = CourseName.Text,
            };

            if (await App._courseDB.Update(course))
            {
                await DisplayAlert("Success", "Course updated", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Course not updated", "OK");
            }
        }
    }
}
using System;
using TutoringAppProject.Models;
using TutoringAppProject.Models.System;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages.CourseCRUD
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseRegistration
    {
        private readonly bool _isUpdate;
        public CourseRegistration()
        {
            InitializeComponent();
            _isUpdate = false;
        }

        public CourseRegistration(Course course)
        {
            InitializeComponent();
            _isUpdate = true;
            SemesterCode.Text = course.SemesterCode;
            CourseCode.Text = course.CourseCode;
            CourseName.Text = course.CourseName;
        }

        private async void AddOrUpdateCourse(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SemesterCode.Text))
            {
                await DisplayAlert("Error", "Please enter a semester code", "OK");
                return;
            }
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

            var course = new Course()
            {
                SemesterCode = SemesterCode.Text,
                CourseCode = CourseCode.Text,
                CourseName = CourseName.Text,

            };

            if (_isUpdate)
            {
                if (await App.CourseDb.Update(course))
                {
                    await DisplayAlert("Success", "Course updated", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Course not updated", "OK");
                }
            }
            else
            {
                if (await App.CourseDb.Create(course))
                {
                    await DisplayAlert("Success", "Course added", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Course not added", "OK");
                } 
            }
        }
    }
}
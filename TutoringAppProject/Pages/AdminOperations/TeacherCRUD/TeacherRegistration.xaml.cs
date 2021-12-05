using System;
using System.Collections.Generic;
using System.Linq;
using TutoringAppProject.Models.Enums;
using TutoringAppProject.Models.System;
using TutoringAppProject.Models.Users;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages.TeacherCRUD
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeacherRegistration
    {
        private List<Semester> _semesters = new List<Semester>();
        private List<Course> _courses = new List<Course>();
        private readonly List<string> _teacherCourses = new List<string>();
        private readonly bool _isUpdate = false;
        private readonly string teacherKey;
        public TeacherRegistration()
        {
            InitializeComponent();
            GetCourses();
            _isUpdate = false;
            TeacherAddOrUpdateButton.Text = "Add";
        }
        
        public TeacherRegistration(Teacher teacher)
        {
            InitializeComponent();
            GetCourses();
            _isUpdate = true;
            teacherKey = teacher.Key;
            TeacherAddOrUpdateButton.Text = "Update";
            TeacherFirstName.Text = teacher.FirstName;
            TeacherLastName.Text =  teacher.LastName;
            TeacherUsername.Text = teacher.Username;
            TeacherPassword.Text = teacher.Password;
            
        }

        private async void GetCourses()
        {
            _semesters = await App.SemesterDb.ReadAll();
            var semesterCodes = _semesters.Select(temp => temp.SemesterCode).ToList();

            SemesterChoice.ItemsSource = semesterCodes;
            SemesterChoice.SelectedIndex = 0;

            _courses = await App.CourseDb.ReadAll();
            var semesterCourses = _courses.Where(temp => temp.SemesterCode.Equals(semesterCodes[0])).ToList();

            CoursesBoxes.ItemsSource = semesterCourses;
        }

        private void SemesterChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedSemester = SemesterChoice.SelectedItem.ToString();

            var semesterCourses = _courses.Where(temp => temp.SemesterCode.Equals(selectedSemester)).ToList();

            CoursesBoxes.ItemsSource = semesterCourses;
        }

        private async void AddTeacher(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TeacherFirstName.Text))
            {
                await DisplayAlert("Error", "Please enter a first name", "OK");
                return;
            }

            if (string.IsNullOrEmpty(TeacherLastName.Text))
            {
                await DisplayAlert("Error", "Please enter a last name", "OK");
                return;
            }

            if (string.IsNullOrEmpty(TeacherUsername.Text))
            {
                await DisplayAlert("Error", "Please enter a username", "OK");
                return;
            }

            if (string.IsNullOrEmpty(TeacherPassword.Text))
            {
                await DisplayAlert("Error", "Please enter a password", "OK");
                return;
            }

            var teacher = new Teacher()
            {
                FirstName = TeacherFirstName.Text,
                LastName = TeacherLastName.Text,
                Username = TeacherUsername.Text,
                Password = TeacherPassword.Text,
                Role = RoleType.Teacher,
                Courses = _teacherCourses.ToArray(),
                IsVerified = true
            };

            if (_isUpdate)
            {
                teacher.Key = teacherKey;
                if (await App.TeacherDb.Update(teacher))
                {
                    await DisplayAlert("Success", "Teacher updated successfully", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Teacher update failed", "OK");
                }
                    
               
            }
            else
            {
                if (await App.TeacherDb.Create(teacher))
                {
                    await DisplayAlert("Success", "Teacher added", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Teacher not added", "OK");
                }
            }
            
        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var code = ((TappedEventArgs)e).Parameter.ToString();

            var isInList = _teacherCourses.Contains(code);

            if (!isInList)
            {
                _teacherCourses.Add(code);
                ((Label)sender).TextColor = Color.Green;
                await DisplayAlert("Adding Tutor", "Adding the tutor to the course", "OK");
            }
            else
            {
                _teacherCourses.Remove(code);
                ((Label)sender).TextColor = Color.Black;
                await DisplayAlert("Removing Tutor", "Removing the tutor from the course", "OK");
            }
        }
    }
}
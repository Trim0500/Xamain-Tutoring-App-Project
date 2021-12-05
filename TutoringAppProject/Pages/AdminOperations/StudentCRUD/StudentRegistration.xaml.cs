using System;
using System.Collections.Generic;
using System.Linq;
using TutoringAppProject.Models;
using TutoringAppProject.Models.Enums;
using TutoringAppProject.Models.System;
using TutoringAppProject.Models.Users;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages.StudentCRUD
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentRegistration : ContentPage
    {
        private List<Semester> _semesters = new List<Semester>();
        private List<Course> _courses = new List<Course>();
        private readonly List<string> _studentCourses = new List<string>();
        private readonly bool _isUpdate;
        private readonly string studentKey;
        public StudentRegistration()
        {
            InitializeComponent();
            GetCourses();
            StudentAddOrUpdateButton.Text = "Add";
            _isUpdate = false;
        }

        public StudentRegistration(Student student)
        {
            InitializeComponent();
            GetCourses();
            StudentAddOrUpdateButton.Text = "Update";
            studentKey = student.Key;
            _isUpdate = true;
            StudentFirstName.Text = student.FirstName;
            StudentLastName.Text = student.LastName;
            StudentUsername.Text = student.Username;
            StudentPassword.Text = student.Password;
        }

        public async void GetCourses()
        {
            _semesters = await App.SemesterDb.ReadAll();
            var semesterCodes = _semesters.Select(temp => temp.SemesterCode).ToList();
            SemesterChoice.ItemsSource = semesterCodes;
            SemesterChoice.SelectedIndex = 0;
            _courses = await App.CourseDb.ReadAll();
            var semesterCourses = _courses.Where(temp => temp.SemesterCode.Equals(semesterCodes[0])).ToList();
            CoursesBoxes.ItemsSource = semesterCourses;
        }

        private async void AddStudentOrUpdate(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(StudentFirstName.Text))
            {
                await DisplayAlert("Error", "Please enter a first name", "OK");
                return;
            }

            if (string.IsNullOrEmpty(StudentLastName.Text))
            {
                await DisplayAlert("Error", "Please enter a last name", "OK");
                return;
            }

            if (string.IsNullOrEmpty(StudentUsername.Text))
            {
                await DisplayAlert("Error", "Please enter a username", "OK");
                return;
            }

            if (string.IsNullOrEmpty(StudentPassword.Text))
            {
                await DisplayAlert("Error", "Please enter a password", "OK");
                return;
            }

            var student = new Student()
            {
                FirstName = StudentFirstName.Text,
                LastName = StudentLastName.Text,
                Username = StudentUsername.Text,
                Password = StudentPassword.Text,
                Role = RoleType.Student,
                Courses = _studentCourses.ToArray(),
                IsVerified = true
            };

            if (_isUpdate)
            {
                student.Key = studentKey;
                if (await App.StudentDb.Update(student))
                {
                    await DisplayAlert("Success", "Student updated", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Student not updated", "OK");
                }
            }
            else
            {
                if (await App.StudentDb.Create(student))
                {
                    await DisplayAlert("Success", "Student added", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Student not added", "OK");
                }
            }
        }

        private void SemesterChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedSemester = SemesterChoice.SelectedItem.ToString();

            var semesterCourses = _courses.Where(temp => temp.SemesterCode.Equals(selectedSemester)).ToList();

            CoursesBoxes.ItemsSource = semesterCourses;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var code = ((TappedEventArgs)e).Parameter.ToString();

            var isInList = _studentCourses.Contains(code);

            if (!isInList)
            {
                _studentCourses.Add(code);
                ((Label)sender).TextColor = Color.Green;
                await DisplayAlert("Adding Tutor", "Adding the tutor to the course", "OK");
            }
            else
            {
                _studentCourses.Remove(code);
                ((Label)sender).TextColor = Color.Black;
                await DisplayAlert("Removing Tutor", "Removing the tutor from the course", "OK");
            }
        }
    }
}

            
            

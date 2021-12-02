using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutoringAppProject.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages.TeacherCRUD
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeacherRegistration : ContentPage
    {
        public Teacher _teacher { get; set; }
        private List<Semester> semesters = new List<Semester>();
        private List<Course> courses = new List<Course>();
        private List<string> teacherCourses = new List<string>();
        public TeacherRegistration()
        {
            InitializeComponent();
            GetCourses();
        }
        
        public TeacherRegistration(Teacher teacher)
        {
            InitializeComponent();
            GetCourses();
            _teacher = teacher;
            TeacherAddButton.IsEnabled = false;
            TeacherFirstName.Text = teacher.firstName;
            TeacherLastName.Text =  teacher.lastName;
            TeacherUsername.Text = teacher.userName;
            TeacherPassword.Text = teacher.password;
            
        }

        public async void GetCourses()
        {
            semesters = await App._semesterDB.ReadAll();
            List<string> semesterCodes = new List<string>();
            foreach (Semester temp in semesters)
            {
                semesterCodes.Add(temp.semesterCode);
            }

            SemesterChoice.ItemsSource = semesterCodes;
            SemesterChoice.SelectedIndex = 0;

            courses = await App._courseDB.ReadAll();
            List<Course> semesterCourses = new List<Course>();
            foreach (Course temp in courses)
            {
                if (temp.semesterCode.Equals(semesterCodes[0]))
                {
                    semesterCourses.Add(temp);
                }
            }

            CoursesBoxes.ItemsSource = semesterCourses;
        }

        private void SemesterChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedSemester = SemesterChoice.SelectedItem.ToString();

            List<Course> semesterCourses = new List<Course>();
            foreach (Course temp in courses)
            {
                if (temp.semesterCode.Equals(selectedSemester))
                {
                    semesterCourses.Add(temp);
                }
            }

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
                firstName = TeacherFirstName.Text,
                lastName = TeacherLastName.Text,
                userName = TeacherUsername.Text,
                password = TeacherPassword.Text,
                courses = teacherCourses.ToArray(),
                isVerified = true
            };

            if (await App._teacherDB.Create(teacher))
            {
                await DisplayAlert("Success", "Student added", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Student not added", "OK");
            }
        }
        
        private async void UpdateTeacher(object sender, EventArgs e)
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

            Teacher teacher = new Teacher()
            {
                key = _teacher.key,
                firstName = TeacherFirstName.Text,
                lastName = TeacherLastName.Text,
                userName = TeacherUsername.Text,
                password = TeacherPassword.Text,
                courses = teacherCourses.ToArray()
            };

            if (await App._teacherDB.Update(teacher))
            {
                await DisplayAlert("Success", "updated Teacher", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Teacher not updated", "OK");
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var code = ((TappedEventArgs)e).Parameter.ToString();

            bool isInList = teacherCourses.Contains(code);

            if (!isInList)
            {
                teacherCourses.Add(code);
                ((Label)sender).TextColor = Color.Green;
                await DisplayAlert("Adding Tutor", "Adding the tutor to the course", "OK");
            }
            else
            {
                teacherCourses.Remove(code);
                ((Label)sender).TextColor = Color.Black;
                await DisplayAlert("Removing Tutor", "Removing the tutor from the course", "OK");
            }
        }
    }
}
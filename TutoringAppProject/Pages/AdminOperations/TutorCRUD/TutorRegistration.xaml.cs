using System;
using System.Collections.Generic;
using System.Linq;
using TutoringAppProject.Models;
using TutoringAppProject.Models.System;
using TutoringAppProject.Models.Users;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages.TutorCRUD
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TutorRegistration : ContentPage
    {
        private Tutor Tutor { get; set; }
        private List<Semester> _semesters = new List<Semester>();
        private List<Course> _courses = new List<Course>();
        private readonly List<string> _tutorCourses = new List<string>();
        private readonly bool _isUpdate;
        public TutorRegistration()
        {
            InitializeComponent();
            GetCourses();
            _isUpdate = false;
            TutorFirstName.Text = "";
        }
        
        public TutorRegistration(Tutor tutor)
        {
            InitializeComponent();
            GetCourses();
            Tutor = tutor;
            _isUpdate = true;
            TutorAddUpdateButton.Text = "Update";
            TutorFirstName.Text = tutor.FirstName;
            TutorLastName.Text =  tutor.LastName;
            TutorUsername.Text = tutor.Username;
            TutorPassword.Text = tutor.Password;

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

        private void SemesterChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedSemester = SemesterChoice.SelectedItem.ToString();

            var semesterCourses = _courses.Where(temp => temp.SemesterCode.Equals(selectedSemester)).ToList();

            CoursesBoxes.ItemsSource = semesterCourses;
        }

        private async void AddTutor(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TutorFirstName.Text))
            {
                await DisplayAlert("Error", "Please enter a first name", "OK");
                return;
            }

            if (string.IsNullOrEmpty(TutorLastName.Text))
            {
                await DisplayAlert("Error", "Please enter a last name", "OK");
                return;
            }

            if (string.IsNullOrEmpty(TutorUsername.Text))
            {
                await DisplayAlert("Error", "Please enter a username", "OK");
                return;
            }

            if (string.IsNullOrEmpty(TutorPassword.Text))
            {
                await DisplayAlert("Error", "Please enter a password", "OK");
                return;
            }

            var tutor = new Tutor()
            {
                FirstName = TutorFirstName.Text,
                LastName = TutorLastName.Text,
                Username = TutorUsername.Text,
                Password = TutorPassword.Text,
                Courses = _tutorCourses.ToArray(),
                IsVerified = true
            };

            if (_isUpdate)
            {
                if (await App.TutorDb.Update(tutor))
                {
                    await DisplayAlert("Success", "updated Tutor", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Teacher not Tutor", "OK");
                }
            }
            else
            {
                if (await App.TutorDb.Create(tutor))
                {
                    await DisplayAlert("Success", "Tutor added", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Tutor not added", "OK");
                }
            }
        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var code = ((TappedEventArgs)e).Parameter.ToString();

            var isInList = _tutorCourses.Contains(code);

            if(!isInList)
            {
                _tutorCourses.Add(code);
                ((Label)sender).TextColor = Color.Green;
                await DisplayAlert("Adding Tutor", "Adding the tutor to the course", "OK");
            }
            else
            {
                _tutorCourses.Remove(code);
                ((Label)sender).TextColor = Color.Black;
                await DisplayAlert("Removing Tutor", "Removing the tutor from the course", "OK");
            }
        }
    }
}
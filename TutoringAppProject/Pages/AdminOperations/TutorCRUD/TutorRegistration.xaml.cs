using System;
using System.Collections.Generic;
using TutoringAppProject.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages.TutorCRUD
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TutorRegistration : ContentPage
    {
        private Tutor _tutor { get; set; }
        private List<Semester> semesters = new List<Semester>();
        private List<Course> courses = new List<Course>();
        private List<string> tutorCourses = new List<string>();
        public TutorRegistration()
        {
            InitializeComponent();
            GetCourses();
            TutorUpdateButton.IsVisible = false;
            TutorUpdateButton.IsEnabled = false;
        }
        
        public TutorRegistration(Tutor tutor)
        {
            InitializeComponent();
            _tutor = tutor;
            GetCourses();
            TutorAddButton.IsVisible = false;
            TutorAddButton.IsEnabled = false;
            TutorFirstName.Text = tutor.firstName;
            TutorLastName.Text =  tutor.lastName;
            TutorUsername.Text = tutor.userName;
            TutorPassword.Text = tutor.password;

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
                if(temp.semesterCode.Equals(semesterCodes[0]))
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
                firstName = TutorFirstName.Text,
                lastName = TutorLastName.Text,
                userName = TutorUsername.Text,
                password = TutorPassword.Text,
                courses = tutorCourses.ToArray(),
                isVerified = true
            };

            if (await App._tutorDB.Create(tutor))
            {
                await DisplayAlert("Success", "Tutor added", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Tutor not added", "OK");
            }
        }
        
        private async void UpdateTutor(object sender, EventArgs e)
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
                key = _tutor.key,
                firstName = TutorFirstName.Text,
                lastName = TutorLastName.Text,
                userName = TutorUsername.Text,
                password = TutorPassword.Text,
                courses = tutorCourses.ToArray()
            };

            if (await App._tutorDB.Update(tutor))
            {
                await DisplayAlert("Success", "updated Tutor", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Teacher not Tutor", "OK");
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var code = ((TappedEventArgs)e).Parameter.ToString();

            bool isInList = tutorCourses.Contains(code);

            if(!isInList)
            {
                tutorCourses.Add(code);
                ((Label)sender).TextColor = Color.Green;
                await DisplayAlert("Adding Tutor", "Adding the tutor to the course", "OK");
            }
            else
            {
                tutorCourses.Remove(code);
                ((Label)sender).TextColor = Color.Black;
                await DisplayAlert("Removing Tutor", "Removing the tutor from the course", "OK");
            }
        }
    }
}
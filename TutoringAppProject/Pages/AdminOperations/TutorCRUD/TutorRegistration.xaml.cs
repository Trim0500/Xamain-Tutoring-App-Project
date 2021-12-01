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
        public TutorRegistration()
        {
            InitializeComponent();
            TutorUpdateButton.IsVisible = false;
            TutorUpdateButton.IsEnabled = false;
            GetCourses();
        }
        
        public TutorRegistration(Tutor tutor)
        {
            InitializeComponent();
            _tutor = tutor;
            TutorAddButton.IsVisible = false;
            TutorAddButton.IsEnabled = false;
            TutorFirstName.Text = tutor.firstName;
            TutorLastName.Text =  tutor.lastName;
            TutorUsername.Text = tutor.userName;
            TutorPassword.Text = tutor.password;
            GetCourses();
        }

        public async void GetCourses()
        {
            semesters = await App._semesterDB.ReadAll();
            courses = await App._courseDB.ReadAll();
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
                password = TutorPassword.Text
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
    }
}
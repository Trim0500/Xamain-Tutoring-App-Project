using System;
using TutoringAppProject.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages.Authentication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserRegistration : ContentPage
    {
        public UserRegistration()
        {
            InitializeComponent();
        }

        private async void AddUser(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FirstName.Text))
            {
                await DisplayAlert("Error", "Please enter a first name", "OK");
                return;
            }

            if (string.IsNullOrEmpty(LastName.Text))
            {
                await DisplayAlert("Error", "Please enter a last name", "OK");
                return;
            }

            if (string.IsNullOrEmpty(Username.Text))
            {
                await DisplayAlert("Error", "Please enter a username", "OK");
                return;
            }

            if (string.IsNullOrEmpty(Password.Text))
            {
                await DisplayAlert("Error", "Please enter a password", "OK");
                return;
            }
            
            if (StudentRadChk.IsChecked)
            {
                var student = new Student
                {
                    firstName= FirstName.Text,
                    lastName = LastName.Text,
                    userName = Username.Text,
                    password = Password.Text,
                    isVerified = false
                };
                
                if (await App._studentDB.Create(student))
                {
                    await DisplayAlert("Success", "Student added. You will need to wait to be verified by a admin or a teacher", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Student not added", "OK");
                }
            }
            else if (TeacherRadChk.IsChecked)
            {
                var teacher = new Teacher
                {
                    firstName = FirstName.Text,
                    lastName = LastName.Text,
                    userName = Username.Text,
                    password = Password.Text,
                    isVerified = false
                };
                
                if (await App._teacherDB.Create(teacher))
                {
                    await DisplayAlert("Success", "Teacher added. You will need to wait to be verified by a admin", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Teacher not added", "OK");
                }
            }
            else if (TutorRadChk.IsChecked)
            {
                var tutor = new Tutor
                {
                    firstName = FirstName.Text,
                    lastName = LastName.Text,
                    userName = Username.Text,
                    password = Password.Text,
                    isVerified = false
                };
                
                if (await App._tutorDB.Create(tutor))
                {
                    await DisplayAlert("Success", "Tutor added. You will need to wait to be verified by a admin", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Tutor not added", "OK");
                }
            }
            else 
            {
                await DisplayAlert("Error", "Please select a user type", "OK");
            }
        }
    }
}
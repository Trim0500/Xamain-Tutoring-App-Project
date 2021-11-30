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
        public TeacherRegistration()
        {
            InitializeComponent();
        }
        
        public TeacherRegistration(Teacher teacher)
        {
            InitializeComponent();
            _teacher = teacher;
            TeacherAddButton.IsEnabled = false;
            TeacherFirstName.Text = teacher.firstName;
            TeacherLastName.Text =  teacher.lastName;
            TeacherUsername.Text = teacher.userName;
            TeacherPassword.Text = teacher.password;
            
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
                password = TeacherPassword.Text
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
    }
}
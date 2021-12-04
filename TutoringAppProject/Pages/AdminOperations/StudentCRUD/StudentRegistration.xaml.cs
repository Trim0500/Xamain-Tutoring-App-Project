using System;
using TutoringAppProject.Models;
using TutoringAppProject.Models.Users;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages.StudentCRUD
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentRegistration : ContentPage
    {
        private readonly bool _isUpdate;
        public StudentRegistration()
        {
            InitializeComponent();
            StudentAddOrUpdateButton.Text = "Add";
            _isUpdate = false;
        }

        public StudentRegistration(Student student)
        {
            InitializeComponent();
            StudentAddOrUpdateButton.Text = "Update";
            _isUpdate = true;
            StudentFirstName.Text = student.FirstName;
            StudentLastName.Text = student.LastName;
            StudentUsername.Text = student.Username;
            StudentPassword.Text = student.Password;
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
                IsVerified = true
            };

            if (_isUpdate)
            {
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
    }
}

            
            

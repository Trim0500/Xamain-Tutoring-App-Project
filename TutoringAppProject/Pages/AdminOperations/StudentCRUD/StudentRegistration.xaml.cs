using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutoringAppProject.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentRegistration : ContentPage
    {
        public Student _student { get; set; }
        public StudentRegistration()
        {
            InitializeComponent();
            StudentUpdateButton.IsEnabled = false;
            StudentUpdateButton.IsVisible = false;
        }
        
        public StudentRegistration(Student student)
        {
            InitializeComponent();
            _student = student;
            StudentAddButton.IsEnabled = false;
            StudentAddButton.IsVisible = false;
            StudentFirstName.Text = _student.firstName;
            StudentLastName.Text = _student.lastName;
            StudentUsername.Text = _student.userName;
            StudentPassword.Text = _student.password;
            
        }
        private async void AddStudent(object sender, EventArgs e)
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
                firstName = StudentFirstName.Text,
                lastName = StudentLastName.Text,
                userName = StudentUsername.Text,
                password = StudentPassword.Text,
                isVerified = true
            };

            if (await App._studentDB.Create(student))
            {
                await DisplayAlert("Success", "Student added", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Student not added", "OK");
            }
        }
        
        private async void UpdateStudent(object sender, EventArgs e)
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

            Student student = new Student()
            {
                key = _student.key,
                firstName = StudentFirstName.Text,
                lastName = StudentLastName.Text,
                userName = StudentUsername.Text,
                password = StudentPassword.Text
            };

            if (await App._studentDB.Update(student))
            {
                await DisplayAlert("Success", "updated Student", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Student not updated", "OK");
            }
        }

    }
}
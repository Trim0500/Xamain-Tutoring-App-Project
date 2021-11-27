using System;
using TutoringAppProject.Models;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages.UserOperations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPage
    {
        private readonly User _user = App._currentUser;

        public UserPage()
        {
            InitializeComponent();

            switch (_user.role)
            {
                case "viewer":
                    TeacherToolbar.IsEnabled = false;
                    TutorToolbar.IsEnabled = false;
                    StudentToolbar.IsEnabled = false;

                    DisplayVerificationAlert();
                    break;
                case "Teacher":
                    TutorToolbar.IsEnabled = false;
                    StudentToolbar.IsEnabled = false;
                    break;
                case "Tutor":
                    TeacherToolbar.IsEnabled = false;
                    StudentToolbar.IsEnabled = false;
                    break;
                case "Student":
                    TeacherToolbar.IsEnabled = false;
                    TutorToolbar.IsEnabled = false;
                    break;
            }
        }

        private async void DisplayVerificationAlert()
        {
            await DisplayAlert("No Access", "No access is permitted to you without verification from administrator or teacher", "OK");
        }

        private async void btnLogout_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void teacher_toolbar_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void tutor_toolbar_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void student_toolbar_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
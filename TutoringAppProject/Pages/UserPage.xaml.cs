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
    public partial class UserPage : ContentPage
    {
        User user = App._currentUser;

        public UserPage()
        {
            InitializeComponent();

            if (user.role == "viewer")
            {
                teacher_toolbar.IsEnabled = false;
                tutor_toolbar.IsEnabled = false;
                student_toolbar.IsEnabled = false;

                DisplayVerificationAlert();
            }
            else if (user.role == "Teacher")
            {
                tutor_toolbar.IsEnabled = false;
                student_toolbar.IsEnabled = false;
            }
            else if (user.role == "Tutor")
            {
                teacher_toolbar.IsEnabled = false;
                student_toolbar.IsEnabled = false;
            }
            else if (user.role == "Student")
            {
                teacher_toolbar.IsEnabled = false;
                tutor_toolbar.IsEnabled = false;
            }
        }

        public async void DisplayVerificationAlert()
        {
            await DisplayAlert("No Access", "No access is permitted to you without verification from administrator or teacher", "OK");
        }

        async void btnLogout_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void teacher_toolbar_Clicked(object sender, EventArgs e)
        {

        }

        private void tutor_toolbar_Clicked(object sender, EventArgs e)
        {

        }

        private void student_toolbar_Clicked(object sender, EventArgs e)
        {

        }
    }
}
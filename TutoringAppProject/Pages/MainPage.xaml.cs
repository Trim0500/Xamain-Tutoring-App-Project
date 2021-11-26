using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutoringAppProject.Models;
using TutoringAppProject.Pages;
using Xamarin.Forms;

namespace TutoringAppProject
{
    public partial class MainPage : ContentPage
    {
        User user = App._currentUser;
        public MainPage()
        {
            InitializeComponent();

            if (user.role != "admin")
            {
                Add_Semester.IsEnabled = false;
            }
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

        private async void logout_toolbar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void user_toolbar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserList());
        }
        private void key_tap_Tapped(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SemesterRegistration());
        }
    }
}

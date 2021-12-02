using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutoringAppProject.Pages.TeacherOperations.StudentPages;
using TutoringAppProject.Pages.TeacherOperations.TutorPages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages.TeacherOperations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeacherHome : ContentPage
    {
        public TeacherHome()
        {
            InitializeComponent();
        }

        private async void TutorList_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TutorHours());
        }

        private async void StudentList_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StudentVisits());
        }

        private async void LogoutToolbar_Clicked(object sender, EventArgs e)
        {
            // display login out confirmation
            var answer = await DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");
            if (answer)
            {
                // logout
                await Navigation.PopAsync();
            }
        }
    }
}
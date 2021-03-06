using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages.TeacherOperations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeacherHome
    {
        public TeacherHome()
        {
            InitializeComponent();
        }

        private async void Button_OnClicked_SessionGrading(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SessionGradingList());
        }

        private async void TutorToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TutorPages.TutorHours());
        }

        private async void StudentToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StudentPages.StudentVisits());
        }
    }
}
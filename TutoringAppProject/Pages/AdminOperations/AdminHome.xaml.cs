using System;
using TutoringAppProject.Pages.CourseCRUD;
using TutoringAppProject.Pages.SemesterCRUD;
using TutoringAppProject.Pages.StudentCRUD;
using TutoringAppProject.Pages.TutorCRUD;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminHome
    {
        public AdminHome()
        {
            InitializeComponent();
        }
        private async void teacher_toolbar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TeacherList());
        }

        private async void tutor_toolbar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TutorList());
        }

        private async void student_toolbar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StudentList());
        }

        private async void logout_toolbar_Clicked(object sender, EventArgs e)
        {
            // display login out confirmation
            var answer = await DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");
            if (answer)
            {
                // logout
                await Navigation.PopAsync();
            }
           
        }
        
        private async void Button_OnClicked_Class_List(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CourseList());
        }

        private async void Button_OnClicked_Semester_List(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SemesterList());
        }
    }
}
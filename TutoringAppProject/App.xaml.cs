using System;
using TutoringAppProject.DB;
using TutoringAppProject.Models;
using TutoringAppProject.Pages;
using TutoringAppProject.Pages.Authentication;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject
{
    public partial class App : Application
    {
        public static UserDb _userDB = new UserDb();
        
        public static SemesterDb _semesterDB = new SemesterDb();

        public static CourseDb _courseDB = new CourseDb();

        public static User _currentUser { get; set; }

        public App()
        {
       
            InitializeComponent();
            MainPage = new NavigationPage(new Login());

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

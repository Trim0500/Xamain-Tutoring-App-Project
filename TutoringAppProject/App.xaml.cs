using System;
using TutoringAppProject.DB;
using TutoringAppProject.Models;
using TutoringAppProject.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject
{
    public partial class App : Application
    {
        public static UserDB _userDB = new UserDB();
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

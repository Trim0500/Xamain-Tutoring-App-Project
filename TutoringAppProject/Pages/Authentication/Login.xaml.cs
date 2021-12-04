using System;
using System.Linq;
using TutoringAppProject.Models;
using TutoringAppProject.Models.Enums;
using TutoringAppProject.Models.Users;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages.Authentication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login
    {
        public Login()
        {
            InitializeComponent();
            CreateAdmin();
        }

        private async void register_btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserRegistration());
        }

        private async void login_btn_Clicked(object sender, EventArgs e)
        {
            var userName = UserEdit.Text;
            var password = PassEdit.Text;
            App.CurrentKey = null;

            if (string.IsNullOrWhiteSpace(UserEdit.Text) || string.IsNullOrWhiteSpace(PassEdit.Text))
            {
                await DisplayActionSheet("Error", "Cancel", null, "Missing Fields, Please fill out properly.");
                return;
            }

            if (StudentRadChk.IsChecked)
            {
                var student = await App.StudentDb.ReadAll();
                foreach (var s in student.Where(s => s.Username == userName && s.Password == password))
                {
                    if (s.IsVerified)
                    {
                        await DisplayAlert("Success", "Login Successful", "OK");
                        // create a new user object
                        App.CurrentKey = s.Key;
                        await Navigation.PushAsync(new StudentOperations.StudentHome());
                        return;
                    }

                    await DisplayActionSheet("Error", "Cancel", null, "Your account is not verified yet.");
                    return;
                }
            }

            if (TeacherRadChk.IsChecked)
            {
                var teacher = await App.TeacherDb.ReadAll();
                foreach (var t in teacher.Where(t => t.Username == userName && t.Password == password))
                {
                    if (t.IsVerified)
                    {
                        
                        await DisplayAlert("Success", "Login Successful", "OK");
                        App.CurrentKey = t.Key;
                        await Navigation.PushAsync(new TeacherOperations.TeacherHome());
                        return;
                    }
                    await DisplayActionSheet("Error", "Cancel", null, "Your account is not verified yet.");
                    return;
                }
            }

            if (TutorRadChk.IsChecked)
            {
                var tutors = await App.TutorDb.ReadAll();
                foreach (var tutor in tutors.Where(tutor => tutor.Username == userName && tutor.Password == password))
                {
                    if (tutor.IsVerified)
                    {
                        await DisplayAlert("Success", "Login Successful", "OK");
                        App.CurrentKey = tutor.Key;
                        await Navigation.PushAsync(new TutorOperations.TutorHome());
                        return;
                    }
                    await DisplayActionSheet("Error", "Cancel", null, "Your account is not verified yet.");
                    return;
                }
            }

            if (AdminRadChk.IsChecked)
            {
                var admins = await App.AdminDb.ReadAll();
                foreach (var admin in admins.Where(admin => admin.Username == userName && admin.Password == password))
                {
                    await DisplayAlert("Success", "Login Successful", "OK");
                    App.CurrentKey = admin.Key;
                    await Navigation.PushAsync(new AdminHome());
                    return;
                }

                
            }
            if (App.CurrentKey == null) return;
            
            await DisplayActionSheet("Error", "Cancel", null, "Invalid Username or Password.");

            if (AdminRadChk.IsChecked || TeacherRadChk.IsChecked || StudentRadChk.IsChecked ||
                TutorRadChk.IsChecked) return;
            await DisplayActionSheet("Error", "Cancel", null, "Please select a user type.");

        }

        //create new admin if not found
        private async void CreateAdmin()
        {
            var admins = await App.AdminDb.ReadAll();
            if (admins.Count != 0) return;
            var newAdmin = new Admin()
            {
                Username = "Trim",
                Password = "TrLa0519.",
                FirstName = "Tristan",
                LastName = "Lafleur",
                Role = RoleType.Admin
            };
            
            if (await App.AdminDb.Create(newAdmin))
            {
                await DisplayAlert("Success", "Saved", "Cancel");
            }
            else
            {
                await DisplayAlert("Failed", "Did not save", "Cancel");
            }
        }
    }
}
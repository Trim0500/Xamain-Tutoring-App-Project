using System;
using System.Linq;
using TutoringAppProject.Enums;
using TutoringAppProject.Models;
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
            App._currentUser = null;

            if (string.IsNullOrWhiteSpace(UserEdit.Text) || string.IsNullOrWhiteSpace(PassEdit.Text))
            {
                await DisplayActionSheet("Error", "Cancel", null, "Missing Fields, Please fill out properly.");
                return;
            }

            if (StudentRadChk.IsChecked)
            {
                var student = await App._studentDB.ReadAll();
                foreach (var s in student.Where(s => s.userName == userName && s.password == password))
                {
                    if (s.isVerified)
                    {
                        await DisplayAlert("Success", "Login Successful", "OK");
                        App._currentUser = s;
                        await Navigation.PushAsync(new StudentOperations.StudentHome());
                        return;
                    }

                    await DisplayActionSheet("Error", "Cancel", null, "Your account is not verified yet.");
                    return;
                }
            }

            if (TeacherRadChk.IsChecked)
            {
                var teacher = await App._teacherDB.ReadAll();
                foreach (var t in teacher.Where(t => t.userName == userName && t.password == password))
                {
                    if (t.isVerified)
                    {
                        
                        await DisplayAlert("Success", "Login Successful", "OK");
                        App._currentUser = t;
                        await Navigation.PushAsync(new TeacherOperations.TeacherHome());
                        return;
                    }
                    await DisplayActionSheet("Error", "Cancel", null, "Your account is not verified yet.");
                    return;
                }
            }

            if (TutorRadChk.IsChecked)
            {
                var tutors = await App._tutorDB.ReadAll();
                foreach (var tutor in tutors.Where(tutor => tutor.userName == userName && tutor.password == password))
                {
                    if (tutor.isVerified)
                    {
                        await DisplayAlert("Success", "Login Successful", "OK");
                        App._currentUser = tutor;
                        await Navigation.PushAsync(new TutorOperations.TutorHome());
                        return;
                    }
                    await DisplayActionSheet("Error", "Cancel", null, "Your account is not verified yet.");
                    return;
                }
            }

            if (AdminRadChk.IsChecked)
            {
                var admins = await App._adminDb.ReadAll();
                foreach (var admin in admins.Where(admin => admin.userName == userName && admin.password == password))
                {
                    await DisplayAlert("Success", "Login Successful", "OK");
                    App._currentUser = admin;
                    await Navigation.PushAsync(new AdminHome());
                    return;
                }

                
            }
            if (App._currentUser == null) return;
            
            await DisplayActionSheet("Error", "Cancel", null, "Invalid Username or Password.");

            if (AdminRadChk.IsChecked || TeacherRadChk.IsChecked || StudentRadChk.IsChecked ||
                TutorRadChk.IsChecked) return;
            await DisplayActionSheet("Error", "Cancel", null, "Please select a user type.");

        }

        private async void CreateAdmin()
        {
            var admins = await App._adminDb.ReadAll();
            if (admins.Count != 0) return;
            var newAdmin = new Admin()
            {
                userName = "Trim",
                password = "TrLa0519.",
                firstName = "Tristan",
                lastName = "Lafleur",
                role = RoleType.Admin
            };
            
            if (await App._adminDb.Create(newAdmin))
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
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
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            CreateAdmin();
        }

        private async void register_btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registration());
        }

        private async void login_btn_Clicked(object sender, EventArgs e)
        {
            String userName = user_edit.Text;
            String password = pass_edit.Text;

            if (string.IsNullOrWhiteSpace(user_edit.Text) || string.IsNullOrWhiteSpace(pass_edit.Text))
            {
                await DisplayActionSheet("Error", "Cancel", null, "Missing Fields, Please fill out properly.");
                return;
            }

            List<User> userList = await App._userDB.ReadAll();
            foreach (User temp in userList)
            {
                if (temp.key != null)
                {
                    if (temp.userName == userName && temp.password == password)
                    {
                        App._currentUser = temp;
                    }
                }
                else
                {
                    await DisplayAlert("Firebase Key Error", "The firebase record for this user was found to be null", "OK");
                    return;
                }
            }

            //Console.WriteLine(App.globalUser);

            if (App._currentUser != null)
            {
                if (App._currentUser.role == "admin")
                {
                    await DisplayAlert("Welcome Admin!", "Login successful for Trim!", "OK");
                    await Navigation.PushAsync(new AdminHomePage());
                }
                else
                {
                    await DisplayAlert("Welcome User!", "Login Successful for user: " + App._currentUser.userName + "!", "OK");
                    await Navigation.PushAsync(new UserPage());
                }
            }
            else
            {
                await DisplayAlert("Login Failed", "Incorrect username or password", "OK");
                return;
            }
        }

        async void CreateAdmin()
        {
            List<User> userList = await App._userDB.ReadAll();
            if (userList.Count == 0)
            {
                User newUser = new User();

                newUser.userName = "Trim";
                newUser.password = "TrLa0519.";
                newUser.firstName = "Tristan";
                newUser.lastName = "Lafleur";
                newUser.role = "admin";

                var isSaved = await App._userDB.Create(newUser);

                if (isSaved)
                {
                    await DisplayAlert("Success", "Saved", "Cancel");
                }
                else
                {
                    await DisplayAlert("Failed", "Did not save", "Cancel");
                }

                //await userDB.Create(new User
                //{
                //    userName = "Trim",
                //    password = "TrLa0519.",
                //    firstName = "Tristan",
                //    lastName = "Lafleur",
                //    role = "admin",
                //    email = "tristanblacklafleur@hotmail.ca",
                //    phoneNum = "(438) 526-5985"
                //});
            }
        }
    }
}
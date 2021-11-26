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
    public partial class Registration : ContentPage
    {
        public Registration()
        {
            InitializeComponent();
        }

        private async void register_btn_Clicked(object sender, EventArgs e)
        {
            if (pass_edit.Text == null || pass_edit.Text.Length < 10)
            {
                await DisplayAlert("Register Result", "Failure, Password should have 10 characters", "OK");
                return;
            }
            else if (string.IsNullOrWhiteSpace(user_edit.Text) || string.IsNullOrWhiteSpace(pass_edit.Text) ||
                     string.IsNullOrWhiteSpace(fName_edit.Text) || string.IsNullOrWhiteSpace(lName_edit.Text) ||
                     string.IsNullOrWhiteSpace(email_edit.Text) || string.IsNullOrWhiteSpace(phone_edit.Text))
            {
                await DisplayActionSheet("Error", "Cancel", null, "Missing Fields, Please fill out properly.");
                return;
            }
            else if (user_edit.Text == "admin" || user_edit.Text == "Admin" || user_edit.Text == "ADMIN")
            {
                await DisplayActionSheet("Error", "Cancel", null, "Forbidden, Cannot use reserved username");
                return;
            }
            else
            {
                User newUser = new User();

                newUser.userName = user_edit.Text;
                newUser.password = pass_edit.Text;
                newUser.firstName = fName_edit.Text;
                newUser.lastName = lName_edit.Text;
                newUser.role = "viewer";

                var isSaved = await App._userDB.Create(newUser);

                if (!isSaved)
                {
                    await DisplayAlert("Failed", "Did not save", "Cancel");
                    return;
                }

                await DisplayAlert("Success", "Saved", "OK");

                await Navigation.PushAsync(new Login());
            }
        }
    }
}
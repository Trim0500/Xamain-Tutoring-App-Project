using System;
using TutoringAppProject.Models;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages.Authentication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registration
    {
        public Registration()
        {
            InitializeComponent();
        }

        private async void register_btn_Clicked(object sender, EventArgs e)
        {
            if (PassEdit.Text == null || PassEdit.Text.Length < 10)
            {
                await DisplayAlert("Register Result", "Failure, Password should have 10 characters", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(UserEdit.Text) || string.IsNullOrWhiteSpace(PassEdit.Text) ||
                string.IsNullOrWhiteSpace(FNameEdit.Text) || string.IsNullOrWhiteSpace(LNameEdit.Text) ||
                string.IsNullOrWhiteSpace(EmailEdit.Text) || string.IsNullOrWhiteSpace(PhoneEdit.Text))
            {
                await DisplayActionSheet("Error", "Cancel", null, "Missing Fields, Please fill out properly.");
                return;
            }

            if (UserEdit.Text == "admin" || UserEdit.Text == "Admin" || UserEdit.Text == "ADMIN")
            {
                await DisplayActionSheet("Error", "Cancel", null, "Forbidden, Cannot use reserved username");
                return;
            }

            var newUser = new User
            {
                userName = UserEdit.Text,
                password = PassEdit.Text,
                firstName = FNameEdit.Text,
                lastName = LNameEdit.Text,
                role = "viewer"
            };

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
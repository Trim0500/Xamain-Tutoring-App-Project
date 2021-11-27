using System;
using System.Collections.Generic;
using TutoringAppProject.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages.UserOperations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserList
    {
        public UserList()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            List<User> users = await App._userDB.ReadAll();

            UserListCollectionView.ItemsSource = users;
        }

        private async void add_btn_Clicked(object sender, EventArgs e)
        {
            if (PassEdit.Text == null || PassEdit.Text.Length < 10)
            {
                await DisplayAlert("Register Result", "Failure, Password should have 10 characters", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(UserEdit.Text) || string.IsNullOrWhiteSpace(PassEdit.Text) ||
                string.IsNullOrWhiteSpace(FNameEdit.Text) || string.IsNullOrWhiteSpace(LNameEdit.Text))
            {
                await DisplayActionSheet("Error", "Cancel", null, "Missing Fields, Please fill out properly.");
                return;
            }

            if (UserEdit.Text == "admin" || UserEdit.Text == "Admin" || UserEdit.Text == "ADMIN")
            {
                await DisplayActionSheet("Error", "Cancel", null, "Forbidden, Cannot use reserved username");
                return;
            }

            if (TeacherRadBtn.IsChecked)
            {
                User newUser = new User
                {
                    userName = UserEdit.Text,
                    password = PassEdit.Text,
                    firstName = FNameEdit.Text,
                    lastName = LNameEdit.Text,
                    role = "Teacher"
                };

                var isSaved = await App._userDB.Create(newUser);

                if (!isSaved)
                {
                    await DisplayAlert("Failed", "Did not save", "Cancel");
                    return;
                }

                await DisplayAlert("Success", "Saved", "OK");

                var userList = await App._userDB.ReadAll();
                foreach (var temp in userList)
                {
                    if (temp.key != null)
                    {
                        if (temp.userName == UserEdit.Text && temp.password == PassEdit.Text)
                        {
                        }
                    }
                    else
                    {
                        await DisplayAlert("Firebase Key Error", "The firebase record for this user was found to be null", "OK");
                        return;
                    }
                }

                // Use the teacher db?
            }
            else if (TutorRadBtn.IsChecked)
            {
                User newUser = new User
                {
                    userName = UserEdit.Text,
                    password = PassEdit.Text,
                    firstName = FNameEdit.Text,
                    lastName = LNameEdit.Text,
                    role = "Tutor"
                };

                var isSaved = await App._userDB.Create(newUser);

                if (!isSaved)
                {
                    await DisplayAlert("Failed", "Did not save", "Cancel");
                    return;
                }

                await DisplayAlert("Success", "Saved", "OK");

                var userList = await App._userDB.ReadAll();
                foreach (var temp in userList)
                {
                    if (temp.key != null)
                    {
                        if (temp.userName == UserEdit.Text && temp.password == PassEdit.Text)
                        {
                        }
                    }
                    else
                    {
                        await DisplayAlert("Firebase Key Error", "The firebase record for this user was found to be null", "OK");
                        return;
                    }
                }

                // Use the tutor DB?
            }
            else if (StudentRadBtn.IsChecked)
            {
                User newUser = new User
                {
                    userName = UserEdit.Text,
                    password = PassEdit.Text,
                    firstName = FNameEdit.Text,
                    lastName = LNameEdit.Text,
                    role = "Student"
                };

                var isSaved = await App._userDB.Create(newUser);

                if (!isSaved)
                {
                    await DisplayAlert("Failed", "Did not save", "Cancel");
                    return;
                }

                await DisplayAlert("Success", "Saved", "OK");

                var userList = await App._userDB.ReadAll();
                foreach (var temp in userList)
                {
                    if (temp.key != null)
                    {
                        if (temp.userName == UserEdit.Text && temp.password == PassEdit.Text)
                        {
                        }
                    }
                    else
                    {
                        await DisplayAlert("Firebase Key Error", "The firebase record for this user was found to be null", "OK");
                        return;
                    }
                }

                // Use student DB?
            }
            await DisplayAlert("Register Result", "Success", "OK");
        }

        private async void update_btn_Clicked(object sender, EventArgs e)
        {
            if (PassEdit.Text == null || PassEdit.Text.Length < 10)
            {
                await DisplayAlert("Register Result", "Failure, Password should have 10 characters", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(UserEdit.Text) || string.IsNullOrWhiteSpace(PassEdit.Text) ||
                string.IsNullOrWhiteSpace(FNameEdit.Text) || string.IsNullOrWhiteSpace(LNameEdit.Text))
            {
                await DisplayActionSheet("Error", "Cancel", null, "Missing Fields, Please fill out properly.");
                return;
            }

            if (UserEdit.Text == "admin" || UserEdit.Text == "Admin" || UserEdit.Text == "ADMIN")
            {
                await DisplayActionSheet("Error", "Cancel", null, "Forbidden, Cannot use reserved username");
                return;
            }

            if (TeacherRadBtn.IsChecked)
            {
                bool isUpdated = await App._userDB.Update(new User
                {
                    key = KeyEdit.Text,
                    userName = UserEdit.Text,
                    password = PassEdit.Text,
                    firstName = FNameEdit.Text,
                    lastName = LNameEdit.Text,
                    role = "Teacher"
                });

                if (isUpdated)
                {
                    await DisplayAlert("Success", "Saved", "Cancel");
                    return;
                }

                await DisplayAlert("Failed", "Did not save", "Cancel");
                return;
            }

            if (TutorRadBtn.IsChecked)
            {
                bool isUpdated = await App._userDB.Update(new User
                {
                    key = KeyEdit.Text,
                    userName = UserEdit.Text,
                    password = PassEdit.Text,
                    firstName = FNameEdit.Text,
                    lastName = LNameEdit.Text,
                    role = "Tutor"
                });

                if (isUpdated)
                {
                    await DisplayAlert("Success", "Saved", "Cancel");
                    return;
                }

                await DisplayAlert("Failed", "Did not save", "Cancel");
                return;
            }

            if (StudentRadBtn.IsChecked)
            {
                bool isUpdated = await App._userDB.Update(new User
                {
                    key = KeyEdit.Text,
                    userName = UserEdit.Text,
                    password = PassEdit.Text,
                    firstName = FNameEdit.Text,
                    lastName = LNameEdit.Text,
                    role = "Student"
                });

                if (isUpdated)
                {
                    await DisplayAlert("Success", "Saved", "Cancel");
                    return;
                }

                await DisplayAlert("Failed", "Did not save", "Cancel");
                return;
            }
            await DisplayAlert("Update Result", "Success", "OK");
        }

        private async void delete_btn_Clicked(object sender, EventArgs e)
        {
            var isDeleted = await App._userDB.Delete(KeyEdit.Text);

            if (isDeleted)
            {
                await DisplayAlert("Deletion Successful", "The user was deleted!", "OK");
                return;
            }

            await DisplayAlert("Deletion failed", "The user couldn't be deleted", "OK");
        }

        private async void back_btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void key_tap_Tapped(object sender, EventArgs e)
        {
            string key = ((TappedEventArgs)e).Parameter.ToString();

            await DisplayAlert("Show ID", key, "OK");

            User user = await App._userDB.ReadById(key);

            KeyEdit.Text = key;
            UserEdit.Text = user.userName;
            PassEdit.Text = user.password;
            FNameEdit.Text = user.firstName;
            LNameEdit.Text = user.lastName;
            switch (user.role)
            {
                case "Teacher":
                    TeacherRadBtn.IsChecked = true;
                    break;
                case "Tutor":
                    TutorRadBtn.IsChecked = true;
                    break;
                case "Student":
                    StudentRadBtn.IsChecked = true;
                    break;
            }
        }
    }
}
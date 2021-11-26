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
    public partial class UserList : ContentPage
    {
        public UserList()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            List<User> users = await App._userDB.ReadAll();

            user_list_collectionView.ItemsSource = users;
        }

        private async void add_btn_Clicked(object sender, EventArgs e)
        {
            if (pass_edit.Text == null || pass_edit.Text.Length < 10)
            {
                await DisplayAlert("Register Result", "Failure, Password should have 10 characters", "OK");
                return;
            }
            else if (string.IsNullOrWhiteSpace(user_edit.Text) || string.IsNullOrWhiteSpace(pass_edit.Text) ||
                     string.IsNullOrWhiteSpace(fName_edit.Text) || string.IsNullOrWhiteSpace(lName_edit.Text))
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
                if (teacher_rad_btn.IsChecked)
                {
                    User newUser = new User();

                    newUser.userName = user_edit.Text;
                    newUser.password = pass_edit.Text;
                    newUser.firstName = fName_edit.Text;
                    newUser.lastName = lName_edit.Text;
                    newUser.role = "Teacher";

                    var isSaved = await App._userDB.Create(newUser);

                    if (!isSaved)
                    {
                        await DisplayAlert("Failed", "Did not save", "Cancel");
                        return;
                    }

                    await DisplayAlert("Success", "Saved", "OK");

                    User teacherUser = null;
                    List<User> userList = await App._userDB.ReadAll();
                    foreach (User temp in userList)
                    {
                        if (temp.key != null)
                        {
                            if (temp.userName == user_edit.Text && temp.password == pass_edit.Text)
                            {
                                teacherUser = temp;
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
                else if (tutor_rad_btn.IsChecked)
                {
                    User newUser = new User();

                    newUser.userName = user_edit.Text;
                    newUser.password = pass_edit.Text;
                    newUser.firstName = fName_edit.Text;
                    newUser.lastName = lName_edit.Text;
                    newUser.role = "Tutor";

                    var isSaved = await App._userDB.Create(newUser);

                    if (!isSaved)
                    {
                        await DisplayAlert("Failed", "Did not save", "Cancel");
                        return;
                    }

                    await DisplayAlert("Success", "Saved", "OK");

                    User tutorUser = null;
                    List<User> userList = await App._userDB.ReadAll();
                    foreach (User temp in userList)
                    {
                        if (temp.key != null)
                        {
                            if (temp.userName == user_edit.Text && temp.password == pass_edit.Text)
                            {
                                tutorUser = temp;
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
                else if (student_rad_btn.IsChecked)
                {
                    User newUser = new User();

                    newUser.userName = user_edit.Text;
                    newUser.password = pass_edit.Text;
                    newUser.firstName = fName_edit.Text;
                    newUser.lastName = lName_edit.Text;
                    newUser.role = "Student";

                    var isSaved = await App._userDB.Create(newUser);

                    if (!isSaved)
                    {
                        await DisplayAlert("Failed", "Did not save", "Cancel");
                        return;
                    }

                    await DisplayAlert("Success", "Saved", "OK");

                    User studentUser = null;
                    List<User> userList = await App._userDB.ReadAll();
                    foreach (User temp in userList)
                    {
                        if (temp.key != null)
                        {
                            if (temp.userName == user_edit.Text && temp.password == pass_edit.Text)
                            {
                                studentUser = temp;
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
        }

        private async void update_btn_Clicked(object sender, EventArgs e)
        {
            if (pass_edit.Text == null || pass_edit.Text.Length < 10)
            {
                await DisplayAlert("Register Result", "Failure, Password should have 10 characters", "OK");
                return;
            }
            else if (string.IsNullOrWhiteSpace(user_edit.Text) || string.IsNullOrWhiteSpace(pass_edit.Text) ||
                     string.IsNullOrWhiteSpace(fName_edit.Text) || string.IsNullOrWhiteSpace(lName_edit.Text))
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
                if (teacher_rad_btn.IsChecked)
                {
                    bool isUpdated = await App._userDB.Update(new User
                    {
                        key = key_edit.Text,
                        userName = user_edit.Text,
                        password = pass_edit.Text,
                        firstName = fName_edit.Text,
                        lastName = lName_edit.Text,
                        role = "Teacher"
                    });

                    if (isUpdated)
                    {
                        await DisplayAlert("Success", "Saved", "Cancel");
                        return;
                    }
                    else
                    {
                        await DisplayAlert("Failed", "Did not save", "Cancel");
                        return;
                    }
                }
                else if (tutor_rad_btn.IsChecked)
                {
                    bool isUpdated = await App._userDB.Update(new User
                    {
                        key = key_edit.Text,
                        userName = user_edit.Text,
                        password = pass_edit.Text,
                        firstName = fName_edit.Text,
                        lastName = lName_edit.Text,
                        role = "Tutor"
                    });

                    if (isUpdated)
                    {
                        await DisplayAlert("Success", "Saved", "Cancel");
                        return;
                    }
                    else
                    {
                        await DisplayAlert("Failed", "Did not save", "Cancel");
                        return;
                    }
                }
                else if (student_rad_btn.IsChecked)
                {
                    bool isUpdated = await App._userDB.Update(new User
                    {
                        key = key_edit.Text,
                        userName = user_edit.Text,
                        password = pass_edit.Text,
                        firstName = fName_edit.Text,
                        lastName = lName_edit.Text,
                        role = "Student"
                    });

                    if (isUpdated)
                    {
                        await DisplayAlert("Success", "Saved", "Cancel");
                        return;
                    }
                    else
                    {
                        await DisplayAlert("Failed", "Did not save", "Cancel");
                        return;
                    }
                }
                await DisplayAlert("Update Result", "Success", "OK");
            }
        }

        private async void delete_btn_Clicked(object sender, EventArgs e)
        {
            var isDeleted = await App._userDB.Delete(key_edit.Text);

            if (isDeleted)
            {
                await DisplayAlert("Deletion Successful", "The user was deleted!", "OK");
                return;
            }
            else
            {
                await DisplayAlert("Deletion failed", "The user couldn't be deleted", "OK");
                return;
            }
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

            key_edit.Text = key;
            user_edit.Text = user.userName;
            pass_edit.Text = user.password;
            fName_edit.Text = user.firstName;
            lName_edit.Text = user.lastName;
            switch (user.role)
            {
                case "Teacher":
                    teacher_rad_btn.IsChecked = true;
                    break;
                case "Tutor":
                    tutor_rad_btn.IsChecked = true;
                    break;
                case "Student":
                    student_rad_btn.IsChecked = true;
                    break;
                default:
                    break;
            }
        }
    }
}
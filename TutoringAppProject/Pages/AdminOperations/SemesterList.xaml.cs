using System;
using System.Collections.Generic;
using TutoringAppProject.Models;
using TutoringAppProject.Pages.UserOperations;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SemesterList
    {
        public SemesterList()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            List<Semester> semesters = await App._semesterDB.ReadAll();
            SemesterCollectionView.ItemsSource = semesters;
        }

        private void teacher_toolbar_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void tutor_toolbar_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void student_toolbar_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private async void logout_toolbar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void user_toolbar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserList());
        }
        private async void key_tap_Tapped_update(object sender, EventArgs e)
        {
            string key = ((TappedEventArgs)e).Parameter.ToString();

            await DisplayAlert("Show ID", key, "OK");

            Semester semester = await App._semesterDB.ReadById(key);
            await Navigation.PushAsync(new SemesterRegistration(semester));
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SemesterRegistration());
        }

        private async void key_tap_Tapped_delete(object sender, EventArgs e)
        {
            string key = ((TappedEventArgs)e).Parameter.ToString();

            await App._semesterDB.Delete(key);

            await DisplayAlert("Delete", "Semester Deleted with key: " + key, "OK");
        }
    }
}
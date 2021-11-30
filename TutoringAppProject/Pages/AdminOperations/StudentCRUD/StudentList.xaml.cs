using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages.StudentCRUD
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentList
    {
        public StudentList()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            var semesters = await App._studentDB.ReadAll();
            StudentCollectionView.ItemsSource = semesters;
        }

        private async void key_tap_Tapped_update(object sender, EventArgs e)
        {
            var key = ((TappedEventArgs)e).Parameter.ToString();

            await DisplayAlert("Show ID", key, "OK");

            var student = await App._studentDB.ReadById(key);
            await Navigation.PushAsync(new StudentRegistration(student));
        }

        private async void key_tap_Tapped_delete(object sender, EventArgs e)
        {
            var key = ((TappedEventArgs)e).Parameter.ToString();

            await App._studentDB.Delete(key);
            
            await DisplayAlert("Delete", "Student Deleted with key: " + key, "OK");
            
            OnAppearing();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StudentRegistration());
        }

        private async void Key_tap_verify_OnTapped(object sender, EventArgs e)
        {
            var key = ((TappedEventArgs)e).Parameter.ToString();
            
            var student = await App._studentDB.ReadById(key);
            
            // verify student

            switch (student.isVerified)
            {
                case false:
                    student.isVerified = true;
                    break;
                case true:
                    await DisplayAlert("Verify", "Student already verified", "OK");
                    return;
            }

            if (await App._studentDB.Update(student))
            {
                await DisplayAlert("Verify", "Student Verified", "OK");
            }
            else
            {
                await DisplayAlert("Verify", "Could not verify student", "OK");
            }
        }
    }
}
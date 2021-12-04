using System;
using TutoringAppProject.Models;
using TutoringAppProject.Models.Users;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages.TutorCRUD
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TutorList
    {
        public TutorList()
        {
            InitializeComponent();
        }
        
        protected override async void OnAppearing()
        {
            var tutors = await App.TutorDb.ReadAll();
            TutorCollectionView.ItemsSource = tutors;
        }

        private async void key_tap_Tapped_update(object sender, EventArgs e)
        {
            var key = ((TappedEventArgs)e).Parameter.ToString();

            await DisplayAlert("Show ID", key, "OK");

            Tutor tutor = await App.TutorDb.ReadById(key);
            
            await Navigation.PushAsync(new TutorRegistration(tutor));
        }

        private async void key_tap_Tapped_delete(object sender, EventArgs e)
        {
            var key = ((TappedEventArgs)e).Parameter.ToString();

            await App.TeacherDb.Delete(key);
            
            await DisplayAlert("Delete", "Tutor Deleted with key: " + key, "OK");
            
            OnAppearing();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TutorRegistration());
        }
        
        private async void Key_tap_verify_OnTapped(object sender, EventArgs e)
        {
            var key = ((TappedEventArgs)e).Parameter.ToString();
            
            var tutor = await App.TutorDb.ReadById(key);
            
            // verify tutor

            switch (tutor.IsVerified)
            {
                case false:
                    tutor.IsVerified = true;
                    break;
                case true:
                    await DisplayAlert("Verify", "Teacher already verified", "OK");
                    return;
            }

            if (await App.TutorDb.Update(tutor))
            {
                await DisplayAlert("Verify", "Teacher Verified", "OK");
            }
            else
            {
                await DisplayAlert("Verify", "Could not verify Teacher", "OK");
            }
        }
    }
}
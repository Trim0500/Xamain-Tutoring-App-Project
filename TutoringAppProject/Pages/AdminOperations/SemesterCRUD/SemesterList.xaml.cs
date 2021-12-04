using System;
using System.Collections.Generic;
using TutoringAppProject.Models;
using TutoringAppProject.Models.System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages.SemesterCRUD
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
            List<Semester> semesters = await App.SemesterDb.ReadAll();
            SemesterCollectionView.ItemsSource = semesters;
        }
        private async void key_tap_Tapped_update(object sender, EventArgs e)
        {
            var key = ((TappedEventArgs)e).Parameter.ToString();

            await DisplayAlert("Show ID", key, "OK");

            var semester = await App.SemesterDb.ReadById(key);
            await Navigation.PushAsync(new SemesterRegistration(semester));
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SemesterRegistration());
        }

        private async void key_tap_Tapped_delete(object sender, EventArgs e)
        {
            var key = ((TappedEventArgs)e).Parameter.ToString();

            await App.SemesterDb.Delete(key);

            await DisplayAlert("Delete", "Semester Deleted with key: " + key, "OK");
            
            OnAppearing();
        }
    }
}
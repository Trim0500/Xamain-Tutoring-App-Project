using System;
using TutoringAppProject.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages.CourseCRUD
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseList
    {
        public CourseList()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            var semesters = await App._courseDB.ReadAll();
            CourseCollectionView.ItemsSource = semesters;
        }

        private async void key_tap_Tapped_update(object sender, EventArgs e)
        {
            string key = ((TappedEventArgs)e).Parameter.ToString();

            await DisplayAlert("Show ID", key, "OK");

            Course course = await App._courseDB.ReadById(key);
            await Navigation.PushAsync(new CourseRegistration(course));
        }

        private async void key_tap_Tapped_delete(object sender, EventArgs e)
        {
            var key = ((TappedEventArgs)e).Parameter.ToString();

            await App._courseDB.Delete(key);

            await DisplayAlert("Delete", "Semester Deleted with key: " + key, "OK");

            OnAppearing();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CourseRegistration());
        }
    }
}
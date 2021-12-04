using System;
using TutoringAppProject.Models;
using TutoringAppProject.Models.System;
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
            var semesters = await App.CourseDb.ReadAll();
            CourseCollectionView.ItemsSource = semesters;
        }

        private async void key_tap_Tapped_update(object sender, EventArgs e)
        {
            string key = ((TappedEventArgs)e).Parameter.ToString();

            await DisplayAlert("Show ID", key, "OK");

            Course course = await App.CourseDb.ReadById(key);
            await Navigation.PushAsync(new CourseRegistration(course));
        }

        private async void key_tap_Tapped_delete(object sender, EventArgs e)
        {
            var key = ((TappedEventArgs)e).Parameter.ToString();

            await App.CourseDb.Delete(key);

            await DisplayAlert("Delete", "Semester Deleted with key: " + key, "OK");

            OnAppearing();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CourseRegistration());
        }
    }
}
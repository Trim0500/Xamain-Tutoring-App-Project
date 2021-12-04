using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutoringAppProject.Models;
using TutoringAppProject.Models.Users;
using TutoringAppProject.Pages.TeacherCRUD;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeacherList : ContentPage
    {
        public TeacherList()
        {
            InitializeComponent();
        }
        
        protected override async void OnAppearing()
        {
            List<Teacher> teachers = await App.TeacherDb.ReadAll();
            TeacherCollectionView.ItemsSource = teachers;
        }

        private async void key_tap_Tapped_update(object sender, EventArgs e)
        {
            var key = ((TappedEventArgs)e).Parameter.ToString();

            await DisplayAlert("Show ID", key, "OK");

            var teacher = await App.TeacherDb.ReadById(key);
            await Navigation.PushAsync(new TeacherRegistration(teacher));
        }

        private async void key_tap_Tapped_delete(object sender, EventArgs e)
        {
            var key = ((TappedEventArgs)e).Parameter.ToString();

            await App.TeacherDb.Delete(key);
            
            await DisplayAlert("Delete", "Teacher Deleted with key: " + key, "OK");
            
            OnAppearing();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TeacherRegistration());
        }
        private async void Key_tap_verify_OnTapped(object sender, EventArgs e)
        {
            var key = ((TappedEventArgs)e).Parameter.ToString();
            
            var teacher = await App.TeacherDb.ReadById(key);
            
            // verify teacher

            switch (teacher.IsVerified)
            {
                case false:
                    teacher.IsVerified = true;
                    break;
                case true:
                    await DisplayAlert("Verify", "Teacher already verified", "OK");
                    return;
            }

            if (await App.TeacherDb.Update(teacher))
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
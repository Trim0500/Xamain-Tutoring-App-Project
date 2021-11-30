using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutoringAppProject.Models;
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
            List<Teacher> teachers = await App._teacherDB.ReadAll();
            TeacherCollectionView.ItemsSource = teachers;
        }

        private async void key_tap_Tapped_update(object sender, EventArgs e)
        {
            var key = ((TappedEventArgs)e).Parameter.ToString();

            await DisplayAlert("Show ID", key, "OK");

            var teacher = await App._teacherDB.ReadById(key);
            await Navigation.PushAsync(new TeacherRegistration(teacher));
        }

        private async void key_tap_Tapped_delete(object sender, EventArgs e)
        {
            string key = ((TappedEventArgs)e).Parameter.ToString();

            await App._teacherDB.Delete(key);
            
            await DisplayAlert("Delete", "Teacher Deleted with key: " + key, "OK");
            
            OnAppearing();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TeacherRegistration());
        }
        
        private async void Button_OnClicked_Verify(object sender, EventArgs e)
        {
            var key = ((Button)sender).CommandParameter.ToString();
            
            var teacher = App._teacherDB.ReadById(key).Result;
            
            // verify teacher

            switch (teacher.isVerified)
            {
                case false:
                    teacher.isVerified = true;
                    break;
                case true:
                    await DisplayAlert("Verify", "Teacher already verified", "OK");
                    return;
            }

            if (await App._teacherDB.Update(teacher))
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
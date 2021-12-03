﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages.TutorOperations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TutorSessionList
    {
        public TutorSessionList()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            var sessions = await App._sessionDB.ReadAll();
            SessionCollectionView.ItemsSource = sessions;
            
        }

        private async void key_tap_Tapped_update(object sender, EventArgs e)
        {
            var key = ((TappedEventArgs)e).Parameter.ToString();

            await DisplayAlert("Show ID", key, "OK");

            var session = await App._sessionDB.ReadById(key);
            
            await Navigation.PushAsync(new TutorSessionRegister(session));
        }

        private async void key_tap_Tapped_delete(object sender, EventArgs e)
        {
            var key = ((TappedEventArgs)e).Parameter.ToString();

            await App._sessionDB.Delete(key);
            
            await DisplayAlert("Delete", "Session Deleted with key: " + key, "OK");
            
            OnAppearing();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TutorSessionRegister());
        }
    }
}
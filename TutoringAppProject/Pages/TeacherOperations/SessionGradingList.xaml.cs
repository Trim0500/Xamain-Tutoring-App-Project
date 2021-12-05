using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages.TeacherOperations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SessionGradingList : ContentPage
    {
        public SessionGradingList()
        {
            InitializeComponent();
        }
        
        protected override async void OnAppearing()
        {
            var sessions = await App.SessionDb.ReadAll();
            SessionCollectionView.ItemsSource = sessions;
            
        }

        private async void Key_tap_grade_OnTapped(object sender, EventArgs e)
        {
            var key = ((TappedEventArgs)e).Parameter.ToString();
            
            var session = await App.SessionDb.ReadById(key);
            
            // pop up to grade the session
            var grade = await DisplayPromptAsync("Grade Session", "Enter a grade for the session", initialValue: session.TutorGrade.ToString());
            if (grade == null) return;
            session.TutorGrade = int.Parse(grade);
            await App.SessionDb.Update(session);
            OnAppearing();
        }
    }
}
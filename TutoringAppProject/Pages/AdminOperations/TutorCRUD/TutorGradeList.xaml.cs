using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutoringAppProject.Models.Users;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages.AdminOperations.TutorCRUD
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TutorGradeList : ContentPage
    {

        private Tutor _tutor{ get; set; }


        public TutorGradeList(Tutor tutor)
        {
            _tutor = tutor;
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            string gay = _tutor.Key;
            var tutors = await App.SessionDb.GetAllSessionsByTutor(gay);
            TutorGradeCollectionView.ItemsSource = tutors;

        }
        private async void GoBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
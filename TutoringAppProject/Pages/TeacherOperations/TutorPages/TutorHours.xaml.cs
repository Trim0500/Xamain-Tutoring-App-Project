using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutoringAppProject.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages.TeacherOperations.TutorPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TutorHours : ContentPage
    {
        List<Tutor> tutors = new List<Tutor>();
        public TutorHours()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            CourseChoice.ItemsSource = App._currentTeacher.courses;
            CourseChoice.SelectedItem = App._currentTeacher.courses[0];

            tutors = await App._tutorDB.ReadAll();

            List<Tutor> courseTutors = new List<Tutor>();
            foreach(Tutor temp in tutors)
            {
                if(temp.courses.Contains<string>(CourseChoice.SelectedItem.ToString()))
                {
                    courseTutors.Add(temp);
                }
            }

            TutorCollectionView.ItemsSource = courseTutors;
        }

        private async void GoBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void CourseChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCourse = CourseChoice.SelectedItem.ToString();

            List<Tutor> courseTutors = new List<Tutor>();
            foreach (Tutor temp in tutors)
            {
                if (temp.courses.Contains<string>(selectedCourse))
                {
                    courseTutors.Add(temp);
                }
            }

            TutorCollectionView.ItemsSource = courseTutors;
        }

        private async void key_tap_verify_Tapped(object sender, EventArgs e)
        {
            var key = ((TappedEventArgs)e).Parameter.ToString();

            var tutor = await App._tutorDB.ReadById(key);

            await Navigation.PushAsync(new TutorDetails(tutor));
        }
    }
}
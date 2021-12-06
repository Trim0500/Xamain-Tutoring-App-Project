using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutoringAppProject.Models.Users;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages.TeacherOperations.TutorPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TutorHours : ContentPage
    {
        List<Tutor> tutors = new List<Tutor>();
        Teacher _currentTeacher { get; set; }
        public TutorHours()
        {
            InitializeComponent();
            SetPicker();
        }

        public async void SetPicker()
        {
            _currentTeacher = await App.TeacherDb.ReadById(App.CurrentKey);

            CourseChoice.ItemsSource = _currentTeacher.Courses;
            CourseChoice.SelectedItem = _currentTeacher.Courses[0];

            tutors = await App.TutorDb.ReadAll();

            List<Tutor> courseTutors = new List<Tutor>();
            foreach (Tutor temp in tutors)
            {
                if (temp.Courses.Contains<string>(CourseChoice.SelectedItem.ToString()))
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
                if (temp.Courses.Contains<string>(selectedCourse))
                {
                    courseTutors.Add(temp);
                }
            }

            TutorCollectionView.ItemsSource = courseTutors;
        }

        private async void key_tap_verify_Tapped(object sender, EventArgs e)
        {
            var key = ((TappedEventArgs)e).Parameter.ToString();

            var tutor = await App.TutorDb.ReadById(key);

            await Navigation.PushAsync(new TutorDetails(tutor));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutoringAppProject.Models.Users;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages.TeacherOperations.StudentPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentVisits : ContentPage
    {
        List<Student> _students = new List<Student>();
        Teacher _currentTeacher { get; set; }
        public StudentVisits()
        {
            InitializeComponent();
            SetPicker();
        }

        public async void SetPicker()
        {
            _currentTeacher = await App.TeacherDb.ReadById(App.CurrentKey);

            CourseChoice.ItemsSource = _currentTeacher.Courses;
            CourseChoice.SelectedItem = _currentTeacher.Courses[0];

            _students = await App.StudentDb.ReadAll();

            List<Student> courseStudents = new List<Student>();
            foreach (Student temp in _students)
            {
                if (temp.Courses.Contains<string>(CourseChoice.SelectedItem.ToString()))
                {
                    courseStudents.Add(temp);
                }
            }

            StudentCollectionView.ItemsSource = courseStudents;
        }

        private async void GoBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void CourseChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCourse = CourseChoice.SelectedItem.ToString();

            List<Student> courseStudents = new List<Student>();
            foreach (Student temp in _students)
            {
                if (temp.Courses.Contains<string>(selectedCourse))
                {
                    courseStudents.Add(temp);
                }
            }

            StudentCollectionView.ItemsSource = courseStudents;
        }

        private async void key_tap_verify_Tapped(object sender, EventArgs e)
        {
            var key = ((TappedEventArgs)e).Parameter.ToString();

            var student = await App.StudentDb.ReadById(key);

            await Navigation.PushAsync(new StudentDetails(student));
        }
    }
}
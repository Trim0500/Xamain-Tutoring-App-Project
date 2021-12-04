using System;
using System.Collections.Generic;
using System.Linq;
using TutoringAppProject.Models;
using TutoringAppProject.Models.Enums;
using TutoringAppProject.Models.System;
using TutoringAppProject.Models.Users;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages.TutorOperations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TutorSessionRegister
    {
        private Tutor _tutor = new Tutor();
        private List<Student> students = new List<Student>();
        private List<string> studentNames = new List<string>();
        private readonly bool _isUpdate;
        private readonly string _tutorKey;

        public TutorSessionRegister()
        {
            InitializeComponent();
            GetTutor();
            _isUpdate = false;
            SessionAddOrUpdateButton.Text = "Add";
            SessionDate.Date = DateTime.Now;
        }

        public TutorSessionRegister(Session session)
        {
            InitializeComponent();
            GetTutor();
            _isUpdate = true;
            SessionAddOrUpdateButton.Text = "Update";
            _tutorKey = session.TutorKey;
            
            // setting prefilled values
            if (session.SessionType == TutoringType.Group)
            {
                RadioButtonGroupTutoring.IsChecked = true;
            }
            else
            {
                RadioButtonIndividualTutoring.IsChecked = true;
            }
            
            SessionDate.Date = session.Date;
            SessionStartTime.Time = session.StartTime;
            SessionEndTime.Time = session.EndTime;

        }

        public async void GetTutor()
        {
            _tutor = await App.TutorDb.ReadById(App.CurrentKey);
            SessionCourse.ItemsSource = _tutor.Courses;
            SessionCourse.SelectedIndex = 0;
            string courseName = SessionCourse.SelectedItem.ToString();
            students = await App.StudentDb.ReadAllByCourse(courseName);
            StudentsList.ItemsSource = students;
        }

        private async void AddOrUpdateSession(object sender, EventArgs e)
        {
            // check if date is in the future
            if (SessionDate.Date < DateTime.Now)
            {
                await DisplayAlert("Error", "Please select a date in the future", "OK");
                return;
            }
            
            if (SessionStartTime.Time > SessionEndTime.Time)
            {
                await DisplayAlert("Error", "Please select a valid time range", "OK");
                return;
            }

            var type = RadioButtonGroupTutoring.IsChecked ? TutoringType.Group : TutoringType.Individual;
            
            var session = new Session()
            {
                Date = SessionDate.Date,
                StartTime = SessionStartTime.Time,
                EndTime = SessionEndTime.Time,
                SessionType = type,
                TutorKey = App.CurrentKey,
                CourseName = SessionCourse.SelectedItem.ToString(),
                AttendingStudents = studentNames.ToArray()
        };

            if (_isUpdate)
            {
                session.Key = _tutorKey;
                if (await App.SessionDb.Update(session))
                {
                    await DisplayAlert("Success", "Session updated", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Session was not updated", "OK");
                }
            }
            else
            {
                if (await App.SessionDb.Create(session))
                {
                    await DisplayAlert("Success", "Semester added", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Semester not added", "OK");
                }
            }
           
        }

        private async void SessionCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedCourse = SessionCourse.SelectedItem.ToString();

            students = await App.StudentDb.ReadAllByCourse(selectedCourse);

            StudentsList.ItemsSource = students;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var studentName = ((TappedEventArgs)e).Parameter.ToString();

            var isInList = studentNames.Contains(studentName);

            if (!isInList)
            {
                studentNames.Add(studentName);
                ((Label)sender).TextColor = Color.Green;
                await DisplayAlert("Adding Student", "Adding the student to the session", "OK");
            }
            else
            {
                studentNames.Add(studentName);
                ((Label)sender).TextColor = Color.Black;
                await DisplayAlert("Removing Student", "Removing the student from the session", "OK");
            }
        }
    }
}
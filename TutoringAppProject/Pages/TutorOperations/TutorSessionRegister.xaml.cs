using System;
using System.Collections.Generic;
using TutoringAppProject.Models;
using TutoringAppProject.Models.Enums;
using TutoringAppProject.Models.System;
using TutoringAppProject.Models.Users;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages.TutorOperations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TutorSessionRegister
    {
        private Tutor _tutor = new Tutor();
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
        }

        private async void AddOrUpdateSession(object sender, EventArgs e)
        {
            // check if date is in the future
            if (SessionDate.Date < DateTime.Now)
            {
                await DisplayAlert("Error", "Please select a date in the future", "OK");
                return;
            }
            
            /*
            // check if time is in the future
            if (SessionStartTime.Time < DateTime.Now.TimeOfDay)
            {
                await DisplayAlert("Error", "Please select a start time in the future", "OK");
                return;
            }
            
            // check if time is in the future
            if (SessionEndTime.Time < DateTime.Now.TimeOfDay)
            {
                await DisplayAlert("Error", "Please select a time in the future", "OK");
                return;
            }
            */
            // check if start time is before end time
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
                TutorKey = App.CurrentKey
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

        private void SessionCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }
    }
}
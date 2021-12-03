using System;
using TutoringAppProject.Enums;
using TutoringAppProject.Models;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages.TutorOperations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TutorSessionRegister
    {
        private readonly Session _session;
        private readonly bool _isUpdate;

        public TutorSessionRegister()
        {
            InitializeComponent();
            _isUpdate = false;
            SessionAddOrUpdateButton.Text = "Add";
            SessionDate.Date = DateTime.Now;
        }

        public TutorSessionRegister(Session session)
        {
            InitializeComponent();
            _isUpdate = true;
            SessionAddOrUpdateButton.Text = "Update";
            _session = session;
            if (_session.SessionType == TutoringType.Group)
            {
                RadioButtonGroupTutoring.IsChecked = true;
            }
            else
            {
                RadioButtonIndividualTutoring.IsChecked = true;
            }
            SessionDate.Date = _session.Date;
            SessionStartTime.Time = _session.StartTime;
            SessionEndTime.Time = _session.EndTime;

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
            
            if (_isUpdate)
            {
                var session = new Session()
                {
                    Key = _session.Key,
                    Date = SessionDate.Date,
                    StartTime = SessionStartTime.Time,
                    EndTime = SessionEndTime.Time,
                    SessionType = type,
                    TutorKey = App._currentUser.key
                };

                if (await App._sessionDB.Update(session))
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
                var session = new Session()
                {
                    Date = SessionDate.Date,
                    StartTime = SessionStartTime.Time,
                    EndTime = SessionEndTime.Time,
                    SessionType = type,
                    TutorKey = App._currentUser.key
                };

                if (await App._sessionDB.Create(session))
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
    }
}
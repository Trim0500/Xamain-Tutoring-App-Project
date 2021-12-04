using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutoringAppProject.Models.System;
using TutoringAppProject.Models.Users;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages.StudentOperations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentHome : ContentPage
    {
        private Student _student = new Student();
        private List<Session> _sessionList = new List<Session>();

        public StudentHome()
        {
            InitializeComponent();
            GetStudent();
        }

        public async void GetStudent()
        {
            _student = await App.StudentDb.ReadById(App.CurrentKey);
            _sessionList = await App.SessionDb.ReadAllByCourseName(_student.Courses.ToList());
            SessionsList.ItemsSource = _sessionList;
        }

        private async void key_tap_details_Tapped(object sender, EventArgs e)
        {
            string sessionKey = ((TappedEventArgs)e).Parameter.ToString();
            Session sessionSelected = await App.SessionDb.ReadById(sessionKey);

            await Navigation.PushAsync(new SessionDetails(sessionSelected));
        }

        private async void key_tap_add_Tapped(object sender, EventArgs e)
        {
            string sessionKey = ((TappedEventArgs)e).Parameter.ToString();
            Session sessionSelected = await App.SessionDb.ReadById(sessionKey);

            List<string> appendedAttendance = new List<string>();
            bool isAttending = false;

            if (sessionSelected.AttendingStudents == null)
            {
                await DisplayAlert("No Attendees", "This session has yet to have any attendees, adding you to the session", "OK");

                appendedAttendance.Add(_student.FirstName);
                sessionSelected.AttendingStudents = appendedAttendance.ToArray();
                sessionSelected.SessionType = Models.Enums.TutoringType.Individual;

                isAttending = true;

                await App.SessionDb.Update(sessionSelected);

                return;
            }
            else
            {
                for (int i = 0; i < sessionSelected.AttendingStudents.Length; i++)
                {
                    isAttending = _student.FirstName.Equals(sessionSelected.AttendingStudents[i]);
                }
            };

            if(!isAttending) {
                appendedAttendance = sessionSelected.AttendingStudents.ToList();
                appendedAttendance.Add(_student.FirstName);
                sessionSelected.AttendingStudents = appendedAttendance.ToArray();
                if(sessionSelected.AttendingStudents.Length >= 2)
                {
                    sessionSelected.SessionType = Models.Enums.TutoringType.Group;
                }

                await App.SessionDb.Update(sessionSelected);

                await DisplayAlert("Marked as Attending", "You've been added to the session!", "OK");
            }
            else
            {
                appendedAttendance = sessionSelected.AttendingStudents.ToList();
                appendedAttendance.Remove(_student.FirstName);
                sessionSelected.AttendingStudents = appendedAttendance.ToArray();
                if (sessionSelected.AttendingStudents.Length < 2)
                {
                    sessionSelected.SessionType = Models.Enums.TutoringType.Individual;
                }

                await App.SessionDb.Update(sessionSelected);

                await DisplayAlert("Removed from Session", "You've been removed from the session!", "OK");
            };
        }
    }
}
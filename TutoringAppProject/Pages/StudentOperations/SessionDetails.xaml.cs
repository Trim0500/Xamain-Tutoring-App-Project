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
    public partial class SessionDetails : ContentPage
    {
        private StackLayout uiContainer;

        private Label courseNameLabel, tutorNameLabel, timeLabel, typeLabel, attendingLabel;
        private Button goBackBtn;

        private Session _session { get; set; }
        private string tutorName { get; set; }
        private Student _student { get; set; }

        public SessionDetails(Session session)
        {
            InitializeComponent();

            _session = session;

            //GetTutorName(_session.TutorKey);
            GenerateInterface();
        }

        //public async void GetTutorName(string key)
        //{
        //    Tutor tutor = await App.TutorDb.ReadById(key);

        //    tutorName = tutor.FirstName + " " + tutor.LastName;

        //    tutorNameLabel = new Label
        //    {
        //        Text = "Hosted by: " + tutorName,
        //        HorizontalTextAlignment = TextAlignment.Center,
        //        VerticalTextAlignment = TextAlignment.Center
        //    };
        //}

        public async void GenerateInterface()
        {
            courseNameLabel = new Label
            {
                Text = _session.CourseName,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };

            Tutor tutor = await App.TutorDb.ReadById(_session.TutorKey);

            tutorName = tutor.FirstName + " " + tutor.LastName;

            tutorNameLabel = new Label
            {
                Text = "Hosted by: " + tutorName,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };

            timeLabel = new Label
            {
                Text = _session.Date.ToString() + " @ " + _session.StartTime.ToString() + " - " + _session.EndTime.ToString(),
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };

            typeLabel = new Label
            {
                Text = _session.SessionType.ToString() + " tutoring",
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };

            _student = await App.StudentDb.ReadById(App.CurrentKey);
            bool isAttending = false;
            List<string> studentsAttending = _session.AttendingStudents.ToList();
            if(studentsAttending.Contains(_student.FirstName)) {
                isAttending = true;
            }

            attendingLabel = new Label
            {
                Text = "Are you attending? " + isAttending,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };

            goBackBtn = new Button
            {
                Text = "Go Back",
                Margin = new Thickness(10)
            };
            goBackBtn.Clicked += GoBackClicked;

            uiContainer = new StackLayout
            {
                Margin = new Thickness(20),
                Children =
                {
                    courseNameLabel,
                    tutorNameLabel,
                    timeLabel,
                    typeLabel,
                    attendingLabel,
                    goBackBtn
                }
            };

            Content = uiContainer;
        }

        private async void GoBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
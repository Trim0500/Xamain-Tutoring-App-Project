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
    public partial class StudentDetails : ContentPage
    {
        private StackLayout uiContainer;

        private Label title ,nameLabel, hoursGathered, hoursRemaining;
        private Button goBackBtn;

        private Student _student { get; set; }
        private string _course { get; set; }

        public StudentDetails()
        {
            InitializeComponent();
        }

        public StudentDetails(Student student, string course)
        {
            // InitializeComponent();
            _student = student;
            _course = course;
            GenerateInterface();
        }

        public async void GenerateInterface()
        {
            var studentName = _student.FirstName + _student.LastName;
            var list = await App.SessionDb.ReadAllByCourseName(_student.Courses.ToList());
            var sortedListByCourse = list.Where(x=> x.CourseName == _course && x.AttendingStudents != null).ToList();
            var sortedListByPerson = list.Where(x => x.AttendingStudents?.Where(s => s == _student.FirstName).FirstOrDefault() == _student.FirstName).ToList();
            int totalVisits = 0;
            long startTime = 0;
            long endTime= 0;
            long realTime = 0;

            foreach(var i in sortedListByPerson)
            {
                startTime += i.StartTime.Ticks;
                endTime += i.EndTime.Ticks;
                realTime += endTime - startTime;
                startTime = 0;
                endTime = 0;
                totalVisits++;
            }
            DateTime totalTime = new DateTime(realTime);
            title = new Label
            {
                Text = _course,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = 20
            };
            nameLabel = new Label
            {
                Text = _student.FirstName + " " + _student.LastName,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };

            hoursGathered = new Label
            {
                Text = "Total Visits Accumulated: "+totalVisits,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };

            hoursRemaining = new Label
            {
                Text = "Total time Accumulated: " 
                +totalTime.Hour+"h and "+totalTime.Minute+" min",
                
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
                    title,
                    nameLabel,
                    hoursGathered,
                    hoursRemaining,
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
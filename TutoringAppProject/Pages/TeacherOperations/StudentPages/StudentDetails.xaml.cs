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

        private Label nameLabel, hoursGathered, hoursRemaining;
        private Button goBackBtn;

        private Student _student { get; set; }

        public StudentDetails()
        {
            InitializeComponent();
        }

        public StudentDetails(Student student)
        {
            // InitializeComponent();
            _student = student;

            GenerateInterface();
        }

        public void GenerateInterface()
        {
            nameLabel = new Label
            {
                Text = _student.FirstName + " " + _student.LastName,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };

            hoursGathered = new Label
            {
                Text = "Total Visits Accumulated: ",
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };

            hoursRemaining = new Label
            {
                Text = "Remaing Visits Accumulated: ",
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
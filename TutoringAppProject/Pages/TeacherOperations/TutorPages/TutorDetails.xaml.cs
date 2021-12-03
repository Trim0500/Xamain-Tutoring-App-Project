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
    public partial class TutorDetails : ContentPage
    {
        private StackLayout uiContainer;

        private Label nameLabel, hoursGathered, hoursRemaining;
        private Button goBackBtn;

        private Tutor _tutor { get; set; }

        public TutorDetails()
        {
            InitializeComponent();
        }

        public TutorDetails(Tutor tutor)
        {
            // InitializeComponent();
            _tutor = tutor;

            GenerateInterface();
        }

        public void GenerateInterface()
        {
            nameLabel = new Label
            {
                Text = _tutor.firstName + " " + _tutor.lastName,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };

            hoursGathered = new Label
            {
                Text = "Total Hours Accumulated: ",
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };

            hoursRemaining = new Label
            {
                Text = "Remaing Hours Accumulated: ",
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
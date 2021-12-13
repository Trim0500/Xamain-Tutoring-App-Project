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
            InitializeComponent();
            GenerateInterface();
        }

        public async void GenerateInterface()
        {
            long totaltime = 0;
            var list = await App.SessionDb.GetAllSessionsByTutor(_tutor.Key);
            var sortedListByTutor = list.Where(x => x.TutorKey == _tutor.Key).ToList();
            foreach(var i in sortedListByTutor)
            {
                totaltime += i.EndTime.Ticks - i.StartTime.Ticks;
            }

            DateTime date = new DateTime(totaltime);

            nameLabel = new Label
            {
                Text = _tutor.FirstName + " " + _tutor.LastName,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };

            hoursGathered = new Label
            {
                Text = "Forecasted tutoring time: "
                + date.Hour + "h and " + date.Minute + " min",
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
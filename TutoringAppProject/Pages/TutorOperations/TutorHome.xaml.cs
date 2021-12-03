using System;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages.TutorOperations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TutorHome
    {
        public TutorHome()
        {
            InitializeComponent();
        }
        private async void Button_OnClicked_TutoringList(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TutorSessionList());
        }
    }
}
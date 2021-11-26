using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SemesterCRUDPage : ContentPage
    {
        public SemesterCRUDPage()
        {
            InitializeComponent();
        }
        
        private void add_btn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SemesterRegistration());
        }

        private void back_btn_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void key_tap_Tapped(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
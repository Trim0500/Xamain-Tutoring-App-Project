using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutoringAppProject.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SemesterRegistration : ContentPage
    {
        public SemesterRegistration()
        {
            InitializeComponent();
        }

        private async void AddSemester(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SemesterName.Text))
            {
                await DisplayAlert("Error", "Please enter a semester name", "OK");
                return;
            }
            
            if (string.IsNullOrEmpty(SemesterCode.Text) && SemesterCode.Text.Length != 4)
            {
                await DisplayAlert("Error", "Please enter a valid semester code", "OK");
                return;
            }
            
            if (string.IsNullOrEmpty(SemesterYear.Text))
            {
                await DisplayAlert("Error", "Please enter a semester year", "OK");
                return;
            }
            
            Semester semester = new Semester
            {
                semesterCode = SemesterName.Text,
                semesterSeason = SemesterCode.Text,
                semesterYear = SemesterYear.Text
            };
            
            if (await App._semesterDB.Create(semester))
            {
                await DisplayAlert("Success", "Semester added", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Semester not added", "OK");
            }
        }
    }
}
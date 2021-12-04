using System;
using TutoringAppProject.Models;
using TutoringAppProject.Models.System;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SemesterRegistration
    {
        private readonly bool _isUpdate;
        public SemesterRegistration()
        {
            InitializeComponent();
            _isUpdate = false;
            SemesterAddUpdateButton .Text = "Add";
        }

        public SemesterRegistration(Semester semester)
        {
            InitializeComponent();
            _isUpdate = true;
            SemesterAddUpdateButton.Text = "Update";
            SemesterCode.Text = semester.SemesterCode;
            SemesterSeason.Text = semester.SemesterSeason;
            SemesterYear.Text = semester.SemesterYear;

        }

        private async void AddSemester(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SemesterSeason.Text))
            {
                await DisplayAlert("Error", "Please enter a semester season", "OK");
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

            var semester = new Semester
            {
                SemesterCode = SemesterCode.Text,
                SemesterSeason = SemesterSeason.Text,
                SemesterYear = SemesterYear.Text
            };

            if (_isUpdate)
            {
                if (await App.SemesterDb.Update(semester))
                {
                    await DisplayAlert("Success", "Semester updated", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Semester was not updated", "OK");
                }
            }
            else
            {
                if (await App.SemesterDb.Create(semester))
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
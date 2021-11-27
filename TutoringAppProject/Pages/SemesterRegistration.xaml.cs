﻿using System;
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
        Semester _semester;
        public SemesterRegistration()
        {
            InitializeComponent();
            SemesterUpdateButton.IsEnabled = false;
        }

        public SemesterRegistration(Semester semester)
        {
            InitializeComponent();
            _semester = semester;
            SemesterAddButton.IsEnabled = false;
            SemesterCode.Text = _semester.semesterCode;
            SemesterSeason.Text = _semester.semesterSeason;
            SemesterYear.Text = _semester.semesterYear;

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

            Semester semester = new Semester
            {
                semesterCode = SemesterCode.Text,
                semesterSeason = SemesterSeason.Text,
                semesterYear = SemesterYear.Text
            };

            if (await App._semesterDB.Create(semester))
            {
                await DisplayAlert("Success", "Semester added", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Semester not added", "OK");
            }
        }

        private async void UpdateSemester(object sender, EventArgs e)
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

            Semester semester = new Semester
            {
                key = _semester.key,
                semesterCode = SemesterCode.Text,
                semesterSeason = SemesterSeason.Text,
                semesterYear = SemesterYear.Text
            };

            if (await App._semesterDB.Update(semester))
            {
                await DisplayAlert("Success", "Semester updated", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Semester was not updated", "OK");
            }
        }
    }
}
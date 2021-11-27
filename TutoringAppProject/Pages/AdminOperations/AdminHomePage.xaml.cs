﻿using System;
using TutoringAppProject.Pages.UserOperations;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminHomePage
    {
        public AdminHomePage()
        {
            InitializeComponent();
        }
        private void teacher_toolbar_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void tutor_toolbar_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void student_toolbar_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private async void logout_toolbar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void user_toolbar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserList());
        }

        private async void Button_OnClicked_Class_List(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CourseList());
        }

        private async void Button_OnClicked_Semester_List(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SemesterList());
        }
    }
}
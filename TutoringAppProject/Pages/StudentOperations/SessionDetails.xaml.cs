using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutoringAppProject.Models.System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutoringAppProject.Pages.StudentOperations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SessionDetails : ContentPage
    {
        public SessionDetails()
        {
            InitializeComponent();
        }

        public SessionDetails(Session session)
        {
            InitializeComponent();
        }
    }
}
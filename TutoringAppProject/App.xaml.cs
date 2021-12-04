using TutoringAppProject.DB;
using TutoringAppProject.Models;
using TutoringAppProject.Models.Users;
using TutoringAppProject.Pages.Authentication;
using Xamarin.Forms;

namespace TutoringAppProject
{
    public partial class App : Application
    {
        public static readonly AdminDb AdminDb = new AdminDb();
        
        public static readonly SemesterDb SemesterDb = new SemesterDb();

        public static readonly CourseDb CourseDb = new CourseDb();

        public static readonly StudentDb StudentDb = new StudentDb();
        
        public static readonly TutorDb TutorDb = new TutorDb();
        
        public static readonly TeacherDb TeacherDb = new TeacherDb();
        
        public static readonly SessionDB SessionDb = new SessionDB();
        
        //global user when logged in
        public static string CurrentKey;
        
        public App()
        {
       
            InitializeComponent();
            MainPage = new NavigationPage(new Login());

        }
    }
}

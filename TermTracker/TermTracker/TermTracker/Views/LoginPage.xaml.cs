using Plugin.LocalNotifications;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TermTracker.Entities;
using Xamarin.Forms;
using TermTracker.HelperClasses;
using TermTracker.Views;
using Xamarin.Forms.Xaml;

namespace TermTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {

            base.OnAppearing();

            CheckData();

            NotificationHelpers.AddAllNotifications();

        }
        private void CheckData()
        {
            int termsCount = 0;
            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                //con.DropTable<Term_DB>();
                //con.DropTable<Users_DB>();
                //con.DropTable<Course_DB>();
                //con.DropTable<Assessment_DB>();


                con.CreateTable<Term_DB>();

                termsCount = con.Table<Term_DB>().ToList().Count();
            }

            if (termsCount < 1)
                PopulateDummyData();

        }

        private void PopulateDummyData()
        {

            populateAssessmentTable(PopulateCourseTable(PoplulateTermsTable()));

            PopulateUsersTable();
        }

        private void populateAssessmentTable(int courseId)
        {
            var o = new Assessment_DB()
            {
                CourseId = courseId,
                AssessmentName = "Test Objective Assesment",
                AssessmentType = "Objective",
                EndDate = DateTime.Now.ToShortDateString(),
                HasNotifications = true
            };

            var p = new Assessment_DB()
            {
                CourseId = courseId,
                AssessmentName = "Test Performance Assesment",
                AssessmentType = "Performance",
                EndDate = DateTime.Now.ToShortDateString(),
                HasNotifications = true

            };

            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<Assessment_DB>();

                con.Insert(o);

                con.Insert(p);
            }
        }

        private int PopulateCourseTable(int termId)
        {
            int courseId = 0;
            var c = new Course_DB()
            {
                CourseTitle = "Test Class",
                StartDate = DateTime.Now.ToShortDateString(),
                EndDate = DateTime.Now.ToShortDateString(),
                TermId = termId,
                HasNotifications = true,
                Status = "Not Started",
                InstructorName = "Randall Fine",
                InstructorPhone = "573-797-0249",
                InstructorEmail = "rfine4@wgu.edu",
                Notes = "Some random notes about some random class"
            };

            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<Course_DB>();

                con.Insert(c);

                var courseRow = con.Table<Course_DB>().FirstOrDefault();

                courseId = courseRow.CourseId;
            }
            return courseId;
        }

        private int PoplulateTermsTable()
        {
            int termId = 0;
            var t = new Term_DB()
            {
                Title = "Test Term",
                StartDate = DateTime.Now.ToShortDateString(),
                EndDate = DateTime.Now.ToShortDateString(),
            };

            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<Term_DB>();

                con.Insert(t);

                var termsRow = con.Table<Term_DB>().FirstOrDefault();

                termId = termsRow.TermId;
            }
            return termId;
        }

        private int PopulateUsersTable()
        {
            int userId = 0;
            var u = new Users_DB()
            {
                Username = "Test User",
                Password = "password"
            };
            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<Users_DB>();

                con.Insert(u);

                var userRow = con.Table<Users_DB>().FirstOrDefault();

                userId = userRow.UserId;
            }
            return userId;
        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            var user = new Users_DB()
            {
                Username = usernameEntry.Text,
                Password = passwordEntry.Text
            };

            var isValid = AreCredentialsCorrect(user);
            if (isValid)
            {
                App.IsUserLoggedIn = true;
                Navigation.InsertPageBefore(new MenuPage(), this);
                await Navigation.PopAsync();
            }
            else
            {

                messageLabel.Text = "Login failed";
                passwordEntry.Text = string.Empty;
            }
        }

        bool AreCredentialsCorrect(Users_DB user)
        {
            bool isCorrect = false;
            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<Users_DB>();

                var userRow = con.Table<Users_DB>().Where(x => x.Username== user.Username && x.Password == user.Password).FirstOrDefault();

                isCorrect = userRow != null;
            }

            return isCorrect;
        }

    }
}
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

namespace TermTracker
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        
        }

        public void AddTermToolBarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddTerm());
        }

        protected override void OnAppearing()
        {

            base.OnAppearing();

            CheckData();

            NotificationHelpers.AddAllNotifications();

            using(SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<Term_DB>();
               

                var terms = con.Table<Term_DB>().ToList();
               

                termListView.ItemsSource = terms;
            }
        }

        private void TermStackLayout_Tapped(object sender, EventArgs e)
        {
            var id = ((TappedEventArgs)e).Parameter;

            Navigation.PushAsync(new TermPage((int)id));
        }

        private void CheckData()
        {
            int termsCount = 0;
            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<Term_DB>();

                termsCount = con.Table<Term_DB>().ToList().Count();
            }

            if (termsCount < 1)
                PopulateDummyData();

        }

        private void PopulateDummyData()
        {

            populateAssessmentTable(PopulateCourseTable(PoplulateTermsTable()));

            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<Term_DB>();
                con.CreateTable<Course_DB>();
                con.CreateTable<Assessment_DB>();
            }

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
                HasNotifications= true,
                Status = "Not Started",
                InstructorName =  "Randall Fine",
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
    }
}

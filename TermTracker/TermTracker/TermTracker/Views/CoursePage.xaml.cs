using Plugin.LocalNotifications;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TermTracker.Entities;
using TermTracker.HelperClasses;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TermTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoursePage : ContentPage
    {
        private readonly int CourseId;
        public CoursePage(int id)
        {
            CourseId = id;
            
            InitializeComponent();

           
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<Course_DB>();
                con.CreateTable<Assessment_DB>();
                var courseRow = con.Table<Course_DB>().Where(c => c.CourseId.Equals(CourseId)).FirstOrDefault();

                courseTitleLabel.Text = courseRow.CourseTitle;
                StartDateLabel.Text = courseRow.StartDate;
                EndDateLabel.Text = courseRow.EndDate;
                StausLabel.Text = courseRow.Status;
                instrunctorNameLabel.Text = courseRow.InstructorName;
                instrunctorEmailLabel.Text = courseRow.InstructorEmail;
                instrunctorPhoneLabel.Text = courseRow.InstructorPhone;
                notesLabel.Text = courseRow.Notes;
            

                var assesments = con.Table<Assessment_DB>().Where(a => a.CourseId.Equals(CourseId)).ToList();

                if (assesments.Count == 2)
                    BtnAddAssessment.IsEnabled = false;

                AssesmentListView.ItemsSource = assesments;
            }

            CheckAssessements();
            CheckNotes();
        }

        private void BtnAddAssessment_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddAssesment(CourseId));
        }

        private void BtnEditCourse_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditCourse(CourseId));
        }

        private void BtnDelete_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                var row = con.Table<Course_DB>().Where(c => c.CourseId.Equals(CourseId)).FirstOrDefault();
                {
                    SqlLiteHelpers.DeleteCourse(row.CourseId);
                }
            }
            Navigation.PopAsync();
        }

        private void BtnSendNotes_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EmailPage(CourseId));
        }

        private void assesmentStackLayout_Tapped(object sender, EventArgs e)
        {
            var id = ((TappedEventArgs)e).Parameter;

            Navigation.PushAsync(new AssessmentPage((int)id));
        }

        private void CheckAssessements()
        {
            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                var assesments = con.Table<Assessment_DB>().Where(a => a.CourseId.Equals(CourseId)).ToList();

                if (assesments.Count == 2)
                    BtnAddAssessment.IsEnabled = false;
            }
        }

        private void CheckNotes()
        {
            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                var courseRow = con.Table<Course_DB>().Where(c => c.CourseId.Equals(CourseId)).FirstOrDefault();

                if (courseRow.Notes == null || courseRow.Notes.Trim().Equals(string.Empty))
                {
                    BtnSendNotes.IsEnabled = false;
                }  
            }
        }

       
    }
}
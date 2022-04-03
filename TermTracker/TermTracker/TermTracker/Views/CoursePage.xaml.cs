using Plugin.LocalNotifications;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TermTracker.Entities;
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

                AssesmentListView.ItemsSource = assesments;
            }
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
                    if (row != null)
                    {
                        var deleted = con.Delete<Course_DB>(CourseId);
                    }
                }
            }
            Navigation.PopAsync();
        }

        private void assesmentStackLayout_Tapped(object sender, EventArgs e)
        {

        }
    }
}
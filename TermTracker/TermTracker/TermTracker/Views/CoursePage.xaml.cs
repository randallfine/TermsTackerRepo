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
                var courseRow = con.Table<Course_DB>().Where(c => c.CourseId.Equals(CourseId)).FirstOrDefault();

                courseTitleLabel.Text = courseRow.CourseTitle;
                StartDateLabel.Text = courseRow.StartDate;
                EndDateLabel.Text = courseRow.EndDate;
                StausLabel.Text = courseRow.Status;
                instrunctorNameLabel.Text = courseRow.InstructorName;
                instrunctorEmailLabel.Text = courseRow.InstructorEmail;
                instrunctorPhoneLabel.Text = courseRow.InstructorPhone;
                notesLabel.Text = courseRow.Notes;
            }
        }

        private void BtnAddAssessment_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnEditTerm_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnDelete_Clicked(object sender, EventArgs e)
        {

        }
    }
}
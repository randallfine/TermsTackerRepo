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
    public partial class EditCourse : ContentPage
    {
        private readonly int CourseId;
        private int TermId;
        public EditCourse(int id)
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

                if (courseRow != null)
                {
                    TermId = courseRow.TermId;
                    courseTitleEntry.Text = courseRow.CourseTitle;
                    StartDatePicker.Date = DateTime.Parse(courseRow.StartDate);
                    EndDatePicker.Date = DateTime.Parse(courseRow.EndDate);
                    statusPicker.SelectedItem = courseRow.Status;
                    instrunctorNameEntry.Text = courseRow.InstructorName;
                    instrunctorEmailEntry.Text = courseRow.InstructorEmail;
                    instrunctorPhoneEntry.Text = courseRow.InstructorPhone;
                    notesEditor.Text = courseRow.Notes;
                }
            }
        }

        private void BtnSave_Clicked(object sender, EventArgs e)
        {
            Course_DB c = new Course_DB();
            {
                c.CourseId = CourseId;
                c.TermId = TermId;
                c.CourseTitle = courseTitleEntry.Text;
                c.StartDate = StartDatePicker.Date.ToShortDateString();
                c.EndDate = EndDatePicker.Date.ToShortDateString();
                c.Status = statusPicker.SelectedItem.ToString();
                c.InstructorName = instrunctorNameEntry.Text;
                c.InstructorEmail = instrunctorEmailEntry.Text;
                c.InstructorPhone = instrunctorPhoneEntry.Text;
                c.Notes = notesEditor.Text;
            };
            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<Course_DB>();
                int rowsAdded = con.Update(c);
            }
            Navigation.PopAsync();
        }
    }
}
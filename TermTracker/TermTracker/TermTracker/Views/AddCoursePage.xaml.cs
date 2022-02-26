using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TermTracker.Entities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TermTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCoursePage : ContentPage
    {
        private readonly int TermId;
        public AddCoursePage(int id)
        {
            TermId = id;
            InitializeComponent();
        }

        private void BtnSave_Clicked(object sender, EventArgs e)
        {
            Course_DB c = new Course_DB();
            {
                c.CourseTitle = courseTitleEntry.Text;
                c.StartDate = StartDatePicker.Date.ToShortDateString();
                c.EndDate = EndDatePicker.Date.ToShortDateString();
                c.Status = "Not Started";
                c.TermId = TermId;
                c.InstructorName = instrunctorNameEntry.Text;
                c.InstructorEmail = instrunctorEmailEntry.Text;
                c.InstructorPhone = instrunctorPhoneEntry.Text;
                c.Notes = notesEditor.Text;
            };
            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<Course_DB>();
                int rowsAdded = con.Insert(c);
            }
            Navigation.PopAsync();
        }
    }
}
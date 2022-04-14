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
            if (HasAllValues())
            {
                Course_DB c = new Course_DB();
                {
                    c.CourseTitle = courseTitleEntry.Text;
                    c.StartDate = StartDatePicker.Date.ToShortDateString();
                    c.EndDate = EndDatePicker.Date.ToShortDateString();
                    c.HasNotifications = notifictionSwitch.IsToggled;
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
                NotificationHelpers.AddCourseNotifications();

                Navigation.PopAsync();
            }
        }

        private bool HasAllValues()
        {
            return (courseTitleEntry.Text != null && instrunctorNameEntry.Text != null && instrunctorEmailEntry.Text != null && instrunctorPhoneEntry.Text != null && notesEditor.Text != null);
        }

        private void StartDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            var startDatePicker = sender as DatePicker;

            if(startDatePicker.Date > EndDatePicker.Date)
            {
                EndDatePicker.Date = startDatePicker.Date;
            }

        }

        private void EndDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            var endDatePicker = sender as DatePicker;

            if (endDatePicker.Date < StartDatePicker.Date)
            {
                endDatePicker.Date = StartDatePicker.Date;
            }
        }
    }
}
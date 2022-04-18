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
                    notifictionSwitch.IsToggled = courseRow.HasNotifications;
                }
            }
        }

        private void BtnSave_Clicked(object sender, EventArgs e)
        {
            if(HasAllValues())
            {
                Course_DB c = new Course_DB();
                {
                    c.CourseId = CourseId;
                    c.TermId = TermId;
                    c.CourseTitle = courseTitleEntry.Text;
                    c.StartDate = StartDatePicker.Date.ToShortDateString();
                    c.EndDate = EndDatePicker.Date.ToShortDateString();
                    c.HasNotifications = notifictionSwitch.IsToggled;
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

        private void notifictionSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            if(!notifictionSwitch.IsToggled)
            {
                NotificationHelpers.CancelCourseNotification(CourseId);
            }
            else
            {
                NotificationHelpers.AddCourseNotification(CourseId);
            }
        }

        private bool HasAllValues()
        {
            return (courseTitleEntry.Text != null && instrunctorNameEntry.Text != null && instrunctorEmailEntry.Text != null && instrunctorPhoneEntry.Text != null && notesEditor.Text != null);
        }

        private void StartDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            var startDatePicker = sender as DatePicker;

            if (startDatePicker.Date > EndDatePicker.Date)
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

        private void StartDatePicker_DateSelected_1(object sender, DateChangedEventArgs e)
        {

        }
    }
}
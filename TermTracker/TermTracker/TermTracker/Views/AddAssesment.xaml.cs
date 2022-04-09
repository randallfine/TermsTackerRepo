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
    public partial class AddAssesment : ContentPage
    {
        private readonly int CourseId;
        public AddAssesment(int courseId)
        {
            CourseId = courseId;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<Assessment_DB>();
                var assessmentRow = con.Table<Assessment_DB>().Where(a => a.CourseId.Equals(CourseId)).FirstOrDefault();

                string assessmentType = assessmentRow.AssessmentType;

                if (assessmentType.Equals("Objective"))
                {
                    rbObjective.IsEnabled = false;
                }
                  
                if(assessmentType.Equals("Performance"))
                {
                    rbPerformance.IsEnabled = false;
                }
            }

        }

        private void BtnSave_Clicked(object sender, EventArgs e)
        {
            string assessmentType = string.Empty;
            if(rbObjective.IsChecked)
            {
                assessmentType = "Objective";
            }
            if(rbPerformance.IsChecked)
            {
                assessmentType = "Performance";
            }
            Assessment_DB a = new Assessment_DB();
            {
                a.AssessmentName = titleEntry.Text;
                a.EndDate = EndDatePicker.Date.ToShortDateString();
                a.AssessmentType = assessmentType;
                a.CourseId = CourseId;
                a.HasNotifications = notifictionSwitch.IsToggled;

            };
            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<Assessment_DB>();
                int rowsAdded = con.Insert(a);
            }
            NotificationHelpers.AddAssessmentNotifications();
            Navigation.PopAsync();

        }
    }
}
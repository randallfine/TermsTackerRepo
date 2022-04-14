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
    public partial class EditAssessment : ContentPage
    {
        private readonly int AssessmentId;
        private int CourseId;
        private string assessmentType;
        private bool hasNotification;
        private int assessmentCount;
        public EditAssessment(int id)
        {
            AssessmentId = id;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<Assessment_DB>();
                var assesmentRow = con.Table<Assessment_DB>().Where(a => a.AssessmentId.Equals(AssessmentId)).FirstOrDefault();

                if (assesmentRow != null)
                {
                    titleEntry.Text = assesmentRow.AssessmentName;
                    EndDatePicker.Date = DateTime.Parse(assesmentRow.EndDate);
                    assessmentType = assesmentRow.AssessmentType;
                    CourseId = assesmentRow.CourseId;
                    hasNotification = notifictionSwitch.IsToggled;

                    assessmentCount = con.Table<Assessment_DB>().Where(a => a.CourseId.Equals(CourseId)).Count();

                    if (assessmentCount == 2)
                    {
                        rbObjective.IsEnabled = false;
                        rbPerformance.IsEnabled = false;
                    }

                    if (assessmentType.Equals("Objective"))
                    {
                        rbObjective.IsChecked = true;
                    }
                    else
                    {
                        rbPerformance.IsChecked = true;
                    }

                    notifictionSwitch.IsToggled = hasNotification;

                }
            }

        }

        private void BtnSave_Clicked(object sender, EventArgs e)
        {
            string assessmentType = string.Empty;
            if (rbObjective.IsChecked)
            {
                assessmentType = "Objective";
            }
            if (rbPerformance.IsChecked)
            {
                assessmentType = "Performance";
            }
            Assessment_DB a = new Assessment_DB();
            {
                if(titleEntry.Text != null)
                {
                    a.AssessmentId = AssessmentId;
                    a.AssessmentName = titleEntry.Text;
                    a.EndDate = EndDatePicker.Date.ToShortDateString();
                    a.AssessmentType = assessmentType;
                    a.CourseId = CourseId;
                    a.HasNotifications = notifictionSwitch.IsToggled;

                };
                using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
                {
                    con.CreateTable<Assessment_DB>();
                    int rowsAdded = con.Update(a);
                }
                NotificationHelpers.AddAssessmentNotifications();
                Navigation.PopAsync();
            }
                
        }
    }
}
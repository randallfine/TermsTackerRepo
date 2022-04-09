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
    public partial class AssessmentPage : ContentPage
    {
        private readonly int AssesmentId;
        public AssessmentPage(int Id)
        {
            AssesmentId = Id;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<Assessment_DB>();
                var AssessmentRow = con.Table<Assessment_DB>().Where(a => a.AssessmentId.Equals(AssesmentId)).FirstOrDefault();

                assessmentLabel.Text = AssessmentRow.AssessmentName;
                assessmentTypeLabel.Text = AssessmentRow.AssessmentType;
                EndDateLabel.Text = AssessmentRow.EndDate;
            }
        }

        private void BtnDelete_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                var row = con.Table<Assessment_DB>().Where(a => a.AssessmentId.Equals(AssesmentId)).FirstOrDefault();
                {
                    if (row != null)
                    {
                        SqlLiteHelpers.DeleteAssessment(row.AssessmentId);
                    }
                }
            }
            Navigation.PopAsync();
        }
        

        private void BtnEditAssessment_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditAssessment(AssesmentId));
        }

    }
}
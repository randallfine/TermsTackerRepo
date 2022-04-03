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
    public partial class AddAssesment : ContentPage
    {
        private readonly int CourseId;
        public AddAssesment(int courseId)
        {
            CourseId = courseId;
            InitializeComponent();
        }

        private void BtnSave_Clicked(object sender, EventArgs e)
        {
            Assessment_DB a = new Assessment_DB();
            {
                a.AssessmentName = titleEntry.Text;
                a.EndDate = EndDatePicker.Date.ToShortDateString();
                a.AssessmentType = statusPicker.SelectedItem.ToString();
                a.CourseId = CourseId;
            };
            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<Assessment_DB>();
                int rowsAdded = con.Insert(a);
            }
            Navigation.PopAsync();

        }
    }
}
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
    public partial class ReportsPage : ContentPage
    {
        public ReportsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {

            base.OnAppearing();

            List<Term_DB> terms = new List<Term_DB>();
            List<TermGridData> termGridDatas= new List<TermGridData>();
            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<Term_DB>();


                terms = con.Table<Term_DB>().ToList();
            }

            foreach (Term_DB term in terms)
            {
                termGridDatas.Add(new TermGridData(term.Title, term.StartDate, term.EndDate, GetCourseCount(term.TermId)));
            }

            TermListView.ItemsSource = termGridDatas;


        }

        private int GetCourseCount(int termId)
        {
            int count = 0;
            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<Course_DB>();
                count = con.Table<Course_DB>().Where(c => c.TermId == termId).ToList().Count();
            }

            return count;
        }
    }

    public class TermGridData
    {
        public string TermName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string CourseCount { get; set; }

        public TermGridData(string termName, string startDate, string endDate, int courseCount)
        {
            TermName = termName;
            StartDate = startDate;
            EndDate = endDate;
            CourseCount = courseCount.ToString();
        }
    }
}
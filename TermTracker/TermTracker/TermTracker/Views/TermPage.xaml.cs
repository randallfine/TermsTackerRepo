using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TermTracker.Entities;
using TermTracker.HelperClasses;
using TermTracker.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TermTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermPage : ContentPage
    {
        private readonly int TermId;
        public TermPage(int id)
        {
            TermId = id;
            InitializeComponent();
            CheckMaxCourses();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<Term_DB>();
                con.CreateTable<Course_DB>();
                var termRow = con.Table<Term_DB>().Where(t => t.TermId.Equals(TermId)).FirstOrDefault();
               
                    if (termRow != null)
                    {
                        termTitle.Text = termRow.Title;
                        TermDetailStart.Text = termRow.StartDate;
                        TermDetailEnd.Text = termRow.EndDate;


                        var courses = con.Table<Course_DB>().Where(c => c.TermId.Equals(TermId)).ToList();

                        CourseListView.ItemsSource = courses;
                    } 
            }
        }

        private void courseStackLayout_Tapped(object sender, EventArgs e)
        {
            var id = ((TappedEventArgs)e).Parameter;

            Navigation.PushAsync(new CoursePage((int)id));
        }

        private void BtnDelete_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                var row = con.Table<Term_DB>().Where(t => t.TermId.Equals(TermId)).FirstOrDefault();
                {
                    if (row != null)
                    {
                        SqlLiteHelpers.DeleteTerm(row.TermId);
                    }
                }
            }
            Navigation.PopAsync();
        }

        private void BtnAddCourse_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddCoursePage(TermId));
        }
        private void BtnEditTerm_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditTermPage(TermId));
        }

        private void CheckMaxCourses()
        {
            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<Course_DB>();
                var courses = con.Table<Course_DB>().Where(c => c.TermId.Equals(TermId)).ToList();
                
                if(courses.Count >= 6)
                {
                    BtnAddCourse.IsEnabled = false;
                }
            }
        }

        private void CourseStackLayout_Tapped(object sender, EventArgs e)
        {

        }


    }
}
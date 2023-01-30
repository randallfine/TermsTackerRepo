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
    public partial class CourseSearchPage : ContentPage
    {
        public CourseSearchPage()
        {
            InitializeComponent();

        }

        protected override void OnAppearing()
        {

            base.OnAppearing();


            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<Course_DB>();
                var courses = con.Table<Course_DB>().ToList();
                courseListView.ItemsSource = courses;
            }

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var course = courseTitleEntry.Text;
            int courseCount = 0;
            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<Course_DB>();

                var courses = con.Table<Course_DB>().ToList().Where(c => c.CourseTitle.Equals(course, StringComparison.OrdinalIgnoreCase));
                courseCount = courses.Count();
                courseListView.ItemsSource = courses;
            }
            if(courseCount == 0)
            {
                messageLabel.Text = "Course Not Found";
            }
        }
    }
}
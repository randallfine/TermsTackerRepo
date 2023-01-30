using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TermTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
        }


        private void BtnViewTerms_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TermsListPage());
        }

        private void BtnSearchCourses_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CourseSearchPage());
        }

        private void BtnViewReports_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ReportsPage());
        }
    }
}
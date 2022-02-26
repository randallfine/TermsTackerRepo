using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TermTracker.Entities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TermTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTerm : ContentPage
    {
        public AddTerm()
        {
            InitializeComponent();
        }

        private void BtnSave_Clicked(object sender, EventArgs e)
        {
            Term_DB t = new Term_DB();
            {
                t.Title = termNameEntry.Text;
                t.StartDate = StartDatePicker.Date.ToShortDateString();
                t.EndDate = EndDatePicker.Date.ToShortDateString();
            };
            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<Term_DB>();
                int rowsAdded = con.Insert(t);
            }
            Navigation.PopAsync();
        }
    }
}
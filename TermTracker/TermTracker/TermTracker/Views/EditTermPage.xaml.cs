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
    public partial class EditTermPage : ContentPage
    {
        private int TermId;
        public EditTermPage(int id)
        {
            TermId = id;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<Term_DB>();
                var termRow = con.Table<Term_DB>().Where(t => t.TermId.Equals(TermId)).FirstOrDefault();

                if (termRow != null)
                {
                    termNameEntry.Text = termRow.Title;
                    StartDatePicker.Date = DateTime.Parse(termRow.StartDate);
                    EndDatePicker.Date = DateTime.Parse(termRow.EndDate);
                }
            }
        }

        private void BtnSave_Clicked(object sender, EventArgs e)
        {
            Term_DB t = new Term_DB();
            {
                t.TermId = TermId;
                t.Title = termNameEntry.Text;
                t.StartDate = StartDatePicker.Date.ToShortDateString();
                t.EndDate = EndDatePicker.Date.ToShortDateString();
            };
            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<Term_DB>();
                int rowsAdded = con.Update(t);
            }
            Navigation.PopAsync();
        }
    }
    
}
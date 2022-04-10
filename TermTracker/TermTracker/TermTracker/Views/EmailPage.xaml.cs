using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TermTracker.Entities;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TermTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmailPage : ContentPage
    {
        private readonly int CourseId;
        public EmailPage(int courseId)
        {
            CourseId = courseId;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<Course_DB>();
                var courseRow = con.Table<Course_DB>().Where(c => c.CourseId.Equals(CourseId)).FirstOrDefault();

                subjectEntry.Text = String.Concat(courseRow.CourseTitle, " Notes");

                bodyEditor.Text = courseRow.Notes;
            }

        }

        private async void BtnSend_Clicked(object sender, EventArgs e)
        {
            var recipients = emailAddressEntry.Text.Split(',').ToList();
            var subject = subjectEntry.Text;
            var body = bodyEditor.Text;

            await SendEmail(subject, body, recipients);

            await Navigation.PopAsync();
        }

        private void BtnCancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        public async Task SendEmail(string subject, string body, List<string> recipients)
        {
            
                var message = new EmailMessage
                {
                    Subject = subject,
                    Body = body,
                    To = recipients,
                   
                };
                await Email.ComposeAsync(message);
           
        }
    }
}
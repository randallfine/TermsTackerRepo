using Plugin.LocalNotifications;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using TermTracker.Entities;

namespace TermTracker.HelperClasses
{
    public static class NotificationHelpers
    {
        public static void AddAllNotifications()
        {
            AddCourseNotifications();
            AddAssessmentNotifications();
        }

        public static void AddCourseNotifications()
        {
            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<Course_DB>();
                var courses = con.Table<Course_DB>().ToList();


                foreach (var course in courses)
                {
                    if (course.HasNotifications)
                    {
                        var title = course.CourseTitle;
                        var startDate = DateTime.Parse(course.StartDate);
                        var endDate = DateTime.Parse(course.EndDate);
                        var startMessage = $"This course will begin on {startDate}";
                        var endMessage = $"This course will end on {endDate}";
                        var startId = int.Parse(string.Concat(course.CourseId, "01"));
                        var endId = int.Parse(string.Concat(course.CourseId, "02"));

                        CrossLocalNotifications.Current.Show(title, startMessage, startId, startDate);
                        CrossLocalNotifications.Current.Show(title, endMessage, endId, endDate);
                    }

                }

               
            }
        }

        public static void AddAssessmentNotifications()
        {
            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<Assessment_DB>();
                var assessments = con.Table<Assessment_DB>().ToList();

                foreach (var assessment in assessments)
                {
                    if (assessment.HasNotifications)
                    {
                        var title = assessment.AssessmentName;
                        var type = assessment.AssessmentType;
                        var dueDate = DateTime.Parse(assessment.EndDate);
                        var endMessage = $"This {type} will end on {dueDate}";
                        var assessmentId = int.Parse(string.Concat(assessment.AssessmentId, "03")); 

                        CrossLocalNotifications.Current.Show(title, endMessage, assessmentId, dueDate);
                    }

                }
            }
        }

        public static void AddCourseNotification(int courseId)
        {
            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<Course_DB>();
                var courseRow = con.Table<Course_DB>().Where(c => c.CourseId.Equals(courseId)).FirstOrDefault();

                var title = courseRow.CourseTitle;
                var startDate = DateTime.Parse(courseRow.StartDate);
                var endDate = DateTime.Parse(courseRow.EndDate);
                var startMessage = $"This course will begin on {startDate}";
                var endMessage = $"This course will end on {endDate}";
                var startId = int.Parse(string.Concat(courseId, "01"));
                var endId = int.Parse(string.Concat(courseId, "02"));

                CrossLocalNotifications.Current.Show(title, startMessage, startId, startDate);
                CrossLocalNotifications.Current.Show(title, endMessage, endId, endDate);


            }
               



        }

        public static void CancelCourseNotification(int courseId)
        {
            var startId = int.Parse(string.Concat(courseId, "01"));
            var endId = int.Parse(string.Concat(courseId, "02"));

            CrossLocalNotifications.Current.Cancel(startId);
            CrossLocalNotifications.Current.Cancel(endId);
        }

        public static void AddAssessmentNotification(int assessmentId)
        {
            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<Assessment_DB>();
                var assessmentRow = con.Table<Assessment_DB>().Where(a => a.AssessmentId == assessmentId).FirstOrDefault();

                var title = assessmentRow.AssessmentName;
                var type = assessmentRow.AssessmentType;
                var dueDate = DateTime.Parse(assessmentRow.EndDate);
                var endMessage = $"This {type} will end on {dueDate}";
                var endId = int.Parse(string.Concat(assessmentId, "03"));

                CrossLocalNotifications.Current.Show(title, endMessage, endId, dueDate);

            }
                
        }

        public static void CancelAssessmentNotification(int assessmentId)
        {
            var endId = int.Parse(string.Concat(assessmentId, "03"));

            CrossLocalNotifications.Current.Cancel(endId);
        }
    }
}

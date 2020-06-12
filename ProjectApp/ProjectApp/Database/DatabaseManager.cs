using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace ProjectApp.Database
{
    static class DatabaseManager
    {
        private static SQLiteConnection db = DependencyService.Get<IDBInterface>().CreateConnection();

        /// <summary>
        /// Stores the current date + time into the database, and also returns it if you want to do anything with it.
        /// </summary>
        /// <returns>A String with the current date and time, formatted as dd/mm/yyyy h:mm AM/PM</returns>
        public static string StoreCurrentDate()
        {
            string date = DateTime.Now.ToString(@"dd\/MM\/yyyy h\:mm tt");
            db.Insert(new Date() { date = date });

            return date;
        }

        /// <summary>
        /// Gets the history dates from the database.
        /// </summary>
        /// <returns>A List of Strings filled with dates.</returns>
        public static List<String> GetHistoryDates()
        {
            List<Date> dates = db.Table<Date>().ToList();

            // Converstion from List<Date> to List<string>
            return dates.ConvertAll(dateObject => dateObject.date);
        }

        /// <summary>
        /// Deletes all the dates from the database. WARNING: Actually deletes all dates.
        /// </summary>
        public static void DeleteDates()
        {
            db.DeleteAll<Date>();
        }

    }
}

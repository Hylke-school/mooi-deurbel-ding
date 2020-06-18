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
        /// <returns>A String with the current date and time</returns>
        public static string StoreCurrentDate()
        {
            string date = DateTime.Now.ToString();
            db.Insert(new Date() { date = date });

            return date;
        }

        /// <summary>
        /// Gets the history dates from the database.
        /// </summary>
        /// <returns>A List of Strings filled with dates.</returns>
        public static List<String> GetHistoryDates()
        {
            List<Date> dates = new List<Date>(db.Table<Date>().ToList());
            List<string> dateStrings = dates.ConvertAll(dateObject => dateObject.date);
            List<string> dateStringsOrdered = dateStrings.OrderByDescending(x => DateTime.Parse(x)).ToList();

            // Conversion from List<Date> to List<string>
            return dateStringsOrdered;
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

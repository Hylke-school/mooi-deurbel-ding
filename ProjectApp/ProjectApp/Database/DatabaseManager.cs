using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace ProjectApp.Database
{
    public static class DatabaseManager
    {
        private static SQLiteConnection db = DependencyService.Get<IDBInterface>().CreateConnection();

        /// <summary>
        /// Stores the current date + time into the database, and also returns it if you want to do anything with it.
        /// </summary>
        /// <returns>A String with the current date and time</returns>
        public static string StoreCurrentDate()
        {
            // SQLite doesn't support dates, so instead we store strings
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

            // Conversion from List<Date> to List<string>
            List<string> dateStrings = dates.ConvertAll(dateObject => dateObject.date);

            // Order dates chronologically 
            List<string> dateStringsOrdered = dateStrings.OrderByDescending(x => DateTime.Parse(x)).ToList();
            
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

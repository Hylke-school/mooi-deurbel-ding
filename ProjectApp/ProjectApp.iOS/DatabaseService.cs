using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Foundation;
using ProjectApp.Database;
using ProjectApp.iOS;
using SQLite;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(DatabaseService))]
namespace ProjectApp.iOS
{

    public class DatabaseService : IDBInterface
    {
        public SQLiteConnection CreateConnection()
        {
            var sqliteFilename = "HistoryDatabase.db";

            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            string path = Path.Combine(libFolder, sqliteFilename);

            if (!File.Exists(path))
            {
                var existingDb = NSBundle.MainBundle.PathForResource("MovieDatabase", "db");
                File.Copy(existingDb, path);
            }

            var connection = new SQLiteConnection(path, false);
            return connection;
        }
    }
}
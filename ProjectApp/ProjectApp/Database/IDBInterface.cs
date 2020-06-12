using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectApp.Database
{
    public interface IDBInterface
    {
        SQLiteConnection CreateConnection();
    }
}

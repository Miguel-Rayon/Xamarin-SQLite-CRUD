using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Registros.Droid;
using Xamarin.Forms;
using SQLite;
using System.IO;

[assembly:Dependency(typeof(SQLiteDB))]

namespace Registros.Droid
{
    class SQLiteDB: ISQLiteDB
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            //Se crea la base de datos
            var path = Path.Combine(documentsPath, "MySQLite.db3");
            return new SQLiteAsyncConnection(path);
        }
    }
}
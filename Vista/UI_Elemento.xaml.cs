using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SQLite;
using Registros.Tabla;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Registros.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UI_Elemento : ContentPage
    {
        public int IdSeleccionado;
        public string nom, us, con;
        private SQLiteAsyncConnection _conn;
        IEnumerable<T_Registro> ResultadoDelete;
        IEnumerable<T_Registro> ResultadoUpdate;
        public UI_Elemento(int id, string nombre, string usuario, string contra)
        {
            InitializeComponent();
            _conn = DependencyService.Get<ISQLiteDB>().GetConnection();
            IdSeleccionado = id;
            nom = nombre;
            us = usuario;
            con = contra;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            mensaje.Text = "Se Afectara al ID [" + IdSeleccionado + "]";
            Nombre.Text = nom;
            Usuario.Text = us;
            Contrasenia.Text = con;
        }
        public static IEnumerable<T_Registro> Read(SQLiteConnection db, string nombre, string usuario, string contrasenia, int id)
        {
            return db.Query<T_Registro>("SELECT  Nombre, Usuario, Contrasenia  FROM T_Registro where Id = ?", nombre, usuario, contrasenia, id);
        }
        public static IEnumerable<T_Registro> Delete(SQLiteConnection db, int id)
        {
            return db.Query<T_Registro>("DELETE FROM T_Registro where Id = ?", id);
        }
        public static IEnumerable<T_Registro> Update(SQLiteConnection db, string nombre, string usuario, string contrasenia, int id)
        {
            return db.Query<T_Registro>("UPDATE T_Registro SET Nombre = ?, Usuario = ?, Contrasenia = ? where Id = ? ", nombre, usuario, contrasenia, id);
        }
        public void LimpiarForm()
        {
            Nombre.Text = "";
            Usuario.Text = "";
            Contrasenia.Text = "";
        }
        private void btn_actualizar(object sender, EventArgs e)
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MySQLite.db3");
            var db = new SQLiteConnection(databasePath);
            ResultadoUpdate = Update(db, Nombre.Text, Usuario.Text, Contrasenia.Text, IdSeleccionado);
            LimpiarForm();
            DisplayAlert("Mensaje", "Registro Actualizado", "OK");
        }
        private void btn_eliminar(object sender, EventArgs e)
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MySQLite.db3");
            var db = new SQLiteConnection(databasePath);
            ResultadoDelete = Delete(db, IdSeleccionado);
            DisplayAlert("Mensaje", "Registro Eliminado", "OK");
        }
    }
}
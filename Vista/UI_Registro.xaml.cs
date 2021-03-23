using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Registros.Tabla;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Registros.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UI_Registro : ContentPage
    {
        private SQLiteAsyncConnection _conn;
        public UI_Registro()
        {
            InitializeComponent();
            _conn = DependencyService.Get<ISQLiteDB>().GetConnection();
        }
        protected void btn_agregar(object sender, EventArgs e)
        {
            var DatosRegistros = new T_Registro { Nombre = Nombre.Text, Usuario = Usuario.Text, Contrasenia = Contrasenia.Text };
            _conn.InsertAsync(DatosRegistros);
            LimpiarFormulario();
            DisplayAlert("Mensaje", "Registro Exitoso", "Aceptar");
        }
        void LimpiarFormulario()
        {
            Nombre.Text = "";
            Usuario.Text = "";
            Contrasenia.Text = "";
        }
    }
}
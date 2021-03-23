using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Registros.Tabla;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace Registros.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UI_ConsultaRegistro : ContentPage
    {
        private SQLiteAsyncConnection _conn;
        private ObservableCollection<T_Registro> _TablaRegistro;
        public UI_ConsultaRegistro()
        {
            InitializeComponent();
            _conn = DependencyService.Get<ISQLiteDB>().GetConnection();
        }
        protected async override void OnAppearing()
        {
            var ResulRegistros = await _conn.Table<T_Registro>().ToListAsync();
            _TablaRegistro = new ObservableCollection<T_Registro>(ResulRegistros);
            ListaUsuarios.ItemsSource = _TablaRegistro;
            base.OnAppearing();
        }
        void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            var Obj = (T_Registro)e.SelectedItem;
            var item = Obj.Id.ToString();
            var item2 = Obj.Nombre.ToString();
            var item3 = Obj.Usuario.ToString();
            var item4 = Obj.Contrasenia.ToString();
            int ID = Convert.ToInt32(item);
            var NOM = item2;
            var US = item3;
            var CON = item4;
            try
            {
                Navigation.PushAsync(new UI_Elemento(ID, NOM, US, CON));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
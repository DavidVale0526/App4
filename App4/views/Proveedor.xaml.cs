// Proveedor.xaml.cs
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace App4.views
{
    public partial class Proveedor : ContentPage
    {
        private string firebaseURL = "https://crud-bootstrap-ab39a-default-rtdb.firebaseio.com/";
        private FirebaseClient firebaseClient;

        public Proveedor()
        {
            InitializeComponent();
            firebaseClient = new FirebaseClient(firebaseURL);
        }

        private async void OnAgregarProveedorClicked(object sender, EventArgs e)
        {
            string nombre = nombreEntry.Text;
            string empresa = empresaEntry.Text;
            string contacto = contactoEntry.Text;

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(empresa) || string.IsNullOrWhiteSpace(contacto))
            {
                await DisplayAlert("Error", "Todos los campos son obligatorios", "OK");
                return;
            }

            var proveedor = new ProveedorModel
            {
                Id = Guid.NewGuid().ToString(),
                Nombre = nombre,
                Empresa = empresa,
                Contacto = contacto,
                FechaRegistro = DateTime.Now.ToString("dd/MM/yyyy HH:mm")
            };

            await firebaseClient
                .Child("proveedores")
                .PostAsync(JsonConvert.SerializeObject(proveedor));

            await DisplayAlert("Mensaje", "Proveedor agregado correctamente", "OK");
            await Navigation.PopAsync();
        }
    }

    public class ProveedorModel
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Empresa { get; set; }
        public string Contacto { get; set; }
        public string FechaRegistro { get; set; }
    }
}
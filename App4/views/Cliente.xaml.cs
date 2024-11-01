using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace App4.views
{
    public partial class Cliente : ContentPage
    {
        private string firebaseURL = "https://crud-bootstrap-ab39a-default-rtdb.firebaseio.com/";
        private FirebaseClient firebaseClient;

        public Cliente()
        {
            InitializeComponent();
            firebaseClient = new FirebaseClient(firebaseURL);
        }

        private async void OnAgregarClienteClicked(object sender, EventArgs e)
        {
            string nombre = nombreEntry.Text;
            string email = emailEntry.Text;
            string telefono = telefonoEntry.Text;

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(telefono))
            {
                await DisplayAlert("Error", "Todos los campos son obligatorios", "OK");
                return;
            }

            var cliente = new ClienteModel
            {
                Id = Guid.NewGuid().ToString(),
                Nombre = nombre,
                Email = email,
                Telefono = telefono,
                FechaRegistro = DateTime.Now.ToString("dd/MM/yyyy HH:mm")
            };

            await firebaseClient
                .Child("clientes")
                .PostAsync(JsonConvert.SerializeObject(cliente));

            await DisplayAlert("Mensaje", "Cliente agregado correctamente", "OK");
            await Navigation.PopAsync();
        }
    }

    public class ClienteModel
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string FechaRegistro { get; set; }
    }
}
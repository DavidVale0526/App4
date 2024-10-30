using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using App4.Models;
using System;
using System.Threading.Tasks;
using App4.views;

namespace App4.views
{
    public partial class EditarVenta : ContentPage
    {
        private string firebaseURL = "https://crud-bootstrap-ab39a-default-rtdb.firebaseio.com/";
        private string saleId;
        private FirebaseClient firebaseClient;

        public EditarVenta(Sales sale)
        {
            InitializeComponent();
            firebaseClient = new FirebaseClient(firebaseURL);
            saleId = sale.Id; // Asumiendo que tienes un Id en tu modelo Sales
            nombreProductoEntry.Text = sale.NombreProducto;
            cantidadEntry.Text = sale.Cantidad.ToString();
            precioEntry.Text = sale.Precio.ToString();
        }

        private async void BtnActualizar_Clicked(object sender, EventArgs e)
        {
            // Obtener los datos del formulario
            string nombreProducto = nombreProductoEntry.Text;
            string cantidadText = cantidadEntry.Text;
            string precioText = precioEntry.Text;

            // Validar que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(nombreProducto) || string.IsNullOrWhiteSpace(cantidadText) || string.IsNullOrWhiteSpace(precioText))
            {
                await DisplayAlert("Error", "Todos los campos son obligatorios", "OK");
                return;
            }

            // Validar que la cantidad y el precio sean válidos
            if (!int.TryParse(cantidadText, out int cantidad) || !double.TryParse(precioText, out double precio))
            {
                await DisplayAlert("Error", "La cantidad y el precio deben ser válidos", "OK");
                return;
            }

            // Actualizar los datos en Firebase
            var updatedSale = new Sales
            {
                Id = saleId,
                NombreProducto = nombreProducto,
                Cantidad = cantidad,
                Precio = precio,
                Fecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm")
            };

            await firebaseClient
                .Child("sales")
                .Child(saleId) // Usar el Id para actualizar
                .PutAsync(JsonConvert.SerializeObject(updatedSale));

            await DisplayAlert("Mensaje", "Venta actualizada correctamente", "OK");
            await Navigation.PopAsync();
        }

        private async void BtnEliminar_Clicked(object sender, EventArgs e)
        {
            await firebaseClient
                .Child("sales")
                .Child(saleId) // Usar el Id para eliminar
                .DeleteAsync();

            await DisplayAlert("Mensaje", "Venta eliminada correctamente", "OK");
            await Navigation.PopAsync();
        }
    }
}
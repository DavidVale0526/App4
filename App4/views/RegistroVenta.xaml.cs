namespace App4.views;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using App4.Models;

public partial class RegistroVenta : ContentPage
{
    private string firebaseURL = "https://crud-bootstrap-ab39a-default-rtdb.firebaseio.com/";

    public RegistroVenta()
    {
        InitializeComponent();
    }

    private async Task EnviarDatosFirebase(string nombreProducto, int cantidad, double precio)
    {
        var firebaseClient = new FirebaseClient(firebaseURL);

        var sales = new Sales
        {
            Id = Guid.NewGuid().ToString(),
            NombreProducto = nombreProducto,
            Cantidad = cantidad,
            Precio = precio,
            Fecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm")
        };

        await firebaseClient
            .Child("sales")
            .PostAsync(JsonConvert.SerializeObject(sales));

        await DisplayAlert("Mensaje", "Datos enviados correctamente", "OK");
        await Navigation.PopAsync();
    }

    private async void BtnEnviar_Clicked(object sender, EventArgs e)
    {
        // Obtener los datos del formulario
        string nombreProducto = nombreProductoEntry.Text;
        string cantidadText = cantidadEntry.Text;
        string precioText = precioEntry.Text;

        // Validar que los campos no están vacíos
        if (string.IsNullOrWhiteSpace(nombreProducto) || string.IsNullOrWhiteSpace(cantidadText) || string.IsNullOrWhiteSpace(precioText))
        {
            await DisplayAlert("Error", "Todos los campos son obligatorios", "OK");
            return;
        }

        // Validar que la cantidad sea un número entero válido
        if (!int.TryParse(cantidadText, out int cantidad))
        {
            await DisplayAlert("Error", "La cantidad debe ser un número entero válido", "OK");
            return;
        }

        // Validar que el precio sea un número decimal válido
        if (!double.TryParse(precioText, out double precio))
        {
            await DisplayAlert("Error", "El precio debe ser un número decimal válido", "OK");
            return;
        }

        // Llamar al método para enviar los datos a Firebase
        await EnviarDatosFirebase(nombreProducto, cantidad, precio);
    }
}

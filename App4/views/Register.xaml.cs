using App4.Models;
namespace App4.views;

public partial class Register : ContentPage
{
    public Register()
    {
        InitializeComponent();
    }

    private async void RegisterButton_Clicked(object sender, EventArgs e)
    {
        string email = emailEntry.Text;
        string password = passwordEntry.Text;
        string nombre = nombreEntry.Text;

        // Validaciones
        if (string.IsNullOrWhiteSpace(email))
        {
            await DisplayAlert("Error", "Por favor, ingrese un correo electrónico.", "Ok");
            return;
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Error", "Por favor, ingrese una contraseña.", "Ok");
            return;
        }

        if (string.IsNullOrWhiteSpace(nombre))
        {
            await DisplayAlert("Error", "Por favor, ingrese su nombre.", "Ok");
            return;
        }

        try
        {
            ConexionFirebase conexionFirebase = new ConexionFirebase();
            var Credenciales = await conexionFirebase.CrearUsuario(email, password, nombre);
            var Uid = Credenciales.User.Uid;

            await DisplayAlert("Registro Exitoso", $"Usuario creado con UID: {Uid}", "Ok");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "Ok");
        }
    }
}

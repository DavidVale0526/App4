using App4.Models;
using App4.views;

namespace App4
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            string email = emailEntry.Text;
            string password = passwordEntry.Text;

            // Validación: Verificar si el campo de correo electrónico está vacío
            if (string.IsNullOrWhiteSpace(email))
            {
                await DisplayAlert("Error", "Por favor, ingrese un correo electrónico.", "Ok");
                return;
            }

            // Validación: Verificar si el campo de contraseña está vacío
            if (string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Error", "Por favor, ingrese una contraseña.", "Ok");
                return;
            }

            try
            {
                ConexionFirebase conexionFirebase = new ConexionFirebase();
                var Credenciales = await conexionFirebase.CargarUsuario(email, password);
                var Uid = Credenciales.User.Uid;

                await DisplayAlert("Inicio de Sesión Exitoso", $"Usuario con UID: {Uid}", "Ok");

                //navegar a la página de home
                await Navigation.PushAsync(new Venta());
            }
            catch (Exception ex)
            {
                // Manejo de excepciones: Mostrar mensaje de error si ocurre una excepción
                await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "Ok");
            }
        }

        // Navegar a la página de registro
        private async void IrCrearUsuario(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Register());
        }
    }

}

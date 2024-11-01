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

            // Mostrar el indicador de carga y deshabilitar el botón
            loadingIndicator.IsVisible = true;
            loadingIndicator.IsRunning = true;
            loginButton.IsEnabled = false;

            try
            {
                ConexionFirebase conexionFirebase = new ConexionFirebase();
                var Credenciales = await conexionFirebase.CargarUsuario(email, password);
                var Uid = Credenciales.User.Uid;
                await DisplayAlert("Inicio de Sesión Exitoso", $"Usuario con UID: {Uid}", "Ok");

                // Navegar a la página de home
                await Navigation.PushAsync(new Venta());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "Ok");
            }
            finally
            {
                // Ocultar el indicador de carga y habilitar el botón
                loadingIndicator.IsVisible = false;
                loadingIndicator.IsRunning = false;
                loginButton.IsEnabled = true;
            }
        }

        // Navegar a la página de registro
        private async void IrCrearUsuario(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Register());
        }
    }

}

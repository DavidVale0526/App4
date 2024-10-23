namespace App4.views;

public partial class Venta : ContentPage
{
	public Venta()
	{
		InitializeComponent();
	}

	public async void OnAgregarVentaClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegistroVenta());
    }
}
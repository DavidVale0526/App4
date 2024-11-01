using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using App4.Models;
using System.Collections.ObjectModel;

namespace App4.views;

public partial class Venta : ContentPage
{
    private string firebaseURL = "https://crud-bootstrap-ab39a-default-rtdb.firebaseio.com/";
    private ObservableCollection<Sales> salesList; // Lista para almacenar las ventas
    private ObservableCollection<Sales> allSalesList; // Nueva lista para almacenar todas las ventas

    private FirebaseClient firebaseClient;// Cliente de Firebase

    public Venta()
    {
        InitializeComponent();
        firebaseClient = new FirebaseClient(firebaseURL);
        salesList = new ObservableCollection<Sales>();
        allSalesList = new ObservableCollection<Sales>();
        salesCollectionView.ItemsSource = salesList; // Aseg�rate de que esto est� configurado
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadSales();
    }

    private async Task LoadSales()
    {
        allSalesList.Clear();
        salesList.Clear();

        var sales = await firebaseClient
            .Child("sales")
            .OnceAsync<object>();

        foreach (var sale in sales)
        {
            var saleData = JsonConvert.DeserializeObject<Sales>(sale.Object.ToString());
            saleData.Id = sale.Key;
            allSalesList.Add(saleData);
            salesList.Add(saleData);
        }
    }

    public async void OnAgregarVentaClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegistroVenta());
    }

    public async void OnVentasTapped(object sender, EventArgs e)
    {
          if (Navigation.NavigationStack.LastOrDefault() is Venta)
    {
        return;
    }
        
        // Navegar a la p�gina de Ventas
        await Navigation.PushAsync(new Venta());
    }

    public async void OnClientesTapped(object sender, EventArgs e)
    {
        // Navegar a la p�gina de Clientes
        await Navigation.PushAsync(new Cliente());
    }

    public async void OnProveedoresTapped(object sender, EventArgs e)
    {
        // Navegar a la p�gina de Proveedores
        await Navigation.PushAsync(new Proveedor());
    }

    public async void OnConfiguracionTapped(object sender, EventArgs e)
    {
        // Navegar a la p�gina de Configuraci�n
        await Navigation.PushAsync(new Configuracion());
    }

    private async void OnSaleTapped(object sender, EventArgs e)
    {
        var sale = (Sales)((TappedEventArgs)e).Parameter; // Obtener la venta seleccionada
        await Navigation.PushAsync(new EditarVenta(sale)); // Navegar a la p�gina de edici�n
    }

    //metodos para buscar ventas
    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        string searchText = e.NewTextValue?.ToLower() ?? string.Empty;
        FilterSales(searchText);
    }

    private void FilterSales(string searchText)
    {
        // Limpiar la lista actual
        salesList.Clear();

        // Cargar todas las ventas desde Firebase
        LoadSales().ContinueWith(task =>
        {
            // Filtrar las ventas seg�n el texto de b�squeda
            var filteredSales = salesList.Where(sale => sale.NombreProducto.ToLower().Contains(searchText)).ToList();

            // Limpiar la lista y agregar las ventas filtradas
            salesList.Clear();
            foreach (var sale in filteredSales)
            {
                salesList.Add(sale);
            }
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }
}
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using App4.Models;
using System.Collections.ObjectModel;

namespace App4.views;

public partial class Venta : ContentPage
{
    private string firebaseURL = "https://crud-bootstrap-ab39a-default-rtdb.firebaseio.com/";
    private ObservableCollection<Sales> salesList;
    private FirebaseClient firebaseClient;

    public Venta()
    {
        InitializeComponent();
        firebaseClient = new FirebaseClient(firebaseURL);
        salesList = new ObservableCollection<Sales>();
        salesCollectionView.ItemsSource = salesList;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadSales();
    }

    private async Task LoadSales()
    {
        salesList.Clear();
        var sales = await firebaseClient
            .Child("sales")
            .OnceAsync<object>();

        foreach (var sale in sales)
        {
            var saleData = JsonConvert.DeserializeObject<Sales>(sale.Object.ToString());
            salesList.Add(saleData);
        }
    }

    public async void OnAgregarVentaClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegistroVenta());
    }
}
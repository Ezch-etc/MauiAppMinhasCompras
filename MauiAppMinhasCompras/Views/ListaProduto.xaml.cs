using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
	//Criando a observable collection para list do arquivo listaproduto.xaml
	ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

	public ListaProduto()
	{
		InitializeComponent();

		lst_produtos.ItemsSource = lista; //Referenciando a lst_produtos com a list para implementar a observable collection
	}

	//Classe protegida sobrecarregada criada para mostrar uma tabela temporária na tela do usuário com a tabela produto
	//Sempre que aparecer na tela do usuário ela será mostrada e se trocar de janela será ocultada usando o método onappearing
    protected override async void OnAppearing()
    {
		List<Produto> tmp = await App.Db.GetAll();

		tmp.ForEach(i => lista.Add(i));
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
	{
		try
		{
			Navigation.PushAsync(new Views.NovoProduto());
		}
		catch (Exception ex)
		{
			DisplayAlert("OPS", ex.Message, "OK");
		}
	}

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
		string q = e.NewTextValue;

		lista.Clear();

        List<Produto> tmp = await App.Db.Search(q);

        tmp.ForEach(i => lista.Add(i));
	}

	private void ToolbarItem_Clicked_1(object sender, EventArgs e)
	{
		double soma = lista.Sum(i => i.Total);

		string msg = $"O total é {soma:C}";

		DisplayAlert("Total dos produtos", msg,"OK");
	}

    private void MenuItem_Clicked(object sender, EventArgs e)
    {
		try
		{
        }
		catch (Exception ex) 
		{
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}
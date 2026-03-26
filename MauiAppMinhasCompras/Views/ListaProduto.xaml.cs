using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
	//Criando a observable collection para list do arquivo listaproduto.xaml
	ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

	string categoriaSelecionada;
    public ListaProduto()
	{
		InitializeComponent();

		lst_produtos.ItemsSource = lista; //Referenciando a lst_produtos com a list para implementar a observable collection
	}

	//Classe protegida sobrecarregada criada para mostrar uma tabela temporária na tela do usuário com a tabela produto
	//Sempre que aparecer na tela do usuário ela será mostrada e se trocar de janela será ocultada usando o método onappearing
    protected override async void OnAppearing()
    {
		lista.Clear();

		List<Produto> tmp = await App.Db.GetAll();

		tmp.ForEach(i => lista.Add(i));
    }

    //Faz navegação
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

	//Ao alterar o texto da barra de pesquisa da tela inicial, ele vai modificar a lista de acordo com o texto na barra de pesquisa
	private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
	{
		try
		{
			string q = e.NewTextValue; //Pega o novo valor que o usuário digitar na barra

            lst_produtos.IsRefreshing = true; //Executa o método refreshing

            lista.Clear();

			List<Produto> tmp = await App.Db.Search(q); //tmp é a lista produto, a partir dele vai usar um Search que faz a busca pelos itens

			tmp.ForEach(i => lista.Add(i)); //Vai detectar cada item e verá se o que estiver na barra de pesquisa corresponde aos itens
		}
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

	//Aqui ele vai fazer a soma de todos os totais de cada linha da lista
	private void ToolbarItem_Clicked_1(object sender, EventArgs e)
	{
		try
		{
			double soma;

            if (categoriaSelecionada == "Todos")
			{
				soma = lista.Sum(i => i.Total);
			}
			else
			{
                soma = lista.Where(i => i.Categoria == categoriaSelecionada).Sum(i => i.Total); // Faz a soma por categoria
            };

			string msg = $"O total é {soma:C}";

			DisplayAlert("Total dos produtos", msg, "OK");
		}
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }

	//Faz a exclusão dos itens
    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			MenuItem selecionado = sender as MenuItem; //Converte o sender para menuitem

			Produto p = selecionado.BindingContext as Produto; //Utiliza o menuitem para receber os valores da tabela produto

			bool confirm = await DisplayAlert("Tem certeza?", $"Remover {p.Descricao}?", "Sim", "Não"); //Bool é para true e false

			if (confirm) //Se o usuário confirmar aplica o delete no SQLite e lista.remove vai remover o item da observable collection
			{
				await App.Db.Delete(p.Id);
				lista.Remove(p);
			}
        }
		catch (Exception ex) 
		{
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		try
		{
			Produto p = e.SelectedItem as Produto; //Faz um item selecionado da lista ser convertido para o tipo produto		

			Navigation.PushAsync(new Views.EditarProduto(p)); //Vai para a tela de edição de produto utilizando o item selecionado com binding
		}
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private async void lst_produtos_Refreshing(object sender, EventArgs e)
    {
        try
        {
            lista.Clear();

            List<Produto> tmp = await App.Db.GetAll(); //tmp é a lista produto, a partir dele vai usar um Search que faz a busca pelos itens

            tmp.ForEach(i => lista.Add(i)); //Vai detectar cada item e verá se o que estiver na barra de pesquisa corresponde aos itens
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        } finally //Vai ser executado mesmo se o try der certo ou errado!
		{
			lst_produtos.IsRefreshing = false; //Aqui não vai carregar quando der erro, e se der certo vai executar o lista.clear() que faria a mesma coisa
		}
    }

    private async void ToolbarItem_Clicked_2(object sender, EventArgs e)
    {
        string categoria = await DisplayActionSheet( //displayaction mostra uma telinha para seleção de itens
			"Filtrar por categoria",
			null, //Remove opções em baixo
			null,
			"Todos",
			"Alimentos",
			"Higiene",
			"Cosméticos",
			"Ferramentas",
			"Material",
			"Outro"
		);
		categoriaSelecionada = categoria; //Vai ser utilizada para soma!

		List<Produto> tmp = await App.Db.GetAll();

        lista.Clear();

        if (categoria == "Todos")
		{
            tmp.ForEach(i => lista.Add(i));
        }
		else
		{
			tmp.Where(p => p.Categoria == categoria).ToList().ForEach(i => lista.Add(i)); ; //Vai fazer a filtragem por categoria de acordo com p.Categoria
		}
    }
}
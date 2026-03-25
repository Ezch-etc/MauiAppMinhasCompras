using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
	public NovoProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        { // Cria uma variável para chamar a tabela em models 
            Produto p = new Produto
            { // Faz a conversão dos textos que o usuário escrever para a variável
                Descricao = txt_descricao.Text,
                Preco = Convert.ToDouble(txt_preco.Text),
                Quantidade = Convert.ToDouble(txt_quantidade.Text)
            };

            //Faz o banco de dados esperar até ter concluído a inserção dos registros
            await App.Db.Insert(p);
            await DisplayAlert("Sucesso!", "Registro inserido!", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("OPS", ex.Message, "OK");
        }
    }
}
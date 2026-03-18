using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class EditarProduto : ContentPage
{
	public EditarProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Produto prod_anexado = BindingContext as Produto; //Cria variável que serve para receber valores da bindingcontext e altera o tipo para produto

            Produto p = new Produto //Cria uma variável para chamar a tabela em models 
            { //Faz a conversão dos textos que o usuário escrever para a variável
                Id = prod_anexado.Id, //Altera os valores pelo prod_anexado utilizando o id
                Descricao = txt_descricao.Text,
                Preco = Convert.ToDouble(txt_preco.Text),
                Quantidade = Convert.ToDouble(txt_quantidade.Text)
            };

            //Faz o banco de dados esperar até ter concluído a atualização dos registros
            await App.Db.Update(p);
            await DisplayAlert("Sucesso!", "Registro atualizado!", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("OPS", ex.Message, "OK");
        }
    }
}
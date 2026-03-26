using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
    public string CategoriaSelecionada { get; set; }
    public List<string> ListaCategoria { get; set; } = new List<string> //lista para categoria
        {
           "Alimentos",
           "Higiene",
           "Cosméticos",
           "Ferramentas",
           "Material",
           "Outro"
        };
    public NovoProduto()
    {
        InitializeComponent();

        ListaCategoria = new List<string> //Mostrando para o NovoProduto() qual é a lista que quero
        {
            "Alimentos",
            "Higiene",
            "Cosméticos",
            "Ferramentas",
            "Material",
            "Outro"
        };
        BindingContext = this; //Sem ele o binding não funciona
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        { // Cria uma variável para chamar a tabela em models 
            Produto p = new Produto
            { // Faz a conversão dos textos que o usuário escrever para a variável
                Descricao = txt_descricao.Text,
                Preco = Convert.ToDouble(txt_preco.Text),
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                Categoria = CategoriaSelecionada
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
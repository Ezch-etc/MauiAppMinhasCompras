using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class EditarProduto : ContentPage
{
    public List<string> ListaCategoria { get; set; }
    public EditarProduto(Produto p) //Foi adicionado parâmetro produto para produtos que já foram criados
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
        pickerCategoria.ItemsSource = ListaCategoria; //Nomeei para não der erro quando a lista for chamada em picker e no abaixo também
        pickerCategoria.SelectedItem = p.Categoria;

        BindingContext = p; //o valor do binding mostra, pois já tem os valores herdados
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Produto prod_anexado = BindingContext as Produto; //Não precisava de todos aqueles códigos, sendo que prod_anexado já estaria
                                                              //chamando a tabela produto

            //Faz o banco de dados esperar até ter concluído a atualização dos registros
            await App.Db.Update(prod_anexado);
            await DisplayAlert("Sucesso!", "Registro atualizado!", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("OPS", ex.Message, "OK");
        }
    }
}
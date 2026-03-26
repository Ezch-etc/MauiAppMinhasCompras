using SQLite;

namespace MauiAppMinhasCompras.Models
{
    public class Produto
    {
        string _descricao;
        string _categoria;
        double _quantidade;
        double _preco;

        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
        public string Descricao
        {
            get => _descricao;

            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Porfavor, insira a descrição");
                }

                _descricao = value;
            }
        }
        public double Quantidade
        {
            get => _quantidade;

            set
            {
                if (value == 0)
                {
                    throw new ArgumentException("Porfavor, insira a quantidade");
                }

                _quantidade = value;
            }
        }
        public double Preco
        {
            get => _preco;

            set
            {
                if (value == 0)
                {
                    throw new ArgumentException("Porfavor, insira o preço");
                }

                _preco = value;
            }
        }
        public double Total { get => Quantidade * Preco; }
        public string Categoria
        {
            get => _categoria;

            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Porfavor, insira uma categoria");
                }

                _categoria = value;
            }
        }
    }
}

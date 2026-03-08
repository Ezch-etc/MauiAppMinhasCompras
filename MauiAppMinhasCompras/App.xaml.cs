using MauiAppMinhasCompras.Helpers;

namespace MauiAppMinhasCompras
{
    public partial class App : Application
    {
        // _db é um campo privado (armazena um valor) 
        static SQLiteDatabaseHelper _db;

        // Db é uma propriedade, neste caso só servirá para leitura (forma segura para acessar um campo)
        public static SQLiteDatabaseHelper Db
        { 
            get
            { // O if vai fazer com que se não tiver banco de dados sqlite, ele vai criar um banco de dados
                if (_db == null)
                {
                    string path = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "bancos_sqlite_compra.db3"
                        ); // Permite que eu encontre o caminho até o arquivo .db3 

                    _db = new SQLiteDatabaseHelper(path); // Utiliza a string path para criar uma database do sqlite
                }
                return _db; //Retorna _db
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.ListaProduto());
        }
    }
}

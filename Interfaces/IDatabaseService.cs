namespace File2SQLite.Interfaces
{
    public interface IDatabaseService
    {
        void InicializarBanco();
        bool InserirLinha(string linha);
    }
}
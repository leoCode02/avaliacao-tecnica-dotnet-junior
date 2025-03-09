namespace File2SQLite.Interfaces
{
    public interface IFileMonitorService
    {
        void ProcessarNovasLinhas();
        long ObterUltimaPosicao(); 
    }
}
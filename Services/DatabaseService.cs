using File2SQLite.Interfaces;
using Microsoft.Data.Sqlite;

namespace File2SQLite.Services
{

    public class DatabaseService : IDatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(string caminhoBanco)
        {
            _connectionString = $"Data Source={caminhoBanco};";
            InicializarBanco();
        }

        public void InicializarBanco()
        {
            using var conexao = new SqliteConnection(_connectionString);
            conexao.Open();

            var comando = conexao.CreateCommand();
             comando.CommandText = @"
             CREATE TABLE IF NOT EXISTS FileLines (
                 Id INTEGER PRIMARY KEY AUTOINCREMENT,
                 Content TEXT NOT NULL UNIQUE,
                 CreatedAt DATETIME DEFAULT (datetime('now', 'localtime'))
             )";
            comando.ExecuteNonQuery();
        }

        public bool InserirLinha(string linha)
        {
            try
            {
                using var conexao = new SqliteConnection(_connectionString);
                conexao.Open();

                var comando = conexao.CreateCommand();
                comando.CommandText = @"
                    INSERT INTO FileLines (Content, CreatedAt)
                    SELECT @Content, datetime('now', 'localtime')
                    WHERE NOT EXISTS (SELECT 1 FROM FileLines WHERE Content = @Content)";
                comando.Parameters.AddWithValue("@Content", linha);
                return comando.ExecuteNonQuery() > 0;
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Erro SQLite: {ex.Message}");
                return false;
            }
        }
    }
}
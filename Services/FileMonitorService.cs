using File2SQLite.Interfaces;
using System;
using System.IO;

namespace File2SQLite.Services
{
    public class FileMonitorService : IFileMonitorService
    {
        private readonly string _caminhoArquivo;
        private readonly IDatabaseService _dbService;
        private long _ultimaPosicao = 0;

        public FileMonitorService(string caminhoArquivo, IDatabaseService dbService)
        {
            _caminhoArquivo = caminhoArquivo;
            _dbService = dbService;
        }

        public void ProcessarNovasLinhas()
        {
            try
            {
                using var stream = new FileStream(_caminhoArquivo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using var leitor = new StreamReader(stream);

                stream.Seek(_ultimaPosicao, SeekOrigin.Begin);
                string? linha;
                int linhasAdicionadas = 0;

                while ((linha = leitor.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(linha) && _dbService.InserirLinha(linha.Trim()))
                        linhasAdicionadas++;
                }

                _ultimaPosicao = stream.Position;
                Console.WriteLine($"[RESULTADO] {linhasAdicionadas} linha(s) inserida(s).");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERRO] {ex.Message}");
            }
        }

        public long ObterUltimaPosicao() => _ultimaPosicao;
    }
}
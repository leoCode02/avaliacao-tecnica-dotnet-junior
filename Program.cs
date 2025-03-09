using File2SQLite.Interfaces;
using File2SQLite.Services;
using System;
using System.IO;
using System.Threading;

namespace File2SQLite
{
    class Program
    {
        private static Timer _timer = null!;
        private static FileMonitorService _monitor = null!;
        private static DatabaseService _dbService = null!;

        static void Main(string[] _)
        {
            try
            {

                string caminhoBanco = "dados.db";
                if (File.Exists(caminhoBanco))
                {
                    Console.WriteLine($"[INFO] Banco de dados encontrado: {Path.GetFullPath(caminhoBanco)}");
                }
                else
                {
                    Console.WriteLine($"[INFO] Banco de dados não encontrado. Um novo será criado: {Path.GetFullPath(caminhoBanco)}");
                }

                string caminhoArquivo = ConfigurarCaminhoArquivo();
                Console.WriteLine($"\nArquivo selecionado: {caminhoArquivo}");

                _dbService = new DatabaseService(caminhoBanco);
                _monitor = new FileMonitorService(caminhoArquivo, _dbService);

                IniciarMonitoramento();
                Console.WriteLine("\n[CONTROLES] ESC - Sair | R - Verificação manual\n");

                MonitorarTeclado();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERRO CRÍTICO] {ex.Message}");
                Console.WriteLine("A aplicação será encerrada.");
            }
        }

        static string ConfigurarCaminhoArquivo()
        {
            string caminho;
            do
            {
                Console.WriteLine("\n=== CONFIGURAÇÃO DO ARQUIVO ===");
                Console.WriteLine("Instruções:");
                Console.WriteLine("1. O arquivo deve ser um .txt existente");
                Console.WriteLine("2. Digite o caminho completo do arquivo");
                Console.WriteLine("Exemplo: C:\\pasta\\arquivo.txt\n");

                Console.Write("Digite o caminho completo do arquivo: ");
                caminho = Console.ReadLine()!.Trim();

                if (string.IsNullOrEmpty(caminho))
                {
                    Console.WriteLine("\nERRO: Caminho não pode ser vazio!");
                    continue;
                }

                if (!File.Exists(caminho))
                {
                    Console.WriteLine("\nERRO: Arquivo não encontrado!");
                    Console.WriteLine($"Caminho verificado: {caminho}");
                    continue;
                }

                if (Path.GetExtension(caminho).ToLower() != ".txt")
                {
                    Console.WriteLine("\nERRO: O arquivo deve ter extensão .txt!");
                    continue;
                }

                return caminho;

            } while (true);
        }

        static void IniciarMonitoramento()
        {
            _timer = new Timer(_ =>
            {
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Verificação automática iniciada");
                _monitor.ProcessarNovasLinhas();
            }, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(30));
        }

        static void MonitorarTeclado()
        {
            while (true)
            {
                var tecla = Console.ReadKey(intercept: true).Key;
                if (tecla == ConsoleKey.Escape)
                {
                    _timer.Dispose();
                    Console.WriteLine("\nMonitoramento encerrado. Obrigado por usar File2SQLite!");
                    break;
                }
                else if (tecla == ConsoleKey.R)
                {
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Verificação manual solicitada!");
                    _monitor.ProcessarNovasLinhas();
                }
            }
        }
    }
}
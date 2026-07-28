using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TesteDeveloper
{
    internal class Program
    {
        private const string CaminhoArquivo = "estoque.csv";

        private static void Main(string[] args)
        {
            IList<EstoqueProduto> estoqueProdutos = CarregarEstoque();

            GerenciadorEstoque gerenciadorEstoque = new GerenciadorEstoque(estoqueProdutos);

            bool sair = false;
            while (!sair)
            {
                Console.WriteLine();
                Console.WriteLine("===== MENU ESTOQUE =====");
                Console.WriteLine("1 - Consultar saldo");
                Console.WriteLine("2 - Adicionar estoque");
                Console.WriteLine("3 - Remover estoque");
                Console.WriteLine("4 - Listar estoque");
                Console.WriteLine("5 - Sair");
                Console.Write("Escolha uma opção: ");

                var opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        Console.Write("Referência: ");
                        var refConsulta = Console.ReadLine();
                        try
                        {
                            var saldo = gerenciadorEstoque.GetSaldo(refConsulta);
                            Console.WriteLine($"Saldo de {refConsulta}: {saldo}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case "2":
                        Console.Write("Referência: ");
                        var refAdicionar = Console.ReadLine();
                        Console.Write("Quantidade a adicionar: ");
                        if (int.TryParse(Console.ReadLine(), out int quantidade))
                        {
                            try
                            {
                                gerenciadorEstoque.AdicionarEstoque(refAdicionar, quantidade);
                                SalvarEstoque(estoqueProdutos);
                                Console.WriteLine("Estoque atualizado!");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Quantidade inválida.");
                        }
                        break;

                    case "3":
                        Console.Write("Referência: ");
                        var refRemover = Console.ReadLine();
                        Console.Write("Quantidade a remover: ");
                        if (int.TryParse(Console.ReadLine(), out int quantidadeRemover))
                        {
                            try
                            {
                                gerenciadorEstoque.RemoverEstoque(refRemover, quantidadeRemover);
                                SalvarEstoque(estoqueProdutos);
                                Console.WriteLine("Estoque atualizado!");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Quantidade inválida.");
                        }
                        break;

                    case "4":
                        Console.WriteLine(gerenciadorEstoque.ToString());
                        break;

                    case "5":
                        sair = true;
                        break;

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
        }

        /// <summary>
        /// Carrega o estoque do arquivo CSV. Se o arquivo não existir, cria com dados padrão.
        /// </summary>
        private static IList<EstoqueProduto> CarregarEstoque()
        {
            if (!File.Exists(CaminhoArquivo))
            {
                IList<EstoqueProduto> padrao = new List<EstoqueProduto>
                {
                    new EstoqueProduto{Referencia = "Camiseta-PP", SaldoEstoque = 4},
                    new EstoqueProduto{Referencia = "Camiseta-P", SaldoEstoque = 5},
                    new EstoqueProduto{Referencia = "Camiseta-M", SaldoEstoque = 15},
                    new EstoqueProduto{Referencia = "Camiseta-G", SaldoEstoque = 20},
                    new EstoqueProduto{Referencia = "Camiseta-GG", SaldoEstoque = 7}
                };

                SalvarEstoque(padrao);
                return padrao;
            }

            var linhas = File.ReadAllLines(CaminhoArquivo);
            var estoque = new List<EstoqueProduto>();

            foreach (var linha in linhas)
            {
                var partes = linha.Split(';');
                if (partes.Length == 2 && int.TryParse(partes[1], out int saldo))
                {
                    estoque.Add(new EstoqueProduto { Referencia = partes[0], SaldoEstoque = saldo });
                }
            }

            return estoque;
        }

        /// <summary>
        /// Salva o estoque atual no arquivo CSV, sobrescrevendo o conteúdo anterior.
        /// </summary>
        private static void SalvarEstoque(IList<EstoqueProduto> estoques)
        {
            var linhas = estoques.Select(e => $"{e.Referencia};{e.SaldoEstoque}");
            File.WriteAllLines(CaminhoArquivo, linhas);
        }
    }
}
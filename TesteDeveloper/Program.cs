using System;
using System.Collections.Generic;

namespace TesteDeveloper
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IList<EstoqueProduto> estoqueProdutos = new List<EstoqueProduto>
            {
                new EstoqueProduto{Referencia = "Camiseta-PP", SaldoEstoque = 4},
                new EstoqueProduto{Referencia = "Camiseta-P", SaldoEstoque = 5},
                new EstoqueProduto{Referencia = "Camiseta-M", SaldoEstoque = 15},
                new EstoqueProduto{Referencia = "Camiseta-G", SaldoEstoque = 20},
                new EstoqueProduto{Referencia = "Camiseta-GG", SaldoEstoque = 7}
            };

            GerenciadorEstoque gerenciadorEstoque = new GerenciadorEstoque(estoqueProdutos);

            bool sair = false;
            while (!sair)
            {
                Console.WriteLine();
                Console.WriteLine("===== MENU ESTOQUE =====");
                Console.WriteLine("1 - Consultar saldo");
                Console.WriteLine("2 - Adicionar estoque");
                Console.WriteLine("3 - Listar estoque");
                Console.WriteLine("4 - Sair");
                Console.Write("Escolha uma opção: ");

                var opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        Console.Write("Referência: ");
                        var refConsulta = Console.ReadLine();
                        var saldo = gerenciadorEstoque.GetSaldo(refConsulta);
                        Console.WriteLine($"Saldo de {refConsulta}: {saldo}");
                        break;

                    case "2":
                        Console.Write("Referência: ");
                        var refAdicionar = Console.ReadLine();
                        Console.Write("Quantidade a adicionar: ");
                        if (int.TryParse(Console.ReadLine(), out int quantidade))
                        {
                            gerenciadorEstoque.AdicionarEstoque(refAdicionar, quantidade);
                            Console.WriteLine("Estoque atualizado!");
                        }
                        else
                        {
                            Console.WriteLine("Quantidade inválida.");
                        }
                        break;

                    case "3":
                        Console.WriteLine(gerenciadorEstoque.ToString());
                        break;

                    case "4":
                        sair = true;
                        break;

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
        }
    }
}
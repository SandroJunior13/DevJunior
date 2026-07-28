using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TesteDeveloper
{
    /// <summary>
    /// Implementação da administração de estoque
    /// </summary>
    public class GerenciadorEstoque
    {
        //Saldos de estoque por referência
        private readonly IList<EstoqueProduto> _estoques;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="estoques">Saldos de estoque por referência</param>
        public GerenciadorEstoque(IList<EstoqueProduto> estoques)
        {
            _estoques = estoques ?? throw new ArgumentNullException(nameof(estoques));
        }

        /// <summary>
        /// Verifica se a quantidade requerida existe no estoque da referência
        /// </summary>
        /// <param name="referencia">Identificador da referência/produto</param>
        /// <param name="quantidadeRequerida">Quantidade requerida</param>
        /// <returns>Indica se a quantidade requerida existe ou não no estoque</returns>
        public bool EstoqueDisponivel(string referencia, int quantidadeRequerida)
        {
            return GetSaldo(referencia) >= quantidadeRequerida;
        }

        /// <summary>
        /// Buscar saldo de estoque da referência
        /// </summary>
        /// <param name="referencia">Identificador da referência/produto</param>
        /// <returns>Saldo de estoque</returns>
        public int GetSaldo(string referencia)
        {
            if (string.IsNullOrWhiteSpace(referencia))
                throw new ArgumentException("A referência não pode ser vazia.");

            var produto = _estoques.FirstOrDefault(x => x.Referencia.Trim().Equals(referencia.Trim(), StringComparison.OrdinalIgnoreCase));

            return produto?.SaldoEstoque ?? 0;
        }

        /// <summary>
        /// Adiciona quantidade ao estoque de uma referência. Se a referência não existir, cria uma nova.
        /// </summary>
        /// <param name="referencia">Identificador da referência/produto</param>
        /// <param name="quantidade">Quantidade a adicionar</param>
        public void AdicionarEstoque(string referencia, int quantidade)
        {
            if (string.IsNullOrWhiteSpace(referencia))
                throw new ArgumentException("A referência não pode ser vazia.");

            if (quantidade < 0)
                throw new ArgumentException("A quantidade não pode ser negativa.");

            var produto = _estoques.FirstOrDefault(x => x.Referencia.Trim().Equals(referencia.Trim(), StringComparison.OrdinalIgnoreCase));

            if (produto != null)
                produto.SaldoEstoque += quantidade;
            else
                _estoques.Add(new EstoqueProduto { Referencia = referencia, SaldoEstoque = quantidade });
        }

        /// <summary>
        /// Remove (dá baixa) uma quantidade do estoque de uma referência
        /// </summary>
        /// <param name="referencia">Identificador da referência/produto</param>
        /// <param name="quantidade">Quantidade a remover</param>
        public void RemoverEstoque(string referencia, int quantidade)
        {
            if (string.IsNullOrWhiteSpace(referencia))
                throw new ArgumentException("A referência não pode ser vazia.");

            if (quantidade < 0)
                throw new ArgumentException("A quantidade não pode ser negativa.");

            if (!EstoqueDisponivel(referencia, quantidade))
                throw new InvalidOperationException("Saldo insuficiente para essa retirada.");

            var produto = _estoques.FirstOrDefault(x => x.Referencia.Trim().Equals(referencia.Trim(), StringComparison.OrdinalIgnoreCase));
            produto.SaldoEstoque -= quantidade;
        }

        /// <summary>
        /// Gera string com os estoques no formato [Referência: {Referencia} Saldo: {SaldoEstoque}] com uma linha para cada referência
        /// Ex: 
        /// Referência: A345 Saldo: 98
        /// Referência: B456 Saldo: 15
        /// </summary>
        /// <returns>String formatada</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var estoque in _estoques)
            {
                sb.Append($"Referência: {estoque.Referencia} Saldo: {estoque.SaldoEstoque}");
                sb.Append('\n');
            }
            return sb.ToString().Replace("\r\n", "\n").TrimEnd();
        }
    }
}
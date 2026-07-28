using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

     /// System.Linq permite usar métodos como FirstOrDefault(), que facilitam procurar um item dentro da lista.
     /// System.Text permite usar StringBuilder, que é uma forma eficiente de montar textos.
     
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
        /// <param name="estoques">Saldos de estoquee por referência</param>
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
        /// Adiciona quantidade ao estoque de uma referência. Se a referência não existir, cria uma nova.
        /// </summary>
        /// <param name="referencia">Identificador da referência/produto</param>
        /// <param name="quantidade">Quantidade a adicionar</param>
        public void AdicionarEstoque(string referencia, int quantidade)
        {
        var produto = _estoques.FirstOrDefault(x => x.Referencia.Trim().Equals(referencia.Trim(), StringComparison.OrdinalIgnoreCase));

         if (produto != null)
        {
        produto.SaldoEstoque += quantidade;
        }
        else
        {
        _estoques.Add(new EstoqueProduto { Referencia = referencia, SaldoEstoque = quantidade });
        }
}
        /// GetSaldo(referencia) chama o método GetSaldo para obter o saldo atual do estoque da referência especificada. 
        /// A comparação >= quantidadeRequerida verifica se o saldo é suficiente para atender à quantidade requerida. 
        /// O resultado é retornado como um valor booleano, indicando se a quantidade requerida está disponível no estoque.
        

        /// <summary>
        /// Buscar saldo de estoque da referência
        /// </summary>
        /// <param name="referencia">Identificador da referência/produto</param>
        /// <returns>Saldo de estoque</returns>
        public int GetSaldo(string referencia)
        {
         var produto = _estoques.FirstOrDefault(x => x.Referencia.Trim().Equals(referencia.Trim(), StringComparison.OrdinalIgnoreCase));

        return produto?.SaldoEstoque ?? 0;
        }


        /// <summary>
        /// Gera string com os estoques no formato [Referência: {Referencia} Saldo: {SaldoEstoque}] com uma linha para cada referência
        /// Ex: 
        /// Referência: A345 Saldo: 98
        /// Referência: B456 Saldo: 15
        /// 
        /// </summary>
        /// <returns>String formatada</returns>
        public override string ToString()
        {
       var sb = new StringBuilder();
        /// StringBuilder é usado para construir a string de forma eficiente, evitando a criação de múltiplas strings imutáveis.
    foreach (var estoque in _estoques)
    {
        sb.Append($"Referência: {estoque.Referencia} Saldo: {estoque.SaldoEstoque}");
        sb.Append('\n');

    }       /// ToString() percorre a lista de estoques e monta uma string com o formato desejado, utilizando StringBuilder para eficiência. 
         /// Cada referência e seu saldo são adicionados manualmente com Append('\n'), em vez de AppendLine(), 
        /// para garantir que a quebra de linha seja sempre "\n", independente do sistema operacional.
         /// O Replace("\r\n", "\n") funciona como uma segurança extra, removendo qualquer "\r" 
         /// que apareça (por exemplo, se o próprio editor de texto inserir esse caractere ao salvar o arquivo).
        return sb.ToString().Replace("\r\n", "\n").TrimEnd();
}        /// TrimEnd() remove a última quebra de linha da string final, já que ela não deve aparecer depois do último item.
        }
}

1. Métodos exigidos pelo desafio (GerenciadorEstoque.cs)
GetSaldo(string referencia)

Antes (TODO original):

csharp
public int GetSaldo(string referencia)
{
    //TODO - Implemente sua lógica para buscar e retornar o estoque da referência
}

Depois:

csharp
public int GetSaldo(string referencia)
{
    var produto = _estoques.FirstOrDefault(x => x.Referencia.Trim().Equals(referencia.Trim(), StringComparison.OrdinalIgnoreCase));
    return produto?.SaldoEstoque ?? 0;
}

Explicação:

FirstOrDefault procura na lista o primeiro item cuja referência bate com a informada.
Trim() remove espaços em branco no início/fim da string, evitando falha de busca por espaço extra digitado sem querer.
StringComparison.OrdinalIgnoreCase ignora diferença entre maiúsculas e minúsculas na comparação.
produto?.SaldoEstoque ?? 0 — se produto for null (referência não encontrada), o operador ?. evita erro de referência nula, e ?? 0 retorna 0 como valor padrão, em vez de quebrar o programa.
EstoqueDisponivel(string referencia, int quantidadeRequerida)

Antes (TODO original):

csharp
public bool EstoqueDisponivel(string referencia, int quantidadeRequerida)
{
    //TODO - Implemente sua lógica para validar o estoque da referência contra a quantidade requerida
}

Depois:

csharp
public bool EstoqueDisponivel(string referencia, int quantidadeRequerida)
{
    return GetSaldo(referencia) >= quantidadeRequerida;
}

Explicação: reaproveita o GetSaldo já implementado (evita duplicar lógica de busca) e compara o saldo retornado com a quantidade requerida.

ToString()

Antes (TODO original):

csharp
public override string ToString()
{
    //TODO - Implemente sua lógica para formatar uma string no formato esperado
}

Depois:

csharp
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

Explicação:

StringBuilder é usado em vez de concatenar strings com + dentro do loop, porque strings em C# são imutáveis — concatenar repetidamente cria várias strings novas na memória a cada iteração, o que é ineficiente. StringBuilder monta o texto internamente sem recriar tudo a cada passo.
Quebra de linha adicionada manualmente com Append('\n'), em vez de usar AppendLine() — que gera \r\n no Windows, mas o teste automatizado do projeto espera só \n.
.Replace("\r\n", "\n") funciona como segurança extra, garantindo que nenhum \r sobre na string final, independente do sistema operacional.
.TrimEnd() remove a quebra de linha sobrando depois do último item.
2. Validação no construtor (já existia no esqueleto, mantida)
csharp
public GerenciadorEstoque(IList<EstoqueProduto> estoques)
{
    _estoques = estoques ?? throw new ArgumentNullException(nameof(estoques));
}

Explicação: se alguém tentar criar um GerenciadorEstoque passando null no lugar da lista de estoques, essa linha lança um erro claro (ArgumentNullException) imediatamente, impedindo que o objeto seja criado em um estado inválido. Isso evita que o erro apareça depois, de forma confusa, quando algum método tentasse usar uma lista inexistente.

3. Extras implementados (além do que o desafio pedia)
AdicionarEstoque(string referencia, int quantidade) — método novo
csharp
public void AdicionarEstoque(string referencia, int quantidade)
{
    var produto = _estoques.FirstOrDefault(x => x.Referencia.Trim().Equals(referencia.Trim(), StringComparison.OrdinalIgnoreCase));

    if (produto != null)
        produto.SaldoEstoque += quantidade;
    else
        _estoques.Add(new EstoqueProduto { Referencia = referencia, SaldoEstoque = quantidade });
}

Explicação: o desafio original só pedia consulta de estoque (leitura). Esse método permite entrada de estoque: se a referência já existe, soma a quantidade ao saldo atual; se não existe, cria um novo item na lista.

Menu interativo (Program.cs)

Antes: o Program.cs original rodava uma única vez, com valores fixos no código, mostrando resultado e encerrando.

Depois: loop de menu no console, com 4 opções:

csharp
bool sair = false;
while (!sair)
{
    Console.WriteLine("1 - Consultar saldo");
    Console.WriteLine("2 - Adicionar estoque");
    Console.WriteLine("3 - Listar estoque");
    Console.WriteLine("4 - Sair");

    var opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            // consulta GetSaldo
            break;
        case "2":
            // lê referência e quantidade, chama AdicionarEstoque
            break;
        case "3":
            // exibe ToString()
            break;
        case "4":
            sair = true;
            break;
    }
}

Explicação:

bool sair controla o loop: começa false ("ainda não é hora de sair"); o while (!sair) mantém o menu rodando enquanto sair for false; ao digitar "4", sair vira true e o loop para.
Usado while em vez de um número fixo de repetições, porque não se sabe de antemão quantas vezes o usuário vai interagir com o menu — o controle de quando parar fica com quem está usando o programa.
Na opção 2, a leitura da quantidade usa int.TryParse(Console.ReadLine(), out int quantidade) em vez de int.Parse(...), porque TryParse tenta converter o texto digitado em número e retorna true/false conforme o sucesso, sem travar o programa se a pessoa digitar algo inválido (como letras). Com int.Parse, qualquer entrada não numérica quebraria o programa com um erro (FormatException).
4. Testes automatizados (TesteDeveloperTests)

Projeto separado com 11 testes (NUnit) cobrindo EstoqueDisponivel, GetSaldo e ToString(). Após os ajustes acima (principalmente a correção da quebra de linha no ToString()), todos os 11 testes passam:

Resumo do teste: total: 11; falhou: 0; bem-sucedido: 11; ignorado: 0

Para rodar: dentro da pasta TesteDeveloperTests, executar dotnet test.
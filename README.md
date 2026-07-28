# Gerenciador de Estoque — TesteDeveloper

Aplicação console em C# (.NET) para consultar e gerenciar o estoque de produtos por referência.

## O que o desafio pedia

O projeto veio com três métodos marcados como `//TODO`, e foram implementados:

- **`GetSaldo`** — devolve o saldo de uma referência (0 se ela não existir).
- **`EstoqueDisponivel`** — diz se tem saldo suficiente pra uma quantidade pedida.
- **`ToString`** — monta o texto com todas as referências e seus saldos, uma por linha.

## O que foi adicionado além do pedido

- **Adicionar estoque** — dá entrada de mercadoria numa referência. Se ela já existe, soma no saldo; se não existe, cria um item novo.
- **Remover estoque** — dá baixa numa referência, verificando antes se tem saldo suficiente (não deixa o estoque ficar negativo).
- **Validações de segurança** — o sistema não aceita: referência em branco, quantidade negativa, ou remover mais do que existe em estoque. Em qualquer um desses casos, mostra uma mensagem de erro clara, sem travar o programa.
- **Busca tolerante a erro de digitação** — buscar "Camiseta-PP" ou "camiseta-pp " (com espaço a mais) é tratado como a mesma referência.
- **Menu interativo no console** — em vez de rodar uma vez só, o programa fica em loop oferecendo: consultar saldo, adicionar estoque, remover estoque, listar tudo, ou sair.
- **Estoque salvo em arquivo** — os dados agora ficam guardados num arquivo `estoque.csv`, na mesma pasta do programa. Assim, ao fechar e abrir de novo, o estoque continua do jeito que estava, em vez de voltar aos valores fixos originais.
- **Na primeira execução, o arquivo `estoque.csv` é criado automaticamente com os dados iniciais. Nas próximas vezes, ele já carrega o que estava salvo.

# Changelog

Registro cronológico das mudanças e evoluções do editor de texto em console.

Este projeto evolui de forma incremental, com foco em aprendizado, controle manual de estado
e comportamento previsível do editor.

---

## [14/01/2026]

### Estrutura Inicial
- Separação da lógica de leitura de arquivo e exibição do conteúdo
- Implementação de um buffer de texto em memória usando `List<string>`
- Carregamento do arquivo apenas uma vez, evitando leituras repetidas
- Criação de um loop principal do editor para execução contínua

### Renderização
- Redesenho completo do terminal usando `Console.Clear()`
- Renderização do conteúdo baseada exclusivamente no estado em memória
- Captura de entrada do teclado com `Console.ReadKey`

### Controle Básico
- Saída controlada do editor usando a tecla `Esc`

---

## [19/01/2026]

### Cursor Vertical
- Implementação do cursor lógico funcional
- Uso da variável `CursorLinha` como estado do cursor
- Cursor desenhado como parte da renderização (`>` na linha ativa)

### Navegação
- `K` → mover cursor para cima
- `J` → mover cursor para baixo
- Controle de limites para evitar sair do buffer

### Correções
- Correção do loop de input
- Ajuste para leitura de uma tecla por iteração
- Correção do bug que exigia múltiplas teclas pressionadas

### Interface
- Implementação de destaque visual da linha selecionada
- Uso correto de `Console.ForegroundColor`
- Aplicação de `Console.ResetColor()` para evitar vazamento de cor

### Usabilidade
- Instruções exibidas no rodapé do editor
- Visual mais claro para navegação

---

## [20/01/2026]

### Modo de Edição (Insert)
- Implementação do modo insert
- Criação de estado para alternar entre modo normal e modo de edição
- Entrada no modo insert através da tecla `I`
- Retorno ao modo normal com `Esc`

### Separação por Modo
- Navegação (`J` / `K`) ativa apenas no modo normal
- Entrada de texto permitida apenas no modo insert
- Tratamento contextual de teclas conforme o estado do editor

### Inserção de Texto
- Digitação de caracteres adicionando conteúdo à linha atual
- Manipulação correta de strings no `SaveMemory`
- Atualização dinâmica do conteúdo exibido

### Backspace
- Remoção do último caractere da linha atual
- Validação para evitar erro em linhas vazias
- Comportamento restrito ao modo insert

### Enter (Quebra de Linha)
- Inserção de nova linha vazia no buffer
- Uso de `List.Insert`
- Movimentação automática do cursor para a nova linha

### Consolidação
- Cada linha representada como uma string
- Texto tratado como buffer (`List<string>`)
- Cursor vertical mapeado diretamente ao índice da lista
- Base do editor estabilizada e preparada para cursor horizontal

---

## [21/01/2026]

### Cursor Horizontal
- Implementação do cursor de coluna (`CursorColuna`)
- Mudança da renderização:
  - De `WriteLine(texto)`
  - Para loop por caractere, permitindo cursor no meio da linha

### Renderização Avançada
- Criação do conceito de linha ativa (`linhaAtiva`)
- Cursor em bloco visual usando `ForegroundColor` e `BackgroundColor`
- Cursor aparece apenas no modo de edição
- Suporte a cursor no final da linha (`coluna == texto.Length`)

### Correções
- Correção do erro lógico com `linha++` e `WriteLine()`
- Identificação e correção do motivo pelo qual o cursor não aparecia

### Navegação Horizontal
- `←` diminui `CursorColuna`
- `→` aumenta `CursorColuna`
- Ajuste automático da coluna ao trocar de linha

---

## [22/01/2026]

### Loop Principal
- Consolidação completa do `LoopEditor`
- Implementação da edição de texto em tempo real

### Inserção Avançada
- Inserção de caracteres na posição do cursor
- Uso de `string.Insert(CursorColuna, char)`
- Atualização correta de `CursorColuna`

### Backspace
- Remove caractere à esquerda do cursor
- Atualiza posição do cursor corretamente

### Enter
- Split da linha atual:
  - Parte esquerda permanece
  - Parte direita vira nova linha
- Cursor movido para o início da nova linha

### Delete
- Remove caractere na posição do cursor
- Junta com a próxima linha ao final da linha

### Navegação Integrada
- Transição automática entre linhas ao atingir início/fim
- Ajuste automático da coluna ao mover verticalmente

### Correções Críticas
- Prevenção de `ArgumentOutOfRangeException`
- Garantia de `CursorLinha` e `CursorColuna` válidos

### Estado do Projeto
- Comportamento semelhante a editores reais
- Identificação de pendências:
  - Backspace no início da linha
  - Clamp global de cursor
  - Edge case de arquivo vazio

---

## [23/01/2026]

### Salvamento
- Implementação da funcionalidade de salvar arquivo
- Criação da função `SalvarArquivo()`
- Uso de `File.WriteAllLines`
- Caminho de arquivo fixo para testes iniciais

### Integração
- Salvamento disponível apenas no modo de edição
- Atalho configurado inicialmente como `Ctrl+S`

### Ajustes
- Substituição de `Ctrl+S` por `F2`
- Evitou conflitos com `Console.ReadKey`
- Garantiu captura confiável do input

### Validação
- Salvamento ocorre antes da lógica de inserção
- Tecla `F2` não interfere na edição
- Fluxo completo validado:
  - Editar
  - Salvar
  - Encerrar
  - Reabrir e confirmar persistência

### Consolidação
- Definição final dos modos:
  - `modo == false` → visualização
  - `modo == true` → edição
- Loop do editor estabilizado

---

## [27/01/2026]

### Carregamento de Arquivo
- Remoção de caminho hardcoded
- Leitura dinâmica do caminho via `Console.ReadLine()`
- Validação de existência do arquivo antes de carregar

### Controle de Alterações
- Introdução das flags `dirty` e `arquivoModificado`
- Indicador visual (`*`) quando há alterações não salvas

### Salvamento
- Salvamento manual usando tecla `F2`
- Escrita no arquivo original carregado
- Feedback visual de sucesso ao salvar

### Proteção de Dados
- Confirmação ao tentar sair sem salvar
- Opção explícita para sair sem salvar (`S/N`)

### Estabilidade
- Todas as operações de edição marcam o arquivo como modificado
- Cursor consistente após concatenação de linhas
- Clamp global garantindo segurança

### Qualidade de Uso
- Cursor em bloco mantido
- `Home` → início da linha
- `End` → fim da linha
- Navegação entre linhas preservando coerência da coluna

### Estado Atual
- Editor de texto funcional em console
- Fluxo completo:
  - Abrir
  - Editar
  - Salvar
  - Proteger contra perda de dados
- Base sólida para futuras melhorias

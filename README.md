Log de mudanças

# 14/01/2026
- Separação da lógica de leitura de arquivo e exibição do conteúdo
- Implementação de um buffer de texto em memória usando List<string>
- Carregamento do arquivo apenas uma vez, evitando leituras repetidas
- Criação de um loop principal do editor para execução contínua
- Implementação de redesenho completo do terminal com Console.Clear()
- Renderização do conteúdo baseada exclusivamente no estado em memória
- Captura de entrada do teclado com Console.ReadKey
- Saída controlada do modo editor usando a tecla ESC

# 19/01/2026
- Implementação do cursor lógico funcional
      > Uso da variável CursorLinha como estad
      > Cursor desenhado como parte da renderização (>)
- Criação do movimento do cursor
    > Tecla K → mover cursor para cima
    > Tecla J → mover cursor para baixo
    > Controle de limites para evitar sair do buffer
- Correção do loop de input
    > Ajuste para leitura de uma tecla por iteração
    > Correção do bug que exigia múltiplas teclas pressionada
- Implementação de destaque visual da linha selecionada
    > Uso correto de Console.ForegroundColor
    > Aplicação da cor antes do WriteLine
    > Uso de Console.ResetColor() para evitar vazamento de cor
- Melhoria da usabilidade
    > Instruções exibidas no rodapé do editor
    > Visual mais claro para navegação


# 20/01/2026
- Implementação do modo insert (edição)
    > Criação de estado para alternar entre modo normal e modo de edição
    > Entrada no modo insert através da tecla I
    > Retorno ao modo normal com Esc
- Separação de comportamentos por modo
    > Navegação (J / K) ativa apenas no modo normal
    > Entrada de texto permitida apenas no modo insert
    > Tratamento contextual de teclas conforme o estado do editor
- Inserção de texto no buffer
    > Digitação de caracteres adicionando conteúdo à linha atual
    > Manipulação correta de strings no SaveMemory
    > Atualização dinâmica do conteúdo exibido
- Implementação do Backspace
    > Remoção do último caractere da linha atual
    > Validação para evitar erro em linhas vazias
    > Comportamento restrito ao modo insert
- Implementação do Enter (quebra de linha simples)
    > Inserção de nova linha vazia no buffer
    > Uso de List.Insert para modificar o texto em memória
    > Movimentação automática do cursor para a nova linha
- Consolidação do modelo de dados do editor
    > Cada linha do arquivo representada como uma string
    > Texto completo tratado como List<string> (buffer de texto)
    > Cursor vertical mapeado diretamente ao índice da lista
- Estabilização da base do editor
    > Loop principal de input consolidado
    > Renderização desacoplada da lógica de edição
    > Base preparada para evolução do cursor horizontal

# 21/01/2026
- Implementação do cursor de coluna (CursorColuna).
- Mudança da renderização:
    > De WriteLine(texto) para loop por caractere, permitindo cursor no meio da linha.
- Criação do conceito de linha ativa (linhaAtiva).
- Cursor em bloco visual usando ForegroundColor e BackgroundColor.
- Cursor aparece:
    > Apenas no modo de edição
    > Na posição correta da coluna
- Suporte a cursor no final da linha (coluna == texto.Length).
- Correção do erro lógico:
    > linha++ e WriteLine() estavam fora do foreach.
- Navegação horizontal implementada:
    > ← diminui CursorColuna
    > → aumenta CursorColuna
- Ajuste automático da coluna ao trocar de linha (evita overflow).
- Identificação e correção do motivo pelo qual o cursor não aparecia no modo edição.    

# 22/01/2026

- Consolidação do loop principal do editor (`LoopEditor`).
- Implementação completa da edição de texto em tempo real.
- Inserção de caracteres na posição do cursor:
  > Uso de `string.Insert(CursorColuna, char)`
  > Atualização correta de `CursorColuna` após inserção
- Implementação do Backspace:
  > Remove caractere à esquerda do cursor
  > Atualiza posição do cursor corretamente
- Implementação do Enter (quebra de linha):
  > Split da linha atual em duas partes
  > Parte esquerda permanece na linha atual
  > Parte direita é inserida como nova linha
  > Cursor movido para início da nova linha
- Implementação da tecla Delete:
  > Remove caractere na posição do cursor
  > Junta com a próxima linha quando no final da linha
- Navegação horizontal avançada:
  > ← move o cursor para a esquerda
  > → move o cursor para a direita
  > Transição automática entre linhas ao atingir início/fim
- Navegação vertical integrada com edição:
  > `CursorColuna` ajustado automaticamente ao mudar de linha
  > Evita `ArgumentOutOfRangeException`
- Correção de erros críticos de índice:
  > Garantia de que `CursorColuna` nunca ultrapassa o tamanho da linha
  > Garantia de que `CursorLinha` referencia uma linha válida
- Evolução do editor para comportamento semelhante a editores reais:
  > Delete e Enter com comportamento contextual
  > Cursor coerente entre linhas
- Identificação de pontos pendentes:
  > Backspace no início da linha ainda não implementado
  > Falta clamp global de segurança do cursor
  > Edge case de arquivo vazio ainda não tratado

# 23/01/2026

- Implementação da funcionalidade de salvar arquivo durante a edição.
- Criação da função SalvarArquivo():
    > Uso de File.WriteAllLines para persistir SaveMemory no arquivo.
    > Caminho de arquivo fixo para testes.
- Integração do salvamento ao LoopEditor:
    > Salvamento disponível apenas no modo de edição.
    > Atalho configurado usando tecla de função (F2).
- Substituição do atalho Ctrl+S por F2:
    > Evitou conflitos com Console.ReadKey e KeyChar.
    > Garantiu captura confiável do input no console.
- Correção e validação do fluxo de execução:
    > Salvamento ocorre antes da lógica de inserção.
    > Tecla F2 não interfere na edição de texto.
- Confirmação do ciclo completo de persistência:
    > Editar arquivo
    > Salvar com F2
    > Encerrar programa
    > Reabrir e validar alterações no arquivo
- Consolidação dos modos do editor:
    > modo == false → visualização
    > modo == true  → edição
- Estabilização geral do LoopEditor:
    > Fluxo mais linear
    > Menos condições redundantes
    > Editor mais previsível e confiável

# 27/01/2026
- Refatoração do sistema de carregamento de arquivos:
    > Remoção do caminho hardcoded.
    > Leitura dinâmica do caminho do arquivo via Console.ReadLine().
    > Validação de existência do arquivo antes de carregar.
- Introdução de estado de modificação do arquivo:
    > Flags dirty e arquivoModificado.
    > Indicação visual (*) no modo de edição/visualização quando há alterações não salvas.
- Implementação de salvamento manual com tecla dedicada:
    > Salvamento usando tecla F2.
    > Escrita do conteúdo atual (SaveMemory) no arquivo original.
    > Feedback visual de “Arquivo salvo com sucesso”.
- Proteção contra saída sem salvar:
    > Prompt de confirmação ao tentar sair com alterações pendentes.
    > Opção explícita para sair sem salvar (S/N).
- Consolidação do fluxo de edição:
    > Inserção, Backspace, Delete e Enter agora marcam o arquivo como modificado.
    > Cursor permanece consistente após concatenação de linhas.
    > Clamp de cursor garante segurança após qualquer operação.
- Melhoria da experiência de uso:
    > Cursor em bloco visual mantido.
    > Home leva ao início da linha.
    > End leva ao final da linha.
    > Navegação entre linhas preservando coerência da coluna.
- Estrutura do editor consolidada como editor de texto funcional em console:
    > Separação clara entre modo de visualização e modo de edição.
    > Arquivo carregado, editado, salvo e protegido contra perda de dados.


Log de mudanças

#14/01/2026
- Separação da lógica de leitura de arquivo e exibição do conteúdo
- Implementação de um buffer de texto em memória usando List<string>
- Carregamento do arquivo apenas uma vez, evitando leituras repetidas
- Criação de um loop principal do editor para execução contínua
- Implementação de redesenho completo do terminal com Console.Clear()
- Renderização do conteúdo baseada exclusivamente no estado em memória
- Captura de entrada do teclado com Console.ReadKey
- Saída controlada do modo editor usando a tecla ESC

#19/01/2026
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

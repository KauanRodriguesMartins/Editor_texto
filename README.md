Log de mudanças

#14/01/2206
- Separação da lógica de leitura de arquivo e exibição do conteúdo
- Implementação de um buffer de texto em memória usando List<string>
- Carregamento do arquivo apenas uma vez, evitando leituras repetidas
- Criação de um loop principal do editor para execução contínua
- Implementação de redesenho completo do terminal com Console.Clear()
- Renderização do conteúdo baseada exclusivamente no estado em memória
- Captura de entrada do teclado com Console.ReadKey
- Saída controlada do modo editor usando a tecla ESC

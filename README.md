# Console Text Editor (C#)

Um editor de texto simples feito em **C# para console**, inspirado em editores modais (como o Vim), com foco em aprendizado, controle manual de cursor e manipulação de texto linha a linha.

O projeto não usa bibliotecas externas nem interface gráfica — tudo é feito diretamente no console.

---

## ✨ Funcionalidades

- Leitura de arquivos de texto a partir de um caminho informado pelo usuário
- Edição de texto diretamente no console
- Cursor em bloco com controle de linha e coluna
- Modos de operação:
  - **Modo de Visualização**
  - **Modo de Edição**
- Inserção de texto
- Remoção de caracteres:
  - Backspace
  - Delete
- Quebra e junção de linhas
- Navegação com:
  - Setas
  - `Home` (início da linha)
  - `End` (fim da linha)
- Salvamento manual do arquivo (F2)
- Detecção de alterações não salvas
- Aviso ao tentar sair sem salvar

---

## 🎮 Controles

### Modo de Visualização
- `K` → Linha acima  
- `J` → Linha abaixo  
- `I` → Entrar no modo de edição  
- `Esc` → Sair do editor  

### Modo de Edição
- Digitação normal → Insere texto
- `Backspace` → Apaga caractere à esquerda / junta linhas
- `Delete` → Apaga caractere à direita / junta linhas
- `Enter` → Quebra a linha
- `←` `→` → Move o cursor
- `Home` → Vai para o início da linha
- `End` → Vai para o fim da linha
- `F2` → Salvar arquivo
- `Esc` → Voltar ao modo de visualização

---

## 💾 Salvamento

- O arquivo **não é salvo automaticamente**
- Qualquer modificação marca o arquivo como alterado (`*`)
- Ao sair com alterações pendentes, o editor pede confirmação

---

## 🧠 Objetivo do Projeto

Este projeto tem como foco:

- Aprender manipulação de strings e listas em C#
- Trabalhar com entrada de teclado no console
- Gerenciar estado de editor (cursor, modo, alterações)
- Criar um editor funcional sem depender de GUI ou frameworks

Não é um editor profissional — é um **editor didático e funcional**.

---

## 🚀 Possíveis Melhorias Futuras

- Criar novo arquivo (não apenas abrir existentes)
- Scroll vertical e horizontal
- Busca de texto
- Numeração fixa de linhas
- Undo / Redo simples
- Melhor uso de códigos ANSI para renderização

---

## 🛠 Tecnologias

- Linguagem: **C#**
- Plataforma: **.NET (Console Application)**

---

## 📌 Status

✔ Funcional  
✔ Estável  
✔ Em evolução

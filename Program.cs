using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

// ===================== ANSI SETUP =====================
Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.Write("\x1b[?25l"); // esconde cursor

// =====================================================
List<string> SaveMemory = new List<string>();

int CursorLinha = 1;
int CursorColuna = 0;
bool modo = false;
bool dirty = false;
bool arquivoModificado = false;

string caminhoArquivo = "";

// =====================================================
void LimparTela()
{
    Console.Write("\x1b[2J"); // limpa tela
    Console.Write("\x1b[H");  // cursor topo
}

// =====================================================
void Ler_arquivo()
{
    Console.Write("Digite o caminho do arquivo: ");
    caminhoArquivo = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(caminhoArquivo) || !File.Exists(caminhoArquivo))
    {
        Console.WriteLine("Arquivo não encontrado.");
        Console.ReadKey();
        return;
    }

    SaveMemory.Clear();
    SaveMemory.AddRange(File.ReadAllLines(caminhoArquivo));
}

// =====================================================
void ClampCursor()
{
    if (SaveMemory.Count == 0)
        SaveMemory.Add("");

    if (CursorLinha < 1)
        CursorLinha = 1;

    if (CursorLinha > SaveMemory.Count)
        CursorLinha = SaveMemory.Count;

    int tamanhoLinha = SaveMemory[CursorLinha - 1].Length;

    if (CursorColuna < 0)
        CursorColuna = 0;

    if (CursorColuna > tamanhoLinha)
        CursorColuna = tamanhoLinha;
}

// =====================================================
void Exibir_arquivo()
{
    LimparTela();

    int linha = 1;

    if (modo)
        Console.WriteLine(dirty
            ? "\x1b[33mModo de Edição *\x1b[0m\n"
            : "Modo de Edição\n");
    else
        Console.WriteLine(dirty
            ? "\x1b[33mModo de Visualização *\x1b[0m\n"
            : "Modo de Visualização\n");

    foreach (var texto in SaveMemory)
    {
        bool linhaAtiva = (linha == CursorLinha);
        bool mostrarSeta = (linhaAtiva && !modo);

        if (mostrarSeta)
            Console.Write($"\x1b[32m>{linha,3} | \x1b[0m");
        else
            Console.Write($"{linha,3} | ");

        for (int coluna = 0; coluna <= texto.Length; coluna++)
        {
            if (modo && linhaAtiva && coluna == CursorColuna)
            {
                Console.Write("\x1b[30;42m");

                if (coluna < texto.Length)
                    Console.Write(texto[coluna]);
                else
                    Console.Write(' ');

                Console.Write("\x1b[0m");
                continue;
            }

            if (coluna < texto.Length)
                Console.Write(texto[coluna]);
        }

        Console.WriteLine();
        linha++;
    }

    if (!modo)
    {
        Console.WriteLine("\n-- Esc sair | K subir | J descer | I editar");
    }
    else
    {
        Console.WriteLine("\n-- Esc voltar | F2 para salvar");
    }
}

// =====================================================
void SalvarArquivo()
{
    if (string.IsNullOrEmpty(caminhoArquivo))
        return;

    File.WriteAllLines(caminhoArquivo, SaveMemory);
    arquivoModificado = false;
    dirty = false;
}

// =====================================================
void LoopEditor()
{
    while (true)
    {
        Exibir_arquivo();
        var tecla = Console.ReadKey(true);

        // ===== SALVAR (F2) =====
        if (modo && tecla.Key == ConsoleKey.F2)
        {
            SalvarArquivo();
            Console.Write($"\x1b[{Console.WindowHeight};1H");
            Console.Write("\x1b[32mArquivo salvo com sucesso!\x1b[0m");
            Thread.Sleep(600);
            continue;
        }

        // ===== ESC =====
        if (tecla.Key == ConsoleKey.Escape)
        {
            if (!modo)
            {
                if (arquivoModificado)
                {
                    Console.Write($"\x1b[{Console.WindowHeight};1H");
                    Console.Write("Sair sem salvar? (S/N) ");
                    var resp = Console.ReadKey(true);
                    if (resp.Key == ConsoleKey.S)
                        break;
                    continue;
                }
                break;
            }

            modo = false;
        }

        // ===== MODO NORMAL =====
        if (!modo)
        {
            if (tecla.Key == ConsoleKey.K) CursorLinha--;
            if (tecla.Key == ConsoleKey.J) CursorLinha++;
            if (tecla.Key == ConsoleKey.I) modo = true;
        }

        // ===== MODO EDIÇÃO =====
        if (modo)
        {
            int index = CursorLinha - 1;
            string linhaAtual = SaveMemory[index];

            if (!char.IsControl(tecla.KeyChar))
            {
                SaveMemory[index] = linhaAtual.Insert(CursorColuna, tecla.KeyChar.ToString());
                CursorColuna++;
                dirty = arquivoModificado = true;
            }
            else if (tecla.Key == ConsoleKey.Backspace)
            {
                if (CursorColuna > 0)
                {
                    SaveMemory[index] = linhaAtual.Remove(CursorColuna - 1, 1);
                    CursorColuna--;
                    dirty = arquivoModificado = true;
                }
                else if (CursorLinha > 1)
                {
                    SaveMemory[index - 1] += linhaAtual;
                    SaveMemory.RemoveAt(index);
                    CursorLinha--;
                    CursorColuna = SaveMemory[CursorLinha - 1].Length;
                    dirty = arquivoModificado = true;
                }
            }
            else if (tecla.Key == ConsoleKey.Enter)
            {
                SaveMemory[index] = linhaAtual[..CursorColuna];
                SaveMemory.Insert(index + 1, linhaAtual[CursorColuna..]);
                CursorLinha++;
                CursorColuna = 0;
                dirty = arquivoModificado = true;
            }
            else if (tecla.Key == ConsoleKey.Delete)
            {
                if (CursorColuna < linhaAtual.Length)
                {
                    SaveMemory[index] = linhaAtual.Remove(CursorColuna, 1);
                }
                else if (CursorLinha < SaveMemory.Count)
                {
                    SaveMemory[index] += SaveMemory[index + 1];
                    SaveMemory.RemoveAt(index + 1);
                }
                dirty = arquivoModificado = true;
            }
            else if (tecla.Key == ConsoleKey.Home) CursorColuna = 0;
            else if (tecla.Key == ConsoleKey.End) CursorColuna = SaveMemory[CursorLinha - 1].Length;
            else if (tecla.Key == ConsoleKey.LeftArrow)
            {
                if (CursorColuna > 0) CursorColuna--;
                else if (CursorLinha > 1)
                {
                    CursorLinha--;
                    CursorColuna = SaveMemory[CursorLinha - 1].Length;
                }
            }
            else if (tecla.Key == ConsoleKey.RightArrow)
            {
                if (CursorColuna < linhaAtual.Length) CursorColuna++;
                else if (CursorLinha < SaveMemory.Count)
                {
                    CursorLinha++;
                    CursorColuna = 0;
                }
            }
        }

        ClampCursor();
    }
}

// =====================================================
int opc;
do
{
    Console.WriteLine("====== Editor de texto ======");
    Console.WriteLine("Opção 1: Acessar arquivo");
    Console.WriteLine("Opção 0: Sair");
    opc = Convert.ToInt32(Console.ReadLine());
    Console.Clear();

    if (opc == 1)
    {
        Ler_arquivo();
        CursorColuna = SaveMemory[CursorLinha - 1].Length;
        LoopEditor();
    }

} while (opc != 0);

Console.Write("\x1b[?25h"); // mostra cursor
Console.Write("\x1b[0m");   // reset ANSI

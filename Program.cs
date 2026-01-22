using System;
using System.Collections.Generic;
using System.IO;


// \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
List<string> SaveMemory = new List<string>();

int CursorLinha = 1;
int CursorColuna = 0;
bool modo = false;
// \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
void Ler_arquivo()
{
    const string filepath = "C:\\EditText\\Arquivo_teste.txt";
    SaveMemory.Clear();

    var data = File.ReadAllLines(filepath);
    SaveMemory.AddRange(data);
}
// \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
void Exibir_arquivo()
{
    Console.Clear();

    int linha = 1;
// =======================================================================================================
    if (modo)
        Console.WriteLine("Modo de Visualização\n");
    else
        Console.WriteLine("Modo de Edição\n");
// =======================================================================================================
    foreach (var texto in SaveMemory)
    {
        bool linhaAtiva = (linha == CursorLinha);
        bool mostrarSeta = (linhaAtiva && modo == false);
// =======================================================================================================
        // ---- número da linha ----
        if (mostrarSeta)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($">{linha,3} | ");
            Console.ResetColor();
        }
        else
        {
            Console.Write($"{linha,3} | ");
        }
    // =======================================================================================================

        // ---- conteúdo da linha + cursor de coluna ----
        for (int coluna = 0; coluna <= texto.Length; coluna++)
        {
// =======================================================================================================
            // cursor em bloco (modo edição)
            if (modo == true && linhaAtiva && coluna == CursorColuna)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.DarkGreen;

                if (coluna < texto.Length)
                    Console.Write(texto[coluna]);
                else
                    Console.Write(' '); // cursor no final da linha

                Console.ResetColor();
                continue;
            }
// =======================================================================================================
            if (coluna < texto.Length)
                Console.Write(texto[coluna]);
// =======================================================================================================
        }

        Console.WriteLine();
        linha++;
    }
// =======================================================================================================
    // ---- rodapé ----
    if (modo == false)
    {
        Console.WriteLine("\n-- Pressione Esc para sair");
        Console.WriteLine("-- Pressione K para subir");
        Console.WriteLine("-- Pressione J para descer");
        Console.WriteLine("-- Pressione I para acessar o modo de edição");
    }
    else
    {
        Console.WriteLine("\n-- Pressione Esc para voltar ao modo de visualização");
    }
// =======================================================================================================
}


// \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
void LoopEditor()
{
    while (true)
    {
        Exibir_arquivo();

        var tecla = Console.ReadKey(true);

// =======================================================================================================
        if (tecla.Key == ConsoleKey.Escape)
        {
// ------------------------------------------------------------------------------------------------------
            if (modo == false)
            {
                break;

            }
            else
            {
                modo = false;
            }
            Console.Clear();
// ------------------------------------------------------------------------------------------------------
        }
// =======================================================================================================
        if (modo == false)
        {
// ------------------------------------------------------------------------------------------------------

            if (CursorLinha > 1 && tecla.Key == ConsoleKey.K)
            {
                CursorLinha--;
                int index = CursorLinha - 1;
                if (CursorColuna > SaveMemory[index].Length)
                    CursorColuna = SaveMemory[index].Length;
            }
// ------------------------------------------------------------------------------------------------------
            if (CursorLinha < SaveMemory.Count && tecla.Key == ConsoleKey.J)
            {
                CursorLinha++;
                int index = CursorLinha - 1;
                if (CursorColuna > SaveMemory[index].Length)
                    CursorColuna = SaveMemory[index].Length;
            }
// ------------------------------------------------------------------------------------------------------
        }

// =======================================================================================================
        if (modo == false)
        {
// ------------------------------------------------------------------------------------------------------
            if (tecla.Key == ConsoleKey.I)
            {
                modo = true;
            }
// ------------------------------------------------------------------------------------------------------
        }

// =======================================================================================================
        if (modo == true)
        {
// ------------------------------------------------------------------------------------------------------
            if (!char.IsControl(tecla.KeyChar))
            {
                int index = CursorLinha - 1;
                SaveMemory[index] = SaveMemory[index].Insert(CursorColuna, tecla.KeyChar.ToString());
                CursorColuna++;
            }
// ------------------------------------------------------------------------------------------------------
        }
// =======================================================================================================
        if (modo == true && tecla.Key == ConsoleKey.Backspace)
        {
            int index = CursorLinha - 1;
// ------------------------------------------------------------------------------------------------------
            if (CursorColuna > 0)
            {
                SaveMemory[index] = SaveMemory[index].Remove(CursorColuna - 1, 1);
                CursorColuna--;
            }
// ------------------------------------------------------------------------------------------------------
        }
// =======================================================================================================
        if (modo == true && tecla.Key == ConsoleKey.Enter)
        {
            int index = CursorLinha - 1;

            string linhaAtual = SaveMemory[index];

            string esquerda = linhaAtual.Substring(0, CursorColuna);
            string direita = linhaAtual.Substring(CursorColuna);

            SaveMemory[index] = esquerda;
            SaveMemory.Insert(index + 1, direita);

            CursorLinha++;
            CursorColuna = 0;
        }
// =======================================================================================================
        if (modo == true && tecla.Key == ConsoleKey.Delete)
        {
            int index = CursorLinha - 1;
            string linhaAtual = SaveMemory[index];
// ------------------------------------------------------------------------------------------------------
            if (CursorColuna < linhaAtual.Length)
            {
                SaveMemory[index] = linhaAtual.Remove(CursorColuna, 1);
            }
            else if (CursorLinha < SaveMemory.Count)
            {
                string proximaLinha = SaveMemory[index + 1];
                SaveMemory[index] = linhaAtual + proximaLinha;
                SaveMemory.RemoveAt(index + 1);
            }
 // ------------------------------------------------------------------------------------------------------
        }
// =======================================================================================================
        if (modo == true)
        {
            int index = CursorLinha - 1;
            int tamanhoLinha = SaveMemory[index].Length;
            int finalLinha = SaveMemory[CursorLinha - 1].Length;

// ------------------------------------------------------------------------------------------------------
            if (tecla.Key == ConsoleKey.LeftArrow)
            {
                if (CursorColuna > 0)
                {
                    CursorColuna--;
                }
                else if (CursorLinha > 1)
                {
                    CursorLinha--;
                    CursorColuna = SaveMemory[CursorLinha - 1].Length;
                }
            }
// ------------------------------------------------------------------------------------------------------
            if (tecla.Key == ConsoleKey.RightArrow)
            {
                if (CursorColuna < tamanhoLinha)
                {
                    CursorColuna++;
                }
                else if (CursorLinha < SaveMemory.Count)
                {
                    CursorLinha++;
                    CursorColuna = 0;
                }
            }
// ------------------------------------------------------------------------------------------------------

        }
// =======================================================================================================
    }
}
// \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

int opc;

do
{
    Console.WriteLine("====== Editor de texto ======");
    Console.WriteLine("Opção 1: Ler um arquivo");
    Console.WriteLine("Editar");
    Console.WriteLine("Excluir");
    Console.WriteLine("Opção 0: Sair");
    opc = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("");
    Console.Clear();

    switch (opc)
    {
        case 1:
            if (SaveMemory.Count == 0)
            {
                Ler_arquivo();
                CursorColuna = SaveMemory[CursorLinha - 1].Length;
                LoopEditor();

            }
            else
            {
                LoopEditor();

            }

            break;

        case 2:

            break;

        case 3:

            break;
    }
} while (opc != 0);
// \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
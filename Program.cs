using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

List<string> SaveMemory = new List<string>();

int CursorLinha = 1;
int CursorColuna = 0;

void Ler_arquivo()
{
    const string filepath = "C:\\EditText\\Arquivo_teste.txt";
    SaveMemory.Clear();

    var data = File.ReadAllLines(filepath);
    SaveMemory.AddRange(data);
    
}
void Exibir_arquivo()
{
    Console.Clear();
    
    int linha = 1;
    foreach (var texto in SaveMemory)
    { 
        if(linha == CursorLinha)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($">{linha,3} | {texto}");
            linha++;
            Console.ResetColor();
            
        }
        else
        {
            Console.WriteLine($"{linha,3} | {texto}");
            linha++;
            
        }
        
    }

    Console.WriteLine("\n-- Precione a tecla Esc para sair");
    Console.WriteLine("\n-- Precione a tecla K para subir");
    Console.WriteLine("\n-- Precione a tecla J para descer");
}

void LoopEditor()
{
    while (true)
    {
        Exibir_arquivo();

        var tecla = Console.ReadKey(true);
       

        if (tecla.Key == ConsoleKey.Escape)
            break;
        Console.Clear();

        if(CursorLinha > 1)
        {
            if(tecla.Key == ConsoleKey.K)
            {
                CursorLinha --;
            }
        }

        if(CursorLinha < SaveMemory.Count)
        {
            if(tecla.Key == ConsoleKey.J)
            {
                CursorLinha ++;
            }
        }
      
    }
}


int opc;

do {
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
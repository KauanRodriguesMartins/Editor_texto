void Exibir_arquivo()
{
    const string filepath = "C:\\EditText\\Arquivo_teste.txt";

    var data = File.ReadAllLines(filepath);
    var count = 0;

    foreach (var linha in data)
    {
        count++;
        Console.WriteLine($"Linha {count} - {linha}");
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
            Exibir_arquivo();
        break;

        case 2:

        break;

        case 3:

        break;
    }





} while (opc != 0);
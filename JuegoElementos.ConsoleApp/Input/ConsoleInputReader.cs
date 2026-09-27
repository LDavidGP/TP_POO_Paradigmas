namespace JuegoElementos.ConsoleApp.Input;

public static class ConsoleInputReader
{
    public static int ReadOption(int min, int max)
    {
        while (true)
        {
            var input = Console.ReadLine();

            if (int.TryParse(input, out var value) && value >= min && value <= max)
            {
                return value;
            }

            Console.Write($" Entrada inválida. Ingrese un número entre {min} y {max}: ");
        }
        }
    }
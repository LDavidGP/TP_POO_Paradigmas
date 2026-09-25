namespace JuegoElementos.ConsoleApp.Input;

public static class ConsoleInputReader
{
    public static int ReadOption(int min, int max)
    {
        while (true)
        {
            var keyInfo = Console.ReadKey(intercept: true);

            if (char.IsDigit(keyInfo.KeyChar))
            {
                var value = keyInfo.KeyChar - '0';

                if (value >= min && value <= max)
                {
                    return value;
                }
            }
        }
    }
}
namespace PhoneBook;

public interface IConsoleReader
{
    string ReadLine();
    void Write(string line);
    void WriteLine(string line);
}

public class ConsoleReader : IConsoleReader
{
    public string ReadLine()
    {
        return Console.ReadLine();
    }

    public void Write(string line)
    {
        Console.Write(line);
    }

    public void WriteLine(string line)
    {
        Console.WriteLine(line);
    }
}
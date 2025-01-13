namespace Programmers;

public interface IRunner
{
    public void Solution();
}

public abstract class Runner : IRunner
{
    public abstract void Solution();

    protected void Start(string title) => Console.WriteLine($"================= start [{title}] =================");

    protected void End(string title)
    {
        Console.WriteLine($"================= end [{title}] =================");
        Console.WriteLine();
    }
}
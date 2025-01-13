namespace Programmers;

public interface IRunner
{
    public void Solution();
}

public abstract class Runner : IRunner
{
    public abstract void Solution();
}
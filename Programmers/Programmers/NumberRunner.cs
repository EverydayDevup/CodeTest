namespace Programmers;

public class NumberRunner : Runner
{
    public override void Solution()
    {
        var primeNumberTest = Number.IsPrimeNumber(7);
        Console.WriteLine($"{nameof(primeNumberTest)}: {primeNumberTest}");

        var sieveOfEratosthenesTest = Number.SieveOfEratosthenes(10);
        var sieveOfEratosthenesResult = new List<int> { 2,3,5,7 };
        
        Console.WriteLine($"{nameof(sieveOfEratosthenesTest)} : {sieveOfEratosthenesTest.SequenceEqual(sieveOfEratosthenesResult)}");
    }
}
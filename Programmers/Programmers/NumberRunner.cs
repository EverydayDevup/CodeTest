namespace Programmers;

public class NumberRunner : Runner
{
    public override void Solution()
    {
        var primeNumberTest = Number.IsPrimeNumber(7);
        Start($"{nameof(primeNumberTest)}");
        Console.WriteLine($"{nameof(primeNumberTest)}: {primeNumberTest}");
        End($"{nameof(primeNumberTest)}");

        var sieveOfEratosthenesTest = Number.SieveOfEratosthenes(10);
        Start($"{nameof(sieveOfEratosthenesTest)}");
        var sieveOfEratosthenesResult = new List<int> { 2,3,5,7 };
        Console.WriteLine($"{nameof(sieveOfEratosthenesTest)} : {sieveOfEratosthenesTest.SequenceEqual(sieveOfEratosthenesResult)}");
        End($"{nameof(sieveOfEratosthenesTest)}");
    }
    
    
}
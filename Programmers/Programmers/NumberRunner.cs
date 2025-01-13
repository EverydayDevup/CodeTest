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

        var intervalSum = Number.IntervalSum(new[] { 2, 4, 6, 8 }, 1, 2);
        Start($"{nameof(intervalSum)}");
        Console.WriteLine($"{nameof(intervalSum)}: {intervalSum == 6}");
        End($"{nameof(intervalSum)}");
        
        var isSqrtNum = Number.IsSquareNumber(4);
        Start($"{nameof(isSqrtNum)}");
        Console.WriteLine($"{nameof(isSqrtNum)}: {isSqrtNum}");
        End($"{nameof(isSqrtNum)}");
        
        var isCompositeNumber = Number.IsCompositeNumber(4);
        Start($"{nameof(isCompositeNumber)}");
        Console.WriteLine($"{nameof(isCompositeNumber)}: {isCompositeNumber}");
        End($"{nameof(isCompositeNumber)}");
        
        var factorial = Number.Factorial(4);
        Start($"{nameof(factorial)}");
        Console.WriteLine($"{nameof(factorial)}: {factorial == 24}");
        End($"{nameof(factorial)}");
    }
    
    
}
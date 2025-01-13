namespace Programmers;

/// <summary>
/// 숫자와 관련된 처리
/// </summary>
public static class Number
{
    // 소수 판정 알고리즘
    // 소수의 정의 : 1보다 큰 자연수 중에서 1과 자기 자신을 제외한 자연수로는 나누어 떨어지지 않는 자연수
    // 자연수란 : 0을 포함하지 않는, 1부터 시작하여 하나씩 더하여 얻는 수를 통틀어 이르는 말
    // 약수 : 어떤 수를 나누어 떨어지게 하는 수
    // 6 : 1, 2, 3, 6 으로 나누어 떨어짐으로 소수가 아님, 7 : 1, 7 소수
    // 모든 약수가 가운데 약수를 기준으로 곱셈 연산에 대칭하는 특징을 가짐
    // 16 : 2 * 8 <-> 8 * 2
    // 가운데 약수까지만 확인하면 약수를 찾을 수 있음
    // 시간복잡도 O(n1/2)
    public static bool IsPrimeNumber(int number)
    {
        if (number <= 1)
            return false;
        
        var range = Math.Sqrt(number) + 1;
        for (int i = 2; i < range; i++)
        {
            if (number % i == 0)
                return false;
        }
        
        return true;
    }
    
    // 에라토스테네스의체알고리즘 : 특정 범위의 수 범위안에서 존재하는 모든 소수를 찾는 방법
    // N보다 작거나 같은 모두 소수를 찾을 때 사용
    // 알고리즘
    // 1. 2 ~ N까지의 모든 자연수를 나열
    // 2. 남은 수 중에서 아직 처리하지 않은 가장 작은 수 i를 찾음
    // 3. 남은 수 중에서 i의 배수를 모두 제거 (i는 제거하지 않음)
    // 4. 더 이상 반복할 수 없을 때까지 2, 3을 반복
    // 시간 복잡도 O(NlonglogN)
    public static List<int> SieveOfEratosthenes(int number)
    {
        // 숫자를 인덱스로 맵핑하여 소수를 판정하기 때문에 +1 을함
        var isPrimeChecked = new bool[number + 1];
        for (int i = 2; i <= number; i++)
            isPrimeChecked[i] = true;

        var range = Math.Sqrt(number) + 1;
        for (int i = 2; i < range; i++)
        {
            if (!isPrimeChecked[i]) 
                continue;
            
            // i x i 이전의 배수는 이미 처리됨
            // j += i 로 i의 배수 체크 가능
            for (int j = i * i; j <= number; j += i)
                isPrimeChecked[j] = false;
        }
        
        var answer = new List<int>();
        for (var i = 0; i < isPrimeChecked.Length; i++)
        {
            if (isPrimeChecked[i])
                answer.Add(i);
        }
        
        return answer;
    }
    
    // 구간 합 : 연속적으로 나열된 N개의 수가 있을 때 특정 구간의 모든 수를 합한 값을 계산하는 문제
    // 배열의 맨아래부터 특정 위치까지의 합을 선 계산
    // 0 : (A1), 1 : (A1) + (A2), 2 :(A1 + A2) + A3, 3 :(A1 + A2 + A3) + A4
    // 특정 구간의 합을 구할 경우 [1, 3] = 3 - 0 => (A1 + A2 + A3) + A4 - A1
    public static int IntervalSum(int[]? numbers, int start, int end)
    {
        if (numbers == null || numbers.Length == 0)
            return 0;
        
        if (start <= 0)
            return 0;
        
        if (end > numbers.Length)
            return 0;

        if (start > end)
            return 0;
        
        var prefix = new int[numbers.Length + 1];
        prefix[0] = 0;
        for (int i = 0; i < numbers.Length; i++)
            prefix[i+1] = prefix[i] + numbers[i];
        
        return prefix[end] - prefix[start -1];
    }
    
    // n이 제곱수인가의 여부 확인
    // 제곱수란 어떤 자연수를 두 번 곱해서 나오는 정수
    public static bool IsSquareNumber(int n)
    {
        if (n < 1)
            return false;
        
        var sqrt = (int)Math.Sqrt(n);
        return sqrt * sqrt == n;
    }
    
    // 합성수 구하기
    // 합성수란 : 1보다 큰 자연수 중에서 소수가 아닌 수, 1과 자신을 제외한 다른 약수가 존재하는 수
    // 시간 복잡도는 O(sqrt(n))
    public static bool IsCompositeNumber(int n)
    {
        if (n <= 2) return false; // 2 이하는 합성수가 아님 
        
        var divisorCount = 0; // 약수의 개수

        // 제곱근으로 계산하면 계산을 절반으로 줄일 수 있음
        for (int i = 1; i * i <= n; i++)
        {
            if (n % i == 0)
            {
                divisorCount++; // i는 약수
                
                if (i != n / i)
                    divisorCount++; // n / i도 약수
            }

            if (divisorCount > 2) // 약수 개수가 3개 이상이면 합성수
                return true;
        }

        return false; // 약수 개수가 2 이하라면 합성수가 아님
    }
    
    // 입력된 숫자의 각 자리수에 특정한 액션을 할려고 할 경우 
    // % 10으로 1의 자릿수를 구하여 계산하고,
    // /10 으로 1의 자릿수를 없앰
    public static int ActionOnDigits(int order)
    {
        if (order <= 0)
            return 0;
        
        while (order > 0)
        {
            var value = order % 10;
            // todo
            
            order /= 10;
        }

        return 0;
    }

    // 팩토리얼이란 : 수학에서 자연수 이하의 모든 자연수를 차례로 곱한 값을 의미 n!
    // 0! = 1
    public static int Factorial(int n)
    {
        if (n < 0)
            return 0;
        
        var result = 1;
        for (int i = 1; i <= n; i++)
            result *= i;

        return result;
    }    
    
    // 소인수분해란 어떤 수를 소수들의 곱으로 표현하는 것 
    // 12 => 2 * 2 * 3 따라서 12의 소인수는 2와3
    public static List<int> PrimeFactorization(int n)
    {
        var factors  = new List<int>();
        var divide = 2;

        while (n > 1)
        {
            // 합성수는 배수로 처리되어 나눠졌기 때문에 divide는 소수가됨
            while (n % divide == 0)
            {
                factors.Add(divide);
                n /= divide;
            }
            
            divide++;
            if (divide * divide <= n || n <= 1)
                continue;
            
            factors.Add(n);
            break;
        }

        return factors;
    }
    
    // 조합 : n개 중에서 순서에 관계없이 r개를 뽑는 경우의 수
    public static long Combination(int n, int r)
    {
        // 뽑을 것이 없거나, 이미 있는것과 뽑을 것의 개수가 같은 경우
        if (r == 0 || n == r)
            return 1;

        // n개의 항목 중 r개를 고르는 경우의 수와, n개의 항목중 n-r개를 고르는 경우의 수가 동일함
        r = Math.Min(r, n - r);
        long result = 1;
        for (int i = 1; i <= r; i++)
            result = result * (n - i + 1) / i;

        return result;
    }
}
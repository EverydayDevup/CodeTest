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
    
}
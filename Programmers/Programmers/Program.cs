using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Text;

namespace Programmers;

class Program
{
    private static List<Runner> _runners = new List<Runner>();
    
    static void Main(string[] args)
    {
        _runners.Add(new NumberRunner());
        
        foreach (var runner in _runners)
        {
            runner.Solution();
        }
    }
    
    public int 카테고리별로조합하는경우의수(string[,] clothes)
    {
        var dic = new Dictionary<string, List<string>>();
        
        for (int i = 0; i < clothes.GetLength(0); i++) // 행 순회
        {
            var category = clothes[i, 1];
            if (dic.TryGetValue(category, out var list))
            {
                list = new List<string>();
                dic.Add(category, list);
            }
            
            dic[category].Add(clothes[i, 0]);
        }

        // 특정 카테고리에서 아이템이 N개 있다면, 해당 카테고리를 선택하는 경우는 N + 1
        // 아이템을 선택하는 경우, 해당 카테고리를 선택하지 않는 경우 
        // 아무것도 착용하지 않는 경우를 제외함
        var answer = 1;
        foreach (KeyValuePair<string, List<string>> pair in dic)
        {
            answer *= (pair.Value.Count + 1);
        }
        return answer - 1;
    }
    
    private long 조합공식(int n, int r)
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
    
    // 배열의 크기를 계산할 때
    // var arr = new string[(my_str.Length + n - 1)/n];
    public string[] 잘라서배열로저장하기(string my_str, int n)
    {
        var remain = my_str.Length % n;
        var divide = my_str.Length / n;
        var arr = new string[divide + (remain > 0 ? 1 : 0)];
        for (int i = 0; i < divide; i++)
            arr[i] = my_str.Substring(i * n, n);
        
        if (remain > 0)
            arr[divide] = my_str.Substring(divide * n, remain);
        
        return arr;
    }
    
    // Convert.ToInt32(str, 2)를 하면 문자열을 정수로 변환할 때 2진법으로 표현된 숫자로 나타나게됨
    public string 이진수의합으로구하기(string bin1, string bin2) {
        var num1 = Convert.ToInt32(bin1, 2);
        var num2 = Convert.ToInt32(bin2, 2);
        
        var sum = num1 + num2;
        return Convert.ToString(sum, 2);
    }
    
    // 소인수분해란 어떤 수를 소수들의 곱으로 표현하는 것 
    // 12 => 2 * 2 * 3 따라서 12의 소인수는 2와3
    public List<int> 소인수분해(int n)
    {
        var list = new List<int>();
        int divide = 2;

        while (n > 1)
        {
            while (n % divide == 0)
            {
                list.Add(n);
                n /= divide;
            }
            
            divide++;
            if (divide * divide > n)
            {
                if (n > 1)
                {
                    list.Add(n);
                    break;
                }
            }
        }

        return list;
    }
    
    
    // char - '0'을 하면 숫자로 계산할 수 있음
    
    // linq Count는 개수를 세는데 사용함
    
    // Array.Sort가 Linq보다 나음
    // Array.Sort는 퀵소트, 히소트 알고리즘을 사용하여 nLogn의 복잡도를 가지고, 배열을 제자리에서 정렬함으로 추가 메모리 할당이 없음
    // Linq는 안정 정렬 알고리즘을 사용하는데, 추가적인 메모리할당과 객체 생성이 발생함
    
    // my_string안의 자연수들의 합, 연속된 숫자 
    // char는 '0' ~ '9' 에 대한 부등호가 가능, '0'을 빼면 숫자가 나옴
    public int 문자열에서자연수의합구하기(string my_string)
    {
        int number = 0;
        int sum = 0;
        foreach (var c in my_string)
        {
            if (c >= '0' && c <= '9')
            {
                number = number * 10 + (c - '0');
            }
            else
            {
                sum += number;
                number = 0;
            }
        }

        // 마지막이 숫자로 끝나는 경우에 대한 예외처리가 필요함
        sum += number;

        return sum;
    }
    
    //정수n이 주어질때 n을 넘지않는 최대 정수
    public int 팩토리얼계산(int n) {
        var result = 1;
        var i = 1;
        while (result <= n)
        {
            i++;
            result *= i;
        }

        return i - 1;
    }    
    
    // 약수의 개수가 3개 이상인 수를 합성수라고함
    private static bool 합성수구하기(int num)
    {
        if (num < 2) return false; // 2 이하는 합성수가 아님 
        
        var divisorCount = 0;

        // 제곱근으로 계산하면 계산을 절반으로 줄일 수 있음
        for (int i = 1; i * i <= num; i++)
        {
            if (num % i == 0)
            {
                divisorCount++; // i는 약수
                
                if (i != num / i)
                    divisorCount++; // num / i도 약수
            }

            if (divisorCount > 2) // 약수 개수가 3개 이상이면 합성수
                return true;
        }

        return false; // 약수 개수가 2 이하라면 합성수가 아님
    }
    
    // string에서 indexOf를 쓰면 문자열에서 특정 위치를 가져올 수 있음 
    public int num에서k의위치찾기(int num, int k) {
        var index = num.ToString().IndexOf(k.ToString());
        return index == -1 ? -1 : index + 1;
    }
    
    // % 연산자를 사용할 때 원래 값이 0일 수도 있으니 해당 부분은 확인해야함
    // 숫자에 각 자리수로 무엇인가 할때는 % 10을 하면됨
    public int order에각자리가3의배수인지확인(int order) {

        var answer = 0;
        while (order > 0)
        {
            var value = order % 10;
            if (value != 0 && value % 3 == 0)
                answer++;
            
            order = order / 10;
        }

        return answer;
    }
    
    // 숫자에서 자리수를 구할 때 숫자 -> toString으로 해서 array로 변화해서 접근하는 방법이 있음
    public string 각자리수의숫자를특정문자로교환(int age)
    {
        // 나머지연산자로 10을 구해서 일의자리수를 구함
        // string answer = "";
        //
        // // 나이가 0보다 클 때까지 반복
        // while (age > 0) {
        //     // 1의 자리 숫자를 알파벳으로 변환하여 앞에 추가
        //     answer = (char)(age % 10 + 97) + answer;
        //
        //     // 나머지를 제외한 나머지 부분으로 나이를 줄임
        //     age /= 10;
        // }
        
        return new string(age.ToString().Select(ch => (char)('a' + (ch - '0'))).ToArray());
    }
    
    // sqrt의 결과는 dobule이기 때문에 1의 나머지가 나오면 소수점이됨
    public int 어떤수가제곱수인지판단하는방법(int n) {
        return Math.Sqrt(n) % 1 == 0 ? 1 : 2;
    }
    
    public int t시간만큼n이2배씩증가하는경우(int n, int t) {
        return n << t;
    }

    public string 문자열에서특정문자제거하기(string my_string, string letter)
    {
        return my_string.Replace(letter, "");
    }
    
    // where는 조걸에 맞는 요소만 반환
    public int[] 배수가아닌인덱스제거(int n, int[] numlist)
    {
        return numlist.Where(num => num % n == 0).ToArray();
    }
    
    // 가장 큰수 찾기
    public int[] 가장큰수찾기(int[] array)
    {
        var maxValue = array.Max();
        var index = Array.IndexOf(array, maxValue);
        
        return new int[] { maxValue, index };
    }
    
    // 약수란 어떤수를 나누어 떨어지게하는 수
    // 6 = 1,2,3,6
    public int[] 약수구하기(int n) {
      
        return Enumerable.Range(0, n).Where(x => n % x == 0).ToArray();
    }
    
    public int 최대값찾기(int[] numbers)
    {
        var order = numbers.OrderBy(x => x).ToArray();
        var orderValue = order[0] * order[1];
        var descValue = order[order.Length -1] * order[order.Length -2];
        
        return orderValue > descValue ? orderValue : descValue;
    }
    
    // 1 ~ n까지 홀수가 오름차순인 배열
    public int[] 홀수가오름차순으로담긴배열(int n) {
        var arr = new int[(n+1)/2];
        for (int i = 0, iCount = arr.Length; i < iCount; i++)
        {
            arr[i] = (i << 1) + 1;
        }

        return arr;
    }
    
    public int 두배열의같은원소의수를구하는방법(string[] s1, string[] s2)
    {
        return s1.Count(txt =>s2.Contains(txt));
    }
    
    // n인 자연수의 약수를 구해서 처리함
    // 약수는 항상 쌍으로 존재 
    // a x b = n / a <= sqrt(n) b >= sqrt(n) 관계를 가짐
    public int 순서쌍구하기(int n)
    {
        var answer = 0;
        
        for (int a = 1; a * a <= n; a++) // a <= sqrt(n)까지만 반복
        {
            if (n % a == 0)
            {
                answer++; // (a, b)에서 a를 추가
                if (a != n / a) // a와 b가 다르면 b도 추가
                {
                    answer++;
                }
            }
        }

        return answer;
    }
    
    public static string 문자열뒤집기(string my_string)
    {
        return new string(my_string.Reverse().ToArray());
    }
    
    // new string을 하면 같은 문자를 n번 반복해서 만들 수 있음 
    public string 문자반복하기(string my_string, int n)
    {
        var sb = new StringBuilder();
        sb.Capacity = my_string.Length * n;
        foreach (var c in my_string)
            sb.Append(new string(c, n));
        
        return sb.ToString();
    }
    
    // 가장 긴 선분의 길이가 다른 두 선분의 길이의 합보다 작아야함
    public static int 선분세개로삼격항만들수있는조건인지확인(int[] sides)
    {
        var arr =sides.OrderBy(x => x).ToArray();
        return (arr[0] + arr[1]) > arr[2] ? 1 : 2;
    }
    
    // num1 ~ num2까지 자른 배열 반환
    public int[] 배열자르기(int[] numbers, int num1, int num2)
    {
        // Skip 은 배열의 앞에서부터 start 개수 만큼 건너띔
        // Take 는 Skip 이후의 배열에서 n개수만큼 요소를 가져옴
        return numbers.Skip(num1).Take(num2 - num1 + 1).ToArray();
    }
    
    public int 배열에서두개를곱해최대값을구하는방법(int[] numbers)
    {
        var arr =numbers.OrderByDescending(x => x).ToArray();
        return arr[0] * arr[1];
    }
    
    // 0~n까지의 수 중에서 짝수 구하기
    public static int 짝수구하기(int n) {
        return n/2 * ((n/ 2) + 1);
    }
    
    // 두수의 곱을 구하는 문제, 곱하기를 사용하지 않은 경우
    private static int 두수의곱구하기(int num1, int num2)
    {
        var result = 0;

        while (num2 > 0)
        {
            if ((num2 & 1) != 0)
                result += num1;

            num1 = num1 << 1;
            num2 = num2 >> 1;
        }
        
        // 1
        // num1 = 0011 num2 = 0010
        // 0010 & 0001 = 0000
        // num1 = 0110
        // num2 = 0001
        
        // 2
        // 0001 & 0001 = 0000
        // result = 0110
        // num1 = 1100
        // num2 = 0000
        
        // 3
        // result = 12
        
        return result;
    }
}


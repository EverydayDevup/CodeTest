using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Text;

namespace Programmers;

public class Runner
{
    public string solution(string letter)
    {
        return string.Empty;
    }
}

class Program
{
    private static Runner _runner = new Runner();
    
    static void Main(string[] args)
    {
       Console.WriteLine(_runner.solution(""));
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


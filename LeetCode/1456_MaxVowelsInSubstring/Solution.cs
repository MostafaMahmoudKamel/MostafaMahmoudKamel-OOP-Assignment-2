namespace LeetCode;

public class Solution
{
    public static int MaxVowels(string s, int k)
    {
        int maxVowels = 0;
        int windowVowels = 0;

        bool IsVowel(char c) => c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u';

        for(int i = 0; i < k; i++)
        {
            if (IsVowel(s[i])) windowVowels++;
        }

        maxVowels = windowVowels;

        for(int j = k; j < s.Length; j++)
        {
            //check if char previous is Vowel 
            if (IsVowel(s[j - k]))
            {
                windowVowels--;
            }
            if (IsVowel(s[j]))
            {
                windowVowels++;
            }
            if (windowVowels > maxVowels)
            {
                maxVowels = windowVowels;
            }
            if (maxVowels == k) break;
        }



        return maxVowels;
    }

    //public static int CountVowel(string s) {
    //    int count = 0;
    //    for(int i = 0; i < s.Length; i++)
    //    {
    //        if (s[i] == 'a' || s[i] == 'e' || s[i] == 'i' || s[i] == 'o' || s[i] == 'u')
    //        {
    //            count++;
    //        }
    //    }
        //return count;
    //}


    static void Main(string[] args)
    {
        var x = MaxVowels("abciiidef", 3);
        Console.WriteLine(x);
    }


}

class x
{


}
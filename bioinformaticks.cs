using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

class BioInformaticsLab
{
    // Метод для получения подстрок
    public static List<string> GetSubstrings(string text)
    {
        List<string> result = new List<string>();
        for (int len = 2; len <= 6; len++)
        {
            for (int i = 0; i <= text.Length - len; i++)
            {
                result.Add(text.Substring(i, len));
            }
        }
        return result;
    }

    // Метод для чтения файла и его чистки
    public static string ReadFile(string path)
    {
        try
        {
            return Regex.Replace(File.ReadAllText(path), @"[\d\s]", "");
        }
        catch (IOException)
        {
            return "Не получилось";
        }
    }

    // Метод для нахождения минимальной по длине подстроки в множестве
    public static string FindMin(HashSet<string> substrings)
    {
        string min = null;
        foreach (string s in substrings)
        {
            if (min == null || s.Length < min.Length)
            {
                min = s;
            }
        }
        return min;
    }

    // Метод для нахождения максимальной общей подпоследовательности двух строк
    public static string FindMaxCommon(string s1, string s2)
    {
        string maxCommon = "";
        for (int i = 0; i < s1.Length; i++)
        {
            for (int j = i + maxCommon.Length + 1; j <= s1.Length; j++)
            {
                string sub = s1.Substring(i, j - i);
                if (s2.Contains(sub))
                {
                    maxCommon = sub;
                }
                else
                {
                    break;
                }
            }
        }
        return maxCommon;
    }

    // Метод для поиска уникальных подстрок в первой строке, отсутствующих во второй
    public static HashSet<string> FindUniqueInFirst(List<string> subs1, List<string> subs2)
    {
        HashSet<string> unique = new HashSet<string>();
        foreach (string sub in subs1)
        {
            if (!subs2.Contains(sub))
            {
                unique.Add(sub);
            }
        }
        return unique;
    }

    // Метод для поиска общих подстрок между двумя списками
    public static HashSet<string> FindCommon(List<string> subs1, List<string> subs2)
    {
        HashSet<string> common = new HashSet<string>();
        foreach (string sub in subs1)
        {
            if (subs2.Contains(sub))
            {
                common.Add(sub);
            }
        }
        return common;
    }

    // Метод для поиска подстрок в первой строке, отсутствующих во второй и третьей
    public static HashSet<string> FindMissingInSecondAndThird(List<string> subs1, List<string> subs2, List<string> subs3)
    {
        HashSet<string> missing = new HashSet<string>();
        foreach (string sub in subs1)
        {
            if (!subs2.Contains(sub) && !subs3.Contains(sub))
            {
                missing.Add(sub);
            }
        }
        return missing;
    }

    static void Main(string[] args)
    {
        string dna1 = ReadFile(@"C:\Users\JefTheMax\Desktop\UCHEBAGG_2\Code C#\example1.txt");
        string dna2 = ReadFile(@"C:\Users\JefTheMax\Desktop\UCHEBAGG_2\Code C#\example2.txt");
        string dna3 = ReadFile(@"C:\Users\JefTheMax\Desktop\UCHEBAGG_2\Code C#\example3.txt");

        List<string> subs1 = GetSubstrings(dna1);
        List<string> subs2 = GetSubstrings(dna2);
        List<string> subs3 = GetSubstrings(dna3);

        HashSet<string> unique1 = FindUniqueInFirst(subs1, subs2);
        HashSet<string> unique2 = FindUniqueInFirst(subs2, subs1);
        HashSet<string> common12 = FindCommon(subs1, subs2);
        HashSet<string> missingIn12 = FindMissingInSecondAndThird(subs3, subs1, subs2);
        HashSet<string> missingIn23 = FindMissingInSecondAndThird(subs1, subs2, subs3);
        string minUnique1 = FindMin(unique1);
        string minUnique2 = FindMin(unique2);
        string minCommon12 = FindMin(common12);
        string minMissing23 = FindMin(missingIn23);
        string minMissing12 = FindMin(missingIn12);
        string maxCommon13 = FindMaxCommon(dna1, dna3);
        double ratio = (double)maxCommon13.Length / dna1.Length;

        Console.WriteLine("Мин. уникальная для 1, отсутствующая в 2: " + minUnique1);
        Console.WriteLine("Мин. уникальная для 2, отсутствующая в 1: " + minUnique2);
        Console.WriteLine("Мин. общая для 1 и 2: " + minCommon12);
        Console.WriteLine("Мин. уникальная для 1, отсутствующая в 2 и 3: " + minMissing23);
        Console.WriteLine("Мин. уникальная для 3, отсутствующая в 1 и 2: " + minMissing12);
        Console.WriteLine("Макс. общая подпоследовательность для 1 и 3: " + maxCommon13);
        Console.WriteLine("Соотношение длины общей подпоследовательности к длине 1: " + ratio.ToString("0.0000"));
    }
}
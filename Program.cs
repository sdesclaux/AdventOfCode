﻿using AdventOfCode;

internal class Program
{
    private static void Main()
    {
        var (year, dayInt) = (2023, 9);

        string day = dayInt.ToString("D2");
        string filePath = $"{AppDomain.CurrentDomain.BaseDirectory}/{year}/Day{day}/input{day}.txt";
        string filePathTest = $"{AppDomain.CurrentDomain.BaseDirectory}/{year}/Day{day}/input{day}test.txt";
        string input = GetCleanInput(filePath);
        string inputTest = GetCleanInput(filePathTest);

        string errorMessage = $"Day {day} / Year {year} doesn't exist, please create file to solve";

        var instanceType = Type.GetType($"AdventOfCode.Day{day}Year{year}");
        if (instanceType != null)
        {
            IResultGenerator? instance = Activator.CreateInstance(instanceType) as IResultGenerator;
            if (instance != null)
            {
                if (!String.IsNullOrEmpty(inputTest))
                {
                    Console.WriteLine($"Result of part 1 test is : {instance.GetResultPart1(inputTest)}");
                    Console.WriteLine($"Result of part 2 test is : {instance.GetResultPart2(inputTest)}");
                }
                if (!String.IsNullOrEmpty(input))
                {
                    Console.WriteLine($"Result of part 1 is : {instance.GetResultPart1(input)}");
                    Console.WriteLine($"Result of part 2 is : {instance.GetResultPart2(input)}");
                }
            }
            else
            {
                Console.WriteLine(errorMessage);
            }
        }
        else
        {
            Console.WriteLine(errorMessage);
        }        
    }

    private static string GetCleanInput(string file)
    {
        var input = File.ReadAllText(file);

        input = input.Replace("\r", "");
        input = input.Trim();

        if (input.EndsWith("\n"))
        {
            input = input.Substring(startIndex: 0, length: input.Length - 1);
        }
        return input;
    }    
}

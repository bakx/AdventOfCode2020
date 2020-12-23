using System;
using System.IO;
using System.Linq;

namespace Day1
{
    public class Program
    {
        public static void Main()
        {
            // Read entries
            StreamReader reader = new StreamReader("input.txt");
            
            int[] items = reader
                .ReadToEnd()
                .Split(Environment.NewLine)
                .Select(int.Parse)
                .ToArray();

            reader.Close();

            foreach (int i in items)
            {
                foreach (int j in items)
                { 
                    if (i + j == 2020)
                    {
                        Console.WriteLine($"The answer for part #1 is {i} * {j} = {i * j} ");
                    }

                    foreach (int k in items)
                    {
                        if (i + j + k != 2020)
                        {
                            continue;
                        }

                        Console.WriteLine($"The answer for part #2 is {i} * {j} * {k} = {i * j * k} ");
                        break;
                    }
                }
            }
        }
    }
}

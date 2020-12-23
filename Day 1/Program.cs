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
                    if (i + j != 2020)
                    {
                        continue;
                    }

                    Console.WriteLine($"The answer is {i} * {j} = {i * j} ");
                    break;
                }
            }
        }
    }
}

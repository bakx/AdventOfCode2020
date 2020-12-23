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
            int[] items = reader.ReadToEnd().Split(Environment.NewLine).Select(int.Parse).ToArray();
            reader.Close();

            for (int i = 0; i < items.Count(); i++)
            {
                for (int j = 0; j < items.Count(); j++)
                {
                    if (items[i] + items[j] != 2020)
                    {
                        continue;
                    }

                    Console.WriteLine($"The answer is {items[i]} * {items[j]} = {items[i] * items[j]} ");
                    break;
                }
            }
        }
    }
}

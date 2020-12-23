using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day2
{
    public class Program
    {
        public static void Main()
        {
            // Read entries
            StreamReader reader = new StreamReader("input.txt");

            List<PasswordPolicy> passwordPolicies = reader
                .ReadToEnd()
                .Split(Environment.NewLine)
                .Select(e => new PasswordPolicy(e))
                .ToList();

            reader.Close();

            int validPolicy1 = 0;
            int invalidPolicy1 = 0;
            int validPolicy2 = 0;
            int invalidPolicy2 = 0;

            foreach (PasswordPolicy passwordPolicy in passwordPolicies)
            {
                // Part #1
                int countChars = CountChars(passwordPolicy.Character, passwordPolicy.Password);

                // Character has to exist at least N times between the range 
                if (countChars < passwordPolicy.MinRange || countChars > passwordPolicy.MaxRange)
                {
                    invalidPolicy1++;
                }
                else
                {
                    validPolicy1++;
                }

                // Part #2
                string firstChar = passwordPolicy.Password.Substring(passwordPolicy.MinRange, 1);
                string secondChar = passwordPolicy.Password.Substring(passwordPolicy.MaxRange, 1);

                // Not allowed to be same character
                if (firstChar == secondChar)
                {
                    invalidPolicy2++;
                }
                // One of these has to match the position
                else if (firstChar == passwordPolicy.Character || secondChar == passwordPolicy.Character)
                {
                    validPolicy2++;
                }
                else
                {
                    invalidPolicy2++;
                }
            }

            Console.WriteLine($"Part #1: Valid {validPolicy1}, invalid {invalidPolicy1}");
            Console.WriteLine($"Part #2: Valid {validPolicy2}, invalid {invalidPolicy2}");
        }

        public static int CountChars(string character, string password)
        {
            int count = 0;
            int i = 0;
            while ((i = password.IndexOf(character, i, StringComparison.Ordinal)) != -1)
            {
                i += character.Length;
                count++;
            }
            return count;
        }
    }

    public class PasswordPolicy
    {
        public PasswordPolicy(string line)
        {
            string[] initialSplit = line.Split(':');
            string[] policySplit = initialSplit[0].Split(' ');
            MinRange = Convert.ToInt32(policySplit[0].Split('-')[0]);
            MaxRange = Convert.ToInt32(policySplit[0].Split('-')[1]);
            Character = policySplit[1];
            Password = initialSplit[1];
        }

        public int MinRange { get; set; }
        public int MaxRange { get; set; }
        public string Character { get; set; }
        public string Password { get; set; }
    }
}

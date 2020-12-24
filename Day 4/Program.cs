using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Day4
{
    public class Program
    {
        public static void Main()
        {
            // Read entries
            StreamReader reader = new StreamReader("input.txt");

            List<Passport> passports = new List<Passport>();

            StringBuilder builder = new StringBuilder();
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrEmpty(line))
                {
                    // Process passport
                    passports.Add(new Passport(builder.ToString()));
                    builder.Clear();
                }

                builder.AppendLine(line);
            }

            // Process final passport
            passports.Add(new Passport(builder.ToString()));


            // Get valid passports
            Console.WriteLine($"Part #1: Valid passports {passports.Count(p => p.HasAllFieldsPart1)}");
            Console.WriteLine($"Part #2: Valid passports {passports.Count(p => p.HasAllFieldsPart2)}");
        }
    }

    public class Passport
    {
        public Passport(string blob)
        {
            string[] data = blob.Replace(Environment.NewLine, " ").Split(' ');

            foreach (string item in data)
            {
                if (item.StartsWith("ecl"))
                {
                    EyeColor = item.Split(':')[1];
                }

                if (item.StartsWith("pid"))
                {
                    PassportId = item.Split(':')[1];
                }

                if (item.StartsWith("eyr"))
                {
                    ExpirationYear = Convert.ToInt32(item.Split(':')[1]);
                }

                if (item.StartsWith("hcl"))
                {
                    HairColor = item.Split(':')[1];
                }

                if (item.StartsWith("byr"))
                {
                    BirthYear = Convert.ToInt32(item.Split(':')[1]);
                }

                if (item.StartsWith("iyr"))
                {
                    IssueYear = Convert.ToInt32(item.Split(':')[1]);
                }

                if (item.StartsWith("cid"))
                {
                    CountryId = Convert.ToInt32(item.Split(':')[1]);
                }

                if (item.StartsWith("hgt"))
                {
                    Height = item.Split(':')[1];
                }
            }
        }

        public bool HasAllFieldsPart1 =>
            BirthYear != default &&
            IssueYear != default &&
            ExpirationYear != default &&
            PassportId != default &&
            Height != default &&
            EyeColor != default &&
            HairColor != default;

        public bool HasAllFieldsPart2 =>
            ValidBirthYear() &&
            ValidIssueYear() &&
            ValidExpirationYear() &&
            ValidPassportId() &&
            ValidEyeColor() &&
            ValidHairColor() &&
            ValidHeight();

        private bool ValidBirthYear()
        {
            return BirthYear != default && BirthYear >= 1920 && BirthYear <= 2002;
        }

        private bool ValidIssueYear()
        {
            return IssueYear != default && IssueYear >= 2010 && IssueYear <= 2020;
        }

        private bool ValidExpirationYear()
        {
            return ExpirationYear != default && ExpirationYear >= 2020 && ExpirationYear <= 2030;
        }

        private bool ValidPassportId()
        {
            return PassportId != default && PassportId.Length == 9 && Regex.IsMatch(PassportId, "[0-9]{9}");
        }

        private bool ValidEyeColor()
        {
            return EyeColor != default && EyeColor == "amb" || EyeColor == "blu" ||
                   EyeColor == "brn" || EyeColor == "gry" ||
                   EyeColor == "grn" || EyeColor == "hzl" || EyeColor == "oth";
        }

        private bool ValidHairColor()
        {
            return HairColor != default && HairColor.Length == 7 && Regex.IsMatch(HairColor, "^#[0-9a-f]{6}");
        }

        public bool ValidHeight()
        {
            if (Height == default)
            {
                return false;
            }

            // Unable to parse
            if (!int.TryParse(Height.Replace("cm", "").Replace("in", ""), out int parsedHeight))
            {
                return false;
            }

            if (Height.EndsWith("cm"))
            {
                return parsedHeight >= 150 && parsedHeight <= 193;
            }

            if (Height.EndsWith("in"))
            {
                return parsedHeight >= 59 && parsedHeight <= 76;
            }

            return false;
        }

        public int BirthYear { get; set; }
        public int IssueYear { get; set; }
        public int ExpirationYear { get; set; }
        public string PassportId { get; set; }
        public int CountryId { get; set; }
        public string Height { get; set; }
        public string EyeColor { get; set; }
        public string HairColor { get; set; }
    }
}
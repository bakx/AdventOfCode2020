using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day3
{
    public class Program
    {
        public static void Main()
        {
            // Read entries
            StreamReader reader = new StreamReader("input.txt");

            List<ForestLine> lines = reader
                .ReadToEnd()
                .Split(Environment.NewLine)
                .Select(e => new ForestLine(e))
                .ToList();

            reader.Close();

            List<Directions> directions = new List<Directions>()
            {
                new Directions {X = 1, Y = 1},
                new Directions {X = 3, Y = 1},
                new Directions {X = 5, Y = 1},
                new Directions {X = 7, Y = 1},
                new Directions {X = 1, Y = 2}
            };


            foreach (Directions direction in directions)
            {
                int directionX = 1;

                Debug.WriteLine($"Starting with direction : X {direction.X} Y: {direction.Y}");

                for (int index = 0; index < lines.Count; index += direction.Y)
                {
                    ForestLine line = lines[index];

                    if (index > 0)
                    {
                        directionX += direction.X;
                    }

                    int checkPosition;
                    if (directionX > line.Line.Length)
                    {
                        // Recalculate the position, taking the endless duplication of the field in the same pattern
                        // into account
                        checkPosition = directionX % line.Line.Length;
                        if (checkPosition == 0)
                        {
                            checkPosition = line.Line.Length;
                        }
                    }
                    else
                    {
                        checkPosition = directionX;
                    }

                    // Next check if 0 based index.
                    checkPosition--;

                    Debug.WriteLine($"X: {checkPosition + 1} Y: {index + 1} Character: {line.Line[checkPosition]}");

                    if (line.Line[checkPosition] == '#')
                    {
                        direction.TreeCount++;
                    }
                }
            }

            Console.WriteLine("Final results:");

            foreach (Directions direction in directions)
            {
                Console.WriteLine($"X: {direction.X} Y: {direction.Y} : Tree's encountered {direction.TreeCount}");
            }

            int totalTrees = directions[0].TreeCount;
            for (int i = 1; i < directions.Count; i++)
            {
                totalTrees *= directions[i].TreeCount;
            }

            Console.WriteLine($"Answer: {totalTrees}");
        }
    }

    public class ForestLine
    {
        public ForestLine(string line)
        {
            Line = line;
        }

        public string Line { get; set; }
    }

    public class Directions
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int TreeCount { get; set; }
    }
}
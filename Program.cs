using System;
using System.IO;

class Program
{
    static string[] lines;

    static void Main()
    {
        string filePath = "input.csv";
        lines = File.ReadAllLines(filePath);

        while (true)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Display Characters");
            Console.WriteLine("2. Add Character");
            Console.WriteLine("3. Level Up Character");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayAllCharacters(lines);
                    break;
                case "2":
                    AddCharacter(ref lines);
                    break;
                case "3":
                    LevelUpCharacter(lines);
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void DisplayAllCharacters(string[] lines)
    {
        // Skip the header row
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];

            string name;
            int commaIndex;

            // Check if the name is quoted
            if (line.StartsWith("\""))
            {
                commaIndex = line.IndexOf("\",") + 1;
                name = line.Substring(1, commaIndex - 2);
            }
            else
            {
                commaIndex = line.IndexOf(",");
                name = line.Substring(0, commaIndex);
            }

            string[] fields = line.Substring(commaIndex + 1).Split(',');

            string characterClass = fields[0];
            int level = int.Parse(fields[1]);
            int hitPoints = int.Parse(fields[2]);
            string[] equipment = fields[3].Split('|');

            Console.WriteLine($"Name: {name}, Class: {characterClass}, Level: {level}, HP: {hitPoints}, Equipment: {string.Join(", ", equipment)}");
        }
    }

    static void AddCharacter(ref string[] lines)
    {
        Console.Write("Enter character name: ");
        string name = Console.ReadLine();

        Console.Write("Enter character class: ");
        string characterClass = Console.ReadLine();

        Console.Write("Enter character level: ");
        int level = int.Parse(Console.ReadLine());

        Console.Write("Enter character hit points: ");
        int hitPoints = int.Parse(Console.ReadLine());

        Console.Write("Enter character equipment (separated by '|'): ");
        string equipment = Console.ReadLine();

        string newCharacter = $"{name},{characterClass},{level},{hitPoints},{equipment}";
        Array.Resize(ref lines, lines.Length + 1);
        lines[^1] = newCharacter;

        Console.WriteLine("Character added successfully.");
    }

    static void LevelUpCharacter(string[] lines)
    {
        Console.Write("Enter the name of the character to level up: ");
        string nameToLevelUp = Console.ReadLine();

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            string name;
            int commaIndex;

            // Check if the name is quoted
            if (line.StartsWith("\""))
            {
                commaIndex = line.IndexOf("\",") + 1;
                name = line.Substring(1, commaIndex - 2);
            }
            else
            {
                commaIndex = line.IndexOf(",");
                name = line.Substring(0, commaIndex);
            }

            if (name.Equals(nameToLevelUp, StringComparison.OrdinalIgnoreCase))
            {
                string[] fields = line.Substring(commaIndex + 1).Split(',');

                int level = int.Parse(fields[1]);
                level++;
                fields[1] = level.ToString();

                if (line.StartsWith("\""))
                {
                    lines[i] = $"\"{name}\",{string.Join(",", fields)}";
                }
                else
                {
                    lines[i] = $"{name},{string.Join(",", fields)}";
                }

                Console.WriteLine($"Character {name} leveled up to level {level}!");
                break;
            }
        }
    }
}

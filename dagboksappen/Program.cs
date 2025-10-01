using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static List<DiaryEntry> entries = new();
    static Dictionary<DateTime, DiaryEntry> entryDict = new();
    static FileService fileService = new();

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Meny-loop som kör tills användaren väljer att avsluta
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔═══════════════════╗");
            Console.WriteLine("║  Dagboksappen     ║");
            Console.WriteLine("╚═══════════════════╝");
            Console.ResetColor();

            Console.WriteLine("\nVälj ett alternativ:");
            Console.WriteLine("-----------------------");
            Console.WriteLine("[1] Ny anteckning");
            Console.WriteLine("[2] Lista alla");
            Console.WriteLine("[3] Sök på datum");
            Console.WriteLine("[4] Uppdatera anteckning");
            Console.WriteLine("[5] Ta bort anteckning");
            Console.WriteLine("[6] Spara till fil");
            Console.WriteLine("[7] Läs från fil");
            Console.WriteLine("[0] Avsluta");
            Console.WriteLine("-----------------------");
            Console.Write("Ditt val: ");

            switch (Console.ReadLine())
            {
                case "1": AddEntry(); break;
                case "2": ListEntries(); break;
                case "3": SearchEntry(); break;
                case "4": UpdateEntry(); break;
                case "5": DeleteEntry(); break;
                case "6": fileService.Save(entries); Success("Sparat till fil."); break;
                case "7": LoadFromFile(); break;
                case "0": return;
                default: Error("Ogiltigt val."); break;
            }
        }
    }

    static void AddEntry()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════╗");
        Console.WriteLine("║  Ny anteckning   ║");
        Console.WriteLine("╚══════════════════╝");
        Console.ResetColor();

        Console.Write("Datum (åååå-mm-dd): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
        {
            Error("Ogiltigt datum!");
            return;
        }

        Console.Write("Text: ");
        string text = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(text))
        {
            Error("Text får inte vara tom!");
            return;
        }

        var entry = new DiaryEntry { Date = date, Text = text };
        entries.Add(entry);
        entryDict[date] = entry;

        Success("Anteckning sparad.");
    }

    static void ListEntries()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔═════════════════════╗");
        Console.WriteLine("║  Alla anteckningar  ║");
        Console.WriteLine("╚═════════════════════╝");
        Console.ResetColor();

        if (entries.Count == 0)
        {
            Error("Inga anteckningar.");
            return;
        }

        foreach (var e in entries.OrderBy(e => e.Date))
            Console.WriteLine(e);

        Pause();
    }

    static void SearchEntry()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔═══════════════════╗");
        Console.WriteLine("║  Sök anteckning   ║");
        Console.WriteLine("╚═══════════════════╝");
        Console.ResetColor();

        Console.Write("Datum (åååå-mm-dd): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
        {
            if (entryDict.TryGetValue(date, out var entry))
                Success($"Hittad: {entry}");
            else
                Error("Ingen anteckning för detta datum.");
        }
        else
        {
            Error("Ogiltigt datum!");
        }
    }

    static void UpdateEntry()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔═════════════════════════╗");
        Console.WriteLine("║  Uppdatera anteckning   ║");
        Console.WriteLine("╚═════════════════════════╝");
        Console.ResetColor();

        Console.Write("Datum (åååå-mm-dd): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime date) && entryDict.ContainsKey(date))
        {
            Console.Write("Ny text: ");
            string newText = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(newText))
            {
                Error("Text får inte vara tom!");
                return;
            }

            entryDict[date].Text = newText;
            Success("Anteckning uppdaterad.");
        }
        else
        {
            Error("Ingen anteckning för detta datum.");
        }
    }

    static void DeleteEntry()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════╗");
        Console.WriteLine("║  Ta bort anteckning  ║");
        Console.WriteLine("╚══════════════════════╝");
        Console.ResetColor();

        Console.Write("Datum (åååå-mm-dd): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime date) && entryDict.ContainsKey(date))
        {
            var entry = entryDict[date];
            entries.Remove(entry);
            entryDict.Remove(date);
            Success("Anteckning borttagen.");
        }
        else
        {
            Error("Ingen anteckning för detta datum.");
        }
    }

    static void LoadFromFile()
    {
        entries = fileService.Load();
        entryDict = entries.ToDictionary(e => e.Date, e => e);
        Success("Anteckningar laddade från fil.");
    }

    // --- Hjälpmetoder ---
    static void Success(string msg)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(msg);
        Console.ResetColor();
        Pause();
    }

    static void Error(string msg)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(msg);
        Console.ResetColor();
        Pause();
    }

    static void Pause()
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
        Console.ResetColor();
        Console.ReadKey();
    }
}

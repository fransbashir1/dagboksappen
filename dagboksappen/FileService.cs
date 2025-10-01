using System;
using System.Collections.Generic;
using System.IO;

public class FileService
{
    private readonly string filePath = "diary.txt";
    private readonly string errorPath = "error.log";

    public void Save(List<DiaryEntry> entries)
    {
        try
        {
            using var writer = new StreamWriter(filePath);
            foreach (var entry in entries)
            {
                writer.WriteLine($"{entry.Date:yyyy-MM-dd}|{entry.Text}");
            }
        }
        catch (Exception ex)
        {
            LogError(ex);
        }
    }

    public List<DiaryEntry> Load()
    {
        var list = new List<DiaryEntry>();
        try
        {
            if (!File.Exists(filePath))
                return list;

            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split('|');
                if (parts.Length == 2 && DateTime.TryParse(parts[0], out DateTime date))
                {
                    list.Add(new DiaryEntry { Date = date, Text = parts[1] });
                }
            }
        }
        catch (Exception ex)
        {
            LogError(ex);
        }
        return list;
    }

    private void LogError(Exception ex)
    {
        File.AppendAllText(errorPath, $"[{DateTime.Now}] {ex}\n");
    }
}

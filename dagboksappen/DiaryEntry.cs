using System;

public class DiaryEntry
{
    public DateTime Date { get; set; }
    public string Text { get; set; }

    // ToString används när vi skriver ut en anteckning i konsolen
    public override string ToString()
    {
        return $"{Date:yyyy-MM-dd}: {Text}";
    }
}

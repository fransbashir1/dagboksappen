# Dagboksappen

En enkel konsolapplikation skriven i C# där användaren kan skriva, lista, söka, uppdatera och ta bort dagboksanteckningar.  
Anteckningarna sparas i en textfil och kan laddas tillbaka nästa gång programmet körs.  
Fel loggas i en separat loggfil (`error.log`) för felsökning.

---

 Hur man kör appen

1. Klona detta repo:
   ```bash
   git clone <repo-url>
2. cd Dagboksappen/Dagboksappen
3. dotnet run



╔═══════════════════╗
║ Dagboksappen		║
╚═══════════════════╝

Välj ett alternativ:
[1] Ny anteckning
[2] Lista alla
[3] Sök på datum
[4] Uppdatera anteckning
[5] Ta bort anteckning
[6] Spara till fil
[7] Läs från fil
[0] Avsluta

Ditt val:
--------------------------------------------
Ny anteckning

╔══════════════════╗
║  Ny anteckning   ║
╚══════════════════╝

Datum (åååå-mm-dd): 2025-10-01
Text: Idag började jag bygga min dagboksapp!
Anteckning sparad.
---------------------------------------------

Lista alla
╔═════════════════════╗
║  Alla anteckningar  ║
╚═════════════════════╝

2025-10-01: Idag började jag bygga min dagboksapp!
-----------------------------------------------------
Reflektion: 

Jag valde att lagra anteckningarna i både en List och en Dictionary. 
Listan gör det enkelt att skriva ut alla anteckningar i datumordning, medan Dictionary ger snabb sökning på ett specifikt datum. 
För I/O används en enkel textfil med formatet yyyy-MM-dd|Text, vilket gör den lätt att läsa in och parsa.
Jag använder DateTime.TryParse för att validera datum och undvika programkrascher. 
Tomma texter hanteras också för att förhindra meningslösa anteckningar. 
Fel vid filhantering fångas upp med try/catch och loggas till error.
log så användaren inte behöver se tekniska felmeddelanden i konsolen.
Strukturen med separata klasser (DiaryEntry, FileService och Program) gör koden mer överskådlig och enkel att vidareutveckla.
Att logga fel i en separat fil är en bra vana eftersom det skiljer användarvänliga felmeddelanden från utvecklarinformation.
Med detta upplägg uppfylls både grundläggande och avancerade krav på I/O, felhantering och kodstruktur.

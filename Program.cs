using System;
using System.Linq;

class Program
{
    static void Main()
    {
        // Opretter et array af Person objekter
        Person[] people = new Person[]
        {
            new Person { Name = "Jens Hansen", Age = 45, Phone = "+4512345678" },
            new Person { Name = "Jane Olsen", Age = 22, Phone = "+4543215687" },
            new Person { Name = "Tor Iversen", Age = 35, Phone = "+4587654322" },
            new Person { Name = "Sigurd Nielsen", Age = 31, Phone = "+4512345673" },
            new Person { Name = "Viggo Nielsen", Age = 28, Phone = "+4543217846" },
            new Person { Name = "Rosa Jensen", Age = 22, Phone = "+4543217846" },
        };

        // Opgave 1: Udregning og analyse af data vha. LINQ

        // Udregner den samlede alder for alle mennesker ved hjælp af LINQ
        int totalAge = people.Sum(person => person.Age);
        Console.WriteLine($"Den samlede alder for alle mennesker er: {totalAge}");

        // Kristian
        var sumAge = people.Sum(person => person.Age);
        Console.WriteLine(sumAge);
        /* Udregner den samlede alder for alle mennesker. - Med brug af løkker
        int totalAge = 0;
        for (int i = 0; i < people.Length; i++)
        {
            totalAge += people[i].Age;
        }
        */

        // Tæller hvor mange der hedder "Nielsen" ved hjælp af LINQ
        int countNielsen = people.Where(person => person.Name.Contains("Nielsen")).Count();
        Console.WriteLine($"Antallet af personer der hedder 'Nielsen' er: {countNielsen}");

        // Kristian
        var countNielsen1 = people.Count(p => p.Name.Contains("Nielsen"));
        Console.WriteLine(countNielsen1);        /*
        // Tæller hvor mange der hedder "Nielsen" - - Med brug af løkker
        int countNielsen = 0;
        for (int i = 0; i < people.Length; i++)
        {
            if (people[i].Name.Contains("Nielsen"))
            {
                countNielsen++;
            }
        }
        */

        // Tæller alle personer i arrayet
        int countPeople = people.Count();
        Console.WriteLine($"Antallet af personer er {countPeople}");


        // Find den ældste person ved hjælp af LINQ
        Person oldestPerson = people.OrderByDescending(person => person.Age).First();
        Console.WriteLine($"Den ældste person er: {oldestPerson.Name}, {oldestPerson.Age} år gammel.");

        // Find den ældste person ved hjælp af Max() metoden på alderen
        int maxAge = people.Max(person => person.Age);
        // Find den ældste person med denne alder
        Person oldestPerson1 = people.First(person => person.Age == maxAge);
        Console.WriteLine($"Den ældste person er: {oldestPerson1.Name}, {oldestPerson1.Age} år gammel.");

        int minAge = people.Min(person => person.Age);
        Person YougestPerson1 = people.First(person => person.Age == minAge);
        Console.WriteLine($"Den yngeste person er: {YougestPerson1.Name}, {YougestPerson1.Age} år gammel.");

        Person YoungestPerson = people.OrderBy(person => person.Age).First();
        Console.WriteLine($"Den yngeste person er: {YoungestPerson.Name}, {YoungestPerson.Age} år gammel.");

        /*
        // Find den ældste person
        Person oldestPerson = null;
        for (int i = 0; i < people.Length; i++)
        {
            if (oldestPerson == null || people[i].Age > oldestPerson.Age)
            {
                oldestPerson = people[i];
            }
        }
        */



        // hvis gentagelser findes for Lavest alder 
        // Find alle personer med den laveste alder ved hjælp af LINQ (hvis der er flere med samme laveste alder)
        var youngestPeople = people.Where(person => person.Age == people.Min(p => p.Age));
        Console.WriteLine($"De yngste personer er:");
        foreach (var person in youngestPeople)
        {
            Console.WriteLine($"{person.Name}, {person.Age} år gammel.");
        }
        /*
        // Find den yngste person med LINQ
        Person youngestPerson = people.OrderBy(person => person.Age).First();

        // Print the youngest person to the console
        Console.WriteLine($"Den yngste person er: {youngestPerson.Name}, {youngestPerson.Age} år gammel.");
        */

        var PeopleSortedByAge = people.OrderBy(person => person.Age);
        PeopleSortedByAge.ToList().ForEach(person => Console.WriteLine($"{person.Name} {person.Age}"));       


        // Find og udskriv personen med mobilnummer “+4543215687”
        var personWithPhoneNumber = people.FirstOrDefault(person => person.Phone == "+4543215687");
        Console.WriteLine(personWithPhoneNumber.Name);

        /* Find og udskriv personen med mobilnummer “+4543215687” ved hjælp af løkker
        Person personWithPhoneNumber = null;
        foreach (var person in people)
        {
            if (person.Phone == "+4543215687")
            {
                personWithPhoneNumber = person;
                break; // Stop løkken når personen er fundet
            }
        }

        if (personWithPhoneNumber != null)
        {
            Console.WriteLine($"Personen med mobilnummeret +4543215687 er: {personWithPhoneNumber.Name}");
        }
        else
        {
            Console.WriteLine("Ingen personer med det angivne telefonnummer blev fundet.");
        }
        */

        // Vælg alle personer over 30 år og udskriv dem uden løkken
        people.Where(person => person.Age > 30)
              .ToList() // Konverter til en liste
              .ForEach(person => Console.WriteLine($"{person.Name}, {person.Age} år gammel"));

        // Lav et nyt array med de samme personer, men hvor "+45" er fjernet fra alle telefonnumre
        var modifiedPhones = people.Select(person => new Person
        {
            Name = person.Name,
            Age = person.Age,
            Phone = person.Phone.Replace("+45", "")
        }).ToList(); // Konverter til en liste
        Console.WriteLine("Nyt array med telefonnumre uden +45:");
        modifiedPhones.ForEach(person => Console.WriteLine($"{person.Name}, {person.Age} år gammel, Telefon: {person.Phone}"));


        // Generér en string med navn og telefonnummer på de personer, der er yngre end 30, adskilt med komma
        string result = string.Join(", ", people
                                        .Where(person => person.Age < 30)
                                        .Select(person => $"{person.Name} - {person.Phone}"));
        Console.WriteLine("Navne og telefonnumre på personer under 30 år:");
        Console.WriteLine(result);

        var CreateWordFilterFn = (string[] words) =>
        {
            return (string filterBadWords) =>
            {
                return String.Join(" ", filterBadWords.Split(" ").Except(words));
            };
        };


        // Test af CreateWordFilterFn
        var badWords = new string[] { "shit", "fuck", "idiot" };
        var FilterBadWords = CreateWordFilterFn(badWords);
        Console.WriteLine(FilterBadWords("Sikke en gang shit")); // Forventet output: "Sikke en gang"
        Console.WriteLine(FilterBadWords("shit fuck idiot")); // Forventet output: ""

        // 


        var CreateWordReplacerFn = (string[] words, string replacementWord) =>
        {
            return (string text) =>
            {
                foreach (var word in words)
                {
                    text = text.Replace(word, replacementWord);
                }
                return text;
            };
        };

        var ReplaceBadWords = CreateWordReplacerFn(badWords, "kage");
        Console.WriteLine(ReplaceBadWords("Sikke en gang shit")); // Forventet output: "Sikke en gang kage"
        Console.WriteLine(ReplaceBadWords("shit fuck idiot")); // Forventet output: "kage kage kage"


        var personsOlderThan25 = people.Where(person => person.Age > 25);
        personsOlderThan25.ToList().ForEach(person => Console.WriteLine($"{person.Name} er {person.Age} som er ældre end 25 år"));

        /*var personsOlderThan25 = people.Where(person => person.Age > 25);
        foreach (var person in personsOlderThan25)
        {
            Console.WriteLine($"{person.Name} er ældre end 25 år.");
        }*/

        var sortedPeopleByAge = people.OrderBy(person => person.Age);
        sortedPeopleByAge.ToList().ForEach(person => Console.WriteLine($"{person.Name} er {person.Age} år gammel."));
        /*var sortedPeopleByAge = people.OrderBy(person => person.Age);
        foreach (var person in sortedPeopleByAge)
        {
            Console.WriteLine($"{person.Name} er {person.Age} år gammel.");
        }*/
        
        // Gruppér personer efter aldersgruppe og udfør en handling på hver gruppe
        var groupedPeopleByAgeGroup = people.GroupBy(person => person.Age / 10 * 10);
        // Lambda-udtrykket (=>) bruges til at definere en anonym funktion, der tager en gruppe som parameter (group)
        // og udfører en handling på denne gruppe
        // Her repræsenterer 'group' hver gruppe i vores gruppebaserede LINQ-forespørgsel
        // Notationen gør koden mere koncis og lettere at læse, især når vi arbejder med LINQ
        groupedPeopleByAgeGroup.ToList().ForEach(group => Console.WriteLine($"Aldersgruppe: {group.Key} - Antal personer: {group.Count()}"));

        var PersonerOver30 = people.Where(person =>person.Age > 30);
        PersonerOver30.ToList().ForEach(person => Console.WriteLine($"{person.Name} er {person.Age} som er over 30 år"));

        /* første klasses værdi
        var lægToTalSammen = (x, y) => x + y; // En lambda-funktion
        Console.WriteLine(lægToTalSammen(2, 2)); // Den printer "4".
        */

        // Herunder vises det hvor typen er angivet explicit
        Func<int, int, int> lægToTalSammen = (x, y) => x + y;
        Console.WriteLine(lægToTalSammen(2, 2)); // Den printer "4".


        // Praktisk eksempel: Mapping
        int[] array = new int[] { 2, 4, 6, 8, 10 };
        // Anvend Select-metoden til at udføre en operation på hvert element i array'et
        // Lambda-udtrykket (x => x * 2) specificerer, at hvert element 'x' skal ganges med 2
        var res = array.Select(x => x * 2);
        // Udskriv resultatet ved at konvertere det til en streng og sammenføje elementerne med et komma og et mellemrum
        Console.WriteLine(string.Join(", ", res));
        // Printer: 4, 8, 12, 16, 2015 / 24

        // Praktisk eksempel: Filtrering
        int[] array1 = new int[] { 2, 3, 4, 5, 6 };
        // Anvend Where-metoden til at filtrere array'et og kun beholde de lige tal
        // Lambda-udtrykket (x => x % 2 == 0) specificerer, at vi kun vil beholde tal, hvor resten af division med 2 er lig med 0 (altså de lige tal)
        var evenNumbers = array1.Where(x => x % 2 == 0);
        // Udskriv de filtrerede tal ved at konvertere dem til en streng og sammenføje dem med et komma og et mellemrum
        Console.WriteLine(string.Join(", ", evenNumbers));
        // Printer: 2, 4, 6


        // Praktisk eksempel: Aggregering (Samling af sum)
        int[] array2 = new int[] { 2, 3, 4, 5, 6 };
        // Anvend Aggregate-metoden til at beregne den samlede sum af alle elementerne i array'et
        // Lambda-udtrykket (sum, next) => sum + next specificerer, hvordan hvert element i array'et skal kombineres for at opnå den samlede sum
        // 'sum' er det akkumulerede resultat hidtil, og 'next' er det næste element i array'et
        var samletSum = array2.Aggregate((sum, next) => sum + next);
        // Udskriv den samlede sum ved at konvertere den til en streng
        Console.WriteLine(samletSum);
        // Printer: 20

        // Opret en liste af tal
        var numbers = new int[] { 5, 3, 8, 2, 7 };

        // First(): Tager det første element
        var firstNumber = numbers.First();
        Console.WriteLine($"Det første tal er: {firstNumber}");

        // FirstOrDefault(): Her kan man angive en default-værdi hvis listen er tom
        var firstOrDefaultNumber = numbers.FirstOrDefault();
        Console.WriteLine($"Det første tal (eller default hvis listen er tom) er: {firstOrDefaultNumber}");

        // Count(): Tæller antallet af elementer
        var count = numbers.Count();
        Console.WriteLine($"Antallet af tal er: {count}");

        // Min() og Max(): Giver henholdsvis den mindste og største værdi i listen
        var minNumber = numbers.Min();
        var maxNumber = numbers.Max();
        Console.WriteLine($"Det mindste tal er: {minNumber} og det største er {maxNumber}");

        // ToList() og ToArray(): Konverterer en IEnumerable til en liste eller et array
        var numbersList = numbers.ToList();
        var numbersArray = numbers.ToArray();
        Console.WriteLine($"Liste af tal: {string.Join(", ", numbersList)}");
        Console.WriteLine($"Array af tal: {string.Join(", ", numbersArray)}");

        // OrderBy(): Sorterer listen
        var sortedNumbers = numbers.OrderBy(n => n);
        Console.WriteLine($"Sorterede tal: {string.Join(", ", sortedNumbers)}");

        // Range(): Opretter en liste af tal fra start til slut
        var rangeOfNumbers = Enumerable.Range(1, 5);
        Console.WriteLine($"Liste af tal fra 1 til 5: {string.Join(", ", rangeOfNumbers)}");

        // Opret en liste af ord
        var words = new string[] { "apple", "banana", "orange", "grape", "kiwi" };

        // First(): Tager det første element
        var firstWord = words.First();
        Console.WriteLine($"Det første ord er: {firstWord}");

        // FirstOrDefault(): Her kan man angive en default-værdi hvis listen er tom
        var firstOrDefaultWord = words.FirstOrDefault();
        Console.WriteLine($"Det første ord (eller default hvis listen er tom) er: {firstOrDefaultWord}");

        // Count(): Tæller antallet af elementer
        var countword = words.Count();
        Console.WriteLine($"Antallet af ord er: {countword}");

        // Min() og Max(): Giver henholdsvis det første og sidste ord i alfabetisk rækkefølge
        var minWord = words.Min();
        var maxWord = words.Max();
        Console.WriteLine($"Det første ord i alfabetisk rækkefølge er: {minWord}");
        Console.WriteLine($"Det sidste ord i alfabetisk rækkefølge er: {maxWord}");

        // ToList() og ToArray(): Konverterer en IEnumerable til en liste eller et array
        var wordsList = words.ToList();
        var wordsArray = words.ToArray();
        Console.WriteLine($"Liste af ord: {string.Join(", ", wordsList)}");
        Console.WriteLine($"Array af ord: {string.Join(", ", wordsArray)}");

        // OrderBy(): Sorterer ordene efter længde
        var sortedWordsByLength = words.OrderBy(w => w.Length);
        Console.WriteLine($"Ordrækkefølge efter længde: {string.Join(", ", sortedWordsByLength)}");

        // Range(): Opretter en liste af ord fra start til slut
        var rangeOfWords = Enumerable.Range(1, 5).Select(i => $"word{i}");
        Console.WriteLine($"Liste af ord fra 'word1' til 'word5': {string.Join(", ", rangeOfWords)}");

        // Forskelle mellem en liste og et array:

        // 1. Størrelse:
        //    - Et array har en fast størrelse, der ikke kan ændres efter oprettelsen.
        //    - En liste kan vokse dynamisk i størrelse efter behov.

        // 2. Funktionalitet:
        //    - List<T> klassen tilbyder funktioner som Tilføj, Fjern, Indsæt osv., hvilket gør det nemmere at administrere data.
        //    - Arrays har ikke de samme indbyggede funktioner, så manuelle operationer er nødvendige for at ændre størrelsen eller tilføje/fjerne elementer.

        // 3. Ydelse:
        //    - Arrays kan være en smule hurtigere end lister, især med store datamængder, da de er mere kompakte og direkte i hukommelsen.
        //    - Lister kan have ekstra overhead på grund af deres dynamiske natur.

        // 4. Brug af indeksering:
        //    - Arrays indekseres direkte ved hjælp af firkantede parenteser og en indeksværdi (f.eks. myArray[0]).
        //    - I en liste bruger vi metoder som .Add(), .Remove() osv. for at tilføje og fjerne elementer, selvom elementer også kan tilgås ved hjælp af indeksering, f.eks. myList[0].

        var createGreeterFn = (string name) => {
            return () => {
                return "Hello, " + name;
            };
        };
        var greetStuderende = createGreeterFn("studerende");
        var greetUnderviser = createGreeterFn("underviser");
        Console.WriteLine(greetStuderende()); // => "Hello, studerende"
        Console.WriteLine(greetUnderviser()); // => "Hello, underviser"

        // Opret en liste af tal
        List<int> numbersList1 = new List<int> { 1, 2, 3, 4, 5 };

        // Tilføj et element til listen
        numbersList1.Add(6);

        // Fjern et element fra listen
        numbersList1.Remove(3);

        // Konverter listen til et array
        int[] numbersArray1 = numbersList1.ToArray();

        // Udskriv elementerne i listen
        Console.WriteLine("Liste af tal:");
        foreach (var number in numbersList1)
        {
            Console.Write($"{number} ");
        }
        Console.WriteLine();

        // Udskriv elementerne i arrayet
        Console.WriteLine("Array af tal:");
        foreach (var number in numbersArray1)
        {
            Console.Write($"{number} ");
        }
        Console.WriteLine();

        // Opret en liste af ord
        List<string> wordsList3 = new List<string> { "apple", "banana", "orange" };

        // Tilføj et ord til listen
        wordsList3.Add("grape");

        // Fjern et ord fra listen
        wordsList3.Remove("banana");

        // Konverter listen til et array
        string[] wordsArray1 = wordsList3.ToArray();

        // Udskriv elementerne i listen
        Console.WriteLine("Liste af ord:");
        foreach (var word in wordsList3)
        {
            Console.Write($"{word} ");
        }
        Console.WriteLine();

        // Udskriv elementerne i arrayet
        Console.WriteLine("Array af ord:");
        foreach (var word in wordsArray1)
        {
            Console.Write($"{word} ");
        }
        Console.WriteLine();

        // Udskriv antallet af elementer i listen
        Console.WriteLine($"Antal tal i listen: {numbersList1.Count}");
        Console.WriteLine($"Antal ord i listen: {wordsList3.Count}");

        // Sorter listen af tal i faldende rækkefølge
        numbersList1.Sort((a, b) => b.CompareTo(a));

        // Sorter listen af ord i stigende alfabetisk rækkefølge
        wordsList3.Sort();

        // Udskriv de sortererede lister
        Console.WriteLine("Sorterede tal:");
        foreach (var number in numbersList1)
        {
            Console.Write($"{number} ");
        }
        Console.WriteLine();

        Console.WriteLine("Sorterede ord:");
        foreach (var word in wordsList3)
        {
            Console.Write($"{word} ");
        }
        Console.WriteLine();
    }

    // Funktion til at oprette et filter
    // Denne funktion modtager et array af uønskede ord og returnerer en ny funktion,
    // der fjerner de uønskede ord fra en tekststreng.
   /* static Func<string, string> CreateWordFilterFn(string[] words)
    {
        // Returner en funktion, der fjerner uønskede ord fra en tekststreng
        return (text) => string.Join(" ",                // Samler de tilbageværende ord til en tekststreng
                                    text.Split(' ')       // Opdeler den givne tekststreng i ord
                                        .Where(word => !words.Contains(word)));  // Filtrerer ordene og beholder kun dem, der ikke er inkluderet i det uønskede ord array
    }*/

    // Funktion til at oprette en erstatningsfunktion
    // Denne funktion modtager et array af uønskede ord og en erstatningsstreng og returnerer en ny funktion,
    // der erstatter de uønskede ord med erstatningsstrengen i en tekststreng.

    // ! betyder "ikke". Når det bruges sammen med en betingelse (!words.Contains(word)),
    // betyder det, at ordet kun bliver inkluderet i den resulterende streng,
    // hvis det ikke er inkluderet i det uønskede ord array.


}

class Person
{
    public string Name { get; set; } = "";
    public int Age { get; set; }
    public string Phone { get; set; } = "";
}

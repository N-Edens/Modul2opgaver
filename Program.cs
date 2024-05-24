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

        //Opgave 1

        // Udregner den samlede alder for alle mennesker ved hjælp af LINQ
        int totalAge = people.Sum(person => person.Age);
        Console.WriteLine($"Den samlede alder for alle mennesker er: {totalAge}");
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
        /*
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


        //Opgave 2
        // Find og udskriv personen med mobilnummer “+4543215687”
        var personWithPhoneNumber = people.FirstOrDefault(person => person.Phone == "+4543215687");
        if (personWithPhoneNumber != null)
        {
            Console.WriteLine($"Personen med mobilnummeret +4543215687 er: {personWithPhoneNumber.Name}");
        }
        else
        {
            Console.WriteLine("Ingen personer med det angivne telefonnummer blev fundet.");
        }
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

        // Test af CreateWordFilterFn
        var badWords = new string[] { "shit", "fuck", "idiot" };
        var FilterBadWords = CreateWordFilterFn(badWords);
        Console.WriteLine(FilterBadWords("Sikke en gang shit")); // Forventet output: "Sikke en gang kage"
        Console.WriteLine(FilterBadWords("shit fuck idiot")); // Forventet output: ""

        // Test af CreateWordReplacerFn
        var ReplaceBadWords = CreateWordReplacerFn(badWords, "kage");
        Console.WriteLine(ReplaceBadWords("Sikke en gang shit")); // Forventet output: "Sikke en gang kage"
        Console.WriteLine(ReplaceBadWords("shit fuck idiot")); // Forventet output: "kage kage kage"
    }

    // Funktion til at oprette et filter
    static Func<string, string> CreateWordFilterFn(string[] words)
    {
        // Returner en funktion der fjerner uønskede ord fra en tekststreng
        return (text) => string.Join(" ", text.Split(' ').Where(word => !words.Contains(word)));
    }

    // Funktion til at oprette en erstatningsfunktion
    static Func<string, string> CreateWordReplacerFn(string[] words, string replacementWord)
    {
        // Returner en funktion der erstatter uønskede ord med et angivet ord i en tekststreng
        return (text) => string.Join(" ", text.Split(' ').Select(word => words.Contains(word) ? replacementWord : word));
    }
}

    class Person
{
    public string Name { get; set; } = "";
    public int Age { get; set; }
    public string Phone { get; set; } = "";
}

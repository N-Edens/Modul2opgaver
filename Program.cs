using System;
using System.Linq;

class Program
{
    static void Main()
    {
        Person[] people = new Person[]
        {
            new Person { Name = "Jens Hansen", Age = 45, Phone = "+4512345678" },
            new Person { Name = "Jane Olsen", Age = 22, Phone = "+4543215687" },
            new Person { Name = "Tor Iversen", Age = 35, Phone = "+4587654322" },
            new Person { Name = "Sigurd Nielsen", Age = 31, Phone = "+4512345673" },
            new Person { Name = "Viggo Nielsen", Age = 28, Phone = "+4543217846" },
            new Person { Name = "Rosa Jensen", Age = 22, Phone = "+4543217846" },
        };

        // Calculate the total age of all people - Med brug af Linq
        int totalAge = people.Sum(person => person.Age);

        /* Udregner den samlede alder for alle mennesker. - Med brug af løkker
        int totalAge = 0;
        for (int i = 0; i < people.Length; i++)
        {
            totalAge += people[i].Age;
        }
        */

        // Print the total age to the console
        Console.WriteLine($"Den samlede alder for alle mennesker er: {totalAge}");

        // Tæller hvor mange der hedder "Nielsen"  - med brug af Linq
        int countNielsen = people.Where(person => person.Name.Contains("Nielsen")).Count();

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

        // Print the count of people with the name "Nielsen" to the console
        Console.WriteLine($"Antallet af personer der hedder 'Nielsen' er: {countNielsen}");

        // Tæller alle personer
        int countPeople = people.Count();

        // Print the count of all people to the console
        Console.WriteLine($"Antallet af personer er  {countPeople}");

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

        // Find den ældste person med LINQ
        Person oldestPerson = people.OrderByDescending(person => person.Age).First();

        // Print the oldest person to the console
        Console.WriteLine($"Den ældste person er: {oldestPerson.Name}, {oldestPerson.Age} år gammel.");
        /*
        // Find den yngste person med LINQ
        Person youngestPerson = people.OrderBy(person => person.Age).First();

        // Print the youngest person to the console
        Console.WriteLine($"Den yngste person er: {youngestPerson.Name}, {youngestPerson.Age} år gammel.");
        */


       // hvis gentagelser findes for Lavest alder alder

        // Find alle personer med den ældste alder med LINQ
        var Youngestpeople = people.Where(person => person.Age == people.Min(p => p.Age));

        // Print the oldest people to the console
        Console.WriteLine($"De yngeste personer er:");
        foreach (var person in Youngestpeople)
        {
            Console.WriteLine($"{person.Name}, {person.Age} år gammel.");
        }
    }
}

class Person
{
    public string Name { get; set; } = "";
    public int Age { get; set; }
    public string Phone { get; set; } = "";
}

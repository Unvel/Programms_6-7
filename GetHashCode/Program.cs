using System;

public class Person
{
    public string FirstName {get;}
    public string LastName {get;}
    public DateTime DateOfBirth {get;}
    public string PlaceOfBirth {get;}
    public string PassportNumber {get;}

    public Person(string firstName, string lastName, DateTime dateOfBirth, string placeOfBirth, string passportNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        PlaceOfBirth = placeOfBirth;
        PassportNumber = passportNumber;
    }
    public override bool Equals(object obj)
    {
        if (obj is Person other)
        {
              return FirstName == other.FirstName &&
              LastName == other.LastName &&
              DateOfBirth == other.DateOfBirth &&
              PlaceOfBirth == other.PlaceOfBirth &&
              PassportNumber == other.PassportNumber;
        }

        else return false;
    }

    public override int GetHashCode()
    {
        int hash = 0;

        hash = hash * 31 + FirstName.GetHashCode();
        hash = hash * 37 + LastName.GetHashCode();
        hash = hash * 41 + DateOfBirth.GetHashCode();
        hash = hash * 43 + PlaceOfBirth.GetHashCode();
        hash = hash * 47 + PassportNumber.GetHashCode();

        return hash;
    }

    public static void Main(string[] args)
    {
        var person1 = new Person("Robert", "Wilson", new DateTime(1980, 12, 26), "Alaska", "192837");
        var person2 = new Person("Robert", "Wilson", new DateTime(1980, 12, 26), "Alaska", "192837");

        Console.WriteLine("Равны ли person1 и person2? - " + person1.Equals(person2));

        var person3 = new Person("John", "Snow", new DateTime(1980, 1, 1), "Nebraska", "183352");

        Console.WriteLine("Равны ли person1 и person3? - " + person1.Equals(person3));

        Console.WriteLine();

        Console.WriteLine($"Хэш-код person1: {person1.GetHashCode()}");
        Console.WriteLine($"Хэш-код person2: {person2.GetHashCode()}");
        Console.WriteLine($"Хэш-код person2: {person3.GetHashCode()}");

        Console.ReadLine();
    }
}
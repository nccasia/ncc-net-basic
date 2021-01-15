# Polymorphism

We’ve seen inheritance and that we can alter the behavior of a type through inheritance. Our Person class had a GetFullName method which was altered in the subclass Employee. We’ve also seen that whenever, at run-time, an object of type Person is expected we can throw in any subclass of Person, like Employee. This is called polymorphism.

In the following example the PrintFullName method takes an object of type Person, but it prints “Rossel, Sander” because the parameter that’s passed into the method is actually of subtype Employee, which overrides the functionality of GetFullName.

*Polymorphism*:

```C#
class Program
{
    static void Main(string[] args)
    {

        Person p = new Employee();
        p.FirstName = "Sander";
        p.LastName = "Rossel";
        PrintFullName(p);
        // Press any key to quit.
        Console.ReadKey();
    }

    public static void PrintFullName(Person p)

    {
        Console.WriteLine(p.GetFullName());
    }
}

public class Person
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public virtual string GetFullName()

    {
        return FirstName + " " + LastName;
    }
}

public class Employee : Person
{
    public decimal Salary { get; set; }

    public sealed override string GetFullName()

    {
        return LastName + ", " + FirstName;
    }
}
```

## The Takeaway

The Three Pillars of OOP are the foundation of object-oriented programming. They haven’t been implemented for nothing and they do solve real problems. It’s crucial that you know these features by heart and practice them in your daily code. Think about encapsulation every time you create a class or method. Use inheritance when necessary, but don’t forget it brings extra complexity to the table as well. Be very wary of polymorphism, know which code will run when you inherit classes and override methods. Even if you don’t practice it you’ll come across code that does.

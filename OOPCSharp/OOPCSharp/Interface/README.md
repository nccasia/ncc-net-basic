# Interfaces

In the previous chapter, we saw that the class Employee inherits from Person. Do you see any potential issues with this approach? What if a person is an employee, but also a stamp collector? Obviously, a person can also be a stamp collector but not an employee (lots of people are “between jobs”). If all we had was inheritance we might write the following code:

*An inheritance chain*:

```c#
public class Person
{
    // ...
}

public class Employee : Person
{
    // ...
}

public class StampCollector : Person
{

    // ...
}

public class EmployeeStampCollector : Employee
{
    // ...
}
```

I don’t know about you, but it just doesn’t feel right to me. The problem we have is that the only thing the EmployeeStampCollector and the StampCollector have in common is the Person base class, but they’re still both stamp collectors! Writing a method that takes a stamp collector as input is now impossible! This problem could be solved using multiple inheritance, but C# doesn’t have that (and many developers say it is better that way).

So how do we solve this problem? We use an Interface. An Interface is something like an abstract class with only abstract methods. It really doesn’t do more than defining a type. The good part though, is that we can inherit, or ‘implement’, multiple interfaces in a single class!

Let’s look at an example of a random interface

*An interface*:

```c#
public interface ISomeInterface
{
    string SomeProperty { get; set; }

    string SomeMethod();

    void SomethingElse();
}
```

Notice that none of the fields have an access modifier. If a class implements an interface everything on that interface is publicly accessible. This is what the actual implementation would look like:

*Interface implementation*:

```c#
public class SomeClass : ISomeInterface
{
    public string SomeProperty { get; set; }

    public string SomeMethod()
    {
        // ...
    }

    public void SomethingElse()
    {
        // ...
    }
}
```

So here SomeClass has the types object, SomeClass, and ISomeInterface. The “I” prefix on ISomeInterface, or any interface, is common practice in C#, but not necessary.

When using inheritance and interface implementation on a single class, you first specify the base class and then use a comma-separated list of interfaces. How would this look for our Person, Employee and StampCollector example?

*Interfaces*:

```C#
public interface IEmployee
{
    // ...
}

public interface IStampCollector
{
    // ...
}

public class Person
{
    // ...
}

public class Employee : Person, IEmployee
{
    // ...
}

public class StampCollector : Person, IStampCollector
{
    // ...
}

public class EmployeeStampCollector : Employee, IStampCollector
{
    // ...
}
```

Now both StampCollector and EmployeeStampCollector are of type Person and of type IStampCollector. EmployeeStampCollector is also of type Employee (and IEmployee) because it inherits from Employee, which implements IEmployee.

Because a class can implement multiple interfaces it is possible to have an interface inherit from multiple interfaces. A class must then simply implement all interfaces that are inherited by the interface. For obvious reasons, an interface can’t inherit a class.

*Interface inheritance*:

``` C#
public interface IPerson { }

public interface IEmployee : IPerson
{ }

public interface IStampCollector : IPerson
{ }

public interface IEmployeeStampCollector : IEmployee, IStampCollector
{ }
```

Now that’s a very theoretical example, but let’s consider a real world example. Let’s say our application needs to log some information. We may want to log to a database in a production environment, but we’d also like to log to the console for debugging purposes. Additionally, in case the database isn’t available, we’d like to log to the Windows Event logs.

*Loggers Example*:

```C#
class Program
{
    static void Main(string[] args)
    {
        List<ILogger> loggers = new List<ILogger>();
        loggers.Add(new ConsoleLogger());
        loggers.Add(new WindowsLogLogger());
        loggers.Add(new DatabaseLogger());
        foreach (ILogger logger in loggers)
        {
            logger.LogError("Some error occurred.");
            logger.LogInfo("All's well!");
        }
        Console.ReadKey();
    }
}

public interface ILogger
{
    void LogError(string error);

    void LogInfo(string info);
}

public class ConsoleLogger : ILogger
{
    public void LogError(string error)
    {
        Console.WriteLine("Error: " + error);
    }

    public void LogInfo(string info)
    {
        Console.WriteLine("Info: " + info);
    }
}

public class WindowsEventLogLogger : ILogger
{
    public void LogError(string error)
    {
        Console.WriteLine("Logging error to Windows Event log: " + error);
    }

    public void LogInfo(string info)
    {
        Console.WriteLine("Logging info to Windows Event log: " + info);
    }
}

public class DatabaseLogger : ILogger
{
    public void LogError(string error)
    {
        Console.WriteLine("Logging error to database: " + error);
    }

    public void LogInfo(string info)
    {
        Console.WriteLine("Logging info to database: " + info);
    }
}

```

That’s pretty nifty! And as you can imagine, we can add or remove loggers as we please.

By the way, having both an interface and a base class (which implements the interface) is perfectly fine. Maybe you have an ILogger, a DbLogger (which implements ILogger and defines some common behavior for logging to a database) and then have a SqlServerLogger, an OracleLogger, a MySqlLogger, etc. (which all inherit from DbLogger).

## Explicitly Implementing Interfaces

I’ve mentioned that when you implement an interface, all members of that interface are public by default. That’s logical behavior; after all, an interface is a sort of contract that promises other code that it will have some methods and properties defined (because it’s of a certain type). It is, however, possible to hide interface members. To do that, you can explicitly implement an interface. When you explicitly implement an interface member, the only way to invoke that member is by using the class as a type of the interface.

*Explicitly Implemented Interface*:

```C#
public interface ISomeInterface
{
    void MethodA();
    void MethodB();
}

public class SomeClass : ISomeInterface
{
    public void MethodA()
    {
        // Even SomeClass can't invoke MethodB without a cast.
        ISomeInterface me = (ISomeInterface)this;
        me.MethodB();
    }
    // Explicitly implemented interface member.
    // Not visible in SomeClass.
    void ISomeInterface.MethodB()
    {
        throw new NotImplementedException();
    }
}
```

Users of SomeClass can’t invoke MethodB either.

*MethodB is not accessible*:

![image](https://www.syncfusion.com/books/Object_Oriented_Programming_C_Sharp_Succinctly/Images/methodb-is-not-accessible.png)

Unless they use it as, or cast it to, ISomeInterface.

*Invoking an Explicitly Implemented Member*:

```C#
ISomeInterface obj = new SomeClass();
obj.MethodA();
obj.MethodB();
```

## The Takeaway

An interface is a means to create additional types without the need for multiple inheritance. They are extremely useful.

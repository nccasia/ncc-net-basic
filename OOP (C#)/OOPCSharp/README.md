# The Three Pillars of OOP

Object-oriented programming has three characteristics that define the paradigm, the so-called “three pillars of OOP.” In this chapter, we’ll look at each one of them in detail. They all play an important role in the design of your applications. That’s not to say that your systems will have a great design by simply applying these concepts. In fact, there is no single correct way of applying these concepts, and applying them correctly can be difficult.

## Inheritance

**Inheritance** is an important part of any object-oriented language. Classes can inherit from each other, which means the inheriting class gets all of the behavior of the inherited class, also known as the base class. Let’s look at the Person example I used earlier.

***Inheritance*** *Example*:

``` C#
public class Person
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string GetFullName()
    {
        return FirstName + " " + LastName;
    }
}

public class Employee : Person
{
    public decimal Salary { get; set; }
}
```

The trick is in the Employee : Person part. That part basically says “Employee inherits from Person”. And remember everything inherits from Object. In this case Employee inherits from Person and Person (because it’s not explicitly inheriting anything) inherits from Object. Now let’s look at how we can use Employee.

*Subclass usage:*

``` C#
Employee employee = new Employee();
employee.FirstName = "Sander";
employee.LastName = "Rossel";
string fullName = employee.GetFullName();
employee.Salary = 1000000;
```

That’s pretty awesome! We got everything from **Person** just by inheriting from it! In this case we can call **Person** a base class or superclass and **Employee** a subclass. Another common way of saying it is that **Employee** extends **Person**.

There’s a lot more though! Let’s say we’d like to write some common behavior in some base class, but we’d like subclasses to be able to extend or override that behavior. Let’s say we’d like subclasses to change the behavior of **GetFullName** in the previous example. We can do this using the **virtual** keyword in the base class and **override** in the subclass.

*Method overriding*:

```C#
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

    public override string GetFullName()
    {
        string originalValue = base.GetFullName();
        return LastName + ", " + FirstName;
    }
}
```

As you can see we can override, or re-define, **GetFullName** because it was marked **virtual** in the base class. We can then call the original method using the **base** keyword (which points to the implementation of the base class) and work with that, or we can return something completely different. Calling the original base method is completely optional, but keep in mind that some classes may break if you don’t.

Now here’s an interesting thought: **Employee** has the type **Employee**, but it also has the type **Person**. That means that in any code where we need a **Person** we can actually also use an **Employee**. When we do this we can’t, of course, access any **Employee** specific members, such as Salary. So here’s a little question: what will the following code print?

*What will the code print?*

```C#
Person person = new Employee();
person.FirstName = "Sander";
person.LastName = "Rossel";
string fullName = person.GetFullName();
Console.WriteLine(fullName);
// Press any key to quit.
Console.ReadKey();
```

If you answered *“Rossel, Sander”* (rather than *"Sander Rossel"*) you were right!

What else can we do? We can force a subclass to inherit certain members. When we do this we must mark a method, and with that the entire class, as **abstract**. An **abstract** class can’t be instantiated and must be **inherited** (with all abstract members overridden).

*An Abstract Class*:

```C#
public abstract class Person
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public abstract string GetFullName();
}

public class Employee : Person
{
    public decimal Salary { get; set; }

    public override string GetFullName()
    {
        return LastName + ", " + FirstName;
    }
}
```

Of course we can’t make a call to **base.GetFullName()** anymore, as it has no implementation. And while overriding **GetFullName** was optional before, it is **mandatory** now. Other than that the usage of **Employee** stays exactly the same.

On the other end of the spectrum, **we can explicitly state that a class or method may not be inherited or overridden.** We can do this using the **sealed** keyword.

*A Sealed Class*:

```C#
public sealed class Person
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string GetFullName()
    {
        return FirstName + " " + LastName;
    }
}
```

Now that **Person** is **sealed** no one can inherit from it. That means we can’t create an **Employee** class and use **Person** as a base class.

Methods can only be sealed in subclasses. After all, if you don’t want people to override your method, simply don’t mark it **virtual**. However, if you do have a **virtual** method and a subclass wants to prevent further overriding behavior it’s possible to mark it as **sealed**.

*A sealed method*:

```C#
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

No subclass of **Employee** can now override **GetFullName**.

Why would you ever use sealed on your classes or methods? First, there is a small performance gain because the .NET runtime doesn’t have to take overridden methods into account. The gain is negligible though, so that’s not really a good reason. A better reason is perhaps because a class implements some security checks that really shouldn’t be extended in any way.

---
***NOTE:***

Some languages, like C++, know multiple inheritance. That means a subclass can inherit from more than one base class. In C# this is not possible; each subclass can inherit from, at most, one base class.

---

### Inheritance vs. Composition

While **inheritance** is very important in OOP, it can be a real pain, too, especially when you get huge inheritance trees with branches to all sides. There is an alternative to inheritance: composition. Inheritance implies an “is-a” relationship between classes. An Employee is a Person. Using composition, we can define a “has-a” relationship.
Let’s take a car. A car is built up from many components, like an engine. When we model a car do we inherit Car from Engine? That would be problematic, because a car also has doors, chairs and a backseat.

*No Go*:

```C#
public class Engine
{
    // ...
}

public class Car : Engine

{
    // ...
}
```

How would this look when using composition?

*Composition*:

```C#
public class Engine
{
    // ...
}
public class Car
{
    private Engine engine = new Engine();
    // ...
}
```

Car can now use the Engine, but it is not an Engine!

We could’ve used this approach with **Person** and **Employee** as well, but how would we set **FirstName** and **LastName**? We could make **Person** public, but we are now breaking a principle called encapsulation (as discussed in the next chapter). We could mimic **Person** by defining a **FirstName** and **LastName** property, but we now have to change the public interface of **Employee** every time the public interface of **Person** changes. Additionally, **Employee** will not be of type **Person** anymore, so the type of **Employee** changes and it will not be interchangeable with **Person** anymore.

A solution will be presented in last part: **Interfaces**.

## Encapsulation

**Encapsulation** is the process of hiding the internal workings of our classes. That means we specify a public specification, used by consumers of our class, while the actual work is hidden away. The advantage is that a class can change how it works without needing to change its consumers.

In **C#** we have four **access modifiers keywords** which enable five ways of controlling code visibility:

- private — only visible to the containing class.
- protected — only visible to the containing class and inheritors.
- internal — only visible to classes in the same assembly.
- protected internal — only visible to the same assembly or in derived classes, even in other assemblies.
- public — visible to everyone.

Let’s say we’re building some class that runs queries on a database. Obviously we need some method of RunQuery that’s visible to every consumer of our class. The method for accessing the database could be different for every database, so perhaps we’re leaving that open for inheritors. Additionally, we use some helper class that’s only visible to our project. Last, we need to store some private state, which may not be altered from outside our class as it could leave it in an invalid state.

*Access Modifiers*:

```C#
public class QueryRunner
{
    private IDbConnection connection;

    public void RunQuery(string query)
    {
        Helper helper = new Helper();
        if (helper.Validate(query))
        {
            OpenConnection();
            // Run the query...
            CloseConnection();
        }
    }

    protected void OpenConnection()
    {
        // ...
    }

    protected void CloseConnection()
    {
        // ...
    }
}

internal class Helper
{
    internal bool Validate(string query)
    {
        // ...
        return true;
    }
}
```

If we were to compile this into an assembly and access it from another project we’d only be able to see the **QueryRunner** class. If we’d create an instance of the **QueryRunner** we could only call the RunQuery method. If we were to inherit **QueryRunner** we could also access **OpenConnection** and **CloseConnection**. The Helper class and the connection field will be forever hidden from us though.

I should mention that classes can contain nested **private** classes, classes that are only visible to the containing class. Private classes can access **private** members of their containing classes.
Likewise, an object can access **private** members of other objects of the same type.

*A private nested class*:

```C#
public class SomeClass
{
    private string someField;

    public void SomeMethod(SomeClass otherInstance)
    {
        otherInstance.someField = "Some value";
    }

    private class InnerClass
    {
        public void SomeMethod(SomeClass param)
        {
            param.someField = "Some value";
        }
    }
}
```

When omitting an access modifier a default is assigned (internal for classes and private for everything else). I’m a big fan of explicitly adding access modifiers though.

A last remark, before moving on, is that subclasses cannot have an accessibility greater than their base class. So if some class has the internal access modifier an inheriting class cannot be made public (but it could be private).

## Polymorphism

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

## Interfaces

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

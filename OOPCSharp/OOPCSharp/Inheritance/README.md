# Inheritance

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

## Inheritance vs. Composition

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

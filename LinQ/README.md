# Language Integrated Query (LINQ)

Source https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/

Language-Integrated Query (LINQ) is the name for a set of technologies based on the integration of query capabilities directly into the programming language.

Can be used to extract and process data from any objects that implement the IEnumerable\<T\> interface.

The LINQ family of technologies provides a consistent query experience for objects (LINQ to Objects), relational databases (LINQ to SQL), and XML (LINQ to XML).

Concepts you should know before getting to LINQ:
* [Generic types and IEnumerable\<T\> interface](#generic-types-and-ienumerablet-interface)
* [Delegates](#delegates)
* [Lambda Expressions](#lambda-expressions)
* [Extension methods](#extension-methods)

## Generic types and IEnumerable\<T\> interface

Source: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/

* Generics allow you to write general-purpose code that’s type safe at compile time using the same type in multiple places without knowing what that type is beforehand.

  ```
  public class GenericCounter<T>
  {
    private static int value;
    
    static GenericCounter()
    {
      Console.WriteLine("Initializing counter for {0}", typeof(T));
    }

    public static void Increment()
    {
      value++;
    }

    public static void Display()
    {
      Console.WriteLine("Counter for {0}: {1}", typeof(T), value);
    }
  }
  ```
* IEnumerable\<T\> is the interface that enables generic collection classes to be enumerated by using the foreach statement. 

## Delegates

Source: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/

* A type that  represents references to methods with a particular parameter list and return type.
* Are used to pass methods as arguments to other methods.
* Can invoke method using delegates.

  ```
  delegate void Delegate(string str);

  static void ShowString(string str)
  {
    Console.WriteLine(str);  
  }

  static void UseDelegate()
  {
    var del = new Delegate(ShowString);
    del("Classic!");  
  }

  static void UseAnonymousMethod()
  {
    Delegate del = delegate(string str)
    {
      Console.WriteLine(str);
    };
    del("Anonymous");
  }
  ```

## Lambda Expressions

Source: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions

* Shorter way to write anonymous methods.
* Basic syntax: *(input-parameters) => expression*

  ```
  static void UseAnonymousMethod()
  {
    Delegate del = delegate(string str)
    {
      Console.WriteLine(str);
    };
    del("Anonymous");
  }

  static void LambdaExpression() 
  {
    Action<string> del = str => Console.WriteLine(str);
    del("Lambda");
  }
  ```

## Extension methods

Source: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods

* Allow adding methods to defined types without creating a new derived type or modifying the original type.

```
public static class ExtensionMethodsExample
{
  public static string StringExtensionMethod(this string str)
  {
    return $"{str} from extension method";
  }
}

public class ExtensionMethodsExampleRun
{
  static void Main()
  {
    Console.WriteLine("Hello World".StringExtensionMethod());
  }
}
```

## Syntax

### Query syntax

```
var querySyntaxResult =
  from c in Collections.Classes
  join s in Collections.Students
  on c.Id equals s.ClassId into ss
  select new
  {
      ClassName = c.Name,
      FirstStudentName = ss.FirstOrDefault()?.Name ?? "null"
  };
```

### Method syntax

```
var methodSyntaxResult = Collections.Classes
  .GroupJoin(Collections.Students, c => c.Id, s => s.ClassId, (c, ss) => new
  {
      ClassName = c.Name,
      FirstStudentName = ss.FirstOrDefault()?.Name ?? "null"
  });
```

## Standard operations
| Operation | Extension method |
| --- | --- |
| Restriction | Where |
| Projection | Select, SelectMany |
| Partitioning | Take, Skip, TakeWhile, SkipWhile |
| Ordering | OrderBy, OrderByDescending, ThenBy, ThenByDescending, Reverse |
| Grouping | GroupBy |
| Set | Distinct, Union, Intersect, Except |
| Conversion | ToArray, ToList, ToDictionary, OfType |
| Element | First, FirstOrDefault, ElementAt, Last, LastOrDefault |
| Generation | Range, Repeat |
| Quantifier | Any, All |
| Aggregate | Count, Sum, Min, Max, Average, Aggregate |
| Join | Join, GroupJoin |
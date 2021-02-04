# C# - Dynamic Types

C# 4.0 (.NET 4.5) introduced a new type called dynamic that avoids compile-time type checking. A dynamic type escapes type checking at compile-time; instead, it resolves type at run time.

A dynamic type variables are defined using the dynamic keyword

```C#
// Example: dynamic Variable
dynamic MyDynamicVar = 1;
```

The compiler compiles dynamic types into object types in most cases. However, the actual type of a dynamic type variable would be resolved at run-time.

```C#
// Example: dynamic Type at run-time
dynamic MyDynamicVar = 1;
Console.WriteLine(MyDynamicVar.GetType());

// OUT PUT: System.Int32
```

Dynamic types change types at run-time based on the assigned value. The following example shows how a dynamic variable changes type based on assigned value.

```C#
// Example: dynamic
static void Main(string[] args)
{
    dynamic MyDynamicVar = 100;
    Console.WriteLine("Value: {0}, Type: {1}", MyDynamicVar, MyDynamicVar.GetType());

    MyDynamicVar = "Hello World!!";
    Console.WriteLine("Value: {0}, Type: {1}", MyDynamicVar, MyDynamicVar.GetType());

    MyDynamicVar = true;
    Console.WriteLine("Value: {0}, Type: {1}", MyDynamicVar, MyDynamicVar.GetType());

    MyDynamicVar = DateTime.Now;
    Console.WriteLine("Value: {0}, Type: {1}", MyDynamicVar, MyDynamicVar.GetType());
}

/* OUT PUT
Value: 100, Type: System.Int32
Value: Hello World!!, Type: System.String
Value: True, Type: System.Boolean
Value: 01-01-2014, Type: System.DateTime
*/
```

The dynamic type variables is converted to other types implicitly.

```C#
// Example: dynamic Type Conversion
dynamic d1 = 100;
int i = d1;
		
d1 = "Hello";
string greet = d1;
		
d1 = DateTime.Now;
DateTime dt = d1;
```

## Methods and Parameters
If you assign a class object to the dynamic type, then the compiler would not check for correct methods and properties name of a dynamic type that holds the custom class object. Consider the following example.

```C#
// Example: Calling Methods
class Program
{
    static void Main(string[] args)
    {
        dynamic stud = new Student();

        stud.DisplayStudentInfo(1, "Bill");// run-time error, no compile-time error
        stud.DisplayStudentInfo("1");// run-time error, no compile-time error
        stud.FakeMethod();// run-time error, no compile-time error
    }
}

public class Student
{
    public void DisplayStudentInfo(int id)
    {
    }
}
```

In the above example, the C# compiler does not check for the number of parameters, parameters type, or non-existent. It validates these things at run-time, and if it is not valid, then throws a run-time exception. Note that Visual Studio IntelliSense is not supported for the dynamic types.
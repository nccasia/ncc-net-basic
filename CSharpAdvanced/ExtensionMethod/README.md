# C# - Extension Method
Extension methods, as the name suggests, are additional methods. Extension methods allow you to inject additional methods without modifying, deriving or recompiling the original class, struct or interface. Extension methods can be added to your own custom class, .NET framework classes, or third party classes or interfaces.

In the following example, **IsGreaterThan()** is an extension method for int type, which returns true if the value of the int variable is greater than the supplied integer parameter.

```C#
// Example: Extension Method
int i = 10;

bool result = i.IsGreaterThan(100); //returns false 
```

The **IsGreaterThan()** method is not a method of int data type (Int32 struct). It is an extension method written by the programmer for the int data type. The **IsGreaterThan()** extension method will be available throughout the application by including the namespace in which it has been defined.

The extension methods have a special symbol in intellisense of the visual studio, so that you can easily differentiate between class methods and extension methods.

![image](https://i.ibb.co/XkDGH9g/image.png)

An extension method is actually a special kind of static method defined in a static class. To define an extension method, first of all, define a static class.

For example, we have created an **IntExtensions** class under the ExtensionMethods namespace in the following example. The **IntExtensions** class will contain all the extension methods applicable to int data type. (You may use any name for namespace and class.)

```C#
// Example: Create a Class for Extension Methods
namespace ExtensionMethods
{
    public static class IntExtensions
    {

    }
}
```

Now, define a static method as an extension method where the first parameter of the extension method specifies the type on which the extension method is applicable. We are going to use this extension method on int type. So the first parameter must be int preceded with the **this** modifier.

For example, the **IsGreaterThan()** method operates on int, so the first parameter would be, **this int i**.

```C#
// Example: Define an Extension Method
namespace ExtensionMethods
{
    public static class IntExtensions
     {
        public static bool IsGreaterThan(this int i, int value)
        {
            return i > value;
        }
    }
}
```

Now, you can include the ExtensionMethods namespace wherever you want to use this extension method.

``` C#
// Example: Extension method
using ExtensionMethods;

class Program
{
    static void Main(string[] args)
    {
        int i = 10;

        bool result = i.IsGreaterThan(100); 

        Console.WriteLine(result);
    }
}

// OutPut: false
```

> **_Note_**
The only difference between a regular static method and an extension method is that the first parameter of the extension method specifies the type that it is going to operator on, preceded by the this keyword.

> **_Points to Remember_**
- Extension methods are additional custom methods which were originally not included with the class.
- Extension methods can be added to custom, .NET Framework or third party classes, structs or interfaces.
- The first parameter of the extension method must be of the type for which the extension method is applicable, preceded by the **this** keyword.
- Extension methods can be used anywhere in the application by including the namespace of the extension method.

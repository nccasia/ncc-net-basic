# C# Generics

Generic means the general form, not specific. In C#, generic means not specific to a particular data type.

C# allows you to define generic classes, interfaces, abstract classes, fields, methods, static methods, properties, events, delegates, and operators using the type parameter and without the specific data type. A type parameter is a placeholder for a particular type specified when creating an instance of the generic type.

A generic type is declared by specifying a type parameter in an angle brackets after a type name, e.g. TypeName<T> where T is a type parameter.

## Generic Class

Generic classes are defined using a type parameter in an angle brackets after the class name. The following defines a generic class.

```C#
// Example: Define Generic Class
class DataStore<T>
{
    public T Data { get; set; }
}
```

Above, the **DataStore** is a generic class. **T** is called type parameter, which can be used as a type of fields, properties, method parameters, return types, and delegates in the **DataStore** class. For example, **Data** is generic property because we have used a type parameter **T** as its type instead of the specific data type.

> **_NOTE:_**  It is not required to use T as a type parameter. You can give any name to a type parameter. Generally, T is used when there is only one type parameter. It is recommended to use a more readable type parameter name as per requirement like TSession, TKey, TValue etc. Learn more about Type Parameter Naming Guidelines

```C#
// Example: Generic Class with Multiple Type Parameters
class KeyValuePair<TKey, TValue>
{
    public TKey Key { get; set; }
    public TValue Value { get; set; }
}
```

## Instantiating Generic Class

You can create an instance of generic classes by specifying an actual type in angle brackets. The following creates an instance of the generic class DataStore.

```C#
DataStore<string> store = new DataStore<string>();
```

Above, we specified the string type in the angle brackets while creating an instance. So, T will be replaced with a string type wherever T is used in the entire class at compile-time. Therefore, the type of Data property would be a string.

The following figure illustrates how generics works.

![image](https://i.ibb.co/hcL9XC3/generics.png)

You can assign a string value to the Data property. Trying to assign values other than string will result in a compile-time error.

``` C#
DataStore<string> store = new DataStore<string>();
store.Data = "Hello World!";
//obj.Data = 123; //compile-time error
```
You can specify the different data types for different objects, as shown below.

```C#
// Example: Generic class
DataStore<string> strStore = new DataStore<string>();
strStore.Data = "Hello World!";
//strStore.Data = 123; // compile-time error

DataStore<int> intStore = new DataStore<int>();
intStore.Data = 100;
//intStore.Data = "Hello World!"; // compile-time error

KeyValuePair<int, string> kvp1 = new KeyValuePair<int, string>();
kvp1.Key = 100;
kvp1.Value = "Hundred";

KeyValuePair<string, string> kvp2 = new KeyValuePair<string, string>();
kvp2.Key = "IT";
kvp2.Value = "Information Technology";
```

### Generic Class Characteristics
- A generic class increases the reusability. The more type parameters mean more reusable it becomes. However, too much generalization makes code difficult to understand and maintain.
- A generic class can be a base class to other generic or non-generic classes or abstract classes.
- A generic class can be derived from other generic or non-generic interfaces, classes, or abstract classes.

## Generic Fields

A generic class can include generic fields. However, it cannot be initialized.

```C#
// Example: Generic Field
class DataStore<T>
{
    public T data;

    // Generic Array
    public T[] data = new T[10];
}

```

## Generic Methods

A method declared with the type parameters for its return type or parameters is called a generic method.

```C#
// Example: Generic Methods
class DataStore<T>
{
    private T[] _data = new T[10];
    
    public void AddOrUpdate(int index, T item)
    {
        if(index >= 0 && index < 10)
            _data[index] = item;
    }

    public T GetData(int index)
    {
        if(index >= 0 && index < 10)
            return _data[index];
        else 
            return default(T);
    }
}
```

Above, the AddorUpdate() and the GetData() methods are generic methods. The actual data type of the item parameter will be specified at the time of instantiating the DataStore<T> class, as shown below.

```C#
// Example: Generic Methods
DataStore<string> cities = new DataStore<string>();
cities.AddOrUpdate(0, "Mumbai");
cities.AddOrUpdate(1, "Chicago");
cities.AddOrUpdate(2, "London");

DataStore<int> empIds = new DataStore<int>();
empIds.AddOrUpdate(0, 50);
empIds.AddOrUpdate(1, 65);
empIds.AddOrUpdate(2, 89);
```

The generic parameter type can be used with multiple parameters with or without non-generic parameters and return type. The followings are valid generic method overloading.

```C#
// Example: Generic Method Overloading
public void AddOrUpdate(int index, T data) { }
public void AddOrUpdate(T data1, T data2) { }
public void AddOrUpdate<U>(T data1, U data2) { }
public void AddOrUpdate(T data) { }
```

A non-generic class can include generic methods by specifying a type parameter in angle brackets with the method name, as shown below.

```C#
// Example: Generic Method in Non-generic Class
class Printer
{
    public void Print<T>(T data)
    {
        Console.WriteLine(data);
    }
}

Printer printer = new Printer();
printer.Print<int>(100);
printer.Print(200); // type infer from the specified value
printer.Print<string>("Hello");
printer.Print("World!"); // type infer from the specified value
```

## Advantages of Generics

- Generics increase the reusability of the code. You don't need to write code to handle different data types.
- Generics are type-safe. You get compile-time errors if you try to use a different data type than the one specified in the definition.
- Generic has a performance advantage because it removes the possibilities of boxing and unboxing.

## C# Generic Constraints

C# allows you to use constraints to restrict client code to specify certain types while instantiating generic types. It will give a compile-time error if you try to instantiate a generic type using a type that is not allowed by the specified constraints.

You can specify one or more constraints on the generic type using the where clause after the generic type name.

```C#
GenericTypeName<T> where T  : contraint1, constraint2
```

The following example demonstrates a generic class with a constraint to reference types when instantiating the generic class.

```C#
// Example: Declare Generic Constraints
class DataStore<T> where T : class
{
    public T Data { get; set; }
}
```

Above, we applied the class constraint, which means only a reference type can be passed as an argument while creating the DataStore class object. So, you can pass reference types such as class, interface, delegate, or array type. Passing value types will give a compile-time error, so we cannot pass primitive data types or struct types.

```C#
DataStore<string> store = new DataStore<string>(); // valid
DataStore<MyClass> store = new DataStore<MyClass>(); // valid
DataStore<IMyInterface> store = new DataStore<IMyInterface>(); // valid
DataStore<IEnumerable> store = new DataStore<IMyInterface>(); // valid
DataStore<ArrayList> store = new DataStore<ArrayList>(); // valid
//DataStore<int> store = new DataStore<int>(); // compile-time error 
```

The following table lists the types of generic constraints.

| Constraint  | Description |
| ------------- | ------------- |
| class  | The type argument must be any class, interface, delegate, or array type.  |
| class?  | The type argument must be a nullable or non-nullable class, interface, delegate, or array type.  |
| struct  | The type argument must be non-nullable value types such as primitive data types int, char, bool, float, etc.  |
| new()  | The type argument must be a reference type which has a public parameterless constructor. It cannot be combined with struct and unmanaged constraints.  |
| notnull  | Available C# 8.0 onwards. The type argument can be non-nullable reference types or value types. If not, then the compiler generates a warning instead of an error.  |
| unmanaged  | The type argument must be non-nullable unmanged types.  |
| < base class name>  | The type argument must be or derive from the specified base class. The Object, Array, ValueType classes are disallowed as a base class constraint. The Enum, Delegate, MulticastDelegate are disallowed as base class constraint before C# 7.3.  |
| < base class name>?  | The type argument must be or derive from the specified nullable or non-nullable base class  |
| < interface name>  | The type argument must be or implement the specified interface.  |
| < interface name>?  | The type argument must be or implement the specified interface. It may be a nullable reference type, a non-nullable reference type, or a value type  |
| where T: U  | The type argument supplied for T must be or derive from the argument supplied for U.  |

## where T : struct
The following example demonstrates the struct constraint that restricts type argument to be non-nullable value type only.

```C#
// Example: struct Constraints
class DataStore<T> where T : struct
{
    public T Data { get; set; }
}

DataStore<int> store = new DataStore<int>(); // valid
DataStore<char> store = new DataStore<char>(); // valid
DataStore<MyStruct> store = new DataStore<MyStruct>(); // valid
//DataStore<string> store = new DataStore<string>(); // compile-time error 
//DataStore<IMyInterface> store = new DataStore<IMyInterface>(); // compile-time error 
//DataStore<ArrayList> store = new DataStore<ArrayList>(); // compile-time error 
```

## where T : new()

The following example demonstrates the struct constraint that restricts type argument to be non-nullable value type only.

```C#
// Example: new() Constraint
class DataStore<T> where T : class, new()
{
    public T Data { get; set; }
}

DataStore<MyClass> store = new DataStore<MyClass>(); // valid
DataStore<ArrayList> store = new DataStore<ArrayList>(); // valid
//DataStore<string> store = new DataStore<string>(); // compile-time error 
//DataStore<int> store = new DataStore<int>(); // compile-time error 
//DataStore<IMyInterface> store = new DataStore<IMyInterface>(); // compile-time error 
```

## where T : baseclass

```C#
// Example: BaseClass Constraint
class DataStore<T> where T : IEnumerable
{
    public T Data { get; set; }
}

DataStore<ArrayList> store = new DataStore<ArrayList>(); // valid
DataStore<List> store = new DataStore<List>(); // valid
//DataStore<string> store = new DataStore<string>(); // compile-time error 
//DataStore<int> store = new DataStore<int>(); // compile-time error 
//DataStore<IMyInterface> store = new DataStore<IMyInterface>(); // compile-time error 
//DataStore<MyClass> store = new DataStore<MyClass>(); // compile-time error 
```
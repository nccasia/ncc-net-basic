# C# - Anonymous Types

In C#, an anonymous type is a type (class) without any name that can contain public read-only properties only. It cannot contain other members, such as fields, methods, events, etc.

You create an anonymous type using the new operator with an object initializer syntax. The implicitly typed variable- var is used to hold the reference of anonymous types.

The following example demonstrates creating an anonymous type variable student that contains three properties named Id, FirstName, and LastName.

``` C#
// Example: Anonymous Type
var student = new { Id = 1, FirstName = "James", LastName = "Bond" };
```

The properties of anonymous types are read-only and cannot be initialized with a null, anonymous function, or a pointer type. The properties can be accessed using dot (.) notation, same as object properties. However, you cannot change the values of properties as they are read-only.

``` C#
// Example: Access Anonymous Type
var student = new { Id = 1, FirstName = "James", LastName = "Bond" };
Console.WriteLine(student.Id); //output: 1
Console.WriteLine(student.FirstName); //output: James
Console.WriteLine(student.LastName); //output: Bond

student.Id = 2;//Error: cannot chage value
student.FirstName = "Steve";//Error: cannot chage value
```

An anonymous type's property can include another anonymous type.

``` C#
// Example: Nested Anonymous Type
var student = new { 
                    Id = 1, 
                    FirstName = "James", 
                    LastName = "Bond",
                    Address = new { Id = 1, City = "London", Country = "UK" }
                };
```

You can create an array of anonymous types also.

```C#
// Example: Array of Anonymous Types
var students = new[] {
            new { Id = 1, FirstName = "James", LastName = "Bond" },
            new { Id = 2, FirstName = "Steve", LastName = "Jobs" },
            new { Id = 3, FirstName = "Bill", LastName = "Gates" }
    };
```
An anonymous type will always be local to the method where it is defined. It cannot be returned from the method. However, an anonymous type can be passed to the method as object type parameter, but it is not recommended. If you need to pass it to another method, then use struct or class instead of an anonymous type.

Mostly, anonymous types are created using the Select clause of a LINQ queries to return a subset of the properties from each object in the collection.

```C#
// Example: LINQ Query returns an Anonymous Type
class Program
{
    static void Main(string[] args)
    {
        IList<Student> studentList = new List<Student>() { 
            new Student() { StudentID = 1, StudentName = "John", age = 18 },
            new Student() { StudentID = 2, StudentName = "Steve",  age = 21 },
            new Student() { StudentID = 3, StudentName = "Bill",  age = 18 },
            new Student() { StudentID = 4, StudentName = "Ram" , age = 20  },
            new Student() { StudentID = 5, StudentName = "Ron" , age = 21 } 
        };

        var students = from s in studentList
                       select new { Id = s.StudentID, Name = s.StudentName };

        foreach(var stud in students)
            Console.WriteLine(stud.Id + "-" + stud.Name);
    }
}

public class Student
{
    public int StudentID { get; set; }
    public string StudentName { get; set; }
    public int age { get; set; }
}
/* OUT PUT
1-John
2-Steve
3-Bill
4-Ram
5-Ron
*/
```

In the above example, a select clause in the LINQ query selects only StudentID and StudentName properties and renames it to Id and Name, respectively. Thus, it is useful in saving memory and unnecessary code. The query result collection includes only StudentID and StudentName properties, as shown in the following debug view.

Visual Studio supports IntelliSense for anonymous types, as shown below.

![imgage](https://i.ibb.co/0ybcg89/anonymoustype-debugview.png)

Internally, all the anonymous types are directly derived from the System.Object class. The compiler generates a class with some auto-generated name and applies the appropriate type to each property based on the value expression. Although your code cannot access it. Use GetType() method to see the name.

```C#
// Example: Internal Name of an Anonymous Type
static void Main(string[] args)
{
    var student = new { Id = 1, FirstName = "James", LastName = "Bond" };
    Console.WriteLine(student.GetType().ToString());
}
```
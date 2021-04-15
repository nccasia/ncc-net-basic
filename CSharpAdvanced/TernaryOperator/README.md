# C# - Ternary Operator ?:

C# includes a decision-making operator ?: which is called the conditional operator or ternary operator. It is the short form of the if else conditions.

``` C#
// Syntax
condition ? statement 1 : statement 2
```
The ternary operator starts with a boolean condition. If this condition evaluates to true then it will execute the first statement after ?, otherwise the second statement after : will be executed.

The following example demonstrates the ternary operator.

```C#
// Example: Ternary operator
int x = 20, y = 10;

var result = x > y ? "x is greater than y" : "x is less than y";

Console.WriteLine(result);

/*Output
x is greater than y
*/
```

Above, a conditional expression x > y returns true, so the first statement after ? will be execute.

The following executes the second statement.

```C#
// Example: Ternary operator
int x = 10, y = 100;

var result = x > y ? "x is greater than y" : "x is less than y";

Console.WriteLine(result);

/*Output
x is less than y
*/
```
Thus, a ternary operator is short form of if else statement. The above example can be re-write using if else condition

## Nested Ternary Operator

Nested ternary operators are possible by including a conditional expression as a second statement.


```C#
// Example: Nested ?:
int x = 10, y = 100;

string result = x > y ? "x is greater than y" : 
                    x < y ? "x is less than y" : 
                        x == y ? "x is equal to y" : "No result";

Console.WriteLine(result);
```

The ternary operator is right-associative. The expression a ? b : c ? d : e is evaluated as a ? b : (c ? d : e), not as (a ? b : c) ? d : e.

```C#
// Example: Nested ?:
var x = 2, y = 10;

var result = x * 3 > y ? x : y > z? y : z;
Console.WriteLine(result);
```

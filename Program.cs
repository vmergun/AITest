Console.WriteLine("Simple Calculator");
Console.WriteLine("Type: number operator number (example: 12.5 * 3)");
Console.Write("Input: ");

var input = Console.ReadLine();
if (!Calculator.TryEvaluate(input, out var result, out var error))
{
    Console.WriteLine(error);
    return;
}

Console.WriteLine($"Result: {result}");

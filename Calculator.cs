using System.Globalization;

public static class Calculator
{
    public static bool TryEvaluate(string? input, out double result, out string error)
    {
        result = 0;
        error = string.Empty;

        if (string.IsNullOrWhiteSpace(input))
        {
            error = "No input provided.";
            return false;
        }

        var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 3)
        {
            error = "Invalid format. Use: number operator number";
            return false;
        }

        if (!TryParseNumber(parts[0], out var left) || !TryParseNumber(parts[2], out var right))
        {
            error = "Invalid number.";
            return false;
        }

        var op = parts[1];
        switch (op)
        {
            case "+":
                result = left + right;
                return true;
            case "-":
                result = left - right;
                return true;
            case "*":
            case "x":
            case "X":
                result = left * right;
                return true;
            case "/":
                if (right == 0)
                {
                    error = "Cannot divide by zero.";
                    return false;
                }

                result = left / right;
                return true;
            default:
                error = "Unsupported operator. Use +, -, *, /";
                return false;
        }
    }

    public static double Abs(double value)
    {
        return Math.Abs(value);
    }

    public static double Pow(double baseValue, double exponent)
    {
        return Math.Pow(baseValue, exponent);
    }

    public static double Square(double value)
    {
        return value * value;
    }

    public static double SafeDivide(double numerator, double denominator)
    {
        if (denominator == 0)
        {
            throw new DivideByZeroException("Cannot divide by zero.");
        }
        var quotient = numerator / denominator;
        return quotient;
    }

    private static bool TryParseNumber(string value, out double number)
    {
        return double.TryParse(value, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out number)
            || double.TryParse(value, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out number);
    }
}

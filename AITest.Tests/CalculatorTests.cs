using Xunit;

public class CalculatorTests
{
    [Theory]
    [InlineData("2 + 3", 5)]
    [InlineData("10 - 4", 6)]
    [InlineData("7 * 8", 56)]
    [InlineData("9 / 3", 3)]
    [InlineData("5.5 + 1.5", 7)]
    public void TryEvaluate_ValidInput_ReturnsExpectedResult(string input, double expected)
    {
        var ok = Calculator.TryEvaluate(input, out var result, out var error);

        Assert.True(ok);
        Assert.Equal(expected, result, 6);
        Assert.Equal(string.Empty, error);
    }

    [Fact]
    public void TryEvaluate_DivideByZero_ReturnsError()
    {
        var ok = Calculator.TryEvaluate("5 / 0", out var result, out var error);

        Assert.False(ok);
        Assert.Equal(0, result);
        Assert.Equal("Cannot divide by zero.", error);
    }

    [Fact]
    public void TryEvaluate_UnsupportedOperator_ReturnsError()
    {
        var ok = Calculator.TryEvaluate("5 ^ 2", out var result, out var error);

        Assert.False(ok);
        Assert.Equal(0, result);
        Assert.Equal("Unsupported operator. Use +, -, *, /", error);
    }

    [Fact]
    public void TryEvaluate_InvalidFormat_ReturnsError()
    {
        var ok = Calculator.TryEvaluate("5+2", out var result, out var error);

        Assert.False(ok);
        Assert.Equal(0, result);
        Assert.Equal("Invalid format. Use: number operator number", error);
    }

    [Fact]
    public void TryEvaluate_InvalidNumber_ReturnsError()
    {
        var ok = Calculator.TryEvaluate("abc + 2", out var result, out var error);

        Assert.False(ok);
        Assert.Equal(0, result);
        Assert.Equal("Invalid number.", error);
    }

    [Fact]
    public void TryEvaluate_EmptyInput_ReturnsError()
    {
        var ok = Calculator.TryEvaluate("", out var result, out var error);

        Assert.False(ok);
        Assert.Equal(0, result);
        Assert.Equal("No input provided.", error);
    }

    [Fact]
    public void TryEvaluate_NullInput_ReturnsError()
    {
        var ok = Calculator.TryEvaluate(null, out var result, out var error);

        Assert.False(ok);
        Assert.Equal(0, result);
        Assert.Equal("No input provided.", error);
    }

    [Fact]
    public void TryEvaluate_WhitespaceInput_ReturnsError()
    {
        var ok = Calculator.TryEvaluate("   ", out var result, out var error);

        Assert.False(ok);
        Assert.Equal(0, result);
        Assert.Equal("No input provided.", error);
    }

    [Fact]
    public void TryEvaluate_InputWithExtraSpaces_ReturnsExpectedResult()
    {
        var ok = Calculator.TryEvaluate("  12   *   2  ", out var result, out var error);

        Assert.True(ok);
        Assert.Equal(24, result, 6);
        Assert.Equal(string.Empty, error);
    }

    [Fact]
    public void TryEvaluate_ThousandsSeparatedNumber_ReturnsExpectedResult()
    {
        var ok = Calculator.TryEvaluate("1,000 + 2", out var result, out var error);

        Assert.True(ok);
        Assert.Equal(1002, result, 6);
        Assert.Equal(string.Empty, error);
    }

    [Fact]
    public void TryEvaluate_NegativeNumbers_ReturnsExpectedResult()
    {
        var ok = Calculator.TryEvaluate("-5 + -3", out var result, out var error);

        Assert.True(ok);
        Assert.Equal(-8, result, 6);
        Assert.Equal(string.Empty, error);
    }

    [Fact]
    public void TryEvaluate_DecimalDivision_ReturnsExpectedResult()
    {
        var ok = Calculator.TryEvaluate("7 / 2", out var result, out var error);

        Assert.True(ok);
        Assert.Equal(3.5, result, 6);
        Assert.Equal(string.Empty, error);
    }

    [Fact]
    public void TryEvaluate_TooManyParts_ReturnsInvalidFormat()
    {
        var ok = Calculator.TryEvaluate("1 + 2 + 3", out var result, out var error);

        Assert.False(ok);
        Assert.Equal(0, result);
        Assert.Equal("Invalid format. Use: number operator number", error);
    }

    [Fact]
    public void TryEvaluate_DivideByDecimalZero_ReturnsError()
    {
        var ok = Calculator.TryEvaluate("5 / 0.0", out var result, out var error);

        Assert.False(ok);
        Assert.Equal(0, result);
        Assert.Equal("Cannot divide by zero.", error);
    }

    [Fact]
    public void TryEvaluate_AddZero_ReturnsSameNumber()
    {
        var ok = Calculator.TryEvaluate("42 + 0", out var result, out var error);

        Assert.True(ok);
        Assert.Equal(42, result, 6);
        Assert.Equal(string.Empty, error);
    }

    [Fact]
    public void TryEvaluate_LargeMultiplication_ReturnsExpectedResult()
    {
        var ok = Calculator.TryEvaluate("1000000 * 3", out var result, out var error);

        Assert.True(ok);
        Assert.Equal(3000000, result, 6);
        Assert.Equal(string.Empty, error);
    }
}

using Xunit;
using StringCalculatorKata.core;

namespace StringCalculatorKata.test;

public class TestCalculatorShould
{
    private StringCalculator _calculator;

    public TestCalculatorShould()
    {

        _calculator = new StringCalculator();
    }

    [Fact]
    public void ReturnsZeroForEmptyInput()
    {
        int actualOutput = _calculator.Add("");

        Assert.Equal(0, actualOutput);
    }

    [Theory]
    [InlineData("2", 2)]
    [InlineData("7", 7)]
    [InlineData("7,2", 9)]
    [InlineData("8,9", 17)]
    public void ReturnSumOfNumbers(string input, int expectedOutput)
    {
        int output = _calculator.Add(input);

        Assert.Equal(expectedOutput, output);
    }

    [Fact]
    public void IgnoreNewLines()
    {
        var input = "1\n2,3";

        int output = _calculator.Add(input);

        Assert.Equal(6, output);
    }

    [Theory]
    [InlineData("//;\n1;2", 3)]
    [InlineData("// \n1 2", 3)]
    [InlineData("//\t\n1\t2", 3)]
    public void SupportDifferentDelimeters(string input, int expectedOutput)
    {
        int output = _calculator.Add(input);

        Assert.Equal(expectedOutput, output);
    }

    [Theory]
    [InlineData("1,2,-3", "negatives not allowed: -3")]
    [InlineData("-1,2,-3", "negatives not allowed: -1, -3")]
    public void NotSupportNegativeNumbers(string input, string expectedMassage)
    {
        var actualMssage = Assert.Throws<ArgumentException>(() => _calculator.Add(input)).Message;

        Assert.Equal(actualMssage, expectedMassage);
    }

    [Theory]
    [InlineData("2,1001", 2)]
    [InlineData("3005,2,1001", 2)]
    public void IgnoreBigNumbers(string input, int expectedOutput)
    {
        int actualOutput = _calculator.Add(input);

        Assert.Equal(expectedOutput, actualOutput);
    }

}
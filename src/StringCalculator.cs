namespace StringCalculatorKata.core;

public class StringCalculator
{
    private const int BIGGEST_SMALL_NUMBER = 1000;

    public int Add(string input)
    {
        if (input == "")
            return 0;

        (string delimiter, string cleanInput) = GetDelimiterAndCleanInput(input);

        var numbers = ToIEnumerableOfIntegers(cleanInput, delimiter);

        ThrowExceptionIfContainsNegatives(numbers);

        var smallNumbers = numbers.Where(n => n <= BIGGEST_SMALL_NUMBER);
        
        return smallNumbers.Sum();
    }

    private static (string Delimiter, string CleanInput) GetDelimiterAndCleanInput(string input)
    {
        string delimiter = ",";
        string cleanInput = input;
        if (input[0] == '/')
        {
            var parts = input.Split("\n");
            delimiter = parts[0].Substring(2);
            cleanInput = parts[1];
        }

        return (delimiter, cleanInput);
    }

    private static void ThrowExceptionIfContainsNegatives(IEnumerable<int> numbers)
    {
        var negatives = numbers.Where(n => n < 0);
        if (negatives.Any())
        {
            var exceptionMassage = "negatives not allowed: " +
            negatives.Select(n => n.ToString()).Aggregate((a, b) => a + ", " + b);
            throw new ArgumentException(exceptionMassage);
        }
    }

    private static IEnumerable<int> ToIEnumerableOfIntegers(string input, string delimiter)
    {
        return input.Split(new String[] { delimiter, "\n" }, StringSplitOptions.None).Select(n => int.Parse(n));
    }
}
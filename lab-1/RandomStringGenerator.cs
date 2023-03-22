using System.Text;

namespace TIiMKD_lab;

public static class RandomStringGenerator
{
    private static string possibleLetters = "qwertyuiopasdfghjklzxcvbnm ";

    public static string Generate(int targetLength)
    {
        var generatedOutput = new StringBuilder();
        for (int i = 0; i < targetLength; i++)
        {
            generatedOutput.Append(possibleLetters[new Random().Next(0, possibleLetters.Length)]);
        }

        return generatedOutput.ToString();
    }
    
    public static string GenerateWeighed(int targetLength, WeightedRandomSelector selector)
    {
        var generatedOutput = new StringBuilder();
        for (int i = 0; i < targetLength; i++)
        {
            generatedOutput.Append(selector.GetRandomCharacter());
        }

        return generatedOutput.ToString();
    }
}
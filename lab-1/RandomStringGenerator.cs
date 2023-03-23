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
    
    public static string GenerateMarkovWeighed(int targetLength, MarkovWeighedRandomSelector selector, int markovLength)
    {
        var generatedOutput = new StringBuilder();
        var prefixWord = "probability";
        generatedOutput.Append(prefixWord);
        for (int i = prefixWord.Length; i < targetLength+prefixWord.Length; i++)
        {
            generatedOutput.Append(selector.GetRandomCharacter(generatedOutput.ToString().Substring(i-markovLength,markovLength)));
        }

        return generatedOutput.ToString().Substring(prefixWord.Length,targetLength);
    }
}
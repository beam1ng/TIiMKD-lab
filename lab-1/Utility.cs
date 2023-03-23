using System.Text;

namespace TIiMKD_lab;

public static class Utility
{
    public static string possibleChars = "abcdefghijklmnopqrstuvwxyz 0123456789";
    public static float CalculateMeanWordLength(string text)
    {
        StringBuilder mutableText = new StringBuilder("");

        //remove sibling spaces
        for (int i = 1; i < text.Length - 1; i++)
        {
            if (text[i] == ' ')
            {
                if (text[i + 1] == ' ' || mutableText.Length == 0)
                    continue;
            }

            mutableText.Append(text[i]);
        }

        if (text[text.Length - 1] != ' ')
        {
            mutableText.Append(text[text.Length - 1]);
        }

        text = mutableText.ToString();
        var split = text.Split();
        return (text.Length - (float) split.Length) / split.Length; //divide spaceless text by words count
    }

    public static Dictionary<char, float> CalculateCharactersFrequencies(string text)
    {
        var charsToFrequencies = new Dictionary<char, float>();
        var charsCounts = new Dictionary<char, int>();

        for (int i = 0; i <possibleChars.Length; i++)
        {
            charsToFrequencies.Add(possibleChars[i],0f);
            charsCounts.Add(possibleChars[i],0);
        }

        for (int i = 0; i < text.Length; i++)
        {
            charsCounts[text[i]]++;
        }

        float textLength = (float) text.Length;
        
        for (int i = 0; i <possibleChars.Length; i++)
        {
            charsToFrequencies[possibleChars[i]]=charsCounts[possibleChars[i]]/textLength;
        }

        return charsToFrequencies;
    }
    
    public static Dictionary<char, float> CalculateCharactersFrequencies(string text, char prefix)
    {
        var charsToFrequencies = new Dictionary<char, float>();
        var charsCounts = new Dictionary<char, int>();
        float validCharsCount = 0f;
        
        for (int i = 0; i <possibleChars.Length; i++)
        {
            charsToFrequencies.Add(possibleChars[i],0f);
            charsCounts.Add(possibleChars[i],0);
        }

        for (int i = 1; i < text.Length; i++)
        {
            if (text[i - 1] == prefix)
            {
                charsCounts[text[i]]++;
                validCharsCount++;
            }
        }

        for (int i = 0; i <possibleChars.Length; i++)
        {
            charsToFrequencies[possibleChars[i]]=charsCounts[possibleChars[i]]/validCharsCount;
        }

        return charsToFrequencies;
    }
    
    public static Dictionary<char, float> CalculateCharactersFrequencies(string text, string prefix)
    {
        var charsToFrequencies = new Dictionary<char, float>();
        var charsCounts = new Dictionary<char, int>();
        
        float validCharsCount = 0f;
        
        for (int i = 0; i <possibleChars.Length; i++)
        {
            charsToFrequencies.Add(possibleChars[i],0f);
            charsCounts.Add(possibleChars[i],0);
        }

        for (int i = prefix.Length; i < text.Length; i++)
        {
            if (text.Substring(i-prefix.Length,prefix.Length) == prefix)
            {
                charsCounts[text[i]]++;
                validCharsCount++;
            }
        }

        for (int i = 0; i <possibleChars.Length; i++)
        {
            charsToFrequencies[possibleChars[i]]=charsCounts[possibleChars[i]]/validCharsCount;
        }

        return charsToFrequencies;
    }

    public static List<string> GetAllSequences(string text, int length)
    {
        List<string> allSequences = new List<string>();
        for (int i = length; i < text.Length; i++)
        {
            string currentSequence = text.Substring(i-length, length);
            if (!allSequences.Contains(currentSequence))
            {
                allSequences.Add(currentSequence);
            }
        }

        return allSequences;
    }
    
    public static Dictionary<string,Dictionary<char, float>> GetSequencesToCharFrequencies(string text, int length)
    {
        Dictionary<string,Dictionary<char, float>> sequencesToCharFrequencies = new Dictionary<string,Dictionary<char, float>>();
        List<string> allSequences = GetAllSequences(text, length);
        foreach (var t in allSequences)
        {
            sequencesToCharFrequencies.Add(t,CalculateCharactersFrequencies(text, t));
        }

        return sequencesToCharFrequencies;
    }
}


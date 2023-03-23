using System.Text;

namespace TIiMKD_lab;

public static class Utility
{
    public static string possibleChars = "abcdefghijklmnopqrstuvwxyz 0123456789";

    public static char GetRandomCharacter()
    {
        return possibleChars[new Random().Next(0, possibleChars.Length)];
    }

    public static float CalculateMeanWordLength(string text)
    {
        var mutableText = new StringBuilder("");

        //remove sibling spaces
        for (var i = 1; i < text.Length - 1; i++)
        {
            if (text[i] == ' ')
                if (text[i + 1] == ' ' || mutableText.Length == 0)
                    continue;

            mutableText.Append(text[i]);
        }

        if (text[text.Length - 1] != ' ') mutableText.Append(text[text.Length - 1]);

        text = mutableText.ToString();
        var split = text.Split();
        return (text.Length - (float)split.Length) / split.Length; //divide spaceless text by words count
    }

    public static Dictionary<char, float> CalculateCharactersFrequencies(string text)
    {
        var charsToFrequencies = new Dictionary<char, float>();
        var charsCounts = new Dictionary<char, int>();

        for (var i = 0; i < possibleChars.Length; i++)
        {
            charsToFrequencies.Add(possibleChars[i], 0f);
            charsCounts.Add(possibleChars[i], 0);
        }

        for (var i = 0; i < text.Length; i++) charsCounts[text[i]]++;

        float textLength = text.Length;

        for (var i = 0; i < possibleChars.Length; i++)
            charsToFrequencies[possibleChars[i]] = charsCounts[possibleChars[i]] / textLength;

        return charsToFrequencies;
    }

    public static Dictionary<char, float> CalculateCharactersFrequencies(string text, char prefix)
    {
        var charsToFrequencies = new Dictionary<char, float>();
        var charsCounts = new Dictionary<char, int>();
        var validCharsCount = 0f;

        for (var i = 0; i < possibleChars.Length; i++)
        {
            charsToFrequencies.Add(possibleChars[i], 0f);
            charsCounts.Add(possibleChars[i], 0);
        }

        for (var i = 1; i < text.Length; i++)
            if (text[i - 1] == prefix)
            {
                charsCounts[text[i]]++;
                validCharsCount++;
            }

        for (var i = 0; i < possibleChars.Length; i++)
            charsToFrequencies[possibleChars[i]] = charsCounts[possibleChars[i]] / validCharsCount;

        return charsToFrequencies;
    }

    public static Dictionary<char, float> CalculateCharactersFrequencies(string text, string prefix)
    {
        var charsToFrequencies = new Dictionary<char, float>();
        var charsCounts = new Dictionary<char, int>();

        var validCharsCount = 0f;

        for (var i = 0; i < possibleChars.Length; i++)
        {
            charsToFrequencies.Add(possibleChars[i], 0f);
            charsCounts.Add(possibleChars[i], 0);
        }

        for (var i = prefix.Length; i < text.Length; i++)
            if (text.Substring(i - prefix.Length, prefix.Length) == prefix)
            {
                charsCounts[text[i]]++;
                validCharsCount++;
            }

        for (var i = 0; i < possibleChars.Length; i++)
            charsToFrequencies[possibleChars[i]] = charsCounts[possibleChars[i]] / validCharsCount;

        return charsToFrequencies;
    }

    public static List<string> GetAllSequences(string text, int length)
    {
        var allSequences = new List<string>();
        for (var i = length; i < text.Length; i++)
        {
            var currentSequence = text.Substring(i - length, length);
            if (!allSequences.Contains(currentSequence)) allSequences.Add(currentSequence);
        }

        return allSequences;
    }

    public static Dictionary<string, Dictionary<char, float>> GetSequencesToCharFrequencies(string text, int length)
    {
        var sequencesToCharFrequencies = new Dictionary<string, Dictionary<char, float>>();
        var sequencesToCharCounts = new Dictionary<string, Dictionary<char, int>>();
        for (var i = length; i < text.Length; i++)
        {
            var currentSeuqence = text.Substring(i - length, length);
            if (!sequencesToCharCounts.ContainsKey(currentSeuqence))
            {
                sequencesToCharCounts.Add(currentSeuqence, new Dictionary<char, int>());
                sequencesToCharFrequencies.Add(currentSeuqence, new Dictionary<char, float>());
                sequencesToCharCounts[currentSeuqence].Add(text[i], 1);
            }
            else
            {
                if (!sequencesToCharCounts[currentSeuqence].ContainsKey(text[i]))
                    sequencesToCharCounts[currentSeuqence].Add(text[i], 1);
                else
                    sequencesToCharCounts[currentSeuqence][text[i]]++;
            }
        }

        foreach (var sequenceToCharCount in sequencesToCharCounts)
        {
            var currentSequence = sequenceToCharCount.Key;
            var totalSequenceCount = sequenceToCharCount.Value.Sum(pair => pair.Value);
            foreach (var charFrequency in sequencesToCharCounts[currentSequence])
                sequencesToCharFrequencies[currentSequence].Add(
                    charFrequency.Key,
                    sequencesToCharCounts[currentSequence][charFrequency.Key] / (float)totalSequenceCount);
        }

        return sequencesToCharFrequencies;
    }
}
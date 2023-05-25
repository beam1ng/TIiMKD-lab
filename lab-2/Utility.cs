using System.Text;

namespace lab_2;

public static class Utility
{
    public static Dictionary<string, int> CalculateWordCounts(string[] textWords)
    {
        var wordFrequencies = new Dictionary<string, int>();
        foreach (var t in textWords)
            if (wordFrequencies.ContainsKey(t))
                wordFrequencies[t]++;
            else
                wordFrequencies.Add(t, 1);

        return wordFrequencies;
    }

    public static IOrderedEnumerable<KeyValuePair<string, int>> GetSubDictionary(
        IOrderedEnumerable<KeyValuePair<string, int>> original, int firstN)
    {
        return original.Take(firstN).OrderByDescending(pair => pair.Value);
    }
    
    public static Dictionary<string, Dictionary<string, float>> GetSequencesToStringFrequencies(string text, int sequenceWordCount)
    {
        var sequencesToCharFrequencies = new Dictionary<string, Dictionary<string, float>>();
        var sequencesToCharCounts = new Dictionary<string, Dictionary<string, int>>();
        var textWords = text.Split();
        var textWordsLength = textWords.Length;

        for (var i = sequenceWordCount; i < textWordsLength; i++)
        {
            var sequence = textWords.Skip(i - sequenceWordCount).Take(sequenceWordCount).ToList();
            var stringifiedSequence = string.Join(" ", sequence);
            
            if (!sequencesToCharCounts.ContainsKey(stringifiedSequence))
            {
                sequencesToCharCounts.Add(stringifiedSequence, new Dictionary<string, int>());
                sequencesToCharFrequencies.Add(stringifiedSequence, new Dictionary<string, float>());
                sequencesToCharCounts[stringifiedSequence].Add(textWords[i], 1);
            }
            else
            {
                if (!sequencesToCharCounts[stringifiedSequence].ContainsKey(textWords[i]))
                    sequencesToCharCounts[stringifiedSequence].Add(textWords[i], 1);
                else
                    sequencesToCharCounts[stringifiedSequence][textWords[i]]++;
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
}
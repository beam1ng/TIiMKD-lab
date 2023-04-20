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
    
    public static Dictionary<List<string>, Dictionary<string, float>> GetSequencesToStringFrequencies(string text, int sequenceWordCount)
    {
        var sequencesToCharFrequencies = new Dictionary<List<string>, Dictionary<string, float>>();
        var sequencesToCharCounts = new Dictionary<List<string>, Dictionary<string, int>>();
        var textWords = text.Split();
        
        for (var i = sequenceWordCount; i < textWords.Length; i++)
        {
            var currentWordSequence = textWords.Skip(i - sequenceWordCount).Take(sequenceWordCount).ToList();
            if (!sequencesToCharCounts.ContainsKey(currentWordSequence))
            {
                sequencesToCharCounts.Add(currentWordSequence, new Dictionary<string, int>());
                sequencesToCharFrequencies.Add(currentWordSequence, new Dictionary<string, float>());
                sequencesToCharCounts[currentWordSequence].Add(textWords[i], 1);
            }
            else
            {
                if (!sequencesToCharCounts[currentWordSequence].ContainsKey(textWords[i]))
                    sequencesToCharCounts[currentWordSequence].Add(textWords[i], 1);
                else
                    sequencesToCharCounts[currentWordSequence][textWords[i]]++;
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
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
}
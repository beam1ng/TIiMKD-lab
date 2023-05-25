using lab_2;

namespace TIiMKD_lab;

public class MarkovWeighedRandomSelector
{
    private readonly Random random = new();

    private readonly Dictionary<string, Dictionary<string, float>> sequencesToWordsFrequencies = new();

    public MarkovWeighedRandomSelector(Dictionary<string, Dictionary<string, float>> sequencesToWordsFrequencies)
    {
        foreach (var sequenceToWordFrequencies in sequencesToWordsFrequencies)
        {
            // var orderedCharactersFrequencies =
            //     sequenceToWordFrequencies.Value.OrderByDescending(pair => pair.Value);
            // this.sequencesToCharactersFrequencies.Add(sequenceToWordFrequencies.Key,
            //     orderedCharactersFrequencies);
            this.sequencesToWordsFrequencies.Add(sequenceToWordFrequencies.Key,sequenceToWordFrequencies.Value);
        }
    }

    public string GetRandomWord(string sequence)
    {
        if (!sequencesToWordsFrequencies.ContainsKey(sequence)) return sequencesToWordsFrequencies.Keys.ToList()[random.Next(0,sequencesToWordsFrequencies.Keys.Count)];
        float totalWeight = 0;
        var weights = sequencesToWordsFrequencies[sequence].Select(pair => pair.Value).ToArray();
        var elements = sequencesToWordsFrequencies[sequence].Select(pair => pair.Key).ToArray();

        foreach (var weight in weights) totalWeight += weight;

        var randomValue = random.NextDouble() * totalWeight;
        var index = 0;

        while (randomValue > weights[index])
        {
            randomValue -= weights[index];
            index++;
        }

        return elements[index];
    }
}
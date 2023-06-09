﻿namespace TIiMKD_lab;

public class MarkovWeighedRandomSelector
{
    private readonly Random random = new();

    private readonly Dictionary<string, IEnumerable<KeyValuePair<char, float>>>
        sequencesToCharactersFrequencies = new();

    public MarkovWeighedRandomSelector(Dictionary<string, Dictionary<char, float>> sequencesToCharactersFrequencies)
    {
        foreach (var sequenceToCharactersFrequencies in sequencesToCharactersFrequencies)
        {
            var orderedCharactersFrequencies =
                sequenceToCharactersFrequencies.Value.OrderByDescending(pair => pair.Value);
            this.sequencesToCharactersFrequencies.Add(sequenceToCharactersFrequencies.Key,
                orderedCharactersFrequencies);
        }
    }

    public char GetRandomCharacter(string sequence)
    {
        if (!sequencesToCharactersFrequencies.ContainsKey(sequence)) return Utility.GetRandomCharacter();
        float totalWeight = 0;
        var weights = sequencesToCharactersFrequencies[sequence].Select(pair => pair.Value).ToArray();
        var elements = sequencesToCharactersFrequencies[sequence].Select(pair => pair.Key).ToArray();

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
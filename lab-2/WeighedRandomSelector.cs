namespace TIiMKD_lab;

using System;
using System.Collections.Generic;

public class WeightedRandomSelector
{
    private readonly List<char> elements = new List<char>();
    private readonly List<float> weights = new List<float>();
    private readonly Random random = new Random();

    public WeightedRandomSelector(Dictionary<char, float> elementsProbabilities)
    {
        foreach (var elementsProbability in elementsProbabilities)
        {
            elements.Add(elementsProbability.Key);
            weights.Add(elementsProbability.Value);
        }
    }

    public void AddElement(char element, float weight)
    {
        elements.Add(element);
        weights.Add(weight);
    }

    public char GetRandomCharacter()
    {
        if (elements.Count == 0) throw new InvalidOperationException("No elements added.");

        float totalWeight = 0;

        foreach (float weight in weights)
        {
            totalWeight += weight;
        }

        double randomValue = random.NextDouble() * totalWeight;
        int index = 0;

        while (randomValue > weights[index])
        {
            randomValue -= weights[index];
            index++;
        }

        return elements[index];
    }
}
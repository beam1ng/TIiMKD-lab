namespace TIiMKD_lab;

public class WeightedRandomSelector
{
    private readonly List<string> elements = new();
    private readonly Random random = new();
    private readonly List<float> weights = new();

    public WeightedRandomSelector(Dictionary<string, float> elementsProbabilities)
    {
        foreach (var elementsProbability in elementsProbabilities)
        {
            elements.Add(elementsProbability.Key);
            weights.Add(elementsProbability.Value);
        }
    }

    public void AddElement(string element, float weight)
    {
        elements.Add(element);
        weights.Add(weight);
    }

    public string GetRandomWord()
    {
        if (elements.Count == 0) throw new InvalidOperationException("No elements added.");

        float totalWeight = 0;

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
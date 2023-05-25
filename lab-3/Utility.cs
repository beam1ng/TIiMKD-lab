namespace lab_3;

public class Utility
{
    public static string possibleChars = "abcdefghijklmnopqrstuvwxyz 0123456789";

    public static float CalculateCharacterEntropy(string inputText, int degree)
    {
        var sequencesToFrequencies = GetSequencesToCharFrequencies(inputText, degree);
        var entropy = CalculateCharacterEntropy(sequencesToFrequencies, degree);
    }
    
    public static float CalculateWordEntropy(string inputText, int degree)
    {
        
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
    
    public float CalculateCharacterEntropy(Dictionary<string,Dictionary<char, float>> sequencesToCharactersFrequencies, int degree)
    {
        int possibleOutputsCount = possibleChars.Length;
        float entropy = 0.0f;
        foreach (var sequenceCharactersFrequencies in sequencesToCharactersFrequencies)
        {
            var sequenceFrequency = CalculateSequenceFrequency(sequenceCharactersFrequencies.Key);
            var sequenceEntropy = 0.0f;
            
            foreach (var characterFrequency in sequenceCharactersFrequencies.Value)
            {
                sequenceEntropy += characterFrequency.Value * MathF.Log(characterFrequency.Value, (float) possibleOutputsCount);
            }
            
            entropy += sequenceFrequency * sequenceEntropy;
        }
        
        return entropy;
    }

    private float CalculateSequencesFrequencies(int length, string text)
    {
        //this is total rubbish, to be replaced todo:<=
        var allSequences = new List<string>();
        for (var i = length; i < text.Length; i++)
        {
            var currentSequence = text.Substring(i - length, length);
            if (!allSequences.Contains(currentSequence)) allSequences.Add(currentSequence);
        }

        return allSequences;
    }
}
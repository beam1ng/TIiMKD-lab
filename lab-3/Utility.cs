using System.Text;

namespace lab_3;

public class Utility
{
    public static string possibleChars = "abcdefghijklmnopqrstuvwxyz 0123456789";

    public static float CalculateCharacterEntropy(string inputText, int degree)
    {
        var sequencesToCharFrequencies = GetSequencesToCharFrequencies(inputText, degree);
        var sequencesFrequencies = CalculateSequencesFrequencies(inputText, degree);
        var entropy = CalculateCharacterEntropy(sequencesToCharFrequencies,sequencesFrequencies, degree);
        return entropy;
    }
    
    public static float CalculateWordEntropy(string inputText, int degree)
    {
        var sequencesToSequencesFrequencies = GetSequencesToStringFrequencies(inputText, degree);
        var sequencesFrequencies = CalculateWordSequencesFrequencies(inputText, degree);
        var entropy = CalculateWordEntropy(sequencesToSequencesFrequencies,sequencesFrequencies,CalculateUniqueWordsCount(inputText), degree);
        return entropy;
    }

    public static Dictionary<string, Dictionary<string, float>> GetSequencesToStringFrequencies(string text, int sequenceWordCount)
    {
        var sequencesToSequenceFrequencies = new Dictionary<string, Dictionary<string, float>>();
        var sequencesToSequenceCounts = new Dictionary<string, Dictionary<string, int>>();
        var textWords = text.Split();
        var textWordsLength = textWords.Length;

        for (var i = sequenceWordCount; i < textWordsLength; i++)
        {
            var sequence = textWords.Skip(i - sequenceWordCount).Take(sequenceWordCount).ToList();
            var stringifiedSequence = string.Join(" ", sequence);
            
            if (!sequencesToSequenceCounts.ContainsKey(stringifiedSequence))
            {
                sequencesToSequenceCounts.Add(stringifiedSequence, new Dictionary<string, int>());
                sequencesToSequenceFrequencies.Add(stringifiedSequence, new Dictionary<string, float>());
                sequencesToSequenceCounts[stringifiedSequence].Add(textWords[i], 1);
            }
            else
            {
                if (!sequencesToSequenceCounts[stringifiedSequence].ContainsKey(textWords[i]))
                    sequencesToSequenceCounts[stringifiedSequence].Add(textWords[i], 1);
                else
                    sequencesToSequenceCounts[stringifiedSequence][textWords[i]]++;
            }
        }

        foreach (var sequenceToCharCount in sequencesToSequenceCounts)
        {
            var currentSequence = sequenceToCharCount.Key;
            var totalSequenceCount = sequenceToCharCount.Value.Sum(pair => pair.Value);
            foreach (var charFrequency in sequencesToSequenceCounts[currentSequence])
                sequencesToSequenceFrequencies[currentSequence].Add(
                    charFrequency.Key,
                    sequencesToSequenceCounts[currentSequence][charFrequency.Key] / (float)totalSequenceCount);
        }

        return sequencesToSequenceFrequencies;
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

    public static float CalculateCharacterEntropy(Dictionary<string,Dictionary<char, float>> sequencesToCharactersFrequencies,Dictionary<string,float> sequencesFrequencies, int degree)
    {
        int possibleOutputsCount = possibleChars.Length;
        float entropy = 0.0f;
        foreach (var sequenceCharactersFrequencies in sequencesToCharactersFrequencies)
        {
            if (!sequencesFrequencies.ContainsKey(sequenceCharactersFrequencies.Key))
            {
                continue;
            }
            var sequenceFrequency = sequencesFrequencies[sequenceCharactersFrequencies.Key];
            var sequenceEntropy = 0.0f;
            
            foreach (var characterFrequency in sequenceCharactersFrequencies.Value)
            {
                sequenceEntropy += characterFrequency.Value * MathF.Log(characterFrequency.Value, (float) possibleOutputsCount);
            }
            
            entropy += sequenceFrequency * sequenceEntropy;
        }
        
        return -1*entropy;
    }
    
    public static float CalculateWordEntropy(Dictionary<string,Dictionary<string, float>> sequencesToSequencesFrequencies,Dictionary<string,float> sequencesFrequencies,int possibleWordsCount, int degree)
    {
        int possibleOutputsCount = possibleWordsCount;
        float entropy = 0.0f;
        foreach (var sequenceSequencesFrequencies in sequencesToSequencesFrequencies)
        {
            if (!sequencesFrequencies.ContainsKey(sequenceSequencesFrequencies.Key))
            {
                continue;
            }
            var sequenceFrequency = sequencesFrequencies[sequenceSequencesFrequencies.Key];
            var sequenceEntropy = 0.0f;
            
            foreach (var sequenceSequenceFrequency in sequenceSequencesFrequencies.Value)
            {
                sequenceEntropy += sequenceSequenceFrequency.Value * MathF.Log(sequenceSequenceFrequency.Value, (float) possibleOutputsCount);
            }
            
            entropy += sequenceFrequency * sequenceEntropy;
        }
        
        return -1*entropy;
    }
    
    private static Dictionary<string,float> CalculateSequencesFrequencies(string text,int degree)
    {
        var allSequencesCounts = new Dictionary<string,int>();
        for (var i = degree; i < text.Length; i++)
        {
            var currentSequence = text.Substring(i - degree, degree);
            if (!allSequencesCounts.ContainsKey(currentSequence))
            {
                allSequencesCounts.Add(currentSequence,1);
            }
            else
            {
                allSequencesCounts[currentSequence]++;
            }
        }

        int totalSequencesCount = text.Length + 1 - degree;
        Dictionary<string, float> sequencesFrequencies = new Dictionary<string, float>();
        foreach (var sequencesCount in allSequencesCounts)
        {
            sequencesFrequencies.Add(sequencesCount.Key,(float)sequencesCount.Value/totalSequencesCount);
        }

        return sequencesFrequencies;
    }
    
    private static Dictionary<string,float> CalculateWordSequencesFrequencies(string text,int degree)
    {
        if (degree == 0)
        {
            var output = new Dictionary<string, float>();
            output.Add("",1.0f);
            return output;
        }
        var allStringifiedSequencesCounts = new Dictionary<string,int>();
        var splitText = text.Split(" ");
        for (var i = degree; i < text.Length; i++)
        {
            var currentSequence = splitText.Skip(i - degree).Take(degree).ToList();
            var stringifiedSequence = string.Join(" ", currentSequence);
            
            if (!allStringifiedSequencesCounts.ContainsKey(stringifiedSequence))
            {
                allStringifiedSequencesCounts.Add(stringifiedSequence,1);
            }
            else
            {
                allStringifiedSequencesCounts[stringifiedSequence]++;
            }
        }

        int totalWordsCount = splitText.Length + 1 - degree;
        Dictionary<string, float> sequencesFrequencies = new Dictionary<string, float>();
        foreach (var sequencesCount in allStringifiedSequencesCounts)
        {
            sequencesFrequencies.Add(sequencesCount.Key,(float)sequencesCount.Value/totalWordsCount);
        }

        return sequencesFrequencies;
    }

    private static int CalculateUniqueWordsCount(string text)
    {
        HashSet<string> uniqueWords = new HashSet<string>();
        StringBuilder currentWord = new StringBuilder();

        foreach (char c in text)
        {
            if (char.IsWhiteSpace(c))
            {
                if (currentWord.Length > 0)
                {
                    uniqueWords.Add(currentWord.ToString());
                    currentWord.Clear();
                }
            }
            else
            {
                currentWord.Append(c);
            }
        }

        if (currentWord.Length > 0)
        {
            uniqueWords.Add(currentWord.ToString());
        }

        return uniqueWords.Count;
    }
}
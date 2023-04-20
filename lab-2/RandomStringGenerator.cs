using System.Text;

namespace TIiMKD_lab;

public static class RandomStringGenerator
{
    public static string GenerateMarkovWeighed(int targetLength, MarkovWeighedRandomSelector selector, string prefix)
    {
        var generatedOutput = new StringBuilder();
        var sequenceQueue = new Queue<string>(prefix.Split());
        generatedOutput.AppendJoin(" ", sequenceQueue.ToList());
        for (var i = sequenceQueue.Count; i < targetLength + sequenceQueue.Count; i++)
        {
            var newWord = selector.GetRandomWord(sequenceQueue.Dequeue());
            sequenceQueue.Enqueue(newWord);
            generatedOutput.Append($" {newWord}");
        }

        return generatedOutput.ToString();
    }
}
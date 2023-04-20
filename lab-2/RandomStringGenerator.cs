using System.Diagnostics;
using System.Text;

namespace TIiMKD_lab;

public static class RandomStringGenerator
{
    public static string GenerateMarkovWeighed(int targetLength, MarkovWeighedRandomSelector selector, string prefix, int markovDegree)
    {
        Debug.Assert(prefix.Split().Length==markovDegree,"Prefixes word count differs from Markov's degree.");
        var generatedOutput = new StringBuilder();
        var sequenceQueue = new Queue<string>(prefix.Split());
        generatedOutput.AppendJoin(" ", sequenceQueue.ToList());
        for (var i = markovDegree; i < targetLength + markovDegree; i++)
        {
            var newWord = selector.GetRandomWord(string.Join(" ",sequenceQueue));
            sequenceQueue.Dequeue();
            sequenceQueue.Enqueue(newWord);
            generatedOutput.Append($" {newWord}");
        }

        return generatedOutput.ToString();
    }
}
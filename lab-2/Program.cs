// See https://aka.ms/new-console-template for more information

using lab_2;

string solutionDir = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())));
string relativeFilePath = Path.Combine(solutionDir, "src", "norm_wiki_sample.txt");
var wikiText = File.ReadAllText(relativeFilePath);
var textWords = wikiText.Split(' ');
var wordsCount = textWords.Length;
var wordsCounts = Utility.CalculateWordCounts(textWords);
var sortedWordsCounts = wordsCounts.OrderByDescending(pair=>pair.Value);
var first30k = Utility.GetSubDictionary(sortedWordsCounts,30000);
var first30kFraction = first30k.Sum(pair => pair.Value) / (float)wordsCount;
var first6k = Utility.GetSubDictionary(sortedWordsCounts,6000);
var first6kFraction = first6k.Sum(pair => pair.Value) / (float)wordsCount;
for (int i = 0; i < 10; i++)
{
    Console.WriteLine($"Word: '{sortedWordsCounts.ElementAt(i).Key}', Frequency: {sortedWordsCounts.ElementAt(i).Value/(float)wordsCount}");
}
Console.WriteLine($"First 30k fraction = {first30kFraction}");
Console.WriteLine($"First 6k fraction = {first6kFraction}");
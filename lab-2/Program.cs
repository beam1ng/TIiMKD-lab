// See https://aka.ms/new-console-template for more information

using lab_2;
using TIiMKD_lab;

var solutionDir = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));
var relativeFilePath = Path.Combine(solutionDir, "src", "norm_wiki_sample.txt");
var wikiText = File.ReadAllText(relativeFilePath);

#pragma region task1
// var textWords = wikiText.Split(' ');
// var wordsCount = textWords.Length;
// var wordsCounts = Utility.CalculateWordCounts(textWords);
// var sortedWordsCounts = wordsCounts.OrderByDescending(pair => pair.Value);
// var first30k = Utility.GetSubDictionary(sortedWordsCounts, 30000);
// var first30kFraction = first30k.Sum(pair => pair.Value) / (float)wordsCount;
// var first6k = Utility.GetSubDictionary(sortedWordsCounts, 6000);
// var first6kFraction = first6k.Sum(pair => pair.Value) / (float)wordsCount;
// for (var i = 0; i < 10; i++)
//     Console.WriteLine(
//         $"Word: '{sortedWordsCounts.ElementAt(i).Key}', Frequency: {sortedWordsCounts.ElementAt(i).Value / (float)wordsCount}");
// Console.WriteLine($"First 30k fraction = {first30kFraction}");
// Console.WriteLine($"First 6k fraction = {first6kFraction}");
#pragma endregion task1

#pragma region homework
var sequencesToFrequencies = Utility.GetSequencesToStringFrequencies(wikiText, 1);
var markovStream = new MarkovWeighedRandomSelector(sequencesToFrequencies);

var prefix = "probability that";
var markovDegrees = new List<int> { 1, 2 };
var markovGenerated = "";
foreach (var markovDegree in markovDegrees)
{
    var sequencesToCharsFrequencies = Utility.GetSequencesToCharFrequencies(wikiText, markovDegree);
    var markovSelector = new MarkovWeighedRandomSelector(sequencesToCharsFrequencies);
    markovGenerated = RandomStringGenerator.GenerateMarkovWeighed(outputFileSize, markovSelector, markovDegree);
    Console.WriteLine($"MarkovDegree: {markovDegree}, Generated text: {markovGenerated.Substring(0, 100)}...");
    Console.WriteLine($"Mean word length: {Utility.CalculateMeanWordLength(markovGenerated)}");
}
File.WriteAllText(relativeOutputFilePath,markovGenerated);
Console.WriteLine($"The text generated using MarkovDegree=5 was saved to the file ${outputFileName}.");

// See https://aka.ms/new-console-template for more information

using TIiMKD_lab;

var solutionDir = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));
var relativeFilePath = Path.Combine(solutionDir, "src", "norm_wiki_sample.txt");
var wikiText = File.ReadAllText(relativeFilePath);

// task 1
// var randomString = RandomStringGenerator.Generate(1000000);
// Console.WriteLine($"mean word length: {Utility.CalculateMeanWordLength(randomString)}");

// task 2
// var charactersFrequencies = Utility.CalculateCharactersFrequencies(wikiText);
// var sortedCharacterFrequencies = charactersFrequencies.OrderBy(pair => pair.Value);
// foreach (var pair in sortedCharacterFrequencies)
// {
//     Console.WriteLine($"Character: {pair.Key}, Frequency {pair.Value:F5}");
// }

// task 3
// var weighedSelector = new WeightedRandomSelector(charactersFrequencies);
// var weighedRandomString = RandomStringGenerator.GenerateWeighed(1000000,weighedSelector);
// Console.WriteLine($"Weighed random string's mean word length: {Utility.CalculateMeanWordLength(weighedRandomString)}");
// Console.WriteLine($"Original text's mean word length: {Utility.CalculateMeanWordLength(wikiText)}");

// task 4
// Character: e, Frequency 0,09354
// Character:  , Frequency 0,17059
// var sortedCharactersFrequenciesAfterSpace = Utility.CalculateCharactersFrequencies(wikiText, ' ').OrderBy(pair => pair.Value);
// foreach (var pair in sortedCharactersFrequenciesAfterSpace)
// {
//     Console.WriteLine($"' '=>{pair.Key}, Frequency {pair.Value:F5}");
// }
// var sortedCharactersFrequenciesAfterE = Utility.CalculateCharactersFrequencies(wikiText, 'e').OrderBy(pair => pair.Value);
// foreach (var pair in sortedCharactersFrequenciesAfterE)
// {
//     Console.WriteLine($"'e'=>{pair.Key}, Frequency {pair.Value:F5}");
// }

//task 5
var markovDegrees = new List<int> { 1, 3, 5 };
foreach (var markovDegree in markovDegrees)
{
    var sequencesToCharsFrequencies = Utility.GetSequencesToCharFrequencies(wikiText, markovDegree);
    var markovSelector = new MarkovWeighedRandomSelector(sequencesToCharsFrequencies);
    var markovGenerated = RandomStringGenerator.GenerateMarkovWeighed(10000, markovSelector, markovDegree);
    Console.WriteLine($"MarkovDegree: {markovDegree}, Generated text: {markovGenerated.Substring(0, 100)}...");
    Console.WriteLine($"Mean word length: {Utility.CalculateMeanWordLength(markovGenerated)}");
}
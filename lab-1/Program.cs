// See https://aka.ms/new-console-template for more information

using TIiMKD_lab;

var wikiText = File.ReadAllText("C:\\Users\\Kuba\\Desktop\\uczelnia\\S6\\TIiMKD-lab\\lab-1\\src\\norm_wiki_sample.txt");

// Console.WriteLine($"mean word length: {Utility.CalculateMeanWordLength(randomString)}");
// var randomString = RandomStringGenerator.Generate(1000000);

Utility.CalculateCharactersFrequencies(wikiText);
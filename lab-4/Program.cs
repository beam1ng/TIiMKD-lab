using lab_4;

var solutionDir = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));
var relativeFilePath = Path.Combine(solutionDir, "src", "norm_wiki_sample.txt");
var wikiText = File.ReadAllText(relativeFilePath);
var outputPath = Path.Combine(solutionDir, "src", "encoded_norm_wiki_sample.txt");

var flcCompressor = new FixedLengthCoding();

//creation + encoding + saving
flcCompressor.Create(wikiText);
flcCompressor.Encode(wikiText);
flcCompressor.Save(outputPath);

//loading + decoding (only operates on the saved data files)
flcCompressor.Load(outputPath);
var decodedText = flcCompressor.Decode();
Console.WriteLine($"decodedText: {decodedText.Substring(0, Math.Min(decodedText.Length, 100))}...");

///komentarze do zadania:
/// Rozmiar pliku oryginalnego: 5.88MB
/// Rozmiar pliku zkompresowanego: 4.41MB
/// stopień kompresji: 25%
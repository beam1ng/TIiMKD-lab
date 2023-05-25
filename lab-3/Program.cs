using lab_3;

var solutionDir = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));
var relativeFilePath = Path.Combine(solutionDir, "src", "norm_wiki_en.txt");
var inputText = File.ReadAllText(relativeFilePath);

//character entropy
var characterEntropy_Deg0 = Utility.CalculateCharacterEntropy(0);
var characterEntropy_Deg1 = Utility.CalculateCharacterEntropy(1);
var characterEntropy_Deg2 = Utility.CalculateCharacterEntropy(2);
var characterEntropy_Deg3 = Utility.CalculateCharacterEntropy(3);
var characterEntropy_Deg4 = Utility.CalculateCharacterEntropy(4);

//word entropy
var wordEntropy_Deg0 = Utility.CalculateWordEntropy(0);
var wordEntropy_Deg1 = Utility.CalculateWordEntropy(1);
var wordEntropy_Deg2 = Utility.CalculateWordEntropy(2);
var wordEntropy_Deg3 = Utility.CalculateWordEntropy(3);
var wordEntropy_Deg4 = Utility.CalculateWordEntropy(4);

//results
enum EntropyType
{
    WORD,
    CHARACTER
}

struct EntropyData
{
    public EntropyData(EntropyType entropyType, int entropyDegree, float entropyValue)
    {
        this.entropyType = entropyType;
        this.entropyDegree = entropyDegree;
        this.entropyValue = entropyValue;
    }

    public EntropyType entropyType;
    public int entropyDegree;
    public float entropyValue;
}

var entropyDatas = new List<EntropyData>()
{
    new EntropyData(EntropyType.CHARACTER, 0, characterEntropy_Deg0),
    new EntropyData(EntropyType.CHARACTER, 1, characterEntropy_Deg1),
    new EntropyData(EntropyType.CHARACTER, 2, characterEntropy_Deg2),
    new EntropyData(EntropyType.CHARACTER, 3, characterEntropy_Deg3),
    new EntropyData(EntropyType.CHARACTER, 4, characterEntropy_Deg4),
    new EntropyData(EntropyType.WORD, 0, wordEntropy_Deg0),
    new EntropyData(EntropyType.WORD, 1, wordEntropy_Deg1),
    new EntropyData(EntropyType.WORD, 2, wordEntropy_Deg2),
    new EntropyData(EntropyType.WORD, 3, wordEntropy_Deg3),
    new EntropyData(EntropyType.WORD, 4, wordEntropy_Deg4)
};

foreach (var entropyData in entropyDatas)
{
    Console.Out.WriteLine($"Type: {entropyData.entropyType}, Degree:{entropyData.entropyDegree}, Entropy: {entropyData.entropyValue}");
}
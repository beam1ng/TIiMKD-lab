using System.Collections;
using System.Text;
using System.Text.Json;

namespace lab_4;

public class FixedLengthCoding : ICompression
{
    private static string possibleCharacters = "qwertyuiopasdfghjklzxcvbnm 0123456789";

    private BitArray encodedText;

    private Dictionary<char, BitArray> characterCodes = new Dictionary<char, BitArray>();
    private Dictionary<string, char> originalCharacters = new Dictionary<string, char>();

    public void Create(string text)
    {
        for (var i = 0; i < possibleCharacters.Length; i++)
        {
            var binaryCode = Convert.ToString(i, 2).PadLeft(6, '0');
            var codeBits = new BitArray(binaryCode.Select(c => c == '1').ToArray());
            characterCodes[possibleCharacters[i]] = codeBits;
            var codeBitsStringified = string.Join("", codeBits.Cast<bool>().Select(bit => bit ? "1" : "0"));
            originalCharacters[codeBitsStringified] = possibleCharacters[i];
        }
    }

    public void Encode(string text)
    {
        var characterCodeLength = (int)Math.Ceiling(Math.Log2(possibleCharacters.Length));
        encodedText = new BitArray(text.Length * characterCodeLength);
        for (int i = 0; i < text.Length; i++)
        {
            var characterCode = characterCodes[text[i]];
            var startIndex = i * characterCodeLength;

            for (int j = 0; j < characterCodeLength; j++)
            {
                encodedText[startIndex + j] = characterCode[j];
            }
        }
    }

    public string Decode()
    {
        var characterCodeLength = (int)Math.Ceiling(Math.Log2(possibleCharacters.Length));
        var decodedText = new StringBuilder("");
        
        for (int i = 0; i < encodedText.Length; i += characterCodeLength)
        {
            var subArray = new BitArray(characterCodeLength);

            for (int j = 0; j < characterCodeLength; j++)
            {
                subArray[j] = encodedText[i + j];
            }
            
            var subArrayStrigified = string.Join("", subArray.Cast<bool>().Select(bit => bit ? "1" : "0"));
            var character = originalCharacters[subArrayStrigified];
            decodedText.Append(character);
        }

        return decodedText.ToString();
        
    }

    public void Save(string path)
    {
        using (var writer = new BinaryWriter(File.Open(path, FileMode.Create)))
        {
            byte currentByte = 0;
            int bitCount = 0;

            foreach (bool bit in encodedText)
            {
                if (bit)
                {
                    currentByte |= (byte)(1 << bitCount);
                }

                bitCount++;

                if (bitCount == 8)
                {
                    writer.Write(currentByte);
                    currentByte = 0;
                    bitCount = 0;
                }
            }

            if (bitCount > 0)
            {
                writer.Write(currentByte);
            }
        }

        
        var data = new
        {
            CharacterCodes = characterCodes,
            OriginalCharacters = originalCharacters
        };
        
        string json = JsonSerializer.Serialize(data);

        File.WriteAllText(path+"_meta", json);
    }


    public void Load(string path)
    {
        using (var reader = new BinaryReader(File.Open(path, FileMode.Open)))
        {
            long bitCount = reader.BaseStream.Length * 8;
            encodedText = new BitArray((int)bitCount);

            for (int i = 0; i < bitCount; i++)
            {
                if (i % 8 == 0)
                {
                    byte currentByte = reader.ReadByte();

                    for (int j = 0; j < 8; j++)
                    {
                        bool bit = (currentByte & (1 << j)) != 0;
                        encodedText[i + j] = bit;
                    }
                }
            }
        }
        
        string json = File.ReadAllText(path+"_meta");
        var document = JsonDocument.Parse(json);
        var originalCharactersElement = document.RootElement.GetProperty("OriginalCharacters");
        originalCharacters = JsonSerializer.Deserialize<Dictionary<string, char>>(originalCharactersElement.GetRawText());
        var characterCodesElement = document.RootElement.GetProperty("CharacterCodes");
        characterCodes = new Dictionary<char, BitArray>();
        foreach (var property in characterCodesElement.EnumerateObject())
        {
            var key = property.Name[0];
            var valueArray = property.Value.EnumerateArray().Select(item => item.GetBoolean()).ToArray();
            var valueBits = new BitArray(valueArray);
            characterCodes.Add(key, valueBits);
        }
    }
}
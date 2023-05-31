using System.Collections;

namespace lab_4;

public interface ICompression
{
    public void Create(string text);
    public void Encode(string text);
    public string Decode();
    public void Save(string path);
    public void Load(string path);
}
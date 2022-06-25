namespace Shared.Models;

public class Document
{
    public string FileName { get; set; }
    public byte[] Content { get; set; }
    public int ChunkSize { get; set; }
    public int Position { get; set; }

    public Document(string filename, byte[] content, int chunkSize, int position)
    {
        FileName = filename;
        Content = content;
        ChunkSize = chunkSize;
        Position = position;
    }

}

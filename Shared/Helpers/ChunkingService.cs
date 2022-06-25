namespace Shared.Helpers;

public static class ChunkingService
{
    public static List<byte[]> GetMessageChunks(byte[] message)
    {
        return message.Chunk(300000).ToList();
    }
}

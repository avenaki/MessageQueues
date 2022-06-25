using MainProcessingService.Models;
using Serilog;
using Shared.Models;
using Shared.Services.Interfaces.Interfaces;

namespace MainProcessingService.Services.Classes;

public class MainProcessingService : IMainProcessingService
{
    public static List<Document> _documents = new List<Document>();
    private static ILogger _logger;

    public MainProcessingService(ILogger logger)
    {
        _logger = logger;
    }

    public void AddMessage(Message message)
    {
        _documents.Add(new Document(message.Key.Key,
                                    message.Value.Content,
                                    message.Value.ChunkSize,
                                    message.Value.Position));

        if (message.Value.ChunkSize -1  == message.Value.Position)
        {
            StoreInLocalFolder(message.Key.Key);
        }
    }

    public void StoreInLocalFolder(string key)
    {
        var fullContent = new List<byte>();
        var documents = _documents.Where(d => d.FileName == key).OrderBy(d => d.Position).ToList();

        for (int i = 0; i < documents.Count(); i++)
        {
            fullContent.AddRange(documents[i].Content);
        }

        File.WriteAllBytes(MainProcessingServiceConstants.FolderPath +$"/{key}", fullContent.ToArray());
        _logger.Information($"File {key} was saved to local folder");

        _documents.RemoveAll(d => d.FileName == key);
    }
}

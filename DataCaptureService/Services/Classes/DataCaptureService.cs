using Env.FileWatcher;
using Shared.Models;
using Shared;
using Shared.KafkaServices.Interfaces;
using DataCaptureService.Services.Interfaces;
using Shared.Helpers;

namespace DataCaptureService.Services.Classes;

public class DataCaptureService: IDataCaptureService
{
    IKafkaProducer _producer;
 
    public DataCaptureService(IKafkaProducer producer)
    {
        _producer = producer;
    }

    public async void MonitorFileFolder()
    {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = DataCaptureServiceConstants.FolderPath;
            watcher.NotifyFilter = NotifyFilters.FileName;
            watcher.EnableRaisingEvents = true;
            watcher.Filter = "*.pdf";
            watcher.Created += new FileSystemEventHandler(OnCreatedAsync);
    }
    private async void OnCreatedAsync(object source, FileSystemEventArgs e)
    {
        if (e.ChangeType == WatcherChangeTypes.Created)
        {
            while (true)
            {
                try
                {
                    var newFile = File.ReadAllBytes(e.FullPath);
                    var chunks = new List<byte[]>() { newFile };

                    if (newFile.Length > 800000)
                    {
                        chunks = ChunkingService.GetMessageChunks(newFile);
                    }

                    for (int chunk = 0; chunk < chunks.Count; chunk++)
                    {
                        var message = new Message(
                                      new DataCaptureService_0_Key()
                                      {
                                          Key = e.Name
                                      },
                                      new DataCaptureService_0_Value()
                                      {
                                          FileName = e.Name,
                                          Content = chunks[chunk],
                                          ChunkSize = chunks.Count,
                                          Position = chunk
                                      });

                        _producer.ProduceMessage(message);
                    }

                    break;
                }
                catch (Exception ex)
                {
                    
                }
            }       

        };


    }
}

using Env.FileWatcher;

namespace Shared.Models;

public class Message
{
    public DataCaptureService_0_Key Key { get; set; }
    public DataCaptureService_0_Value Value { get; set; }

    public Message(DataCaptureService_0_Key key, DataCaptureService_0_Value value)
    {
        Key = key;
        Value = value;
    }
}

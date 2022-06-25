using Shared.Models;

namespace Shared.Services.Interfaces.Interfaces
{
    public interface IMainProcessingService
    {
        public void AddMessage(Message message);
        public void StoreInLocalFolder(string key);
    }
}

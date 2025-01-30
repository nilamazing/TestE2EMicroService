using PlatformService.Models;

namespace PlatformService.Data
{
    public interface IPlatformRepo
    {
        public Task<IEnumerable<Platform>> GetPlatformsAsync();
        public Task<Platform> GetPlatform(int platformId);
        public Task CreatePlatform(Platform platform);
        public Task SaveChangesAsync();
    }
}

using PlatformService.Dtos;

namespace PlatformService.Abstract.Http
{
    public interface ICommandDataClient
    {
        Task<bool> SendPlatformToCommand(PlatformReadDto platformReadDto);
    }
}

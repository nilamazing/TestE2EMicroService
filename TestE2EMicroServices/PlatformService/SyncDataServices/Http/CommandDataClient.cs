using Microsoft.Extensions.Options;
using PlatformService.Abstract.Http;
using PlatformService.Dtos;
using System.Text;
using System.Text.Json;

namespace PlatformService.SyncDataServices.Http
{
    public class CommandDataClient : ICommandDataClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptions<CommandeServiceMetadataDto> _commandServiceMetadata;
        public CommandDataClient(IHttpClientFactory httpClientFactory, IOptions<CommandeServiceMetadataDto> commandServiceMetadata)
        {

            _httpClientFactory = httpClientFactory;
            _commandServiceMetadata = commandServiceMetadata;
        }
        public async Task<bool> SendPlatformToCommand(PlatformReadDto platformReadDto)
        {
            bool status = false;
            try
            {
                var client = _httpClientFactory.CreateClient("CommandClient");
                var response = await client.PostAsync(_commandServiceMetadata.Value.Endpoint, 
                                        new StringContent(JsonSerializer.Serialize(platformReadDto), Encoding.UTF8, "application/json"));
                status = response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {

            }
            return status;
        }
    }
}

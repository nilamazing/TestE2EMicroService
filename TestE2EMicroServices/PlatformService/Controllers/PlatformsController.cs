using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PlatformService.Abstract.Http;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _platformRepo;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandClient;

        public PlatformsController(IPlatformRepo platformRepo, IMapper mapper,
                        ICommandDataClient commandClient)
        {
            _platformRepo = platformRepo;
            _mapper = mapper;
            _commandClient = commandClient;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<PlatformReadDto>>> GetAllPlatforms()
        {
            var allPlatforms = await _platformRepo.GetPlatformsAsync();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(allPlatforms));
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public async Task<ActionResult<PlatformReadDto>> GetPlatformById(int id)
        {
            var platform = await _platformRepo.GetPlatform(id);
            return platform == null ? NotFound() : Ok(_mapper.Map<PlatformReadDto>(platform));
        }

        [HttpPost("")]
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            var platformModel = _mapper.Map<Platform>(platformCreateDto);
            await _platformRepo.CreatePlatform(platformModel);
            await _platformRepo.SaveChangesAsync();

            var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);
            return CreatedAtRoute(nameof(GetPlatformById), new {id = platformReadDto.Id}, platformReadDto);
        }

        [HttpPost("commands/comm")]
        public async Task<ActionResult<bool>> CommandsComm()
        {
            int platformId = new Random().Next(1, 3);
            var platform = await _platformRepo.GetPlatform(platformId);
            bool commandStatus = await _commandClient.SendPlatformToCommand(_mapper.Map<PlatformReadDto>(platform));
            return Ok(commandStatus);
        }

    }
}

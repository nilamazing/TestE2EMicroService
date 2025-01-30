using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        public PlatformsController(IPlatformRepo platformRepo, IMapper mapper)
        {
            _platformRepo = platformRepo;
            _mapper = mapper;
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
    }
}

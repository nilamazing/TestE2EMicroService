using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepo : IPlatformRepo
    {
        private readonly AppDbContext _context;
        public PlatformRepo(AppDbContext context)
        {

            _context = context;

        }
        public async Task CreatePlatform(Platform platform)
        {
            if (platform == null)
            {
                throw new ArgumentNullException(nameof(platform));
            }
            await _context.Platforms.AddAsync(platform);
        }

        public async Task<Platform> GetPlatform(int platformId)
        {
            return await _context.Platforms.AsNoTracking().FirstOrDefaultAsync(p => p.Id == platformId);
        }

        public async Task<IEnumerable<Platform>> GetPlatformsAsync()
        {
            return await _context.Platforms.AsNoTracking().ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

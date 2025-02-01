using Microsoft.EntityFrameworkCore;
using PlatformService.Abstract.Http;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.SyncDataServices.Http;

namespace PlatformService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMem");
            });
            services.Configure<CommandeServiceMetadataDto>(options => Configuration.GetSection("CommandsService").Bind(options));
            services.AddHttpClient("CommandClient", options =>
            {
                options.BaseAddress = new Uri(Configuration.GetSection("CommandsService:BaseUrl").Value);
            });
            services.AddScoped<ICommandDataClient, CommandDataClient>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IPlatformRepo, PlatformRepo>();
            services.AddControllers();
            //// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            if (env.IsDevelopment())
            {

                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            SeedDatabase.Seed(app);
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

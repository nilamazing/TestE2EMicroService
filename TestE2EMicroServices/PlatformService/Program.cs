using Microsoft.AspNetCore.Hosting;
using PlatformService;

internal class Program
{
    public static async Task Main(string[] args)
    {

        var hostBuilder = CreateHostBuilder(args);

        var host = hostBuilder.Build();
        host.Run();
        //// Add services to the container.

        //builder.Services.AddControllers();
        //// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        //builder.Services.AddEndpointsApiExplorer();
        //builder.Services.AddSwaggerGen();

        //var app = builder.Build();

        //// Configure the HTTP request pipeline.
        //if (app.Environment.IsDevelopment())
        //{
        //    app.UseSwagger();
        //    app.UseSwaggerUI();
        //}

        //app.UseHttpsRedirection();

        //app.UseAuthorization();

        //app.MapControllers();

        //app.Run();
    }
    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        var configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
           .Build();
        return Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
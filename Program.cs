using ChatAPI.Entities;
using ChatAPI.Interface;
using ChatAPI.Middleware;
using ChatAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NLog;
using NLog.Web;


var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    string[] connectionType = { "PrivateConnection", "BusinessConnection" };
    builder.Services.AddControllers();
    builder.Services.AddDbContext<ChatDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString(connectionType[1])));
    builder.Services.AddAutoMapper(typeof(Program).Assembly);
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IChannelService, ChannelService>();
    builder.Services.AddScoped<IChatService, ChatService>();

    builder.Services.AddScoped<ErrorHandlingMiddleware>();
    builder.Services.AddSwaggerGen();
    builder.Services.AddSignalR();

    // Nlog
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();


    var app = builder.Build();


    // Configure the HTTP request pipeline.

    app.UseMiddleware<ErrorHandlingMiddleware>();
    //app.MapHub<ChatHub>("/chathub");

    app.UseHttpsRedirection();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Chat API");
    });

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}
using ChatAPI;
using ChatAPI.Entities;
using ChatAPI.Interface;
using ChatAPI.Middleware;
using ChatAPI.Services;
using ChatAPI.Services.ScheduleService;
using ChatAPI.Services.SendMail;
using Microsoft.AspNetCore.HttpOverrides;
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
    builder.Services.AddScoped<ITokenService, TokenService>();
    builder.Services.AddScoped<ISendMail, SendMail>();
    builder.Services.AddScoped<IAccountService, AccountService>();
    builder.Services.AddScoped<ISessionService, SessionService>();

    builder.Services.AddHostedService<ScheduleService>();
    builder.Services.AddScoped<ErrorHandlingMiddleware>();
    builder.Services.AddScoped<BCryptHash>();
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

    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });

    app.UseRouting();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

    //app.MapControllers();

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
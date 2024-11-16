using ChatAPI.Interface;

namespace ChatAPI.Services.ScheduleService
{
    public class ScheduleService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public ScheduleService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var sessionService = scope.ServiceProvider.GetRequiredService<ISessionService>();
                    sessionService.RemoveInactiveSession();
                }

                await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
            }
        }

    }
}

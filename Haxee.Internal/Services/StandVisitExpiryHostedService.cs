using Haxee.Internal.Data;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Haxee.Internal.Services
{
    public class StandVisitExpiryHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<StandVisitExpiryHostedService> _logger;
        private readonly IDbContextFactory<DataContext> _dbContextFactory;
        private Timer? _timer = null;

        public StandVisitExpiryHostedService(
            ILogger<StandVisitExpiryHostedService> logger,
            IDbContextFactory<DataContext> dbContextFactory
        )
        {
            _logger = logger;
            _dbContextFactory = dbContextFactory;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service for stand visit expiry running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        private async void DoWork(object? state)
        {
            var db = _dbContextFactory.CreateDbContext();

            int count = 0;
            var runningStandVisits = await db
                .StandVisits.Include(x => x.Stand)
                .Where(x => x.LeaveTime == null)
                .ToListAsync();

            foreach (var standVisit in runningStandVisits)
            {
                if (
                    standVisit.StartTime.AddMinutes(standVisit.Stand.TimeLimitInMinutes)
                    > DateTime.Now
                )
                    continue;

                standVisit.LeaveTime = DateTime.Now;
                count++;
            }

            if (count == 0)
                return;

            await db.SaveChangesAsync();
            _logger.LogInformation($"Wrapped up {count} stand visits.");
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service for stand visit expiry is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}

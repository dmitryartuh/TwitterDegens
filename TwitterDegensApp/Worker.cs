using TwitterDegensApp.Services;

namespace TwitterDegensApp;

public class Worker : BackgroundService
{
	private readonly ILogger<Worker> _logger;
	private readonly IServiceScopeFactory _serviceScopeFactory;

	public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory)
	{
		_logger = logger;
		_serviceScopeFactory = serviceScopeFactory;
	}

	protected override async Task ExecuteAsync(CancellationToken ct)
	{
		using var serviceScope = _serviceScopeFactory.CreateScope();
		var twitterService = serviceScope.ServiceProvider.GetService<ITwitterService>();
		ArgumentNullException.ThrowIfNull(twitterService);
		
		await twitterService.UpsertCurrentUser(ct);
		
		while (!ct.IsCancellationRequested)
		{
			if (_logger.IsEnabled(LogLevel.Information))
			{
				_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
			}

			await Task.Delay(1000, ct);
		}
	}
}
using Tweetinvi;

namespace TwitterDegensApp;

public class Worker : BackgroundService
{
	private readonly ILogger<Worker> _logger;
	private readonly TwitterClient _twitterClient;

	public Worker(ILogger<Worker> logger, TwitterClient twitterClient)
	{
		_logger = logger;
		_twitterClient = twitterClient;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			if (_logger.IsEnabled(LogLevel.Information))
			{
				_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
			}
			
			var user = await _twitterClient.Users.GetAuthenticatedUserAsync();
			
			_logger.LogInformation(user.Name);

			await Task.Delay(1000, stoppingToken);
		}
	}
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Tweetinvi;
using TwitterDegensApp.Services;

namespace TwitterDegensApp;

internal class Program
{
	private static IConfiguration _configuration;

	public static void Main(string[] args)
	{
		_configuration = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json")
#if DEBUG
			.AddJsonFile("appsettings.Development.json")
#endif
			.Build();

		var twitterSettings = _configuration.GetSection("Twitter");

		var builder = Host.CreateApplicationBuilder(args);
		builder.Services.AddHostedService<Worker>();
		builder.Services.AddSingleton<TwitterClient>(_ => new TwitterClient(twitterSettings["ConsumerKey"], twitterSettings["ConsumerSecret"],
			twitterSettings["AccessToken"], twitterSettings["AccessTokenSecret"]));
		builder.Services.AddDbContext<Db>(options =>
		{
			options.UseSqlServer(builder.Configuration.GetConnectionString("Db"));
			options.ConfigureWarnings(c => c.Log((RelationalEventId.CommandExecuting, LogLevel.Debug)));
		});
		builder.Services.AddScoped<ITwitterService, TwitterService>();

		var host = builder.Build();
		host.Run();
	}
}
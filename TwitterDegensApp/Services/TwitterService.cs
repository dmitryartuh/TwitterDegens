using Microsoft.EntityFrameworkCore;
using Tweetinvi;
using TwitterDegensApp.Models;

namespace TwitterDegensApp.Services;

public class TwitterService(TwitterClient twitterClient, ILogger<TwitterService> logger, Db db) : ITwitterService
{
	public async Task UpsertCurrentUser(CancellationToken ct)
	{
		await db.Database.EnsureCreatedAsync(ct);
		
		var user = await twitterClient.Users.GetAuthenticatedUserAsync();
		logger.LogInformation($"Current user: {user.Name}, {user.Url}");
		
		var existingUser = await db.Users.FirstOrDefaultAsync(x => x.TwitterId == user.Id, ct);
		if (existingUser == null)
		{
			await db.Users.AddAsync(new User
			{
				TwitterId = user.Id,
				Name = user.Name,
			}, ct);
			
			await db.SaveChangesAsync(ct);
		}
	}
}
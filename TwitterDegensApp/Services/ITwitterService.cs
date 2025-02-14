namespace TwitterDegensApp.Services;

public interface ITwitterService
{
	Task UpsertCurrentUser(CancellationToken ct);
}
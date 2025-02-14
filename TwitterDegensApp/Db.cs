using Microsoft.EntityFrameworkCore;
using TwitterDegensApp.Models;

namespace TwitterDegensApp;

public class Db : DbContext
{
	public Db (DbContextOptions<Db> options)
		: base(options)
	{
	}
	
	public DbSet<User> Users { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<User>().ToTable("Users");
	}
}
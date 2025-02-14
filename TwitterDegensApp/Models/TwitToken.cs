using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterDegensApp.Models;

public class TwitToken
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	public int TwitId { get; set; }
	public int TokenId { get; set; }

	public Twit Twit { get; set; }
	public Token Token { get; set; }
}
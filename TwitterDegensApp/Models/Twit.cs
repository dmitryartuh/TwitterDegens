using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterDegensApp.Models;

public class Twit
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	public string Link { get; set; }
	public string Text { get; set; }
	public DateTime TwittedAt { get; set; }
	
	[InverseProperty(nameof(TwitToken.TwitId))]
	public IEnumerable<TwitToken> Tokens { get; set; }
}
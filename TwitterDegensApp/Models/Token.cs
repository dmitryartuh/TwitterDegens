using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterDegensApp.Models;

public class Token
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	public string Address { get; set; }
	public Chain Chain { get; set; }
	
	[InverseProperty(nameof(TwitToken.TokenId))]
	public IEnumerable<TwitToken> Twits { get; set; }
}
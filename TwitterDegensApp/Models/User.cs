using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterDegensApp.Models;

public class User
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
	public long TwitterId { get; set; }
	public string Name { get; set; }
}
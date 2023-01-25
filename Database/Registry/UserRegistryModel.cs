namespace App;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

/// Store registeration-order of user.
/// Each time a client attempt to register new user,
/// we store [email & deadline] of the client for register later.
[Table(DbConst.table_user_registry)]
[Index(nameof(token))]
public class UserRegistryModel {
	/// PK
	[Key]
	[Column("id")]
	public long id { get; set; }

	[Required]
	[Column("token", TypeName = "varchar(255)"), MaxLength(255)]
	public string token { get; set; }

	[Required]
	[EmailAddress]
	[Column("email", TypeName = "varchar(255)"), MaxLength(255)]
	public string email { get; set; }

	[Column("user_type")]
	public byte? user_type { get; set; } = UserRegistryModelConst.TYPE_USER;

	[Required]
	[Column("expired_at")]
	public DateTime expired_at { get; set; }

	[Required]
	[Column("created_at")]
	public DateTime created_at { get; set; }

	/// Indicates user has checked email, and confirmed the registeration.
	[Column("confirmed_at")]
	public DateTime? confirmed_at { get; set; }
}

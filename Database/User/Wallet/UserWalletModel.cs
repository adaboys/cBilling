namespace App;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[Table(DbConst.table_user_wallet)]
[Index(nameof(user_id), nameof(wallet_address))]
public class UserWalletModel : AutoGenerateUpdateTime {
	/// Id of user_wallet
	[Key]
	[Column("id")]
	public long id { get; set; }

	/// Point to field `user.id`.
	[Required]
	[ForeignKey(DbConst.table_user)]
	[Column("user_id")]
	public Guid user_id { get; set; }

	/// User wallet address which be generated at Cardano.
	[Required]
	[Column("wallet_address", TypeName = "varchar(255)"), MaxLength(255)]
	public string wallet_address { get; set; }

	/// Indicates the wallet is default or not.
	/// For a transaction if the user does not specify which wallet to be used,
	/// then default wallet will be used.
	[Required]
	[Column("is_default")]
	public bool is_default { get; set; }

	/// Indicates the wallet is ready for use. Otherwise, we consider it as inactive or invalid.
	[Required]
	[Column("is_active")]
	public bool is_active { get; set; }

	[Required]
	[Column("created_at")]
	public DateTime created_at { get; set; }

	[Column("updated_at")]
	public DateTime? updated_at { get; set; }

	[Column("deleted_at")]
	public DateTime? deleted_at { get; set; }

	/// Foreign key property attributes (property name must be same with table name)
	public UserModel user { get; set; }
}

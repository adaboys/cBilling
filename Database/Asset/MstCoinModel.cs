namespace App;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

/// Native token in Cardano.
[Table(DbConst.table_mst_coin)]
[Index(nameof(coin_name), IsUnique = true)]
[Index(nameof(token_id), IsUnique = true)]
public class MstCoinModel {
	/// PK
	[Key]
	[Column("id")]
	public int id { get; set; }

	/// Unique coin name (in our system) which be associated with Cardano token. See `MstNativeTokenModelConst.TOKEN_NAME_*`
	/// For eg,. ADA <=> lovelace, ISKY <=> Akas23023sadasd02dsadk.123efab
	[Required]
	[Column("coin_name", TypeName = "varchar(255)"), MaxLength(255)]
	public string coin_name { get; set; }

	/// Unique id of Cardano token.
	[Required]
	[Column("token_id", TypeName = "varchar(255)"), MaxLength(255)]
	public string token_id { get; set; }

	[Required]
	[Column("created_at")]
	public DateTime created_at { get; set; }
}

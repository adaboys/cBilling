namespace App;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

/// Detail history of asset transaction.
/// Each tx may contain multiple tx_detail records.
[Table(DbConst.table_tx_detail)]
[Index(nameof(tx_id), nameof(asset))]
public class TxDetailModel {
	/// PK
	[Key]
	[Column("id")]
	public long id { get; set; }

	[ForeignKey(DbConst.table_tx)]
	[Column("tx_id")]
	public long tx_id { get; set; }

	// [ForeignKey(DbConst.table_user)]
	[Column("sender_id")]
	public Guid? sender_id { get; set; }

	/// Source address (maybe not in our system)
	[Required]
	[Column("sender_address", TypeName = "varchar(255)"), MaxLength(255)]
	public string sender_address { get; set; }

	// [ForeignKey(DbConst.table_user)]
	[Column("receiver_id")]
	public Guid? receiver_id { get; set; }

	/// Destination address (maybe not in our system)
	[Required]
	[Column("receiver_address", TypeName = "varchar(255)"), MaxLength(255)]
	public string receiver_address { get; set; }

	/// Cardano asset id to be sent, for eg,. lovelace, Asadlkas023.adsa2222,...
	[Required]
	[Column("asset", TypeName = "varchar(255)"), MaxLength(255)]
	public string asset { get; set; }

	/// Amount of asset to be sent, for eg,. 123456000 lovelace, 3 Asadlkas023.adsa2222,...
	[Required]
	[Column("quantity")]
	public long quantity { get; set; }

	[Required]
	[Column("created_at")]
	public DateTime created_at { get; set; }

	/// FK models (property name must be same as table name)
	public TxModel tx { get; set; }
}

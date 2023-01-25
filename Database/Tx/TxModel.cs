namespace App;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

/// Asset Transaction. This is main tx of our system, so we name as `tx` for shorter.
[Table(DbConst.table_tx)]
[Index(nameof(action_type))]
public class TxModel : AutoGenerateUpdateTime {
	/// PK
	[Key]
	[Column("id")]
	public long id { get; set; }

	/// Tx type. See TxConst.ACTION_TYPE_*
	[Required]
	[Column("action_type")]
	public byte action_type { get; set; }

	/// Quick way to store number of tx detail item.
	[Required]
	[Column("tx_count")]
	public int tx_count { get; set; }

	/// Fee (in lovelace) of this transaction that the payer has paid to Cardano.
	[Required]
	[Column("fee_in_lovelace")]
	public int fee_in_lovelace { get; set; }

	/// Id of user_wallet that pays the fee
	[Required]
	[ForeignKey(DbConst.table_user_wallet)]
	[Column("fee_payerwallet_id")]
	public long fee_payerwallet_id { get; set; }

	[Required]
	[Column("created_at")]
	public DateTime created_at { get; set; }

	[Column("updated_at")]
	public DateTime? updated_at { get; set; }
}

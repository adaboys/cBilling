namespace App;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

/// Exchange-rate of currencies.
/// Formulas: closing_rate = from_currency / to_currency.
/// For eg,. if closing_rate of ADA/USD is 0.402, then 1 ADA = 0.402 USD, or 100 ADA = 40.2 USD.
/// Ref: https://dba.stackexchange.com/questions/221182/database-table-for-exchange-rates
[Table(DbConst.table_mst_exchange_rate)]
[Index(nameof(from_currency), nameof(to_currency))]
public class MstExchangeRateModel : AutoGenerateUpdateTime {
	[Key]
	[Column("id")]
	public int id { get; set; }

	/// For eg,. USD
	[Required]
	[Column("from_currency", TypeName = "varchar(255)"), MaxLength(255)]
	public string from_currency { get; set; }

	/// For eg,. ADA
	[Required]
	[Column("to_currency", TypeName = "varchar(255)"), MaxLength(255)]
	public string to_currency { get; set; }

	/// Final rate.
	[Required]
	[Column("closing_rate", TypeName = "decimal(19,9)")]
	public decimal closing_rate { get; set; }

	[Required]
	[Column("created_at", TypeName = "datetime")]
	public DateTime created_at { get; set; }

	[Column("updated_at", TypeName = "datetime")]
	public DateTime? updated_at { get; set; }
}

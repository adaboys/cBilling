namespace App;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table(DbConst.table_mst_app)]
public class MstAppModel : AutoGenerateUpdateTime {
	[Key]
	[Column("id")]
	public int id { get; set; }

	[Required]
	[Column("os_type")]
	public byte os_type { get; set; }

	/// For eg,. "2.12.108"
	[Required]
	[Column("version", TypeName = "varchar(255)"), MaxLength(255)]
	public string version { get; set; }

	[Required]
	[Column("created_at", TypeName = "datetime")]
	public DateTime created_at { get; set; }

	[Column("updated_at", TypeName = "datetime")]
	public DateTime? updated_at { get; set; }

	/// For soft-delete. Note that: avoid physical deletion.
	[Column("deleted_at")]
	public DateTime? deleted_at { get; set; }
}

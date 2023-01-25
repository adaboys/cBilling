namespace App;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[Table(DbConst.table_project)]
[Index(nameof(partner_id), nameof(project_name))]
[Index(nameof(project_code), IsUnique = true)]
public class ProjectModel : AutoGenerateUpdateTime {
	/// PK
	[Key]
	[Column("id")]
	public long id { get; set; }

	/// Partner id.
	[ForeignKey(DbConst.table_partner)]
	[Column("partner_id")]
	public long partner_id { get; set; }

	/// Unique project id of our system
	[Required]
	[Column("project_code", TypeName = "varchar(255)"), MaxLength(255)]
	public string project_code { get; set; }

	/// Set by partner
	[Required]
	[Column("project_name", TypeName = "varchar(255)"), MaxLength(255)]
	public string project_name { get; set; }

	/// To call api
	//todo use shakey more??
	[Required]
	[Column("secret_key", TypeName = "varchar(255)"), MaxLength(255)]
	public string secret_key { get; set; }

	/// To call api
	[Required]
	[Column("callback_url", TypeName = "varchar(255)"), MaxLength(255)]
	public string callback_url { get; set; }

	/// Cardano wallet address which be used to receive coin from users.
	[Required]
	[Column("wallet_address", TypeName = "varchar(255)"), MaxLength(255)]
	public string wallet_address { get; set; }

	/// Project status (active, inactive,...). See `PartnerProjectModelConst.STATUS_*`.
	[Column("status")]
	public byte status { get; set; } = ProjectModelConst.STATUS_ACTIVE;

	[Required]
	[Column("created_at")]
	public DateTime created_at { get; set; }

	[Column("updated_at")]
	public DateTime? updated_at { get; set; }

	[Column("deleted_at")]
	public DateTime? deleted_at { get; set; }

	/// Foreign key property attributes (property name must be same with table name)
	public PartnerModel partner { get; set; }
}

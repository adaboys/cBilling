namespace App;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

/// When an user perform authenticate (login, logout,...), a new token will be registered/unregistered in this table.
[Table(DbConst.table_partner_auth_token)]
[Index(nameof(partner_id), nameof(token))]
public class PartnerAuthTokenModel : AutoGenerateUpdateTime {
	[Key]
	[Column("id")]
	public long id { get; set; }

	/// Point to field `partner.id`.
	[ForeignKey(DbConst.table_partner)]
	[Column("partner_id")]
	public long partner_id { get; set; }

	/// [See Const] Indicates which way (id/pwd, provider, token,...) that user has used to login.
	[Required]
	[Column("login_type", TypeName = "tinyint")]
	public PartnerAuthTokenModelConst.LoginType login_type { get; set; }

	/// [See Const] Indicates about client environment (android, ios, web,...) that be used for login.
	[Required]
	[Column("client_type", TypeName = "tinyint")]
	public PartnerAuthTokenModelConst.ClientType client_type { get; set; }

	/// In general, this is unique, but should combine with user_id to conduct a PK.
	[Required]
	[Column("token", TypeName = "varchar(255)"), MaxLength(255)]
	public string token { get; set; }

	[Required]
	[Column("token_expired_at")]
	public DateTime token_expired_at { get; set; }

	[Required]
	[Column("created_at")]
	public DateTime created_at { get; set; }

	/// Ip of the client who creates this auth
	[Column("created_by_ip", TypeName = "varchar(255)"), MaxLength(255)]
	public string? created_by_ip { get; set; }

	/// UserAgent of the client who creates this auth
	[Column("created_by_agent", TypeName = "varchar(255)"), MaxLength(255)]
	public string? created_by_agent { get; set; }

	[Column("updated_at")]
	public DateTime? updated_at { get; set; }

	/// This is same as deleted_at, but is set when user want to revoke access (for eg,. log out, silent login,...).
	[Column("revoked_at")]
	public DateTime? revoked_at { get; set; }

	/// Ip of the client who revokes this auth
	[Column("revoked_by_ip", TypeName = "varchar(255)"), MaxLength(255)]
	public string? revoked_by_ip { get; set; }

	/// UserAgent of the client who revokes this auth
	[Column("revoked_by_agent", TypeName = "varchar(255)"), MaxLength(255)]
	public string? revoked_by_agent { get; set; }

	[Column("revoked_by_token", TypeName = "varchar(255)"), MaxLength(255)]
	public string? revoked_by_token { get; set; }

	/// Foreign key property attributes (property name must be same with table name)
	public PartnerModel partner { get; set; }
}

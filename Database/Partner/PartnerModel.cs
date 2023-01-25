namespace App;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

[Table(DbConst.table_partner)]
[Index(nameof(email), IsUnique = true)]
[Index(nameof(code), IsUnique = true)]
[Index(nameof(name))]
public class PartnerModel : AutoGenerateUpdateTime {
	/// PK
	[Key]
	[Column("id")]
	public long id { get; set; }

	/// Unique email that can be used for login
	[Required]
	[EmailAddress]
	[Column("email", TypeName = "varchar(255)"), MaxLength(255)]
	public string email { get; set; }

	/// Unique partner id
	[Required]
	[Column("code", TypeName = "varchar(255)"), MaxLength(255)]
	public string code { get; set; }

	/// Encrypted password
	[JsonIgnore]
	[Column("password", TypeName = "varchar(255)"), MaxLength(255)]
	public string? password { get; set; }

	/// Which level of privilege that partner have.
	/// Higher value indicates higher privilege.
	[Column("role", TypeName = "tinyint")]
	public PartnerModelConst.Role role { get; set; } = PartnerModelConst.Role.Admin;

	/// [See Const] Where is the partner signup from (idpwd, google, facebook,...)
	[Required]
	[Column("signup_type", TypeName = "tinyint")]
	public PartnerModelConst.SignupType signup_type { get; set; }

	/// Shop name, for eg,. MyShop
	[Required]
	[Column("name", TypeName = "varchar(255)"), MaxLength(255)]
	public string name { get; set; }

	/// Telephone number, for eg,. +84 978 999 485
	[Column("telno", TypeName = "varchar(255)"), MaxLength(255)]
	public string? telno { get; set; }

	/// [See Const] General status for use permission.
	[Column("status", TypeName = "tinyint")]
	public PartnerModelConst.Status status { get; set; } = PartnerModelConst.Status.Normal;

	/// For normal login via email/password, this will be increased on each login with wrong password.
	/// In general, exceed 3 times will be blocked for 3 months.
	[Column("login_failed_count")]
	public int login_failed_count { get; set; } = 0;

	/// When a client exceeds limit count of login via email/password, then login will be denied.
	[Column("login_denied_at")]
	public DateTime? login_denied_at { get; set; }

	[Required]
	[Column("created_at")]
	public DateTime created_at { get; set; }

	[Column("updated_at")]
	public DateTime? updated_at { get; set; }

	[Column("deleted_at")]
	public DateTime? deleted_at { get; set; }
}

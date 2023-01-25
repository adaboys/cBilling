namespace App;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

/// MS SQL data types:
/// https://docs.microsoft.com/en-us/sql/t-sql/data-types/data-types-transact-sql?view=sql-server-ver15
/// Below is some data types, note that, `ByteCount <= 256` when storing string
/// will better performance since they are stored in memory instead of disk storage.
/// - char: fixed length characters in UTF-8., it take 1 byte per character.
/// - varchar: various length characters in UTF-8, it take 1 byte per character.
/// - nchar: fixed length characters in Unicode (UTF-16?), it take 2 bytes per character.
/// - nvarchar: various length characters in Unicode (UTF-16?), it take 2 bytes per character.

/// [C# Data Type] =>  [SQL Server Data Type]
/// int            =>  int
/// string         =>  nvarchar(Max)
/// decimal        =>  decimal(18,2)
/// float          =>  real
/// byte[]         =>  varbinary(Max)
/// datetime       =>  datetime
/// bool           =>  bit
/// byte           =>  tinyint
/// short          =>  smallint
/// long           =>  bigint
/// double         =>  float
/// char           =>  No mapping
/// sbyte          =>  No mapping (throws exception)
/// object         =>  No mapping

/// About decimal(p,s) where p is precision (max 38 digits), and s is scale (max 38 digit).
/// TechNote: Precision is the number of digits in a number. Scale is the number of digits to the right of the decimal point in a number.
/// For example, the number 123.45 has a precision of 5 and a scale of 2.
/// In SQL Server, the default maximum precision of numeric and decimal data types is 38, that is max value is in decimal(38, 38).
/// In earlier versions of SQL Server, the default maximum is 28.
/// SQL server type: https://docs.microsoft.com/en-us/sql/t-sql/data-types/precision-scale-and-length-transact-sql?view=sql-server-ver16
/// Cardano db sync schema: https://github.com/input-output-hk/cardano-db-sync/blob/master/doc/schema.md
[Table(DbConst.table_user)]
[Index(nameof(email), IsUnique = true)]
[Index(nameof(code), IsUnique = true)]
public class UserModel : AutoGenerateUpdateTime {
	/// We use unique identifier (uuid, guid) for better data-merging, id-confliction, hard-guessing,...
	/// Use `Key` attribute to mark this as PK.
	[Key]
	[Column("id")]
	public Guid id { get; set; }

	/// Unique email that can be used for login
	[Required]
	[EmailAddress]
	[Column("email", TypeName = "varchar(255)"), MaxLength(255)]
	public string email { get; set; }

	/// Unique nickname that can be used for login (see `email`)
	[Column("code", TypeName = "varchar(255)"), MaxLength(255)]
	public string? code { get; set; }

	/// Encrypted password
	[JsonIgnore]
	[Column("password", TypeName = "varchar(255)"), MaxLength(255)]
	public string? password { get; set; }

	/// Full name, for eg,. Dark Compet
	[Column("full_name", TypeName = "varchar(255)"), MaxLength(255)]
	public string? full_name { get; set; }

	/// [See Const] Sex type (male, female,...)
	[Column("gender", TypeName = "tinyint")]
	public UserModelConst.Gender gender { get; set; } = UserModelConst.Gender.Nothing;

	/// [See Const] Where is the partner signup from (idpwd, google, facebook,...)
	[Required]
	[Column("signup_type", TypeName = "tinyint")]
	public UserModelConst.SignupType signup_type { get; set; }

	/// Telephone number, for eg,. +84 978 999 485
	[Column("telno", TypeName = "varchar(255)"), MaxLength(255)]
	public string? telno { get; set; }

	/// [See Const] Which level of privilege that user have.
	/// Higher value indicates higher privilege.
	[Column("role", TypeName = "tinyint")]
	public UserModelConst.Role role { get; set; } = UserModelConst.Role.User;

	/// [See Const] General status for use permission.
	[Column("status", TypeName = "tinyint")]
	public UserModelConst.Status status { get; set; } = UserModelConst.Status.Normal;

	/// For normal login via email/password, this will be increased on each login with wrong password.
	/// In general, exceed 3 times will be blocked for 3 months.
	[Column("login_failed_count")]
	public int login_failed_count { get; set; } = 0;

	/// When a client exceeds limit count of login via email/password, then login will be denied.
	[Column("login_denied_at")]
	public DateTime? login_denied_at { get; set; }

	/// Ref: https://english.stackexchange.com/questions/104740/created-at-or-created-in
	/// - Use `created at` for precise times.
	/// - Use `created on` for days, dates.
	/// - Use `created in` for duration.
	[Required]
	[Column("created_at")]
	public DateTime created_at { get; set; }

	[Column("updated_at")]
	public DateTime? updated_at { get; set; }

	[Column("deleted_at")]
	public DateTime? deleted_at { get; set; }
}

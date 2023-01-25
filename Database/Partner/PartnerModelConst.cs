namespace App;

/// Constants for table `user`.
public class PartnerModelConst {
	public enum Status {
		Normal = 1, // OK, valid user
		Blocked = 2, // NG, the user was blocked since take invalid our policy
	}

	public enum Role {
		Operator = 40,
		Admin = 80,
		Root = 100,
	}

	public enum SignupType {
		IdPwd = 1,
		Google = 2,
		Facebook = 3,
		Apple = 4,
	}

	public static string CalcSimpleProvider(SignupType signup_type) {
		return signup_type switch {
			SignupType.Google => "gg",
			SignupType.Facebook => "fb",
			_ => throw new InternalServerErrorException($"Not support signup type: {signup_type}"),
		};
	}
}

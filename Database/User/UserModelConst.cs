namespace App;

/// Constants for table `user`.
/// Should NOT use zero-constant since 0 is considered as nothing/null (unset).
public class UserModelConst {
	public enum Gender {
		Nothing = 0,
		Male = 1,
		Female = 2,
		Other = 3,
	}

	public enum Role {
		User = 10,
		Admin = 80,
		Root = 100,
	}

	public enum Status {
		Normal = 1, // OK, valid user
		Blocked = 2, // NG, the user was blocked since take invalid our policy
	}

	public enum SignupType {
		IdPwd = 1,
		Google = 2,
		Facebook = 3,
		Apple = 4,
	}
}

namespace App;

using Microsoft.EntityFrameworkCore;

public class PartnerAuthTokenModelConst {
	public enum ClientType {
		Android = 1,
		Ios = 2,
		Web = 3,
	}

	public enum LoginType {
		IdPwd = 1,
		Provider = 2,
		Silent = 3,
	}
}

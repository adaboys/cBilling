namespace App;

using System.Security.Cryptography;

public class Helper {
	const string optCodeChars = "123456789ABCDEFGHJKLMNPQRSTWXYZ";
	const string alphabets = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

	public static string GenerateOtpCode(int length = 6) {
		var optCode = "";
		while (length-- > 0) {
			optCode += optCodeChars[Random.Shared.Next(optCodeChars.Length)];
		}
		return optCode;
	}

	/// Generate unique code that each character is in alphabet only.
	public static string GenerateAlphabetCode(int codeLength = 40) {
		var guid = Guid.NewGuid().ToStringDk();

		if (codeLength > guid.Length) {
			// Old code:
			// var randomNumber = new byte[codeLength - guid.Length];
			// using var rng = RandomNumberGenerator.Create();
			// rng.GetBytes(randomNumber);
			// return guid + Convert.ToBase64String(randomNumber);

			var suffix = string.Empty;
			var remainCount = codeLength - guid.Length;
			while (remainCount-- > 0) {
				suffix += alphabets[Random.Shared.Next(alphabets.Length)];
			}

			return guid + suffix;
		}

		return guid;
	}

	public static string GenerateSecretKey(int codeLength = 50) {
		var guid = Guid.NewGuid().ToStringDk();

		if (codeLength > guid.Length) {
			var randomNumber = new byte[codeLength - guid.Length];
			using var rng = RandomNumberGenerator.Create();
			rng.GetBytes(randomNumber);

			return guid + Convert.ToBase64String(randomNumber);
		}

		return guid;
	}
}

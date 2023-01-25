namespace App;

public static class GuidExt {
	/// Express guid in string without hyphen.
	public static string ToStringDk(this Guid me) {
		return me.ToString("N");
	}
}

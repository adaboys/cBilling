namespace App;

public class TxModelConst {
	/// For field `action_type`
	public const byte ACTION_TYPE_MINT_NFT = 1;
	public const byte ACTION_TYPE_SELL_NFT = 2;
	public const byte ACTION_TYPE_BUY_NFT = 3;
	public const byte ACTION_TYPE_SEND_NFT = 4;
	public const byte ACTION_TYPE_SEND_COIN = 5;

	/// Hold all actions on NFT.
	public static readonly byte[] NFT_ACTIONS = new[] {
		ACTION_TYPE_MINT_NFT,
		ACTION_TYPE_BUY_NFT,
		ACTION_TYPE_SELL_NFT,
		ACTION_TYPE_SEND_NFT
	};
}

namespace Rino.GameFramework.BuffSystem
{
	/// <summary>
	/// Buff 過期資訊，用於 Model Observable 通知 Controller Buff 過期
	/// </summary>
	public struct BuffExpiredInfo
	{
		/// <summary>
		/// Buff 識別碼
		/// </summary>
		public string BuffId;

		/// <summary>
		/// 擁有者識別碼
		/// </summary>
		public string OwnerId;

		/// <summary>
		/// Buff 名稱
		/// </summary>
		public string BuffName;

		public BuffExpiredInfo(string buffId, string ownerId, string buffName)
		{
			BuffId = buffId;
			OwnerId = ownerId;
			BuffName = buffName;
		}
	}
}

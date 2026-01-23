using Rino.GameFramework.GameManagerBase;

namespace Rino.GameFramework.BuffSystem
{
	/// <summary>
	/// Buff 資料編輯器
	/// </summary>
	public class BuffDataEditor : CreateNewDataEditor<BuffData>
	{
		public override string TabName => "Buff 資料";

		protected override string DataRoot => "Data/Buff";

		protected override string DataTypeLabel => "Buff";
	}
}

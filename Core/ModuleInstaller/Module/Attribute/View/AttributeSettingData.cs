using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Rino.GameFramework.AttributeSystem
{
	/// <summary>
	/// 屬性系統配置，包含所有屬性的定義
	/// </summary>
	public class AttributeSettingData : SerializedScriptableObject
	{
		[OdinSerialize]
		[ListDrawerSettings(ListElementLabelName = "AttributeName", DraggableItems = false)]
		[LabelText("屬性列表")]
		public List<AttributeConfig> Attributes = new();
	}
}
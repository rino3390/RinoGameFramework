using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Rino.GameFramework.AttributeSystem
{
	/// <summary>
	/// 屬性系統設定，包含所有屬性的定義
	/// </summary>
	[CreateAssetMenu(menuName = "RinoGameFramework/Setting/AttributeSettingData")]
	public class AttributeSettingData : SerializedScriptableObject
	{
		[OdinSerialize]
		[ListDrawerSettings(
			HideAddButton = true,
			ListElementLabelName = "AttributeName",
			DraggableItems = false)]
		[LabelText("屬性列表")]
		public List<AttributeEntry> Attributes = new();

		[Button("新增屬性", ButtonSizes.Medium)]
		[GUIColor(0.4f, 0.8f, 0.4f)]
		private void AddAttribute()
		{
			Attributes.Add(new AttributeEntry { AttributeName = "NewAttribute" });
		}

		/// <summary>
		/// 轉換為 AttributeConfig 列表
		/// </summary>
		/// <returns>屬性配置列表</returns>
		public List<AttributeConfig> ToConfigs()
		{
			return Attributes?.Select(a => a.ToConfig()).ToList() ?? new List<AttributeConfig>();
		}
	}

	/// <summary>
	/// 屬性定義項目
	/// </summary>
	[Serializable]
	public class AttributeEntry
	{
		[LabelText("屬性名稱")]
		public string AttributeName;

		[FoldoutGroup("最小值設定")]
		[LabelText("啟用最小值限制")]
		public bool HasMin;

		[FoldoutGroup("最小值設定")]
		[ShowIf(nameof(HasMin))]
		[LabelText("使用關聯屬性")]
		public bool UseRelationMin;

		[FoldoutGroup("最小值設定")]
		[ShowIf("@" + nameof(HasMin) + " && !" + nameof(UseRelationMin))]
		[LabelText("最小值")]
		public int Min;

		[FoldoutGroup("最小值設定")]
		[ShowIf("@" + nameof(HasMin) + " && " + nameof(UseRelationMin))]
		[LabelText("關聯屬性")]
		public string RelationMin;

		[FoldoutGroup("最大值設定")]
		[LabelText("啟用最大值限制")]
		public bool HasMax;

		[FoldoutGroup("最大值設定")]
		[ShowIf(nameof(HasMax))]
		[LabelText("使用關聯屬性")]
		public bool UseRelationMax;

		[FoldoutGroup("最大值設定")]
		[ShowIf("@" + nameof(HasMax) + " && !" + nameof(UseRelationMax))]
		[LabelText("最大值")]
		public int Max;

		[FoldoutGroup("最大值設定")]
		[ShowIf("@" + nameof(HasMax) + " && " + nameof(UseRelationMax))]
		[LabelText("關聯屬性")]
		public string RelationMax;

		[LabelText("比例轉換")]
		public int Ratio = 1;

		/// <summary>
		/// 轉換為 AttributeConfig
		/// </summary>
		/// <returns>屬性配置</returns>
		public AttributeConfig ToConfig()
		{
			return new AttributeConfig
			{
				AttributeName = AttributeName,
				Min = HasMin && !UseRelationMin ? Min : int.MinValue,
				Max = HasMax && !UseRelationMax ? Max : int.MaxValue,
				RelationMin = HasMin && UseRelationMin ? RelationMin ?? "" : "",
				RelationMax = HasMax && UseRelationMax ? RelationMax ?? "" : "",
				Ratio = Ratio
			};
		}
	}
}

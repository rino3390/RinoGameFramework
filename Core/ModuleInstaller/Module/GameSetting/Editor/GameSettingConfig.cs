using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace Rino.GameFramework.GameSetting
{
	/// <summary>
	/// 遊戲設定配置，管理要顯示的 Setting 項目
	/// </summary>
	[CreateAssetMenu(menuName = "RinoGameFramework/Setting/GameSettingConfig")]
	public class GameSettingConfig : SerializedScriptableObject
	{
		[OdinSerialize]
		[ListDrawerSettings(
			CustomAddFunction = nameof(CreateNewItem),
			ListElementLabelName = "Name",
			DraggableItems = true)]
		[LabelText("Setting 列表")]
		public List<SettingItemData> Settings = new();

		private SettingItemData CreateNewItem()
		{
			return new SettingItemData { Name = "New Setting" };
		}
	}

	/// <summary>
	/// Setting 項目資料
	/// </summary>
	[Serializable]
	public class SettingItemData
	{
		[HorizontalGroup("Main", 0.08f)]
		[HideLabel]
		[LabelText("圖示")]
		public SdfIconType Icon = SdfIconType.GearFill;

		[HorizontalGroup("Main")]
		[LabelText("名稱")]
		public string Name;

		[LabelText("Setting Data")]
		[InlineEditor(InlineEditorObjectFieldModes.Foldout)]
		public ScriptableObject Data;
	}
}

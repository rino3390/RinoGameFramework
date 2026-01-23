using Rino.GameFramework.AttributeSystem;
using Rino.GameFramework.GameManagerBase;
using Rino.GameFramework.RinoUtility;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace Rino.GameFramework.GameSetting
{
	/// <summary>
	/// 遊戲設定編輯器選單
	/// </summary>
	public class GameSettingEditorMenu : GameEditorMenuBase
	{
		public override string TabName => "遊戲設定";

		protected override OdinMenuTree BuildMenuTree()
		{
			var tree = SetTree(iconSize: 20);
			tree.Add("屬性設定", GetAttributeSettingData(), SdfIconType.Sliders);
			return tree;
		}

		private AttributeSettingData GetAttributeSettingData()
		{
			var data = RinoEditorUtility.FindAsset<AttributeSettingData>();
			if (data != null) return data;

			data = ScriptableObject.CreateInstance<AttributeSettingData>();
			RinoEditorUtility.CreateSOData(data, "Data/AttributeSettingData");
			return data;
		}
	}
}

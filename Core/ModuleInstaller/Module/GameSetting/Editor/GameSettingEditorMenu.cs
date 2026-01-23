using Rino.GameFramework.GameManagerBase;
using Rino.GameFramework.RinoUtility;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace Rino.GameFramework.GameSetting
{
	/// <summary>
	/// 遊戲設定編輯器選單
	/// </summary>
	public class GameSettingEditorMenu : GameEditorMenuBase
	{
		public override string TabName => "遊戲設定";

		private GameSettingConfig config;

		protected override OdinMenuTree BuildMenuTree()
		{
			var tree = SetTree(iconSize: 20);
			config = GetOrCreateConfig();

			foreach (var item in config.Settings)
			{
				if (item.Data != null)
				{
					tree.Add(item.Name, item.Data, item.Icon);
				}
			}

			return tree;
		}

		private GameSettingConfig GetOrCreateConfig()
		{
			var data = RinoEditorUtility.FindAsset<GameSettingConfig>();
			if (data != null) return data;

			data = ScriptableObject.CreateInstance<GameSettingConfig>();
			RinoEditorUtility.CreateSOData(data, "Data/GameSetting/Config");
			return data;
		}
	}
}

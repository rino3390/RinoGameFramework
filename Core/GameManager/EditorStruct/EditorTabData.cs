using Rino.GameFramework.GameManagerBase;
using Rino.GameFramework.RinoUtility.Editor;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Rino.GameFramework.GameManager
{
	/// <summary>
	/// Editor 頁籤資料
	/// </summary>
	[HideReferenceObjectPicker]
	public class EditorTabData
	{
		/// <summary>
		/// 頁籤圖示
		/// </summary>
		[FoldoutGroup("標籤設定", true)]
		public SdfIconType TabIcon;

		/// <summary>
		/// 是否有左側選單
		/// </summary>
		[FoldoutGroup("Editor 設定", true)]
		[LabelText("是否有左側菜單")]
		public bool HasMenuTree;

		/// <summary>
		/// 是否繪製圖示
		/// </summary>
		[FoldoutGroup("Editor 設定")]
		[ShowIf("HasMenuTree"), LabelText("左列繪製 Icon")]
		public bool HasIcon;

		/// <summary>
		/// 圖示大小
		/// </summary>
		[FoldoutGroup("Editor 設定")]
		[ShowIf("@HasMenuTree && HasIcon"), LabelText("Icon 大小")]
		public float IconSize = 28;

		/// <summary>
		/// 對應的 Editor 視窗類型
		/// </summary>
		[FoldoutGroup("Editor 設定")]
		[ValueDropdown("GetWindowTypeList")]
		[LabelText("繪製視窗")]
		[Required]
		public Type CorrespondingWindowType;

		private static IEnumerable GetWindowTypeList()
		{
			// 掃描有 [DataEditorConfig] 的 SODataBase，取得 DynamicDataEditor<T> 類型
			var dataEditorTypes = RinoEditorUtility.GetTypesWithAttribute<SODataBase, DataEditorConfigAttribute>()
				.Select(dataType => new ValueDropdownItem(
					dataType.GetCustomAttribute<DataEditorConfigAttribute>().TabName,
					typeof(DynamicDataEditor<>).MakeGenericType(dataType)
				));

			// 掃描其他 GameEditorMenuBase（排除 DynamicDataEditor）
			var otherEditorTypes = RinoEditorUtility.GetDerivedClasses<GameEditorMenuBase>(excludeGenericBase: typeof(DynamicDataEditor<>))
				.Select(type =>
				{
					var instance = Activator.CreateInstance(type) as GameEditorMenuBase;
					return new ValueDropdownItem(instance?.TabName ?? type.Name, type);
				});

			return dataEditorTypes.Concat(otherEditorTypes).ToList();
		}
	}
}
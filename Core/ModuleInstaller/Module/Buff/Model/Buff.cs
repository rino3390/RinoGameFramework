using Rino.GameFramework.DDDCore;
using Rino.GameFramework.RinoUtility;
using System;
using System.Collections.Generic;

namespace Rino.GameFramework.BuffSystem
{
	/// <summary>
	/// Buff Entity，效果容器
	/// </summary>
	public class Buff: Entity
	{
		/// <summary>
		/// Buff 名稱
		/// </summary>
		public string BuffName { get; }

		/// <summary>
		/// 擁有者識別碼
		/// </summary>
		public string OwnerId { get; }

		/// <summary>
		/// 來源識別碼（施加者）
		/// </summary>
		public string SourceId { get; }

		/// <summary>
		/// 剩餘持續時間（秒），null 表示永久
		/// </summary>
		public float? RemainingDuration { get; private set; }

		/// <summary>
		/// 剩餘回合數，null 表示永久
		/// </summary>
		public int? RemainingTurns { get; private set; }

		/// <summary>
		/// 是否已過期
		/// </summary>
		public bool IsExpired => RemainingDuration is <= 0 || RemainingTurns is <= 0 || StackCount <= 0;

		/// <summary>
		/// 最大堆疊數，null 表示無上限
		/// </summary>
		public int? MaxStack { get; }

		/// <summary>
		/// 當前堆疊數
		/// </summary>
		public int StackCount { get; private set; }

		/// <summary>
		/// Modifier 記錄列表
		/// </summary>
		public List<ModifierRecord> ModifierRecords { get; }

		/// <summary>
		/// 堆疊變化事件
		/// </summary>
		public ReactiveEvent<BuffStackChangedInfo> OnStackChanged { get; } = new();

		/// <summary>
		/// 過期事件（當 Buff 剛好變成過期時觸發）
		/// </summary>
		public ReactiveEvent<BuffExpiredInfo> OnExpired { get; } = new();

		public Buff(string id, string buffName, string ownerId, string sourceId, int? maxStack, float? duration, int? turns): base(id)
		{
			if(string.IsNullOrEmpty(buffName)) throw new ArgumentException("BuffName cannot be null or empty.", nameof(buffName));

			if(string.IsNullOrEmpty(ownerId)) throw new ArgumentException("OwnerId cannot be null or empty.", nameof(ownerId));

			if(string.IsNullOrEmpty(sourceId)) throw new ArgumentException("SourceId cannot be null or empty.", nameof(sourceId));

			BuffName = buffName;
			OwnerId = ownerId;
			SourceId = sourceId;
			StackCount = 1;
			MaxStack = maxStack;
			RemainingDuration = duration;
			RemainingTurns = turns;
			ModifierRecords = new List<ModifierRecord>();
		}

		/// <summary>
		/// 變更疊層數
		/// </summary>
		/// <param name="delta">變更量（正數增加，負數減少）</param>
		public void ChangeStack(int delta)
		{
			var oldStack = StackCount;
			StackCount += delta;
			if (MaxStack.HasValue)
			{
				StackCount = Math.Min(StackCount, MaxStack.Value);
			}

			StackCount = Math.Max(0, StackCount);

			if (StackCount != oldStack)
			{
				OnStackChanged.Invoke(new BuffStackChangedInfo(Id, OwnerId, BuffName, oldStack, StackCount));
			}
		}

		/// <summary>
		/// 刷新持續時間
		/// </summary>
		/// <param name="duration">新的持續時間</param>
		public void RefreshDuration(float? duration)
		{
			RemainingDuration = duration;
		}

		/// <summary>
		/// 刷新回合數
		/// </summary>
		/// <param name="turns">新的回合數</param>
		public void RefreshTurns(int? turns)
		{
			RemainingTurns = turns;
		}

		/// <summary>
		/// 調整持續時間
		/// </summary>
		/// <param name="delta">變更量（正數減少，負數增加）</param>
		public void AdjustDuration(float delta)
		{
			if (!RemainingDuration.HasValue) return;

			var wasExpired = IsExpired;
			RemainingDuration -= delta;

			if (!wasExpired && IsExpired) OnExpired.Invoke(new BuffExpiredInfo(Id, OwnerId, BuffName));
		}

		/// <summary>
		/// 調整回合數
		/// </summary>
		/// <param name="delta">變更量（正數減少，負數增加）</param>
		public void AdjustTurns(int delta)
		{
			if (!RemainingTurns.HasValue) return;

			var wasExpired = IsExpired;
			RemainingTurns -= delta;

			if (!wasExpired && IsExpired) OnExpired.Invoke(new BuffExpiredInfo(Id, OwnerId, BuffName));
		}

		/// <summary>
		/// 記錄 Modifier
		/// </summary>
		/// <param name="attributeName">屬性名稱</param>
		/// <param name="modifierId">Modifier 識別碼</param>
		public void RecordModifier(string attributeName, string modifierId)
		{
			ModifierRecords.Add(new ModifierRecord(attributeName, modifierId));
		}

		/// <summary>
		/// 移除最後一筆 Modifier 記錄（用於堆疊減少時，以 LIFO 順序移除對應的 Modifier）
		/// </summary>
		/// <returns>被移除的記錄，若無記錄則回傳 null</returns>
		public ModifierRecord RemoveLastModifierRecord()
		{
			if(ModifierRecords.Count == 0) return null;

			var last = ModifierRecords[^1];
			ModifierRecords.RemoveAt(ModifierRecords.Count - 1);
			return last;
		}
	}
}
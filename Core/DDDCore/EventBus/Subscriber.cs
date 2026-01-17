using System;
using Cysharp.Threading.Tasks;

namespace Rino.GameFramework.DDDCore
{
    /// <summary>
    /// 事件訂閱工具，提供簡化的事件訂閱 API
    /// </summary>
    public class Subscriber
    {
        private readonly IEventBus eventBus;

        public Subscriber(IEventBus eventBus)
        {
            this.eventBus = eventBus;
        }

        /// <summary>
        /// 訂閱同步事件
        /// </summary>
        /// <typeparam name="TEvent">事件類型，必須實作 IEvent</typeparam>
        /// <param name="handler">事件處理器</param>
        /// <param name="filter">事件過濾器，可選</param>
        /// <returns>訂閱的 Disposable，呼叫 Dispose 可取消訂閱</returns>
        public IDisposable Subscribe<TEvent>(Action<TEvent> handler, Predicate<TEvent> filter = null) where TEvent : IEvent
        {
            return eventBus.Subscribe(handler, filter);
        }

        /// <summary>
        /// 訂閱非同步事件
        /// </summary>
        /// <typeparam name="TEvent">事件類型，必須實作 IEvent</typeparam>
        /// <param name="handler">非同步事件處理器</param>
        /// <param name="filter">事件過濾器，可選</param>
        /// <returns>訂閱的 Disposable，呼叫 Dispose 可取消訂閱</returns>
        public IDisposable SubscribeAsync<TEvent>(Func<TEvent, UniTask> handler, Predicate<TEvent> filter = null) where TEvent : IEvent
        {
            return eventBus.SubscribeAsync(handler, filter);
        }
    }
}

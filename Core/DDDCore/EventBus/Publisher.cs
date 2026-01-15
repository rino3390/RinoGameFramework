using System;
using Cysharp.Threading.Tasks;

namespace Rino.GameFramework.DDDCore
{
    /// <summary>
    /// 事件發布工具，提供簡化的事件發布 API
    /// </summary>
    public class Publisher
    {
        private readonly IEventBus eventBus;

        public Publisher(IEventBus eventBus)
        {
            this.eventBus = eventBus;
        }

        /// <summary>
        /// 發布同步事件
        /// </summary>
        /// <typeparam name="TEvent">事件類型，必須實作 IEvent</typeparam>
        /// <param name="evt">要發布的事件</param>
        /// <exception cref="ArgumentNullException">evt 為 null 時拋出</exception>
        public void Publish<TEvent>(TEvent evt) where TEvent : IEvent
        {
            if (evt == null)
                throw new ArgumentNullException(nameof(evt));

            eventBus.Publish(evt);
        }

        /// <summary>
        /// 發布非同步事件
        /// </summary>
        /// <typeparam name="TEvent">事件類型，必須實作 IEvent</typeparam>
        /// <param name="evt">要發布的事件</param>
        /// <returns>非同步任務</returns>
        /// <exception cref="ArgumentNullException">evt 為 null 時拋出</exception>
        public UniTask PublishAsync<TEvent>(TEvent evt) where TEvent : IEvent
        {
            if (evt == null)
                throw new ArgumentNullException(nameof(evt));

            return eventBus.PublishAsync(evt);
        }
    }
}

using System;
using Cysharp.Threading.Tasks;
using MessagePipe;

namespace Rino.GameFramework.DDDCore
{
    /// <summary>
    /// 事件匯流排實作，封裝 MessagePipe
    /// </summary>
    public class EventBus : IEventBus
    {
        private readonly IServiceProvider serviceProvider;

        public EventBus(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        /// <inheritdoc />
        public void Publish<TEvent>(TEvent evt) where TEvent : IEvent
        {
            if (evt == null)
                throw new ArgumentNullException(nameof(evt));

            var publisher = serviceProvider.GetService(typeof(IPublisher<TEvent>)) as IPublisher<TEvent>;
            publisher?.Publish(evt);
        }

        /// <inheritdoc />
        public async UniTask PublishAsync<TEvent>(TEvent evt) where TEvent : IEvent
        {
            if (evt == null)
                throw new ArgumentNullException(nameof(evt));

            var publisher = serviceProvider.GetService(typeof(IAsyncPublisher<TEvent>)) as IAsyncPublisher<TEvent>;
            if (publisher != null)
            {
                await publisher.PublishAsync(evt);
            }
        }

        /// <inheritdoc />
        public IDisposable Subscribe<TEvent>(Action<TEvent> handler, Predicate<TEvent> filter = null) where TEvent : IEvent
        {
            var subscriber = serviceProvider.GetService(typeof(ISubscriber<TEvent>)) as ISubscriber<TEvent>;
            if (subscriber == null)
                return new EmptyDisposable();

            if (filter != null)
            {
                return subscriber.Subscribe(evt =>
                {
                    if (filter(evt))
                        handler(evt);
                });
            }

            return subscriber.Subscribe(handler);
        }

        /// <inheritdoc />
        public IDisposable SubscribeAsync<TEvent>(Func<TEvent, UniTask> handler, Predicate<TEvent> filter = null) where TEvent : IEvent
        {
            var subscriber = serviceProvider.GetService(typeof(IAsyncSubscriber<TEvent>)) as IAsyncSubscriber<TEvent>;
            if (subscriber == null)
                return new EmptyDisposable();

            if (filter != null)
            {
                return subscriber.Subscribe(async (evt, _) =>
                {
                    if (filter(evt))
                        await handler(evt);
                });
            }

            return subscriber.Subscribe(async (evt, _) => await handler(evt));
        }

        private class EmptyDisposable : IDisposable
        {
            public void Dispose() { }
        }
    }
}

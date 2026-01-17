using MessagePipe;
using MessagePipe;
using Zenject;

namespace Rino.GameFramework.DDDCore
{
    /// <summary>
    /// DDDCore Zenject Installer，負責註冊 EventBus 相關服務
    /// </summary>
    public class DDDCoreInstaller : Installer<DDDCoreInstaller>
    {
        public override void InstallBindings()
        {
            var options = Container.BindMessagePipe();
            Container.Bind<MessagePipeOptions>().FromInstance(options).AsSingle();

            Container.Bind<IEventBus>().To<EventBus>().AsSingle();
            Container.Bind<Publisher>().AsSingle();
            Container.Bind<Subscriber>().AsSingle();
        }
    }
}

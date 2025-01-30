using Configs;
using Ecs;
using Ecs.Buttons;
using Ecs.Windows;
using MessagePipe;
using MessagePipeEvents;
using UI;
using UI.Mediators;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installers
{
    public class GameInstaller : LifetimeScope
    {
        [SerializeField] private UIConfig m_UIConfig;
        [SerializeField] private GameConfig m_GameConfig;
        
        protected override void Configure(IContainerBuilder builder)
        {
            InstallMessagePipe(builder);
            InstallUI(builder);
            InstallGameLogic(builder);
        }

        private void InstallMessagePipe(IContainerBuilder builder)
        {
            var options = builder.RegisterMessagePipe();
            builder.RegisterMessageBroker<ButtonOpenWindowClickedEvent>(options);
            builder.RegisterMessageBroker<CloseWindowClickedEvent>(options);
            builder.RegisterMessageBroker<OpenWindowEvent>(options);
            builder.RegisterMessageBroker<UIInitEvent>(options);
        }

        private void InstallUI(IContainerBuilder builder)
        {
            builder.RegisterInstance(m_UIConfig);
            builder.RegisterComponentInHierarchy<Canvas>();
            builder.Register<UISpawner>(Lifetime.Singleton);

            builder.Register<ButtonMediator>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<StatusBarMediator>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<WindowMediator>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }

        private void InstallGameLogic(IContainerBuilder builder)
        {
            builder.RegisterInstance(m_GameConfig);
            
            builder.RegisterEntryPoint<GameControllerSystem>();
        }
    }
}
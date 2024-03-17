using Common.Circle;
using Common.Containers.GameManagerServices;
using Common.Environment_System;
using Common.Game_Manager_System;
using Common.Initializers;
using Common.Sounds;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Common.Containers
{
    public class GameBaseLifetimeScope: LifetimeScope
    {
        [SerializeField] private WhiteCircleController _whiteCircleController;
        [SerializeField] private BlackCircleController _blackCircleController;

        [SerializeField] private SoundManager _soundManager;

        [SerializeField] private SettingsPanel _settingsPanel;
        [SerializeField] private GridSpawner _gridSpawner;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IGameManagerService, GameManager>(Lifetime.Singleton);

            builder.Register<ICircleSpawnService, CircleSpawner>(Lifetime.Singleton)
                .WithParameter(_whiteCircleController)
                .WithParameter(_blackCircleController);

            builder.RegisterInstance<ISoundManagerService, SoundManager>(_soundManager).AsSelf();
            
            builder.RegisterInstance<ISettingsPanelService, SettingsPanel>(_settingsPanel);
            
            builder.RegisterInstance(_gridSpawner).AsSelf();
            
            builder.RegisterEntryPoint<GameInitializer>();
        }
    }
}
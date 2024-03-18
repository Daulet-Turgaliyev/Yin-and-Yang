using Common.Containers.GameManagerServices;
using Common.Data;
using Common.Game_Manager_System;
using Common.Main_Menu;
using Common.Sounds;
using UnityEngine;
using VContainer.Unity;

namespace Common.Initializers
{
    public class GameInitializer: IStartable
    {
        private readonly IGameManagerService _gameManagerService;
        private readonly ISettingsPanelService _settingsPanelService;
        private readonly ICellSpawnerService _spawnerService;
        private readonly ISoundManagerService _soundManagerService;
        private readonly ICircleSpawnService _circleSpawnService;

        public GameInitializer(
            IGameManagerService gameManagerService, 
            ISettingsPanelService settingsPanelService,
            ICellSpawnerService spawnerService,
            ISoundManagerService soundManagerService,
            ICircleSpawnService circleSpawnService)
        {
            _gameManagerService = gameManagerService;
            _settingsPanelService = settingsPanelService;
            _spawnerService = spawnerService;
            _soundManagerService = soundManagerService;
            _circleSpawnService = circleSpawnService;
        }
        
        public void Start()
        {
            SoundPackPreset soundPackPreset = GameSettings.SoundPackPreset;

            _gameManagerService.StartInitialize();
            _settingsPanelService.StartInitialize();
            _spawnerService.Initialize(soundPackPreset);
            _soundManagerService.Initialize(soundPackPreset);
            _circleSpawnService.Initialize(soundPackPreset);
        }
    }
}
using Common.Containers.GameManagerServices;
using VContainer.Unity;

namespace Common.Initializers
{
    public class GameInitializer: IStartable
    {
        private readonly IGameManagerService _gameManagerService;
        private readonly ISettingsPanelService _settingsPanelService;

        public GameInitializer(IGameManagerService gameManagerService, ISettingsPanelService settingsPanelService)
        {
            _gameManagerService = gameManagerService;
            _settingsPanelService = settingsPanelService;
        }
        
        public void Start()
        {
            _gameManagerService.StartInitialize();
            _settingsPanelService.StartInitialize();
        }
    }
}
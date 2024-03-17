using System;
using Common.Circle;
using Common.Containers.GameManagerServices;
using Common.Game_Manager_System;
using Common.Sounds;
using TMPro;
using UnityEngine;
using VContainer;

namespace Common
{
    public class SettingsPanel : MonoBehaviour, ISettingsPanelService
    {
        [SerializeField] private GameObject _loadingPanelObject;
        
        private ICircleSpawnService _circleSpawnService;
        private ISoundManagerService _soundManagerService;
        
        public bool IsNeedVibration { get; set; }

        public TextMeshProUGUI _soundPackIdText;

        private IGameManagerService _gameManagerService;
        
        [Inject]
        public void Construct(ICircleSpawnService circleSpawnService, IGameManagerService gameManagerService, ISoundManagerService soundManagerService)
        {
            _circleSpawnService = circleSpawnService;
            _gameManagerService = gameManagerService;
            _soundManagerService = soundManagerService;
            
            SetSoundPack(0);
        }

        private void Start()
        {
            Destroy(_loadingPanelObject, 2);
        }

        public void StartInitialize()
        {
        }
        
        public void SetVibration(bool isNeedVibro)
        {
            IsNeedVibration = isNeedVibro;
        }

        public void SetSoundPack(float soundPackId)
        {
            _soundManagerService.SetIndex(soundPackId);
            _soundPackIdText.text = soundPackId.ToString("0");
        }

        public void SetWhiteCount(float count)
        {
            _circleSpawnService.SpawnBalls(CircleType.White, (int)count);
        }

        public void SetBlackCount(float count)
        {
            _circleSpawnService.SpawnBalls(CircleType.Black, (int)count);
        }
        public void SetCirclesSpeed(float speed)
        {
            _gameManagerService.ChangeCircleSpeed(speed);
        }
        
        public void SetCircleSoundVolume(float volume)
        {
            _soundManagerService.SetCircleSoundVolume(volume);
        }
        
        public void SetBackgroundVolume(float volume)
        {
            _soundManagerService.SetBackgroundMusicVolume(volume);
        }
    }
}
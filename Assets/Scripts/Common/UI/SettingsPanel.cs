using System;
using Common.Circle;
using Common.Containers.GameManagerServices;
using Common.Game_Manager_System;
using Common.Sounds;
using Common.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Common
{
    public class SettingsPanel : MonoBehaviour, ISettingsPanelService
    {
        private ICircleSpawnService _circleSpawnService;
        private ISoundManagerService _soundManagerService;
        private IGameManagerService _gameManagerService;
        public bool IsNeedVibration { get; set; }

        [SerializeField] private SwipePanel _swipePanel;

        [SerializeField] private Toggle _toggleVibration;
        [SerializeField] private Slider _collisionSound;
        [SerializeField] private Slider _backgroundSound;
        [SerializeField] private Slider _whiteCountSlide;
        [SerializeField] private Slider _blackCountSlide;
        [SerializeField] private Slider _circleSpeedSlide;
   
        
        [Inject]
        public void Construct(ICircleSpawnService circleSpawnService, ISoundManagerService soundManagerService, IGameManagerService gameManagerService)
        {
            _circleSpawnService = circleSpawnService;
            _soundManagerService = soundManagerService;
            _gameManagerService = gameManagerService;
        }

        private void Start()
        {
            _swipePanel.onOpened += UpdatePanel;

            float colissionSoundVolume = 1;
            float backgroundnSoundVolume = 1;

            if (PlayerPrefs.HasKey("COLLISION_SOUND"))
            {
                colissionSoundVolume = PlayerPrefs.GetFloat("COLLISION_SOUND");
            }
            
            if (PlayerPrefs.HasKey("BACKGROUND_SOUND"))
            {
                backgroundnSoundVolume = PlayerPrefs.GetFloat("BACKGROUND_SOUND");
            }
            
            SetCircleSoundVolume(colissionSoundVolume);
            SetBackgroundVolume(backgroundnSoundVolume);
        }

        private void OnDestroy()
        {
            _swipePanel.onOpened -= UpdatePanel;

        }

        public void StartInitialize()
        {
        }
        
        public void SetVibration(bool isNeedVibro)
        {
            IsNeedVibration = isNeedVibro;
        }
        
        public void SetWhiteCount(float count)
        {
            _circleSpawnService.SpawnBalls(CircleType.White, (int)count);
        }

        public void SetBlackCount(float count)
        {
            _circleSpawnService.SpawnBalls(CircleType.Black, (int)count);
        }
        
        public void SetCircleSpeed(float speed)
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

        private void UpdatePanel()
        {
            _toggleVibration.isOn = IsNeedVibration;
            _collisionSound.value = _soundManagerService.CollisionSoundVolume;
            _backgroundSound.value = _soundManagerService.BackgroundVolume;
            _whiteCountSlide.value = _circleSpawnService.WhiteCount;
            _blackCountSlide.value = _circleSpawnService.BlackCount;
            _circleSpeedSlide.value = _gameManagerService.CurrentSpeed;
        }
    }
}
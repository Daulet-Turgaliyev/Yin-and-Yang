using System;
using Common.Data;
using UnityEngine;

namespace Common.UI
{
    public class SoundPackLoader : MonoBehaviour
    {
        [SerializeField] private MainMenu _mainMenu;
        
        [SerializeField] private Transform _contentTransform;
        [SerializeField] private SoundPackPreset[] _soundPackPresets;
        [SerializeField] private SoundPackElement _soundPackElement;

        private void Start()
        {
            Load();
        }

        private void Load()
        {
            foreach (var soundPackPreset in _soundPackPresets)
            {
                var element = Instantiate(_soundPackElement, _contentTransform);
                element.Initialize(soundPackPreset);
                element.onPackSelected += StartGame;
            }
        }

        private void StartGame(SoundPackPreset soundPackPreset)
        {
            _mainMenu.LoadLevel(soundPackPreset);
        }
    }
}

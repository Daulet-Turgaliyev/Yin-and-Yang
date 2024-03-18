using System;
using Common.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Common.UI
{
    public class SoundPackElement : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _labelText;
        [SerializeField] private Image _labelImage;
        private SoundPackPreset _soundPackPreset;

        public Action<SoundPackPreset> onPackSelected = data => { };

        public void Initialize(SoundPackPreset soundPackData)
        {
            _labelText.text = soundPackData.SoundPackName;
            _labelImage.sprite = soundPackData.SoundLogo;

            _soundPackPreset = soundPackData;
        }
        
        public void OnClick()
        {
            onPackSelected?.Invoke(_soundPackPreset);
        }
    }
}
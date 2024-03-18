using System;
using Common.Data;
using Common.Sounds;
using UnityEngine;
using VContainer.Unity;

namespace Common.Environment_System
{
    public class SoundManager : MonoBehaviour, ISoundManagerService
    {
        [SerializeField]
        private AudioSource _audioSource;
        [SerializeField]
        private AudioSource _backGroundAudioSource;

        private SoundPackData _soundPacks;

        public float BackgroundVolume => _backGroundAudioSource.volume;

        public float CollisionSoundVolume => _audioSource.volume;

        public void PlayBackgroundSound()
        {
            _backGroundAudioSource.clip = _soundPacks.GetBackgroundMusicClip();
            _backGroundAudioSource.Play();
        }

        public void Initialize(SoundPackPreset soundPackPreset)
        {
            _soundPacks = soundPackPreset.SoundPackData;
        }

        public void PlayRandomSound()
        {
            _audioSource.PlayOneShot(_soundPacks.GetRandomClip());
        }
    
        public void PlayRandomWallSound()
        {
            _audioSource.PlayOneShot(_soundPacks.GetRandomClip());
        }
        
        public void SetBackgroundMusicVolume(float volume)
        {
            _backGroundAudioSource.volume = volume;
        }

        public void SetCircleSoundVolume(float volume)
        {
            _audioSource.volume = volume;
        }
    }
}
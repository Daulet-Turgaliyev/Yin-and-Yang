using System;
using Common.Data;
using Common.Sounds;
using UnityEngine;
using VContainer.Unity;

namespace Common.Environment_System
{
    public class SoundManager : MonoBehaviour, ISoundManagerService
    {
        private int _index;
        
        [SerializeField]
        private AudioSource _audioSource;
        [SerializeField]
        private AudioSource _backGroundAudioSource;

        [SerializeField] private SoundPackData[] _soundPacks;

        public void PlayBackgroundSound()
        {
            _backGroundAudioSource.clip = _soundPacks[_index].GetBackgroundMusicClip();
            _backGroundAudioSource.Play();
        }
    
        public void PlayRandomSound()
        {
            _audioSource.PlayOneShot(_soundPacks[_index].GetRandomClip());
        }
    
        public void PlayRandomWallSound()
        {
            _audioSource.PlayOneShot(_soundPacks[_index].GetRandomClip());
        }

        public void SetIndex(float index)
        {
            _index = (int)index;
            PlayBackgroundSound();
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
using UnityEngine;

namespace Common.Data
{
    [CreateAssetMenu(fileName = "newSoundPack", menuName = "Create Sound Pack", order = 0)]
    public class SoundPackData : ScriptableObject
    {
        [SerializeField] public AudioClip _backgroundMusic;
        [SerializeField] public AudioClip[] _clips;
        [SerializeField] public AudioClip[] _wallClips;

        public AudioClip GetBackgroundMusicClip()
        {
            return _backgroundMusic;
        }
    
        public AudioClip GetRandomClip()
        {
            int randomIndex = Random.Range(0, _clips.Length);
            return _clips[randomIndex];
        }
    
        public AudioClip GetRandomWallClip()
        {
            int randomIndex = Random.Range(0, _wallClips.Length);
            return _wallClips[randomIndex];
        }
    }
}
using Common.Data;

namespace Common.Sounds
{
    public interface ISoundManagerService
    {
        void Initialize(SoundPackPreset soundPackPreset);
         void PlayRandomSound();
         void PlayRandomWallSound();
         void SetBackgroundMusicVolume(float volume);
         void SetCircleSoundVolume(float volume);
         float BackgroundVolume { get; }
         float CollisionSoundVolume { get; }
    }
}
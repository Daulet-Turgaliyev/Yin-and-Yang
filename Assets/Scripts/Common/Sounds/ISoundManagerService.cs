namespace Common.Sounds
{
    public interface ISoundManagerService
    {
         void PlayBackgroundSound();
         void PlayRandomSound();
         void PlayRandomWallSound();
         void SetIndex(float index);
         void SetBackgroundMusicVolume(float volume);
         void SetCircleSoundVolume(float volume);
    }
}
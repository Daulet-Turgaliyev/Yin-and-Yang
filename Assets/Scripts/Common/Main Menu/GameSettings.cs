using Common.Data;

namespace Common.Main_Menu
{
    public static class GameSettings
    {
        private static SoundPackPreset _soundPackPreset;

        public static SoundPackPreset SoundPackPreset => _soundPackPreset;

        public static void SetSoundDataPreset(SoundPackPreset soundPackPreset)
        {
            _soundPackPreset = soundPackPreset;
        }
    }
}
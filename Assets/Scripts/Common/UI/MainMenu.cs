using Common.Data;
using Common.GlobalScene;
using Common.Main_Menu;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Common.UI
{
    public class MainMenu : MonoBehaviour
    {
        public void LoadLevel(SoundPackPreset soundPackPreset)
        {
            GameSettings.SetSoundDataPreset(soundPackPreset);   
            GlobalSceneManager.LoadSceneAsync("Assets/Scenes/GameScene.unity");
        }
    }
}

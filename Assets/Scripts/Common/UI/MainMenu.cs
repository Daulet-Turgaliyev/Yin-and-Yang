using Common.Data;
using Common.GlobalScene;
using Common.Main_Menu;
using DG.Tweening;
using UnityEngine;

namespace Common.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _startPanelCanvasGroup;
        [SerializeField] private CanvasGroup _levelPanelCanvasGroup;

        private void Start()
        {
            _startPanelCanvasGroup.DOFade(1, 1);
        }

        public void OpenLevelSelect()
        {
            _startPanelCanvasGroup.DOFade(0, 1).OnComplete(() =>
            {
                _levelPanelCanvasGroup.gameObject.SetActive(true);
                _levelPanelCanvasGroup.DOFade(1, 1);
            });
        }
        

        public void LoadLevel(SoundPackPreset soundPackPreset)
        {
            _levelPanelCanvasGroup.DOFade(0, 1).OnComplete(() =>
            {
                GameSettings.SetSoundDataPreset(soundPackPreset);   
                GlobalSceneManager.LoadSceneAsync("Assets/Scenes/GameScene.unity");
            });
        }
    }
}

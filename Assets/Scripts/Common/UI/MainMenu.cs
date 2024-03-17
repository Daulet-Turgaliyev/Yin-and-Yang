using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common.UI
{
    public class MainMenu : MonoBehaviour
    {
        public void LoadLevel()
        {
            SceneManager.LoadScene("Scenes/GameScene");
        }
    }
}

using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Common.GlobalScene
{
    public static class GlobalSceneManager
    {
        private const string loadingSceneName = "Assets/Scenes/LoadingScene.unity";
        public static bool HasActivated;
        
        public static async void LoadSceneAsync(string sceneName)
        {
            
            await SceneManager.LoadSceneAsync(loadingSceneName, LoadSceneMode.Additive);

            var asyncLoad = SceneManager.LoadSceneAsync(sceneName);

            while (!asyncLoad.isDone)
            {
                // loadingScreen.SetProgress(asyncLoad.progress);
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
            
        }
    }
}
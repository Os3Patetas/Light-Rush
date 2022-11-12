using UnityEngine;
using UnityEngine.SceneManagement;

namespace com.icypeak.scene
{
    public class MainMenuScene : MonoBehaviour
    {
        void Start()
        {
            Screen.orientation = ScreenOrientation.Portrait;
            Screen.autorotateToLandscapeRight = false;
            Screen.autorotateToLandscapeLeft = false;
            Screen.autorotateToPortrait = true;
            Screen.autorotateToPortraitUpsideDown = false;
        }
        public void RedirectToGameScene()
        {
            SceneManager.LoadScene("GameScene");
        }
        public void RedirectToStoreScene()
        {
            SceneManager.LoadScene("StoreScene");
        }
    }
}

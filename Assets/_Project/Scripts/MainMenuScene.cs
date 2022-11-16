using UnityEngine;
using UnityEngine.SceneManagement;

namespace com.icypeak.scene
{
    public class MainMenuScene : MonoBehaviour
    {
        void Start()
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            Screen.autorotateToLandscapeRight = true;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToPortrait = false;
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

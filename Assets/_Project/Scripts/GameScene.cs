using UnityEngine;
using UnityEngine.SceneManagement;

namespace com.icypeak
{
    public class GameScene : MonoBehaviour
    {
        [SerializeField] GameObject PauseUI;
        void Start()
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            Screen.autorotateToLandscapeRight = true;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToPortrait = false;
            Screen.autorotateToPortraitUpsideDown = false;
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
            PauseUI.SetActive(true);
        }

        public void UnpauseGame()
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1;
        }

        public void RestartGame()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ReturnMainMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu");
        }
    }
}

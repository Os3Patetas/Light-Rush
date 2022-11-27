using UnityEngine;
using UnityEngine.SceneManagement;
using com.icypeak.player;


namespace com.icypeak
{
    public class GameScene : MonoBehaviour
    {
        [SerializeField] GameObject PauseUI;
        [SerializeField] GameObject GameOverUI;
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

        void ActivateGameOverUI()
        {
            Time.timeScale = 0;
            GameOverUI.SetActive(true);
        }

        void OnEnable()
        {
            Player.OnDeath += ActivateGameOverUI;
        }

        void OnDisable()
        {
            Player.OnDeath -= ActivateGameOverUI;
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using com.Icypeak.Data;

namespace com.icypeak.scene
{
    public class MainMenuScene : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI coins;
        [SerializeField] TextMeshProUGUI cash;

        void Start()
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            Screen.autorotateToLandscapeRight = true;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToPortrait = false;
            Screen.autorotateToPortraitUpsideDown = false;
            RefreshCurrencyUI();
        }
        void RefreshCurrencyUI()
        {
            coins.text = "Coins: " + LocalDataManager.Instance.Currency.Coins.ToString();
            cash.text = "Cash: " + LocalDataManager.Instance.Currency.Cash.ToString();
        }

        public void RedirectToGameScene()
        {
            SceneManager.LoadScene("GameScene");
        }
        public void RedirectToStoreScene()
        {
            SceneManager.LoadScene("StoreScene");
        }

        void OnEnable()
        {
            LocalDataManager.Instance.OnCurrencyChange += RefreshCurrencyUI;
        }

        void OnDisable()
        {
            if (LocalDataManager.Instance is null) return;
            LocalDataManager.Instance.OnCurrencyChange -= RefreshCurrencyUI;
        }
    }
}

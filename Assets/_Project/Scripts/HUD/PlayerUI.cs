using UnityEngine;
using TMPro;
using com.icypeak.managers;

namespace com.icypeak.ui
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _scoreTextComponent;

        void Start() =>
            _scoreTextComponent.text = "0";

        void OnEnable() =>
            ScoreManager.Instance.OnScoreChange += UpdateScoreText;

        void OnDisable()
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.OnScoreChange -= UpdateScoreText;
            }
        }

        void UpdateScoreText() =>
            _scoreTextComponent.text = ScoreManager.Instance.Score.ToString();
    }
}

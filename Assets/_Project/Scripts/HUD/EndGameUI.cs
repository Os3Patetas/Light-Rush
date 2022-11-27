using UnityEngine;
using TMPro;

using com.icypeak.managers;
using com.Icypeak.Data;

namespace com.icypeak
{
    public class EndGameUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI scoreText;
        [SerializeField] TextMeshProUGUI bonusText;
        [SerializeField] TextMeshProUGUI coinsText;
        [SerializeField] TextMeshProUGUI totalText;

        void OnEnable()
        {
            var score = ScoreManager.Instance.Score;
            var bonus = 0;
            var coins = score;
            var total = LocalDataManager.Instance.Currency.Coins + coins + bonus;

            LocalDataManager.Instance.UpdateLocalCoins(total);

            scoreText.text = $"Score: {score.ToString()}";
            bonusText.text = bonus.ToString();
            coinsText.text = coins.ToString();
            totalText.text = total.ToString();
        }
    }
}

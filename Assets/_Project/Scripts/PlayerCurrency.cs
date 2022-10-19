using UnityEngine;

namespace com.icypeak
{
    [CreateAssetMenu(fileName ="Player Currency", menuName ="Database/Currency")]
    public class PlayerCurrency : ScriptableObject
    {
        public int Gold;
        public int Cash;

        public PlayerCurrency(int gold, int cash)
        {
            Gold = gold;
            Cash = cash;
        }
    }
}

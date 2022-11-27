using System;
using UnityEngine;

namespace com.Icypeak.Data
{
    public class LocalDataManager : MonoBehaviour
    {
        public static LocalDataManager Instance;

        public GameData Game;
        public CurrencyData Currency;
        public GameInfo Info;

        public Action OnGameDataChange;
        public Action OnCurrencyChange;

        bool succesfullSignIn;
        string googleID;

        void Awake()
        {
            if (Instance != this && Instance != null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }
            DontDestroyOnLoad(this.gameObject);

            succesfullSignIn = true;
            if (succesfullSignIn)
            {
                googleID = "crookedLegs";
                Info.LastUserID = googleID;
                UpdateLocalGameInfoData();
            }
            print(Info.LastUserID);
        }

        void OnEnable()
        {
            print(Application.persistentDataPath + '/' + "fileName" + ".save");
            RefreshLocalData();
        }

        public void RefreshLocalData()
        {
            Currency = SaveSystem.Load<CurrencyData>();
            Game = SaveSystem.Load<GameData>();
            Info = SaveSystem.Load<GameInfo>();
        }

        public void UpdateLocalGameData(GameData newData)
        {
            SaveSystem.Save<GameData>(newData);
            Game = newData;
            OnGameDataChange?.Invoke();
        }

        public void UpdateLocalCoins(int newValue)
        {
            Currency.Coins = newValue;
            SaveSystem.Save<CurrencyData>(Currency);
            OnCurrencyChange?.Invoke();
        }

        public void UpdateLocalCurrencyData(CurrencyData newData)
        {
            Currency = newData;
            SaveSystem.Save<CurrencyData>(Currency);
            OnCurrencyChange?.Invoke();
        }

        public void UpdateLocalGameInfoData()
        {
            SaveSystem.Save<GameInfo>(Info);
        }

    }
}
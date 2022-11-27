using System;
using Firebase.Database;
using UnityEngine;
using System.Collections;

namespace com.Icypeak.Data
{
    public class DatabaseManager : MonoBehaviour
    {
        public static DatabaseManager Instance;
        public string userId;
        DatabaseReference dbRef;


        private void Awake()
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
        }

        void Start()
        {
            userId = LocalDataManager.Instance.Info.LastUserID;
            print(userId);
            dbRef = FirebaseDatabase.DefaultInstance.RootReference;

            StartCoroutine(StartDB());
        }

        IEnumerator StartDB()
        {
            var currency = dbRef.Child("currency").Child(userId).GetValueAsync();
            var gameData = dbRef.Child(LocalDataManager.Instance.Info.GameName).Child(userId).GetValueAsync();
            yield return new WaitUntil(predicate: () => currency.IsCompleted && gameData.IsCompleted);
            if (currency != null && gameData != null)
            {
                DataSnapshot snapshotCurrency = currency.Result;
                DataSnapshot snapshotGameData = gameData.Result;
                print("Currency Exists: " + snapshotCurrency.Exists);
                print("GameInfo Exists: " + snapshotGameData.Exists);

                if (snapshotCurrency.Exists)
                {
                    var dbData = new CurrencyData();
                    dbData.Coins = int.Parse(snapshotCurrency.Child("Coins").Value.ToString());
                    dbData.Cash = int.Parse(snapshotCurrency.Child("Cash").Value.ToString());
                    print(dbData.Coins);
                    LocalDataManager.Instance.UpdateLocalCurrencyData(dbData);
                }
                else
                {
                    var infoToJson = JsonUtility.ToJson(LocalDataManager.Instance.Currency);
                    dbRef.Child("currency").Child(userId).SetRawJsonValueAsync(infoToJson);
                }

                if (snapshotGameData.Exists)
                {
                    var dbData = new GameData();
                    dbData.AlltimeScore = int.Parse(snapshotGameData.Child("AlltimeScore").Value.ToString());
                    dbData.DailyScore = int.Parse(snapshotGameData.Child("DailyScore").Value.ToString());
                    dbData.MonthlyScore = int.Parse(snapshotGameData.Child("MonthlyScore").Value.ToString());
                    dbData.WeeklyScore = int.Parse(snapshotGameData.Child("WeeklyScore").Value.ToString());

                    LocalDataManager.Instance.UpdateLocalGameData(dbData);
                }
                else
                {
                    var infoToJson = JsonUtility.ToJson(LocalDataManager.Instance.Game);
                    dbRef.Child(LocalDataManager.Instance.Info.GameName).Child(userId).SetRawJsonValueAsync(infoToJson);
                }
            }
        }

        public void UpdateCurrencyDB()
        {
            var infoCurrencyToJson = JsonUtility.ToJson(LocalDataManager.Instance.Currency);
            dbRef.Child("currency").Child(userId).SetRawJsonValueAsync(infoCurrencyToJson);
        }

        public void UpdateGameDataDB()
        {
            var infoGameToJson = JsonUtility.ToJson(LocalDataManager.Instance.Game);
            dbRef.Child(LocalDataManager.Instance.Info.GameName).Child(userId).SetRawJsonValueAsync(infoGameToJson);
        }

        void OnEnable()
        {
            LocalDataManager.Instance.OnCurrencyChange += UpdateCurrencyDB;
            LocalDataManager.Instance.OnGameDataChange += UpdateGameDataDB;
        }

        void OnDisable()
        {
            if (LocalDataManager.Instance is null) return;

            LocalDataManager.Instance.OnCurrencyChange -= UpdateCurrencyDB;
            LocalDataManager.Instance.OnGameDataChange -= UpdateGameDataDB;
        }

    }
}
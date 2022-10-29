using Firebase.Database;
using UnityEngine;
using TMPro;
using System.Collections;
using System;
using com.icypeak;

public class DatabaseManager : MonoBehaviour
{
    public TMP_InputField Gold;
    public TMP_InputField Cash;

    string userId;
    DatabaseReference dbRef;
    public bool DataIsReadyToGather;
    [SerializeField] PlayerCurrency playerCurrency;

    private void Awake()
    {
        playerCurrency = Resources.Load<PlayerCurrency>("Database/Player Currency");
    }


    void Start()
    {
        userId = SystemInfo.deviceUniqueIdentifier;
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;

        StartCoroutine(GetCurrency((string a) => { }));
    }

    public void UpdateCurrency()
    {
        var info = new Currency(int.Parse(Gold.text), int.Parse(Cash.text));
        string infoToJson = JsonUtility.ToJson(info);

        dbRef.Child("currency").Child(userId).SetRawJsonValueAsync(infoToJson);
        StartCoroutine(GetCurrency((string a) => { }));
    }

    public IEnumerator GetCurrency(Action<string> onCallback)
    {
        var userGold = dbRef.Child("currency").Child(userId).GetValueAsync();
        yield return new WaitUntil(predicate: () => userGold.IsCompleted);
        if(userGold != null)
        {
            DataSnapshot snapshot = userGold.Result;
            Gold.text = snapshot.Child("gold").Value.ToString();
            Cash.text = snapshot.Child("cash").Value.ToString();

            playerCurrency.Gold = int.Parse(snapshot.Child("gold").Value.ToString());
            playerCurrency.Cash = int.Parse(snapshot.Child("cash").Value.ToString());
        }
    }
}

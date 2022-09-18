using Firebase.Database;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DatabaseManager : MonoBehaviour
{
    public TMP_InputField Name;
    public TMP_InputField Gold;

    string userId;
    DatabaseReference dbRef;


    void Start()
    {
        userId = SystemInfo.deviceUniqueIdentifier;
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void CreateUser()
    {
        User newUser = new User(Name.text, int.Parse(Gold.text));
        string json = JsonUtility.ToJson(newUser);

        dbRef.Child("users").Child(userId).SetRawJsonValueAsync(json);
    }

}

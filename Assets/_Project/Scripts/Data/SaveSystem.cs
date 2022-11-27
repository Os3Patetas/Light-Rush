using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace com.Icypeak.Data
{
    public static class SaveSystem
    {

        public static void Save<T>(T data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + $"/{typeof(T).Name}.save";
            FileStream stream = new FileStream(path, FileMode.Create);

            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static T Load<T>() where T : class, new()
        {
            string path = Application.persistentDataPath + $"/{typeof(T).Name}.save";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                T data = formatter.Deserialize(stream) as T;

                stream.Close();

                return data;
            }
            var newData = new T();
            Save<T>(newData);

            return newData;
        }

        // public static void SaveCurrency(CurrencyData data)
        // {
        //     BinaryFormatter formatter = new BinaryFormatter();
        //     string path = Application.persistentDataPath + "Currency.save";
        //     FileStream stream = new FileStream(path, FileMode.Create);

        //     formatter.Serialize(stream, data);
        //     stream.Close();
        // }

        // public static void SaveGameData(GameData data)
        // {
        //     BinaryFormatter formatter = new BinaryFormatter();
        //     string path = Application.persistentDataPath + "Currency.save";
        //     FileStream stream = new FileStream(path, FileMode.Create);

        //     formatter.Serialize(stream, data);
        //     stream.Close();
        // }

        // public static CurrencyData LoadCurrency()
        // {
        //     string path = Application.persistentDataPath + "/Currency.save";
        //     if (File.Exists(path))
        //     {
        //         BinaryFormatter formatter = new BinaryFormatter();
        //         FileStream stream = new FileStream(path, FileMode.Open);
        //         CurrencyData data = formatter.Deserialize(stream) as CurrencyData;

        //         stream.Close();

        //         return data;
        //     }
        //     var newData = new CurrencyData();
        //     SaveCurrency(newData);

        //     return newData;
        // }

        // public static GameData LoadGameData()
        // {
        //     string path = Application.persistentDataPath + "/Currency.save";
        //     if (File.Exists(path))
        //     {
        //         BinaryFormatter formatter = new BinaryFormatter();
        //         FileStream stream = new FileStream(path, FileMode.Open);
        //         GameData data = formatter.Deserialize(stream) as GameData;

        //         stream.Close();

        //         return data;
        //     }
        //     var newData = new GameData();
        //     SaveGameData(newData);

        //     return newData;
        // }
    }
}
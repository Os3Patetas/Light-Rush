using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace com.Icypeak.Data
{
    public static class SaveSystem
    {

        public static void Save<T>(T data)
        {
            string path;
            if (typeof(T) == typeof(GameInfo))
            {
                path = Application.persistentDataPath + $"/{typeof(T).Name}.save";
            }
            else
            {
                path = Application.persistentDataPath + $"/{LocalDataManager.Instance.Info.LastUserID}.{typeof(T).Name}.save";
            }

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);

            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static T Load<T>() where T : class, new()
        {
            string path;
            if (typeof(T) == typeof(GameInfo))
            {
                path = Application.persistentDataPath + $"/{typeof(T).Name}.save";
            }
            else
            {
                path = Application.persistentDataPath + $"/{LocalDataManager.Instance.Info.LastUserID}.{typeof(T).Name}.save";
            }

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
    }
}
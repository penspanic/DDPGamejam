using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

namespace DDP.Data
{
    public class DataManager : Singleton<DataManager>
    {
        private GameData data;
        private string dataStrKey = "DataStr";
        protected override void Awake()
        {
            base.Awake();
        }

        private void LoadData()
        {
            if(PlayerPrefs.HasKey(dataStrKey) == false)
            {
                data = new GameData();
            }

            string str = PlayerPrefs.GetString(dataStrKey);
            BinaryFormatter bf = new BinaryFormatter();
            byte[] bytes = System.Text.UTF8Encoding.UTF8.GetBytes(str);
            data = (GameData)bf.Deserialize(new System.IO.MemoryStream(bytes));
        }

        private void SaveData()
        {
            BinaryFormatter bf = new BinaryFormatter();
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            bf.Serialize(stream, data);
            string str = System.Text.UTF8Encoding.UTF8.GetString(stream.ToArray());
            PlayerPrefs.SetString(dataStrKey, str);
            PlayerPrefs.Save();
        }
    }
}
using UnityEngine;
using System.Collections;

namespace DDP.Logic
{
    public class Hotel : MonoBehaviour, ISynableObject
    {
        public Data.Hotel Data { get { return _data; } }
        private Data.Hotel _data;

        public void Sync(ISyncableData data)
        {
            this._data = (Data.Hotel)data;
        }

        public void SetScore(int newScore)
        {
            _data.Score = newScore;
        }
    }
}
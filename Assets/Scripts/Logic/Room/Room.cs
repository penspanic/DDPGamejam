using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DDP.Logic
{
    public class Room : MonoBehaviour, ISynableObject
    {
        public Data.Room Data { get { return _data; } }
        private Data.Room _data;
        public void Sync(ISyncableData data)
        {
            _data = (Data.Room)data;
        }
    }
}
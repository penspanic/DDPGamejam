using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DDP.Logic
{
    public class Visitor : MonoBehaviour, ISynableObject
    {
        public int Serial { get; set; }
        public Data.Visitor Data { get { return _data; } }
        private Data.Visitor _data;

        private void Awake()
        {
        }

        public void Sync(ISyncableData data)
        {
            _data = (Data.Visitor)data;
        }
    }
}
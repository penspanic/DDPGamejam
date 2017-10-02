using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DDP.Logic
{
    public class Visitor : MonoBehaviour, ISynableObject
    {
        public Data.Visitor Data { get { return _data; } }
        private Data.Visitor _data;

        private void Awake()
        {
        }

        private IEnumerator StayProcess()
        {
            while(_data.ElapsedTime >= _data.StayTime)
            {
                _data.ElapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        public void Sync(ISyncableData data)
        {
            _data = (Data.Visitor)data;
            if(_data.ElapsedTime < _data.StayTime)
            {
                StartCoroutine(StayProcess());
            }
        }

        public void SetSerial(int serial)
        {
            _data.Serial = serial;
        }

        public void MoveToCounter(Vector3 endPosition)
        {
            StartCoroutine(MoveToCounterProcess(endPosition));
        }

        private IEnumerator MoveToCounterProcess(Vector3 endPos)
        {
            float elapsedTime = 0f;
            float movetime = 2f;
            Vector3 startPos = transform.position;
            while(elapsedTime < movetime)
            {
                elapsedTime += Time.deltaTime;
                transform.position = EasingUtil.EaseVector3(EasingUtil.linear, startPos, endPos, elapsedTime / movetime);
                yield return null;
            }
            transform.position = endPos;
        }
    }
}
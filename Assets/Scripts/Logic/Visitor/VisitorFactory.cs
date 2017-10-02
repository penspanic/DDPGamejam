﻿using UnityEngine;
using System.Collections;

namespace DDP.Logic
{
    public class VisitorFactory : Singleton<VisitorFactory>
    {
        [SerializeField]
        private GameObject visitorPrefab;
        [SerializeField]
        private Transform visitorCreateTransform;
        [SerializeField]
        private Transform visitorArriveTransform;

        public Vector3 CounterPosition { get { return visitorArriveTransform.position; } }

        private SerialGenerator serialGenerator = new SerialGenerator();

        protected override void Awake()
        {
            base.Awake();
        }

        public Visitor Create()
        {
            Visitor visitorInstance = Instantiate(visitorPrefab).GetComponent<Visitor>();
            visitorInstance.SetSerial(serialGenerator.Get());
            visitorInstance.transform.position = visitorCreateTransform.position;
            return visitorInstance;
        }
    }
}
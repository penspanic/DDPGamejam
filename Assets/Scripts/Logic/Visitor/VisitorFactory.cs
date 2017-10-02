using UnityEngine;
using System.Collections;

namespace DDP.Logic
{
    public class VisitorFactory : Singleton<VisitorFactory>
    {
        private SerialGenerator serialGenerator = new SerialGenerator();
        protected override void Awake()
        {
            base.Awake();
        }

        public Visitor Create()
        {
            int serial = serialGenerator.Get();
            return null;
        }
    }
}
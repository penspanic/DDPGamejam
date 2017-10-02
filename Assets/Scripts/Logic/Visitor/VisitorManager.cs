using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DDP.Logic
{
    public class VisitorManager : Singleton<VisitorManager>
    {
        private List<Visitor> visitors = new List<Visitor>();
        protected override void Awake()
        {
            base.Awake();
            StartCoroutine(VisitorCreateProcess());
        }

        private IEnumerator VisitorCreateProcess()
        {
            while(true)
            {
                yield return new WaitForSeconds(1f);
            }
        }

        public Visitor Get(int serial)
        {
            return visitors.Find((visitor) => { return visitor.Serial == serial; });
        }
    }
}
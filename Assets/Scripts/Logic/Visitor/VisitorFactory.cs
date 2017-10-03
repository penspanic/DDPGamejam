using UnityEngine;
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

		public Visitor Create(Constants.RaceType raceType)
        {
            Visitor visitorInstance = Instantiate(visitorPrefab).GetComponent<Visitor>();
			var visitorInfo = SdbInstance<Sdb.VisitorInfo>.Get(raceType.ToString());

			if (visitorInfo == null)
			{
				Debug.LogError("VisitorInfo is null, Id : " + raceType.ToString());
			}

            visitorInstance.Info = visitorInfo;
            visitorInstance.Serial = serialGenerator.Get();

			visitorInstance.ApplySpriteSources(raceType);
            visitorInstance.transform.position = visitorCreateTransform.position;
            return visitorInstance;
        }
    }
}	
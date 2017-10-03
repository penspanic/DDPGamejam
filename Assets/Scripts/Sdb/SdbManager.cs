using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DDP
{
    public static class SdbInstance<T> where T : Sdb.SdbIdentifiableBase
    {
        private static Dictionary<string, T> sdbDatas = new Dictionary<string, T>();

        static SdbInstance()
        {
            T[] datas = Resources.LoadAll<T>("Sdb/" + typeof(T).Name);
            for (int i = 0; i < datas.Length; ++i)
            {
                sdbDatas.Add(datas[i].Id, datas[i]);
            }
        }

        public static T Get(string id)
        {
            if (sdbDatas.ContainsKey(id) == false)
            {
                return null;
            }

            return sdbDatas[id];
        }

		public static List<T> GetAll()
		{
			List<T> values = new List<T>();
			values.AddRange(sdbDatas.Values);

			return values;
		}
    }

    public static class SpecificSdb<T> where T : UnityEngine.ScriptableObject
    {
        private static T sdbData;

        static SpecificSdb()
        {
            sdbData = Resources.Load<T>("Sdb/" + typeof(T).Name);
        }

        public static T Get()
        {
            return sdbData;
        }
    }
}
using UnityEngine;
using System.Collections;

namespace DDP.UI
{
    public class RoomKey : MonoBehaviour
    {
        public static RoomKey Create()
        {
            RoomKey newKey = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/RoomKey")).GetComponent<RoomKey>();
            newKey.transform.SetParent(null);
            newKey.transform.localScale = Vector3.one;

            return newKey;
        }
    }
}
using UnityEngine;
using System.Collections;

namespace DDP.UI
{
    public class RoomKey : MonoBehaviour
    {
        public Logic.Room TargetRoom;
        public static RoomKey Create(Logic.Room room)
        {
            RoomKey newKey = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/RoomKey")).GetComponent<RoomKey>();
            newKey.TargetRoom = room;

            newKey.transform.SetParent(null);
            newKey.transform.localScale = Vector3.one;

            return newKey;
        }
    }
}
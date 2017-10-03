using UnityEngine;
using System.Collections;

namespace DDP.UI
{
    public class RoomKey : MonoBehaviour
    {
        public Constants.AttributeType Attribute;
        public static RoomKey Create(Constants.AttributeType attribute)
        {
            RoomKey newKey = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/RoomKey")).GetComponent<RoomKey>();
            newKey.Attribute = attribute;

            newKey.transform.SetParent(null);

            return newKey;
        }
    }
}
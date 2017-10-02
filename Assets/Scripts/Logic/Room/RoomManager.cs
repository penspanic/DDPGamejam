using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DDP.Logic
{
    public class RoomManager : Singleton<RoomManager>
    {
        [SerializeField]
        private Transform uiRoomsParent = null;
        private List<UI.Room> uiRooms = new List<UI.Room>();
        private List<Logic.Room> rooms = new List<Room>();

        protected override void Awake()
        {
            base.Awake();
            FindUiRooms();
        }

        private void FindUiRooms()
        {
            int childCount = uiRoomsParent.childCount;
            for(int i = 0; i < childCount; ++i)
            {
                uiRooms.Add(uiRoomsParent.GetChild(i).GetComponent<UI.Room>());
            }
        }
    }
}
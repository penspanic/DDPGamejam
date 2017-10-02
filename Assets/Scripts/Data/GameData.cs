using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DDP
{
    public interface ISyncableData
    {

    };

    public interface ISynableObject
    {
        void Sync(ISyncableData data);
    }
}

namespace DDP.Data
{
    [System.Serializable]
    public struct Visitor : ISyncableData
    {
        public string InfoId;
        public int Seed;
        public int Satisfaction;
        public int ElapsedTime;
    }

    [System.Serializable]
    public struct RoomAttribute : ISyncableData
    {
        public Constants.AttributeType Type;
        public int Level;
    }

    [System.Serializable]
    public struct Room : ISyncableData
    {
        public Visitor Guest;
        public List<RoomAttribute> Attributes;
    }

    [System.Serializable]
    public struct Hotel : ISyncableData
    {
        public int Score;
        public List<Constants.FacilityType> Facilities;
    }

    [System.Serializable]
    public struct GameData : ISyncableData
    {
        public List<Room> Rooms;
        public List<Visitor> LobbyVisitors;
    }
}
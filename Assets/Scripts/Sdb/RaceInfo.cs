using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DDP.Sdb
{
    [System.Serializable]
    public struct FaceInfo
    {
        Constants.FaceType Type;
        string filePath;
    }

    [CreateAssetMenu(fileName = "RaceInfo", menuName = "RaceInfo")]
    public class RaceInfo : SdbIdentifiableBase
    {
        public List<FaceInfo> Faces;
    }
}
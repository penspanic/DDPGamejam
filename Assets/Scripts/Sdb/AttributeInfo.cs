using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DDP.Sdb
{
    [CreateAssetMenu(fileName = "AttributeInfo", menuName = "AttributeInfo")]
    public class AttributeInfo : SdbIdentifiableBase
    {
        public Constants.AttributeType Type;
        public List<Constants.AttributeType> CounterTypes;
    }
}
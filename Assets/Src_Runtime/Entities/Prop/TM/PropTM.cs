using System;
using UnityEngine;
namespace GJ {

    [Serializable]
    public struct PropTM {
        public TypeID typeID;
        public string typeName;
        public float testField1;

        // 预制体绑定
        public GameObject entityPrefab;
    }
}
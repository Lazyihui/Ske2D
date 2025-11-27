using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace GJ {

    [CreateAssetMenu(fileName = "So_Mission_", menuName = "GJ/Mission")]
    public class MissionSO : ScriptableObject {

        public TypeID typeID;

        public Vector3 originPos;

        // 所以要用的TM放这里
        public PropSpawnerTM[] propSpawners;

    }

}
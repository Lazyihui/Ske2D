using System;
using System.Collections.Generic;
using UnityEngine;

namespace GJ {

    public class PropRepository {

        Dictionary<UniqueID, PropEntity> all;
        PropEntity[] tempArray;

        public PropRepository() {
            all = new Dictionary<UniqueID, PropEntity>();
            tempArray = new PropEntity[100];
        }

        public void Add(PropEntity prop) {
            bool succ = all.TryAdd(prop.uniqueID, prop);
            if (!succ) {
                Debug.LogWarning("Prop entity added: " + prop.uniqueID);
            }
        }

        public void Remove(PropEntity prop) {
            bool succ = all.Remove(prop.uniqueID);
            if (!succ) {
                Debug.LogWarning("Prop entity removed: " + prop.uniqueID);
            }
        }

        public bool TryGet(UniqueID uniqueID, out PropEntity prop) {
            return all.TryGetValue(uniqueID, out prop);
        }


        public int TakeAll(out PropEntity[] array) {
            int count = all.Values.Count;
            if (tempArray.Length < count) {
                tempArray = new PropEntity[count];
            }
            all.Values.CopyTo(tempArray, 0);
            array = tempArray;
            return count;
        }

    }
}
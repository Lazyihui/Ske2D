using System;
using System.Collections.Generic;
using UnityEngine;

namespace GJ {

    public class RoleRepository {

        Dictionary<UniqueID, RoleEntity> all;
        RoleEntity[] tempAll;
        List<RoleEntity> tempList;

        public RoleRepository() {
            all = new Dictionary<UniqueID, RoleEntity>();
            tempAll = new RoleEntity[100];
            tempList = new List<RoleEntity>(100);
        }

        public void Add(RoleEntity role) {
            bool succ = all.TryAdd(role.uniqueID, role);
            if (!succ) {
                Debug.LogError($"Failed to add RoleEntity with ID: {role.uniqueID}");
            }
        }

        public bool TryGet(UniqueID id, out RoleEntity role) {
            return all.TryGetValue(id, out role);
        }

        public void Remove(RoleEntity role) {
            bool succ = all.Remove(role.uniqueID);
            if (!succ) {
                Debug.LogError($"Failed to remove RoleEntity with ID: {role.uniqueID}");
            }
        }

        public int TakeAll(out RoleEntity[] result) {
            int count = all.Count;
            if (tempAll.Length < count) {
                tempAll = new RoleEntity[count];
            }
            all.Values.CopyTo(tempAll, 0);
            result = tempAll;
            return count;
        }

    }
}
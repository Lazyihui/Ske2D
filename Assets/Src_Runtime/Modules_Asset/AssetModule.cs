using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GJ {

    public class AssetModule : MonoBehaviour {


        Dictionary<PanelType, GameObject> panels;
        AsyncOperationHandle panelHandle;

        Dictionary<EntityType, GameObject> entities;

        AsyncOperationHandle entityHandle;
        Dictionary<TypeID, MissionSO> missions;
        AsyncOperationHandle missionHandle;

        Dictionary<TypeID, PropSO> props;
        AsyncOperationHandle propHandle;


        public void Ctor() {
            panels = new Dictionary<PanelType, GameObject>();
            entities = new Dictionary<EntityType, GameObject>();
            missions = new Dictionary<TypeID, MissionSO>();
            props = new Dictionary<TypeID, PropSO>();
        }

        public IEnumerator LoadAllIE() {
            yield return Panel_Load();
            yield return Entity_Load();
            yield return Mission_Load();
            yield return Prop_Load();
        }

        public void ReleaseAll() {
            Panel_Release();
            Entity_Release();
            Mission_Release();
            Prop_Release();
        }

        #region Panel
        IEnumerator Panel_Load() {
            var handle = Addressables.LoadAssetsAsync<GameObject>("Panel", null);
            while (!handle.IsDone) {
                yield return null;
            }
            var list = handle.Result;
            if (list == null || list.Count == 0) {
                Debug.LogWarning("No Panel assets found.");
                yield break;
            }
            foreach (var go in list) {
                var panel = go.GetComponent<IPanelAsset>();
                bool succ = panels.TryAdd(panel.Type, go);
                if (!succ) {
                    Debug.LogError($"Panel Type: {panel.Type} already exists!");
                }
            }
            panelHandle = handle;
        }

        void Panel_Release() {
            if (panelHandle.IsValid()) {
                Addressables.Release(panelHandle);
            }
        }

        public bool Panel_TryGet(PanelType type, out GameObject go) {
            return panels.TryGetValue(type, out go);
        }
        #endregion

        #region Entity
        IEnumerator Entity_Load() {
            var handle = Addressables.LoadAssetsAsync<GameObject>("Entity", null);
            while (!handle.IsDone) {
                yield return null;
            }
            var list = handle.Result;
            if (list == null || list.Count == 0) {
                Debug.LogWarning("No Entity assets found.");
                yield break;
            }
            foreach (var go in list) {
                var entity = go.GetComponent<IEntityAsset>();
                bool succ = entities.TryAdd(entity.EntityType, go);
                if (!succ) {
                    Debug.LogError($"Entity Type: {entity.EntityType} already exists!");
                }
            }
            entityHandle = handle;
        }

        void Entity_Release() {
            if (entityHandle.IsValid()) {
                Addressables.Release(entityHandle);
            }
        }

        public bool Entity_TryGet(EntityType type, out GameObject go) {
            return entities.TryGetValue(type, out go);
        }

        #endregion

        #region Mission
        IEnumerator Mission_Load() {
            var handle = Addressables.LoadAssetsAsync<MissionSO>("Mission", null);
            while (!handle.IsDone) {
                yield return null;
            }
            var list = handle.Result;
            if (list == null || list.Count == 0) {
                Debug.LogWarning("No MissionSO assets found.");
                yield break;
            }
            foreach (var mission in list) {
                bool succ = missions.TryAdd(mission.typeID, mission);
                if (!succ) {
                    Debug.LogWarning($"Duplicate MissionSO found: {mission.typeID}");
                }
            }
            missionHandle = handle;
        }

        void Mission_Release() {
            if (missionHandle.IsValid()) {
                Addressables.Release(missionHandle);
            }
        }

        public bool Mission_TryGet(TypeID typeID, out MissionSO so) {
            return missions.TryGetValue(typeID, out so);
        }
        #endregion
        #region Prop
        IEnumerator Prop_Load() {
            var handle = Addressables.LoadAssetsAsync<PropSO>("Prop", null);
            while (!handle.IsDone) {
                yield return null;
            }
            var list = handle.Result;
            if (list == null || list.Count == 0) {
                Debug.LogWarning("No PropSO assets found.");
                yield break;
            }
            foreach (var prop in list) {
                bool succ = props.TryAdd(prop.tm.typeID, prop);
                if (!succ) {
                    Debug.LogWarning($"Duplicate PropSO found: {prop.tm.typeID}");
                }
            }
            propHandle = handle;
        }

        void Prop_Release() {
            if (propHandle.IsValid()) {
                Addressables.Release(propHandle);
            }
        }

        public bool Prop_TryGet(TypeID typeID, out PropSO so) {
            return props.TryGetValue(typeID, out so);
        }

        public ICollection<PropSO> Prop_GetAll() {
            return props.Values;
        }
        #endregion

    }

}
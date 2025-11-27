using System;
using UnityEngine;

namespace GJ.Systems_Game {

    public static class PropController {

        public static PropEntity Spawn_BySpawner(GameSystemContext ctx, Vector3 originPos, PropSpawnerTM spawnerTM) {
            var tm = spawnerTM.so.tm;
            var prop = Spawn(ctx, tm.typeID);

            // 要改要改 用的时候和我说
            prop.TF_Pos_Set(originPos + spawnerTM.whereOffset);
            return prop;
        }

        public static PropEntity Spawn(GameSystemContext ctx, TypeID typeID) {
            bool has = ctx.assetModule.Prop_TryGet(typeID, out var so);
            if (!has) {
                Debug.LogError($"Prop with TypeID {typeID} not found.");
                return null;
            }

            var tm = so.tm;
            var prefab = tm.entityPrefab;
            var prop = GameObject.Instantiate(prefab).GetComponent<PropEntity>();

            Debug.Assert(prefab != null, $"Prop Prefab is null: {typeID}+");

            prop.uniqueID = ctx.userEntity.userIDComponent.Prop();
            prop.typeID = typeID;

            ctx.propRepository.Add(prop);

            return prop;
        }

        public static void Unspawn(GameSystemContext ctx, PropEntity prop) {
            ctx.propRepository.Remove(prop);
        }
    }
}
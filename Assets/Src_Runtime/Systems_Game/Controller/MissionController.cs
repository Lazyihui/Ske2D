using System;
using GJ.Systems_Game;
using UnityEditor.Rendering;
using UnityEngine;


namespace GJ {

    public static class MissionController {

        public static void Load(GameSystemContext ctx, TypeID typeID) {
            bool has = ctx.assetModule.Mission_TryGet(typeID, out var so);
            if (!has) {
                Debug.LogError($"Mission SO not found for TypeID: {typeID}");
                return;
            }

            var tm = so;

            // 更具TM生成的任务数据进行初始化
            // propSpawners
            foreach (var propSpawnerTM in tm.propSpawners) {
                PropController.Spawn_BySpawner(ctx, Vector2.zero, propSpawnerTM);
            }
        }
    }
}
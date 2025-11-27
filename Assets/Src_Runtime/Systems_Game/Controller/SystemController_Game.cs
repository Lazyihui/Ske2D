using System;
using UnityEngine;

namespace GJ.Systems_Game {

    public static class SystemController {

        public static void NewGame(GameSystemContext ctx) {
            TypeID typeID = TypeID.Invalid;
            RoleEntity owner = RoleController.Spawn(ctx, typeID);
            ctx.gameEntity.ownerID = owner.uniqueID;
            GJLog.Log("进入游戏");
            ctx.status = SystemStatus.Running;

            MissionController.Load(ctx, new TypeID(0, 0, 0));
        }

        public static void OnResume(GameSystemContext ctx) {

        }

        public static void OnExitGame(GameSystemContext ctx) {
            ctx.status = SystemStatus.Stopped;
            // Role
            int roleLen = ctx.roleRepository.TakeAll(out var roles);
            for (int i = 0; i < roleLen; i++) {
                var role = roles[i];
                RoleController.UnSpawn(ctx, role);
            }
        }

    }
}
using System;
using UnityEngine;
using GJ.Systems_Game;


namespace GJ {

    public class GameSystem {

        GameSystemContext ctx;
        public GameSystemEvents Events => ctx.events;

        public GameSystem() {
            ctx = new GameSystemContext();
        }

        public void Inject(AssetModule assetModule, UICore ui,
        RoleRepository roleRepository,
        PropRepository propRepository,
        GameEntity gameEntity,
        InputModule inputModule
        , UserEntity userEntity
        ) {
            ctx.ui = ui;
            ctx.assetModule = assetModule;
            ctx.roleRepository = roleRepository;
            ctx.propRepository = propRepository;
            ctx.gameEntity = gameEntity;
            ctx.userEntity = userEntity;
            ctx.inputModule = inputModule;
        }
        #region Tick
        public void Tick(float dt) {

            if (ctx.status != SystemStatus.Running) {
                return;
            }

            PreTick(dt);

            ref float restSec = ref ctx.gameEntity.restSec;
            restSec += dt;
            const float interval = 0.05f;
            while (restSec > 0) {
                float step = Math.Min(interval, restSec);
                restSec -= step;
                FixTick(step);
            }

            LateTick(dt);

        }

        void PreTick(float dt) {
        }

        void FixTick(float dt) {

            // Owner 
            var owner = ctx.Get_Owner();
            if (owner != null) {
                RoleController.Tick_Owner(ctx, owner, dt);
            }

            // Other Roles
            int roleLen = ctx.roleRepository.TakeAll(out var roles);
            for (int i = 0; i < roleLen; i++) {
                var role = roles[i];
                // 不是主角
            }

        }

        void LateTick(float dt) {
        }
        #endregion

        #region  Events
        public void NewGame() {
            SystemController.NewGame(ctx);
            Debug.Log("新游戏");
        }

        public void OnResume() {
            SystemController.OnResume(ctx);
        }

        public void OnExitGame() {
            SystemController.OnExitGame(ctx);
            Debug.Log("退出游戏");
        }

        #endregion

    }
}
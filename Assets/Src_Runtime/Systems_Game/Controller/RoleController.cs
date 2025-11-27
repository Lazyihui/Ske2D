using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

namespace GJ.Systems_Game {

    public static partial class RoleController {

        public static RoleEntity Spawn(GameSystemContext ctx, TypeID typeID) {

            bool has = ctx.assetModule.Entity_TryGet(EntityType.Role, out var prefab);
            if (!has) {
                Debug.LogError($"没有找到角色预制体 {typeID}");
                return null;
            }
            var go = GameObject.Instantiate(prefab);
            var role = go.GetComponent<RoleEntity>();
            role.Ctor();
            role.typeID = typeID;
            role.uniqueID = ctx.userEntity.userIDComponent.Role();

            ctx.roleRepository.Add(role);
            return role;
        }

        public static void UnSpawn(GameSystemContext ctx, RoleEntity role) {
            ctx.roleRepository.Remove(role);
        }

        public static void Tick_Owner(GameSystemContext ctx, RoleEntity role, float dt) {
            // 用户或者主角才是这个
            Input_Record(ctx, role, dt);
            // Loco
            Loco_PressE(ctx, role, dt);
            Loco_MoveAxis(ctx, role, dt);

            // Physics
            CheckCollisions(role);
        }

        public static void Input_Record(GameSystemContext ctx, RoleEntity role, float dt) {
            var input = ctx.inputModule.inputEntity;
            var inputComp = role.inputComponent;
            inputComp.PressE_Set(input.isKeyDownE);
            inputComp.MoveAxis_Set(input.moveAxis);
        }

        #region Loco
        static void Loco_PressE(GameSystemContext ctx, RoleEntity role, float dt) {
            var inputComp = role.inputComponent;
            if (inputComp.PressE_Get()) {
                Debug.Log("Loco_PressE");
                ctx.events.Curtain_Open_Invoke();
            }
        }

        static void Loco_MoveAxis(GameSystemContext ctx, RoleEntity role, float dt) {
            var inputComp = role.inputComponent;
            Vector2 axis = inputComp.MoveAxis_Get();
            // TODO: Speed from config
            // float speed = role.attributeComponent.GetFloat(AttributeFloatValueType.MoveSpeed);//还没配置
            float speed = 15f;
            role.Move(axis, speed, dt);
        }

        #endregion

        #region Physics
        static Collider2D[] results = new Collider2D[10];

        static void CheckCollisions(RoleEntity role) {
            float radius = 1f; // 定义检测半径
            Vector2 center = role.transform.position;

            // 执行圆形区域检测
            int hitCount = Physics2D.OverlapCircleNonAlloc(center, radius, results, LayerMask.GetMask("Prop"));

            // 调试可视化 - 绘制圆形边界
            DrawCircle(center, radius, 32, Color.green);

            if (hitCount > 0) {
                Debug.Log($"碰到 {hitCount} collisions");

                for (int i = 0; i < hitCount; i++) {
                    Collider2D collider = results[i];
                    Debug.Log($"碰到: {collider.gameObject.name}");

                    // 可选：绘制到碰撞体的连线
                    Debug.DrawLine(center, collider.transform.position, Color.yellow, 0.1f);
                }
            }
        }
        // TODO:到时候可以放到一个公共的工具类里
        // 绘制圆形的方法 TODO
        static void DrawCircle(Vector2 center, float radius, int segments, Color color) {
            float angle = 0f;
            float angleIncrement = 360f / segments;

            for (int i = 0; i < segments; i++) {
                Vector2 start = center + new Vector2(
                    Mathf.Cos(Mathf.Deg2Rad * angle) * radius,
                    Mathf.Sin(Mathf.Deg2Rad * angle) * radius
                );

                Vector2 end = center + new Vector2(
                    Mathf.Cos(Mathf.Deg2Rad * (angle + angleIncrement)) * radius,
                    Mathf.Sin(Mathf.Deg2Rad * (angle + angleIncrement)) * radius
                );

                Debug.DrawLine(start, end, color, 0.1f);
                angle += angleIncrement;
            }
        }
        #endregion
    }
}
using System;
using UnityEngine;

namespace GJ.Systems_Game {

    public static class PanelController {

        #region Login
        public static void Login_Open(LoginSystemContext ctx) {
            PanelOpenResult res = ctx.uiCore.P_TryOpen<Panel_Login>(PanelType.Login, UIRootLevel.SuperTop, out var panel);
            if (res == PanelOpenResult.FirstOpen) {
                panel.Ctor();

                panel.OnStartHandle = ctx.events.OnStart;
                panel.OnExitGameHandle = () => {
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
                };
            }
            panel.Init();
        }

        public static void Login_Close(LoginSystemContext ctx) {
            bool has = ctx.uiCore.P_TryGet<Panel_Login>(PanelType.Login, out var panel);
            if (has) {
                ctx.uiCore.P_Remove(panel);
                panel.Close();
            }
        }
        #endregion

    }

}
using System;
using UnityEngine;
using UnityEngine.Video;

namespace GJ {

    public class UICore : MonoBehaviour {

        UICoreContext ctx;

        [SerializeField] Transform root_top;
        [SerializeField] Transform root_superTop;


        public void Ctor() {
            ctx = new UICoreContext();
            var roots = ctx.roots;
            roots.Add(UIRootLevel.Top, root_top);
            roots.Add(UIRootLevel.SuperTop, root_superTop);
        }

        public void Inject(AssetModule assetModule) {
            ctx.assetModule = assetModule;
        }

        Transform Root_Get(UIRootLevel level) {
            return ctx.roots[level];
        }
        #region Panel
        public PanelOpenResult P_TryOpen<T>(PanelType panelType, UIRootLevel root, out T panel) where T : class, IPanelAsset {
            if (ctx.panelRepository.TryGetUnique<T>(panelType, out panel)) {
                return PanelOpenResult.ReOpen;
            }

            var rootTransform = Root_Get(root);

            if (ctx.assetModule.Panel_TryGet(panelType, out var prefab)) {
                var go = GameObject.Instantiate(prefab, rootTransform);
                panel = go.GetComponent<T>();
                ctx.panelRepository.AddUnique(panel);
                return PanelOpenResult.FirstOpen;
            }

            Debug.LogError($"TryOpen: Failed to get {panelType}");
            return default;
        }

        public bool P_TryGet<T>(PanelType panelType, out T panel) where T : class, IPanelAsset {
            return ctx.panelRepository.TryGetUnique<T>(panelType, out panel);
        }

        public void P_Remove(IPanelAsset panel) {
            ctx.panelRepository.RemoveUnique(panel);
        }
        #endregion
    }
}
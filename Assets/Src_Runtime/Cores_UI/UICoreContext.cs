using System;
using System.Collections.Generic;
using UnityEditor.Build.Pipeline.Tasks;
using UnityEngine;

namespace GJ {

    public class UICoreContext {

        public AssetModule assetModule;

        public PanelRepository panelRepository;

        public Dictionary<UIRootLevel, Transform> roots;

        public UICoreContext() {
            panelRepository = new PanelRepository();
            roots = new Dictionary<UIRootLevel, Transform>();
        }

    }
}
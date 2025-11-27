using System;
using System.Collections.Generic;
using UnityEngine;

namespace GJ {

    public class PanelRepository {

        Dictionary<PanelType, IPanelAsset> uniques;

        public PanelRepository() {
            uniques = new Dictionary<PanelType, IPanelAsset>();
        }

        public void AddUnique(IPanelAsset panel) {
            bool succ = uniques.TryAdd(panel.Type, panel);
            if (!succ) {
                Debug.LogError($"Same PanelType: {panel.Type}");
            }
        }

        public void RemoveUnique(PanelType type) {
            bool succ = uniques.Remove(type);
            if (!succ) {
                Debug.LogError($"No Such PanelType: {type}");
            }
        }

        public void RemoveUnique(IPanelAsset panel) {
            bool succ = uniques.Remove(panel.Type);
            if (!succ) {
                Debug.LogError($"No Such PanelType: {panel.Type}");
            }
        }

        public bool TryGetUnique<T>(PanelType type, out T panel) where T : class, IPanelAsset {
            bool has = uniques.TryGetValue(type, out var result);
            panel = result as T;
            return has;
        }

    }
}
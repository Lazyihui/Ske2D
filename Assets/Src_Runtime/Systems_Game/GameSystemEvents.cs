using System;
using UnityEngine;

namespace GJ {

    public class GameSystemEvents {

        #region  Curtain
        public Action OnCurtainHande;
        public void Curtain_Open_Invoke() => OnCurtainHande?.Invoke();

        #endregion
    }
}
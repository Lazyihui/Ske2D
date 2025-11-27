using System;
using System.Collections.Generic;
using UnityEngine;

namespace GJ {

    public class RoleInputComponent {

        bool pressE;
        public void PressE_Set(bool value) => pressE = value;
        public bool PressE_Get() => pressE;

        Vector2 moveAxis;
        public void MoveAxis_Set(Vector2 axis) => moveAxis = axis;
        public Vector2 MoveAxis_Get() => moveAxis;

        InputKeyFlag castKeys;
        public InputKeyFlag CastKeys => castKeys;

        public void RecoredCastKey(InputKeyFlag key) {
            if (key == InputKeyFlag.None) {
                return;
            }
            castKeys = key;
        }

        public void Clear() {
            castKeys = InputKeyFlag.None;
        }

        public void ClearCastKeys() {
            castKeys = InputKeyFlag.None;
        }
    }

}
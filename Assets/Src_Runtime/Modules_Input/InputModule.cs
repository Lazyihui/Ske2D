using System;
using UnityEngine;

namespace GJ {

    public class InputModule : MonoBehaviour {

        public InputEntity inputEntity;

        public void Ctor() {
            inputEntity = new InputEntity();
        }

        public void Init() {
        }

        public void Tick(float dt) {
            inputEntity.Tick(dt);
        }

    }

}
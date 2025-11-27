using System;
using UnityEngine;

namespace GJ {

    public class InputEntity {
        public InputSystem_Action input_Role;
        public Vector2 moveAxis;

        public bool isKeyDownE;

        public InputEntity() {
            input_Role = new InputSystem_Action();
            input_Role.Enable();
        }

        public void Dispose() {
            input_Role.Disable();
        }

        public void Tick(float dt) {
            var World = input_Role.World;
            // // move
            {
                float kbxLeft = World.MoveLeft.ReadValue<float>();
                float kbxRight = World.MoveRight.ReadValue<float>();

                float kbxUp = World.MoveUp.ReadValue<float>();
                float kbxDown = World.MoveDown.ReadValue<float>();

                Vector2 axis = new Vector2(kbxRight - kbxLeft, kbxUp - kbxDown);
                moveAxis = axis;
            }

            //PressE
            {
                if (World.PressE.triggered) {
                    isKeyDownE = true;
                } else {
                    isKeyDownE = false;
                }
            }

        }
    }
}
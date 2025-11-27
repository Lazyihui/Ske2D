using System;
using UnityEngine;


namespace GJ {

    public class PropEntity : MonoBehaviour {

        public EntityType EntityType => EntityType.Prop;
        public UniqueID uniqueID;
        public TypeID typeID;

        public float testField1;

        public void Ctor() {

        }

        public void TearDown() {
            Destroy(this.gameObject);
        }

        public void TF_Pos_Set(Vector3 pos) {
            this.transform.position = pos;
        }
    }
}
using System;
using System.Collections.Generic;

namespace GJ {

    public class AttributeComponent {

        Dictionary<AttributeFloatValueType, float> floatValues;
        Dictionary<AttributeIntValueType, int> intValues;

        public AttributeComponent() {
            floatValues = new Dictionary<AttributeFloatValueType, float>();
            intValues = new Dictionary<AttributeIntValueType, int>();
        }

        public void Recycle() {
            floatValues.Clear();
            intValues.Clear();
        }

        public void FromTM_Float(AttributeFloatValuePairTM[] floatAttributes) {
            floatValues.Clear();
            if (floatAttributes != null) {
                foreach (var pair in floatAttributes) {
                    SetFloat(pair.type, pair.value);
                }
            }
        }

        public void FromTM_Int(AttributeIntValuePairTM[] intAttributes) {
            intValues.Clear();
            if (intAttributes != null) {
                foreach (var pair in intAttributes) {
                    SetInt(pair.type, pair.value);
                }
            }
        }

        #region Float
        public void SetFloat(AttributeFloatValueType type, float value) {
            if (floatValues.ContainsKey(type)) {
                floatValues[type] = value;
            } else {
                floatValues.Add(type, value);
            }
        }

        public float GetFloat(AttributeFloatValueType type) {
            if (floatValues.TryGetValue(type, out float value)) {
                return value;
            }
            // NJLog.Warning($"AttributeFloatValueType '{type}' not found in AttributeComponent.");
            return 0f; // Default value if not set
        }
        #endregion

        #region Int
        public void SetInt(AttributeIntValueType type, int value) {
            if (intValues.ContainsKey(type)) {
                intValues[type] = value;
            } else {
                intValues.Add(type, value);
            }
        }

        public int GetInt(AttributeIntValueType type) {
            if (intValues.TryGetValue(type, out int value)) {
                return value;
            }
            // NJLog.Warning($"AttributeIntValueType '{type}' not found in AttributeComponent.");
            return 0; // Default value if not set
        }
        #endregion

    }

}
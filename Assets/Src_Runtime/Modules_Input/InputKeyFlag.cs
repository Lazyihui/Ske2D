using System;
using System.Collections.Generic;
using UnityEngine;


namespace GJ {
    public enum InputKeyFlag {
        // World
        None = 0,
        pressE = 1 << 1,

    }

    public static class InputKeyFlagExtension {

        static readonly Dictionary<InputKeyFlag, string> enumToStringDict = new Dictionary<InputKeyFlag, string>() {
            // World
            { InputKeyFlag.None, "None" },
            { InputKeyFlag.pressE, "PressE" },

            };

        public static string ToInputKeyString(this InputKeyFlag key) {
            if (enumToStringDict.TryGetValue(key, out string value)) {
                return value;
            }
            Debug.LogError($"InputKeyFlag: {key} not found in dictionary." + string.Empty);
            return string.Empty;
        }

    }

}
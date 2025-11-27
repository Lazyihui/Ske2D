using UnityEngine;
using System.Collections.Generic;

namespace GJ {

    public static class GJLog {

        // 日志类型配置
        public static Dictionary<LogType, (Color color, bool bold)> logTypeConfigs = new Dictionary<LogType, (Color, bool)> {
            { LogType.Normal, (Color.white, true) },
            { LogType.Todo, (Color.yellow, true) },
            { LogType.Lazy, (Color.cyan, true) },
            { LogType.DogCat, (Color.magenta, true) },
            { LogType.Jack, (new Color(1f, 0.5f, 0f), true) }, // Orange
        };

        // 筛选配置
        public static HashSet<LogType> enabledLogTypes = new HashSet<LogType> {
            LogType.Normal, LogType.Todo, LogType.Lazy, LogType.Jack,
            LogType.DogCat, LogType.Warning, LogType.Error, LogType.Assert
        };


        public static void Log(object msg) {
#if UNITY_EDITOR
            if (!Application.isPlaying) return;
#endif

            if (enabledLogTypes.Contains(LogType.Normal)) {
                LogInternal(msg?.ToString(), LogType.Normal);
            }
        }

        public static void LogTodo(object msg) {
#if UNITY_EDITOR
            if (!Application.isPlaying) return;
#endif

            if (enabledLogTypes.Contains(LogType.Todo)) {
                LogInternal(msg?.ToString(), LogType.Todo);
            }
        }

        public static void LogLazy(object msg) {
#if UNITY_EDITOR
            if (!Application.isPlaying) return;
#endif

            if (enabledLogTypes.Contains(LogType.Lazy)) {
                LogInternal(msg?.ToString(), LogType.Lazy);
            }
        }

        public static void LogJack(object msg) {
#if UNITY_EDITOR
            if (!Application.isPlaying) return;
#endif

            if (enabledLogTypes.Contains(LogType.Jack)) {
                LogInternal(msg?.ToString(), LogType.Jack);
            }
        }

        public static void LogDogCat(object msg) {
#if UNITY_EDITOR
            if (!Application.isPlaying) return;
#endif

            if (enabledLogTypes.Contains(LogType.DogCat)) {
                LogInternal(msg?.ToString(), LogType.DogCat);
            }
        }

        public static void Warning(object msg) {
#if UNITY_EDITOR
            if (!Application.isPlaying) return;
#endif

            if (enabledLogTypes.Contains(LogType.Warning)) {
                Debug.LogWarning($"{msg?.ToString()}");
            }
        }

        public static void Error(object msg) {
#if UNITY_EDITOR
            if (!Application.isPlaying) return;
#endif

            if (enabledLogTypes.Contains(LogType.Error)) {
                Debug.LogError($"{msg?.ToString()}");
            }
        }

        public static void Assert(bool condition, object msg) {
#if UNITY_EDITOR
            if (!Application.isPlaying) return;
#endif

            if (enabledLogTypes.Contains(LogType.Assert)) {
                Debug.LogAssertion($"{msg?.ToString()}");
            }
        }

        static void LogInternal(string message, LogType type) {
            if (logTypeConfigs.TryGetValue(type, out var config)) {
                string formattedMsg = FormatMessage(message, config.color, config.bold);
                Debug.Log($"{formattedMsg}");
            }
        }

        static string FormatMessage(string message, Color color, bool bold) {
            string colorHex = ColorUtility.ToHtmlStringRGB(color);
            string boldTag = bold ? "<b>" : "";
            string boldEndTag = bold ? "</b>" : "";

            return $"{boldTag}<color=#{colorHex}>{message}</color>{boldEndTag}";
        }

        // 筛选控制方法
        public static void EnableLogType(LogType type) {
            enabledLogTypes.Add(type);
        }

        public static void DisableLogType(LogType type) {
            enabledLogTypes.Remove(type);
        }

        public static void SetLogTypeEnabled(LogType type, bool enabled) {
            if (enabled)
                enabledLogTypes.Add(type);
            else
                enabledLogTypes.Remove(type);
        }

        public static bool IsLogTypeEnabled(LogType type) {
            return enabledLogTypes.Contains(type);
        }
    }
}
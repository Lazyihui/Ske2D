using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace GJ.Editor {

    public class GJLogFilterWindow : EditorWindow {

        static Dictionary<LogType, bool> logTypeStates = new Dictionary<LogType, bool> {
            { LogType.Normal, true },
            { LogType.Todo, true },
            { LogType.Lazy, true },
            { LogType.Jack, true },
            { LogType.DogCat, true },
            { LogType.Warning, true },
            { LogType.Error, true },
            { LogType.Assert, true }
        };

        Vector2 scrollPosition;
        bool enableAll = true;

        [MenuItem("Tools/GJ Log Filter")]
        public static void ShowWindow() {
            GJLogFilterWindow window = GetWindow<GJLogFilterWindow>("GJ Log Filter");
            window.minSize = new Vector2(250, 300);
            LoadSettings();
        }

        void OnEnable() {
            LoadSettings();
        }

        void OnGUI() {
            DrawHeader();
            DrawLogTypeFilters();
            DrawActions();
        }

        void DrawHeader() {
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("GJ Log Filter Settings", EditorStyles.boldLabel);
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("Enable/Disable specific log types:", EditorStyles.label);
            EditorGUILayout.Space(10);
        }

        void DrawLogTypeFilters() {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            // 启用所有开关
            EditorGUI.BeginChangeCheck();
            enableAll = EditorGUILayout.Toggle("Enable All", enableAll);
            if (EditorGUI.EndChangeCheck()) {
                SetAllLogTypes(enableAll);
                ApplySettings();
            }

            EditorGUILayout.Space(10);

            // 各个日志类型开关
            foreach (var logType in System.Enum.GetValues(typeof(LogType))) {
                LogType type = (LogType)logType;
                EditorGUI.BeginChangeCheck();
                bool newState = EditorGUILayout.ToggleLeft(GetLogTypeDisplayName(type), logTypeStates[type]);
                if (EditorGUI.EndChangeCheck()) {
                    logTypeStates[type] = newState;
                    GJLog.SetLogTypeEnabled(type, newState);
                    SaveSettings();
                }
            }

            EditorGUILayout.EndScrollView();
        }

        void DrawActions() {
            EditorGUILayout.Space(20);

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Enable All", GUILayout.Height(25))) {
                SetAllLogTypes(true);
                ApplySettings();
            }

            if (GUILayout.Button("Disable All", GUILayout.Height(25))) {
                SetAllLogTypes(false);
                ApplySettings();
            }

            GUILayout.EndHorizontal();

            EditorGUILayout.Space(10);

            if (GUILayout.Button("Apply Settings", GUILayout.Height(30))) {
                ApplySettings();
            }

            EditorGUILayout.Space(5);

            // 显示当前状态
            int enabledCount = 0;
            foreach (var state in logTypeStates.Values) {
                if (state) enabledCount++;
            }

            EditorGUILayout.HelpBox($"{enabledCount}/{logTypeStates.Count} log types enabled", MessageType.Info);
        }

        void SetAllLogTypes(bool enabled) {
            enableAll = enabled;
            foreach (var logType in System.Enum.GetValues(typeof(LogType))) {
                LogType type = (LogType)logType;
                logTypeStates[type] = enabled;
            }
        }

        void ApplySettings() {
            foreach (var kvp in logTypeStates) {
                GJLog.SetLogTypeEnabled(kvp.Key, kvp.Value);
            }
            SaveSettings();
            Repaint();
        }

        string GetLogTypeDisplayName(LogType type) {
            switch (type) {
                case LogType.Normal: return "Normal Logs";
                case LogType.Todo: return "TODO Logs";
                case LogType.Lazy: return "Lazy Logs";
                case LogType.Jack: return "Jack Logs";
                case LogType.DogCat: return "DogCat Logs";
                case LogType.Warning: return "Warnings";
                case LogType.Error: return "Errors";
                case LogType.Assert: return "Asserts";
                default: return type.ToString();
            }
        }

        static void SaveSettings() {
            foreach (var kvp in logTypeStates) {
                EditorPrefs.SetBool($"GJLog_{kvp.Key}", kvp.Value);
            }
        }

        static void LoadSettings() {
            foreach (var logType in System.Enum.GetValues(typeof(LogType))) {
                LogType type = (LogType)logType;
                bool defaultValue = true;
                if (type == LogType.Warning || type == LogType.Error) {
                    defaultValue = true; // 默认开启警告和错误
                }
                logTypeStates[type] = EditorPrefs.GetBool($"GJLog_{type}", defaultValue);
                GJLog.SetLogTypeEnabled(type, logTypeStates[type]);
            }
        }
    }
}
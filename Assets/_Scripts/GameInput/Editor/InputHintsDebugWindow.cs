#if UNITY_EDITOR
using System;
using GameInput.InputHintSystem;
using GameInput.InputHintSystem.Interfaces;
using UnityEditor;
using UnityEngine;

namespace GameInput.Editor {
    public sealed class InputHintsDebugWindow : EditorWindow {
        private InputService _inputService;
        private IHintSystem _hintSystem;

        private InputContextType _context;
        private InputDeviceType _device;

        [MenuItem("Tools/Input/Input Hints Debug")]
        public static void Open() {
            GetWindow<InputHintsDebugWindow>("Input Hints Debug");
        }

        private void OnEnable() {
            _inputService = new InputService();

            var resolver = new HintBindingResolver();
            var glyphs = new ControlGlyphProvider();
            var factory = new HintFactory(glyphs);

            _hintSystem = new HintSystem(_inputService, resolver, factory);

            _context = InputContextType.Character;
            _device = InputDeviceType.KeyboardMouse;

            _inputService.SetContext(_context);
            ForceDevice(_device);
        }

        private void OnDisable() {
            _inputService.Dispose();
        }

        private void OnGUI() {
            DrawToolbar();
            GUILayout.Space(10);
            DrawHints();
        }

        private void DrawToolbar() {
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);

            _context = (InputContextType)EditorGUILayout.EnumPopup("Context", _context);
            _device = (InputDeviceType)EditorGUILayout.EnumPopup("Device", _device);

            if (GUILayout.Button("Apply", EditorStyles.toolbarButton)) {
                _inputService.SetContext(InputContextType.None);
                ForceDevice(_device);
                _inputService.SetContext(_context);
            }

            EditorGUILayout.EndHorizontal();
        }

        private void ForceDevice(InputDeviceType device) {
            typeof(InputService)
                .GetProperty("CurrentDevice")
                ?.SetValue(_inputService, device);
        }

        private void DrawHints() {
            foreach (InputActionId actionId in Enum.GetValues(typeof(InputActionId))) {
                var hint = _hintSystem.GetHint(actionId);
                if (hint == null || hint == InputHint.Empty) {
                    continue;
                }
            
                EditorGUILayout.BeginHorizontal();
            
                GUILayout.Label(actionId.ToString(), GUILayout.Width(120));
                DrawHintTokens(hint);
            
                EditorGUILayout.EndHorizontal();
            }
        }

        private void DrawHintTokens(InputHint hint) {
            if (hint == null || hint.Tokens.Count == 0) {
                GUILayout.Label("—");
                return;
            }

            foreach (var token in hint.Tokens) {
                GUILayout.Label(token.Value, EditorStyles.helpBox, GUILayout.MinWidth(24));
            }
        }
    }
}
#endif
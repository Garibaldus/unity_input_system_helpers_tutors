using System.Collections.Generic;
using Generated.Input.Enums;

namespace Generated.Input {
    public sealed class ControlGlyphProvider : IControlGlyphProvider {
        private static readonly Dictionary<string, string> KeyboardMap = new() {
            ["space"] = "Space",
            ["enter"] = "Enter",
            ["escape"] = "Esc",
            ["tab"] = "Tab",
            ["leftShift"] = "Shift",
            ["rightShift"] = "Shift",
            ["leftCtrl"] = "Ctrl",
            ["rightCtrl"] = "Ctrl",
            ["leftAlt"] = "Alt",
            ["rightAlt"] = "Alt",
        };

        private static readonly Dictionary<string, string> MouseMap = new() {
            ["leftButton"] = "LMB",
            ["rightButton"] = "RMB",
            ["middleButton"] = "MMB",
        };

        private static readonly Dictionary<string, string> GamepadMap = new() {
            ["buttonSouth"] = "A",
            ["buttonEast"] = "B",
            ["buttonWest"] = "X",
            ["buttonNorth"] = "Y",

            ["leftShoulder"] = "LB",
            ["rightShoulder"] = "RB",
            ["leftTrigger"] = "LT",
            ["rightTrigger"] = "RT",

            ["start"] = "Start",
            ["select"] = "Select",
        };

        public HintToken GetToken(string controlPath, InputDeviceType deviceType) {
            if (string.IsNullOrEmpty(controlPath))
                return HintToken.Text("?");

            var key = ExtractControlName(controlPath);

            return deviceType switch {
                InputDeviceType.KeyboardMouse => ResolveKeyboardOrMouse(key),
                InputDeviceType.Gamepad => ResolveGamepad(key),
                _ => HintToken.Text("?")
            };
        }

        private HintToken ResolveKeyboardOrMouse(string key) {
            if (KeyboardMap.TryGetValue(key, out var value))
                return HintToken.Text(value);

            if (MouseMap.TryGetValue(key, out value))
                return HintToken.Text(value);

            // fallback: буквы и цифры
            return HintToken.Text(key.ToUpperInvariant());
        }

        private HintToken ResolveGamepad(string key) {
            if (GamepadMap.TryGetValue(key, out var value))
                return HintToken.Text(value);

            return HintToken.Text(key);
        }

        private string ExtractControlName(string path) {
            // "<Keyboard>/e" → "e"
            // "<Gamepad>/buttonSouth" → "buttonSouth"
            var slashIndex = path.LastIndexOf('/');
            return slashIndex >= 0 ? path[(slashIndex + 1)..] : path;
        }
    }
}
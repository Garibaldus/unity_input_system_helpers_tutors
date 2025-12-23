using System.Collections.Generic;
using Generated.Input.Enums;
using UnityEngine.InputSystem;

namespace Generated.Input {
    public sealed class HintBindingResolver : IHintBindingResolver {
        public IReadOnlyList<ResolvedBinding> Resolve(InputAction action, InputDeviceType deviceType) {
            var result = new List<ResolvedBinding>();
            var bindings = action.bindings;

            for (var i = 0; i < bindings.Count; i++) {
                var binding = bindings[i];

                // 1. Пропускаем части composite (обрабатываются родителем)
                if (binding.isPartOfComposite)
                    continue;

                // 2. Проверка устройства
                if (!IsBindingForDevice(binding, deviceType))
                    continue;

                // 3. Composite
                if (binding.isComposite) {
                    var compositeParts = new List<InputBinding>();

                    for (int j = i + 1; j < bindings.Count; j++) {
                        var part = bindings[j];
                        if (!part.isPartOfComposite)
                            break;

                        if (!IsBindingForDevice(part, deviceType))
                            continue;

                        compositeParts.Add(part);
                    }

                    if (compositeParts.Count > 0)
                        result.Add(new ResolvedBinding(compositeParts));
                }
                else {
                    // 4. Обычный биндинг
                    if (string.IsNullOrEmpty(binding.effectivePath))
                        continue;

                    result.Add(new ResolvedBinding(binding));
                }
            }

            return result;
        }

        private bool IsBindingForDevice(InputBinding binding, InputDeviceType deviceType) {
            var path = binding.effectivePath;
            if (string.IsNullOrEmpty(path))
                return false;

            return deviceType switch {
                InputDeviceType.KeyboardMouse =>
                    path.StartsWith("<Keyboard>") ||
                    path.StartsWith("<Mouse>"),

                InputDeviceType.Gamepad =>
                    path.StartsWith("<Gamepad>"),

                _ => false
            };
        }
    }
}
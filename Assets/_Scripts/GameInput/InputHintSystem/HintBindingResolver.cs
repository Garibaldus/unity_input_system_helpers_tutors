using System.Collections.Generic;
using GameInput.InputHintSystem.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameInput.InputHintSystem {
    public sealed class HintBindingResolver : IHintBindingResolver {
        public IReadOnlyList<ResolvedBinding> Resolve(InputAction action, InputDeviceType deviceType) {
            var result = new List<ResolvedBinding>();
            var bindings = action.bindings;

            for (int i = 0; i < bindings.Count; i++) {
                var binding = bindings[i];

                // 1️⃣ Пропускаем части composite — их обрабатывает родитель
                if (binding.isPartOfComposite)
                    continue;

                // 2️⃣ Composite binding
                if (binding.isComposite) {
                    var parts = new List<InputBinding>();

                    for (int j = i + 1; j < bindings.Count; j++) {
                        var part = bindings[j];
                        if (!part.isPartOfComposite)
                            break;

                        if (!IsBindingForDevice(part, deviceType))
                            continue;

                        if (string.IsNullOrEmpty(part.effectivePath))
                            continue;

                        parts.Add(part);
                    }

                    // ✔ Если есть хотя бы один валидный part — composite жив
                    if (parts.Count > 0)
                        result.Add(new ResolvedBinding(parts));

                    continue;
                }

                // 3️⃣ Обычный биндинг
                if (!IsBindingForDevice(binding, deviceType))
                    continue;

                if (string.IsNullOrEmpty(binding.effectivePath))
                    continue;

                result.Add(new ResolvedBinding(binding));
            }

            return result;
        }

        private bool IsBindingForDevice(
            InputBinding binding,
            InputDeviceType deviceType) {
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
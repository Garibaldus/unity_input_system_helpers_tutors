using System;
using System.Collections.Generic;
using GameInput.InputHintSystem.Interfaces;
using UnityEngine;

namespace GameInput.InputHintSystem {
    public sealed class HintSystem : IHintSystem, IDisposable {
        public event Action onHintsChanged;

        private readonly InputService _inputService;
        private readonly IHintBindingResolver _resolver;
        private readonly IHintFactory _factory;

        private readonly Dictionary<InputActionId, InputHint> _cache = new();

        public HintSystem(InputService inputService, IHintBindingResolver resolver,
            IHintFactory factory) {
            _inputService = inputService;
            _resolver = resolver;
            _factory = factory;

            _inputService.onDeviceChanged += OnInvalidated;
            _inputService.onContextChanged += OnInvalidated;
        }

        public InputHint GetHint(InputActionId actionId) {
            if (_cache.TryGetValue(actionId, out var cached))
                return cached;

            var action = _inputService.GetAction(actionId);
            if (action == null)
                return InputHint.Empty;

            var bindings = _resolver.Resolve(action, _inputService.CurrentDevice);

            if (bindings.Count == 0)
                return InputHint.Empty;

            var hint = _factory.Create(bindings, _inputService.CurrentDevice);

            _cache[actionId] = hint;
            return hint;
        }

        private void OnInvalidated(InputDeviceType _) => Invalidate();
        private void OnInvalidated(InputContextType _) => Invalidate();

        private void Invalidate() {
            _cache.Clear();
            onHintsChanged?.Invoke();
        }

        public void Dispose() {
            _inputService.onDeviceChanged -= OnInvalidated;
            _inputService.onContextChanged -= OnInvalidated;
        }
    }
}
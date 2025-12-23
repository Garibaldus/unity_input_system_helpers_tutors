// AUTO-GENERATED

using System;
using System.Collections.Generic;
using Generated.Input.Enums;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Generated.Input.Contexts {
    public sealed class SimpleInputContext : IInputContext, InputSystem_Actions.ISimpleActions
    {
        public InputContextType ContextType => InputContextType.Simple;

        public event Action<Vector2> onLeftStick;
        public event Action<Vector2> onRightStick;
        public event Action onItneract;
        public event Action onCancel;
        public event Action onPan;

        private readonly InputSystem_Actions.SimpleActions actions;

        public SimpleInputContext(InputSystem_Actions input)
        {
            actions = input.Simple;
            actions.SetCallbacks(this);
        }

        public void Enable() => actions.Enable();
        public void Disable() => actions.Disable();
        public InputAction GetAction(InputActionId actionId) {
            throw new NotImplementedException();
        }

        public IEnumerable<InputAction> GetAllActions() {
            throw new NotImplementedException();
        }

        public void AddCallbacks(InputSystem_Actions.ISimpleActions instance) => actions.AddCallbacks(instance);
        public void RemoveCallbacks(InputSystem_Actions.ISimpleActions instance) => actions.RemoveCallbacks(instance);

        public void OnLeftStick(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            onLeftStick?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnRightStick(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            onRightStick?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnItneract(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            onItneract?.Invoke();
        }

        public void OnCancel(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            onCancel?.Invoke();
        }

        public void OnPan(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            onPan?.Invoke();
        }

    }
}

// AUTO-GENERATED

using System;
using System.Collections.Generic;
using Generated.Input.Enums;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Generated.Input.Contexts {
    public sealed class CharacterInputContext : IInputContext, InputSystem_Actions.ICharacterActions
    {
        public InputContextType ContextType => InputContextType.Character;

        public event Action<Vector2> onMove;
        public event Action<Vector2> onLook;
        public event Action onAttack;
        public event Action onInteract;
        public event Action onCrouch;
        public event Action onJump;
        public event Action onPrevious;
        public event Action onNext;
        public event Action onSprint;

        private readonly InputSystem_Actions.CharacterActions actions;

        public CharacterInputContext(InputSystem_Actions input)
        {
            actions = input.Character;
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

        public void AddCallbacks(InputSystem_Actions.ICharacterActions instance) => actions.AddCallbacks(instance);
        public void RemoveCallbacks(InputSystem_Actions.ICharacterActions instance) => actions.RemoveCallbacks(instance);

        public void OnMove(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            onMove?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            onLook?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            onAttack?.Invoke();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            onInteract?.Invoke();
        }

        public void OnCrouch(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            onCrouch?.Invoke();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            onJump?.Invoke();
        }

        public void OnPrevious(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            onPrevious?.Invoke();
        }

        public void OnNext(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            onNext?.Invoke();
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            onSprint?.Invoke();
        }

    }
}

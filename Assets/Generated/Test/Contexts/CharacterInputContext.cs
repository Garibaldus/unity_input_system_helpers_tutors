// AUTO-GENERATED
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private readonly Dictionary<InputActionId, InputAction> _actionsMap;
    private readonly InputSystem_Actions.CharacterActions actions;

    public CharacterInputContext(InputSystem_Actions input)
    {
        actions = input.Character;
        actions.SetCallbacks(this);
        _actionsMap = new Dictionary<InputActionId, InputAction> {
            { InputActionId.Move, actions.Move },
            { InputActionId.Look, actions.Look },
            { InputActionId.Attack, actions.Attack },
            { InputActionId.Interact, actions.Interact },
            { InputActionId.Crouch, actions.Crouch },
            { InputActionId.Jump, actions.Jump },
            { InputActionId.Previous, actions.Previous },
            { InputActionId.Next, actions.Next },
            { InputActionId.Sprint, actions.Sprint },
        };
    }

    public void Enable() => actions.Enable();
    public void Disable() => actions.Disable();

    public InputAction GetAction(InputActionId actionId) {
        _actionsMap.TryGetValue(actionId, out var action);
        return action;
    }

#if UNITY_EDITOR
    public IEnumerable<InputAction> GetAllActions() {
        return actions.Get().actions;
    }
#endif

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

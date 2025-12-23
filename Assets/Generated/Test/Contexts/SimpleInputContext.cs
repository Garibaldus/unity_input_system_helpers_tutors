// AUTO-GENERATED
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class SimpleInputContext : IInputContext, InputSystem_Actions.ISimpleActions
{
    public InputContextType ContextType => InputContextType.Simple;

    public event Action<Vector2> onLeftStick;
    public event Action<Vector2> onRightStick;
    public event Action onInteract;
    public event Action onCancel;
    public event Action onPan;

    private readonly Dictionary<InputActionId, InputAction> _actionsMap;
    private readonly InputSystem_Actions.SimpleActions actions;

    public SimpleInputContext(InputSystem_Actions input)
    {
        actions = input.Simple;
        actions.SetCallbacks(this);
        _actionsMap = new Dictionary<InputActionId, InputAction> {
            { InputActionId.LeftStick, actions.LeftStick },
            { InputActionId.RightStick, actions.RightStick },
            { InputActionId.Interact, actions.Interact },
            { InputActionId.Cancel, actions.Cancel },
            { InputActionId.Pan, actions.Pan },
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

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        onInteract?.Invoke();
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

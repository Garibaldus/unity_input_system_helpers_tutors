// AUTO-GENERATED
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class UIInputContext : IInputContext, InputSystem_Actions.IUIActions
{
    public InputContextType ContextType => InputContextType.UI;

    public event Action<Vector2> onNavigate;
    public event Action onSubmit;
    public event Action onCancel;
    public event Action<Vector2> onPoint;
    public event Action onClick;
    public event Action onRightClick;
    public event Action onMiddleClick;
    public event Action<Vector2> onScrollWheel;
    public event Action onTrackedDevicePosition;
    public event Action onTrackedDeviceOrientation;

    private readonly Dictionary<InputActionId, InputAction> _actionsMap;
    private readonly InputSystem_Actions.UIActions actions;

    public UIInputContext(InputSystem_Actions input)
    {
        actions = input.UI;
        actions.SetCallbacks(this);
        _actionsMap = new Dictionary<InputActionId, InputAction> {
            { InputActionId.Navigate, actions.Navigate },
            { InputActionId.Submit, actions.Submit },
            { InputActionId.Cancel, actions.Cancel },
            { InputActionId.Point, actions.Point },
            { InputActionId.Click, actions.Click },
            { InputActionId.RightClick, actions.RightClick },
            { InputActionId.MiddleClick, actions.MiddleClick },
            { InputActionId.ScrollWheel, actions.ScrollWheel },
            { InputActionId.TrackedDevicePosition, actions.TrackedDevicePosition },
            { InputActionId.TrackedDeviceOrientation, actions.TrackedDeviceOrientation },
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

    public void AddCallbacks(InputSystem_Actions.IUIActions instance) => actions.AddCallbacks(instance);
    public void RemoveCallbacks(InputSystem_Actions.IUIActions instance) => actions.RemoveCallbacks(instance);

    public void OnNavigate(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        onNavigate?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        onSubmit?.Invoke();
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        onCancel?.Invoke();
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        onPoint?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        onClick?.Invoke();
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        onRightClick?.Invoke();
    }

    public void OnMiddleClick(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        onMiddleClick?.Invoke();
    }

    public void OnScrollWheel(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        onScrollWheel?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnTrackedDevicePosition(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        onTrackedDevicePosition?.Invoke();
    }

    public void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        onTrackedDeviceOrientation?.Invoke();
    }

}

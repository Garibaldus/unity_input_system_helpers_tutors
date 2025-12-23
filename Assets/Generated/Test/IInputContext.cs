
// AUTO-GENERATED
using UnityEngine.InputSystem;

#if UNITY_EDITOR
using System.Collections.Generic;
#endif

public interface IInputContext
{
    InputContextType ContextType { get; }
    void Enable();
    void Disable();

    InputAction GetAction(InputActionId actionId);

#if UNITY_EDITOR
    IEnumerable<InputAction> GetAllActions();
#endif
}

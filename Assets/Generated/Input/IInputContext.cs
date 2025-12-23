// AUTO-GENERATED

using Generated.Input.Enums;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
using System.Collections.Generic;
#endif

namespace Generated.Input {
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
}
using System.Collections.Generic;
using Generated.Input.Enums;
using UnityEngine.InputSystem;

namespace Generated.Input {
    public interface IHintBindingResolver
    {
        IReadOnlyList<ResolvedBinding> Resolve(InputAction action, InputDeviceType deviceType);
    }
}
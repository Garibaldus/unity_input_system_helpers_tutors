using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace GameInput.InputHintSystem.Interfaces {
    public interface IHintBindingResolver {
        IReadOnlyList<ResolvedBinding> Resolve(InputAction action, InputDeviceType deviceType);
    }
}
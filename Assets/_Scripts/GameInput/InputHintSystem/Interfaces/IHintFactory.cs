using System.Collections.Generic;

namespace GameInput.InputHintSystem.Interfaces {
    public interface IHintFactory {
        InputHint Create(IReadOnlyList<ResolvedBinding> bindings, InputDeviceType deviceType);
    }
}
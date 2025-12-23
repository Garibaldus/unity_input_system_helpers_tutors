using System.Collections.Generic;
using Generated.Input.Enums;

namespace Generated.Input {
    public interface IHintFactory {
        InputHint Create(IReadOnlyList<ResolvedBinding> bindings, InputDeviceType deviceType);
    }
}
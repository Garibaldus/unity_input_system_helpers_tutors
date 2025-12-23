using System;

namespace Generated.Input {
    public interface IHintSystem {
        InputHint GetHint(InputActionId actionId);
        event Action onHintsChanged;
    }
}
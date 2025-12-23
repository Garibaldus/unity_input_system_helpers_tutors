using System;

namespace GameInput.InputHintSystem.Interfaces {
    public interface IHintSystem {
        InputHint GetHint(InputActionId actionId);
        event Action onHintsChanged;
    }
}
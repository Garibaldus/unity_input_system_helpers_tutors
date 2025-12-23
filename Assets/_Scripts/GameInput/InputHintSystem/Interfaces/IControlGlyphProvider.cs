namespace GameInput.InputHintSystem.Interfaces {
    public interface IControlGlyphProvider {
        HintToken GetToken(string controlPath, InputDeviceType deviceType);
    }
}
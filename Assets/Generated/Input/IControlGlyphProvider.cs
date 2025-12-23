using Generated.Input.Enums;

namespace Generated.Input {
    public interface IControlGlyphProvider
    {
        HintToken GetToken(string controlPath, InputDeviceType deviceType);
    }
}
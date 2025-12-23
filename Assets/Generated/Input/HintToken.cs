namespace Generated.Input {
    public enum HintTokenType {
        Text, // "E", "Enter"
        Icon, // UI решит, что это
        Separator // "/", "+"
    }

    public sealed class HintToken {
        public HintTokenType Type { get; }
        public string Value { get; }

        private HintToken(HintTokenType type, string value) {
            Type = type;
            Value = value;
        }

        public static HintToken Text(string value)
            => new(HintTokenType.Text, value);

        public static HintToken Icon(string id)
            => new(HintTokenType.Icon, id);

        public static HintToken Separator(string value = "/")
            => new(HintTokenType.Separator, value);
    }
}
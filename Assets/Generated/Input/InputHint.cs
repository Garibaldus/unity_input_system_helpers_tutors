using System;
using System.Collections.Generic;

namespace Generated.Input {
    public sealed class InputHint {
        public IReadOnlyList<HintToken> Tokens { get; }

        public static readonly InputHint Empty = new(Array.Empty<HintToken>());

        public InputHint(IReadOnlyList<HintToken> tokens) {
            Tokens = tokens;
        }
    }
}
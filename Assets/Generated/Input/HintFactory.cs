using System.Collections.Generic;
using Generated.Input.Enums;

namespace Generated.Input {
    public sealed class HintFactory : IHintFactory {
        private readonly IControlGlyphProvider _glyphProvider;

        public HintFactory(IControlGlyphProvider glyphProvider) {
            _glyphProvider = glyphProvider;
        }

        public InputHint Create(IReadOnlyList<ResolvedBinding> bindings, InputDeviceType deviceType) {
            var tokens = new List<HintToken>();

            for (var i = 0; i < bindings.Count; i++) {
                var binding = bindings[i];

                foreach (var part in binding.Parts) {
                    var token = _glyphProvider.GetToken(part.effectivePath, deviceType);

                    tokens.Add(token);
                }

                // разделитель между альтернативами
                if (i < bindings.Count - 1)
                    tokens.Add(HintToken.Separator("/"));
            }

            return new InputHint(tokens);
        }
    }
}
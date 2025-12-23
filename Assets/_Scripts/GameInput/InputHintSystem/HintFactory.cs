using System.Collections.Generic;
using GameInput.InputHintSystem.Interfaces;

namespace GameInput.InputHintSystem {
    public sealed class HintFactory : IHintFactory {
        private readonly IControlGlyphProvider _glyphProvider;

        public HintFactory(IControlGlyphProvider glyphProvider) {
            _glyphProvider = glyphProvider;
        }

        public InputHint Create(IReadOnlyList<ResolvedBinding> bindings, InputDeviceType deviceType) {
            var tokens = new List<HintToken>();

            for (int i = 0; i < bindings.Count; i++) {
                var binding = bindings[i];

                // 1️⃣ Рендерим ОДИН способ ввода
                foreach (var part in binding.Parts) {
                    var token = _glyphProvider.GetToken(part.effectivePath, deviceType);

                    tokens.Add(token);
                }

                // 2️⃣ Разделитель между альтернативами
                if (i < bindings.Count - 1)
                    tokens.Add(HintToken.Separator("/"));
            }

            return new InputHint(tokens);
        }
    }
}
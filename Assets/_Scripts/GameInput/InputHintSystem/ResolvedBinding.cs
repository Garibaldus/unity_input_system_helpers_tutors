using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;

namespace GameInput.InputHintSystem {
    public sealed class ResolvedBinding {
        public bool IsComposite { get; }
        public IReadOnlyList<InputBinding> Parts { get; }

        public ResolvedBinding(InputBinding binding) {
            IsComposite = false;
            Parts = new[] { binding };
        }

        public ResolvedBinding(List<InputBinding> compositeParts) {
            IsComposite = true;
            Parts = compositeParts;
        }

        // public override string ToString() {
        //     return Parts.Aggregate(string.Empty, (current, binding) => current + $"'{binding.effectivePath}' ");
        // }
    }
}
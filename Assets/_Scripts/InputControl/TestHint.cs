using GameInput.InputHintSystem;
using GameInput.InputHintSystem.Interfaces;
using TMPro;
using UnityEngine;

namespace _Scripts.InputControl {
    public class TestHint : MonoBehaviour {
        [SerializeField] private InputActionId _actionId;
        [SerializeField] private TextMeshProUGUI _lblInfo;
        
        private InputService _inputService;
        private IHintSystem _hintSystem;

        private string _msg = "Press '@' fore move";

        private void Awake() {
            _inputService = new InputService();

            var resolver = new HintBindingResolver();
            var glyphs = new ControlGlyphProvider();
            var factory = new HintFactory(glyphs);

            _hintSystem = new HintSystem(_inputService, resolver, factory);

            _inputService.SetContext(InputContextType.Simple);

            _inputService.Character.onInteract += ShowMessageOnInteract;
            _hintSystem.onHintsChanged += OnHintsChanged;
        }

        private void OnHintsChanged() {
            PrintMessage();
        }

        private void Start() {
            PrintMessage();
        }

        private void PrintMessage() {
            var tokens = _hintSystem.GetHint(_actionId).Tokens;
            var tokenStr = string.Empty;
            foreach (var token in tokens) {
                tokenStr += $"'{token.Value}' ";
            }
            
            _lblInfo.text = _msg.Replace("@", tokenStr);
        }

        private void ShowMessageOnInteract() {
            var tokens = _hintSystem.GetHint(InputActionId.Interact).Tokens;
            var tokenStr = string.Empty;
            foreach (var token in tokens) {
                tokenStr += token.Value;
            }

            Debug.Log($"Pressed: {tokenStr} {(_inputService.CurrentDevice == InputDeviceType.KeyboardMouse ? "key" : "button")}!");
        }

        private void OnDestroy() {
            _inputService.Character.onInteract -= ShowMessageOnInteract;
            _inputService.Dispose();
        }
    }
}
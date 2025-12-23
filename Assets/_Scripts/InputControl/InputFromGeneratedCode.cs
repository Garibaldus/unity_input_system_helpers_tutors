using Generated.Input;
using Generated.Input.Enums;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.InputControl {
    public class InputFromGeneratedCode : MonoBehaviour, InputSystem_Actions.ICharacterActions {
        private InputService _inputService;
        
        private void Awake() {
            _inputService = new InputService();
            _inputService.SetContext(InputContextType.Character);
        }
        
        private void Start() {
            _inputService.Character.onInteract += CharacterOnInteract;
            _inputService.Character.onJump += () => _inputService.Character.AddCallbacks(this);
            _inputService.Character.AddCallbacks(this);
        }
        
        private void OnDestroy() {
            _inputService.Character.onInteract -= CharacterOnInteract;
            _inputService.Character.RemoveCallbacks(this);
        }

        private void CharacterOnInteract() {
            Debug.Log("Interact");
        }

        public void OnMove(InputAction.CallbackContext context) {
            
        }

        public void OnLook(InputAction.CallbackContext context) {
            
        }

        public void OnAttack(InputAction.CallbackContext context) {
            if (!context.performed)
                return;
            Debug.Log("Attack!");
        }

        public void OnInteract(InputAction.CallbackContext context) {
            _inputService.Character.RemoveCallbacks(this);
            Debug.Log("From Interface Interact");
        }

        public void OnCrouch(InputAction.CallbackContext context) {
            
        }

        public void OnJump(InputAction.CallbackContext context) {
            if (!context.performed)
                return;
            Debug.Log("From Interface Jump");
        }

        public void OnPrevious(InputAction.CallbackContext context) {
           
        }

        public void OnNext(InputAction.CallbackContext context) {
            
        }

        public void OnSprint(InputAction.CallbackContext context) {
            
        }
    }
}
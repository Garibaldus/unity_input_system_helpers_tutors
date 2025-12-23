// AUTO-GENERATED

using System;
using System.Collections.Generic;
using Generated.Input.Contexts;
using Generated.Input.Enums;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Generated.Input {
    public sealed class InputService : IDisposable
    {
        public InputDeviceType CurrentDevice { get; private set; }
        public event Action<InputDeviceType> onDeviceChanged;
        public event Action<InputContextType> onContextChanged;

        public CharacterInputContext Character { get; }
        public UIInputContext UI { get; }
        public SimpleInputContext Simple { get; }

        private readonly InputSystem_Actions _input;
        private readonly Dictionary<InputContextType, IInputContext> _contexts;
        private InputContextType _activeContext = InputContextType.None;
        private IDisposable _anyButtonListener;

        public InputService()
        {
            _input = new InputSystem_Actions();
            Character = new CharacterInputContext(_input);
            UI = new UIInputContext(_input);
            Simple = new SimpleInputContext(_input);

            _contexts = new Dictionary<InputContextType, IInputContext>
            {
                { InputContextType.Character, Character },
                { InputContextType.UI, UI },
                { InputContextType.Simple, Simple },
            };

            // IInitialize
            SetContext(InputContextType.None);
            DetectInitialDevice();
            _anyButtonListener = InputSystem.onAnyButtonPress.Call(OnAnyButtonPress);
        }

        public void SetContext(InputContextType context)
        {
            if (_activeContext == context) return;
        
            foreach (var ctx in _contexts.Values) ctx.Disable();
            _activeContext = context;
            _contexts[context].Enable();
            onContextChanged?.Invoke(_activeContext);
        }
    
        public InputAction GetAction(InputActionId actionId)
        {
            if (_activeContext == InputContextType.None)
                return null;

            return _contexts[_activeContext].GetAction(actionId);
        }


        private void DetectInitialDevice()
        {
            CurrentDevice = Gamepad.current != null ? InputDeviceType.Gamepad : InputDeviceType.KeyboardMouse;
        }

        private void OnAnyButtonPress(InputControl control)
        {
            var newDevice = control.device is Gamepad ? InputDeviceType.Gamepad : InputDeviceType.KeyboardMouse;
            if (newDevice == CurrentDevice) return;
            CurrentDevice = newDevice;
            onDeviceChanged?.Invoke(CurrentDevice);
        }

        public void Dispose()
        {
            _anyButtonListener?.Dispose();
            _input.Dispose();
        }
    }
}

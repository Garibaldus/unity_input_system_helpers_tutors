#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameInput.Editor {
    public static class InputCodeGeneratorUtility {
        private static string _rootFolder = string.Empty;

        [MenuItem("Tools/Input/Сгенерировать код для Action ассета")]
        public static void Generate() {
            var asset = Selection.activeObject as InputActionAsset;
            if (asset == null) {
                EditorUtility.DisplayDialog("Input Generator", "Выбери InputActionAsset", "OK");
                return;
            }

            // Запрашиваем путь у пользователя
            var folder = EditorUtility.OpenFolderPanel("Выбери корневую папку для генерации", "Assets", "");
            if (string.IsNullOrEmpty(folder)) {
                EditorUtility.DisplayDialog("Input Generator", "Генерация отменена пользователем.", "OK");
                return;
            }

            // Преобразуем в путь относительно Assets
            if (!folder.StartsWith(Application.dataPath)) {
                EditorUtility.DisplayDialog("Input Generator", "Папка должна быть внутри проекта (Assets).", "OK");
                return;
            }

            _rootFolder = "Assets" + folder[Application.dataPath.Length..];

            EnsureFolders();

            try {
                GenerateEnums(asset);
                GenerateInputActionIds(asset);
                GenerateIInputContext();
                GenerateContexts(asset);
                GenerateInputService(asset);

                AssetDatabase.Refresh();

                EditorUtility.DisplayDialog("Input Generator", "Код успешно сгенерирован", "OK");
            }
            catch (System.Exception ex) {
                EditorUtility.DisplayDialog("Input Generator", "Ошибка генерации: " + ex.Message, "OK");
                Debug.LogException(ex);
            }
        }

        // ======================================================
        // Folders
        // ======================================================
        private static void EnsureFolders() {
            Create(_rootFolder);
            Create(Path.Combine(_rootFolder, "Contexts"));
            Create(Path.Combine(_rootFolder, "Enums"));
            return;

            static void Create(string path) {
                if (!AssetDatabase.IsValidFolder(path)) {
                    var parent = Path.GetDirectoryName(path);
                    var name = Path.GetFileName(path);
                    AssetDatabase.CreateFolder(parent, name);
                }
            }
        }

        // ======================================================
        // Enums
        // ======================================================
        private static void GenerateEnums(InputActionAsset asset) {
            // InputDeviceType
            WriteSafe("Enums/InputDeviceType.cs", @"
// AUTO-GENERATED
public enum InputDeviceType
{
    KeyboardMouse,
    Gamepad
}
");

            // InputContextType
            var sb = new StringBuilder();
            sb.AppendLine("// AUTO-GENERATED");
            sb.AppendLine("public enum InputContextType");
            sb.AppendLine("{");
            sb.AppendLine("    None,");

            foreach (var map in asset.actionMaps) {
                var safeName = ToSafeIdentifier(map.name);
                sb.AppendLine($"    {safeName},");
            }

            sb.AppendLine("}");
            WriteSafe("Enums/InputContextType.cs", sb.ToString());
        }

        // ======================================================
        // InputActionId
        // ======================================================
        private static void GenerateInputActionIds(InputActionAsset asset) {
            var actionNames = new HashSet<string>();

            foreach (var map in asset.actionMaps) {
                foreach (var action in map.actions) {
                    var safeName = ToSafeIdentifier(action.name);
                    actionNames.Add(safeName);
                }
            }

            var sb = new StringBuilder();
            sb.AppendLine("// AUTO-GENERATED");
            sb.AppendLine("public enum InputActionId");
            sb.AppendLine("{");

            foreach (var name in actionNames)
                sb.AppendLine($"    {name},");

            sb.AppendLine("}");

            WriteSafe("Enums/InputActionId.cs", sb.ToString());
        }


        // ======================================================
        // IInputContext
        // ======================================================
        private static void GenerateIInputContext() {
            WriteSafe("IInputContext.cs", @"
// AUTO-GENERATED
public interface IInputContext
{
    InputContextType ContextType { get; }
    void Enable();
    void Disable();
}
");
        }

        // ======================================================
        // Contexts
        // ======================================================
        private static void GenerateContexts(InputActionAsset asset) {
            foreach (var map in asset.actionMaps)
                GenerateContext(asset, map);
        }

        private static void GenerateContext(InputActionAsset asset, InputActionMap map) {
            var className = ToSafeIdentifier(map.name) + "InputContext";
            var inputClass = asset.name;
            var originalMapName = map.name;

            var sb = new StringBuilder();
            sb.AppendLine("// AUTO-GENERATED");
            sb.AppendLine("using System;");
            sb.AppendLine("using UnityEngine;");
            sb.AppendLine("using UnityEngine.InputSystem;");
            sb.AppendLine();
            sb.AppendLine($"public sealed class {className} : IInputContext, {inputClass}.I{ToSafeIdentifier(map.name)}Actions");
            sb.AppendLine("{");

            sb.AppendLine($"    public InputContextType ContextType => InputContextType.{ToSafeIdentifier(map.name)};");
            sb.AppendLine();

            foreach (var action in map.actions) {
                var actionName = ToSafeIdentifier(action.name);
                if (action.expectedControlType == "Vector2")
                    sb.AppendLine($"    public event Action<Vector2> on{actionName};");
                else
                    sb.AppendLine($"    public event Action on{actionName};");
            }

            sb.AppendLine();
            sb.AppendLine($"    private readonly {inputClass}.{ToSafeIdentifier(map.name)}Actions actions;");
            sb.AppendLine();

            sb.AppendLine($"    public {className}({inputClass} input)");
            sb.AppendLine("    {");
            sb.AppendLine($"        actions = input.{ToSafeIdentifier(originalMapName)};");
            sb.AppendLine("        actions.SetCallbacks(this);");
            sb.AppendLine("    }");
            sb.AppendLine();

            sb.AppendLine("    public void Enable() => actions.Enable();");
            sb.AppendLine("    public void Disable() => actions.Disable();");
            sb.AppendLine();
            sb.AppendLine($"    public void AddCallbacks({inputClass}.I{ToSafeIdentifier(map.name)}Actions instance) => actions.AddCallbacks(instance);");
            sb.AppendLine($"    public void RemoveCallbacks({inputClass}.I{ToSafeIdentifier(map.name)}Actions instance) => actions.RemoveCallbacks(instance);");
            sb.AppendLine();

            foreach (var action in map.actions) {
                var actionName = ToSafeIdentifier(action.name);
                sb.AppendLine($"    public void On{actionName}(InputAction.CallbackContext context)");
                sb.AppendLine("    {");
                sb.AppendLine("        if (!context.performed) return;");
                if (action.expectedControlType == "Vector2")
                    sb.AppendLine($"        on{actionName}?.Invoke(context.ReadValue<Vector2>());");
                else
                    sb.AppendLine($"        on{actionName}?.Invoke();");
                sb.AppendLine("    }");
                sb.AppendLine();
            }

            sb.AppendLine("}");
            WriteSafe($"Contexts/{className}.cs", sb.ToString());
        }

        // ======================================================
        // InputService
        // ======================================================
        private static void GenerateInputService(InputActionAsset asset) {
            var inputClass = asset.name;
            var sb = new StringBuilder();

            sb.AppendLine("// AUTO-GENERATED");
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using UnityEngine.InputSystem;");
            sb.AppendLine("using UnityEngine.InputSystem.Utilities;");
            sb.AppendLine();
            sb.AppendLine("public sealed class InputService : IDisposable");
            sb.AppendLine("{");
            sb.AppendLine("    public InputDeviceType CurrentDevice { get; private set; }");
            sb.AppendLine("    public event Action<InputDeviceType> onDeviceChanged;");
            sb.AppendLine();

            foreach (var map in asset.actionMaps)
                sb.AppendLine($"    public {ToSafeIdentifier(map.name)}InputContext {ToSafeIdentifier(map.name)} {{ get; }}");

            sb.AppendLine();
            sb.AppendLine($"    private readonly {inputClass} _input;");
            sb.AppendLine("    private readonly Dictionary<InputContextType, IInputContext> _contexts;");
            sb.AppendLine("    private InputContextType _activeContext = InputContextType.None;");
            sb.AppendLine("    private IDisposable _anyButtonListener;");
            sb.AppendLine();

            sb.AppendLine("    public InputService()");
            sb.AppendLine("    {");
            sb.AppendLine($"        _input = new {inputClass}();");

            foreach (var map in asset.actionMaps)
                sb.AppendLine($"        {ToSafeIdentifier(map.name)} = new {ToSafeIdentifier(map.name)}InputContext(_input);");

            sb.AppendLine();
            sb.AppendLine("        _contexts = new Dictionary<InputContextType, IInputContext>");
            sb.AppendLine("        {");
            foreach (var map in asset.actionMaps)
                sb.AppendLine($"            {{ InputContextType.{ToSafeIdentifier(map.name)}, {ToSafeIdentifier(map.name)} }},");
            sb.AppendLine("        };");
            sb.AppendLine();

            sb.AppendLine("        // IInitialize");
            sb.AppendLine($"        SetContext(InputContextType.None);");
            sb.AppendLine("        DetectInitialDevice();");
            sb.AppendLine("        _anyButtonListener = InputSystem.onAnyButtonPress.Call(OnAnyButtonPress);");
            sb.AppendLine("    }");
            sb.AppendLine();

            sb.AppendLine("    public void SetContext(InputContextType context)");
            sb.AppendLine("    {");
            sb.AppendLine("        if (_activeContext == context) return;");
            sb.AppendLine("        foreach (var ctx in _contexts.Values) ctx.Disable();");
            sb.AppendLine("        _activeContext = context;");
            sb.AppendLine("        _contexts[context].Enable();");
            sb.AppendLine("    }");
            sb.AppendLine();

            sb.AppendLine("    private void DetectInitialDevice()");
            sb.AppendLine("    {");
            sb.AppendLine("        CurrentDevice = Gamepad.current != null ? InputDeviceType.Gamepad : InputDeviceType.KeyboardMouse;");
            sb.AppendLine("    }");
            sb.AppendLine();

            sb.AppendLine("    private void OnAnyButtonPress(InputControl control)");
            sb.AppendLine("    {");
            sb.AppendLine("        var newDevice = control.device is Gamepad ? InputDeviceType.Gamepad : InputDeviceType.KeyboardMouse;");
            sb.AppendLine("        if (newDevice == CurrentDevice) return;");
            sb.AppendLine("        CurrentDevice = newDevice;");
            sb.AppendLine("        onDeviceChanged?.Invoke(CurrentDevice);");
            sb.AppendLine("    }");
            sb.AppendLine();

            sb.AppendLine("    public void Dispose()");
            sb.AppendLine("    {");
            sb.AppendLine("        _anyButtonListener?.Dispose();");
            sb.AppendLine("        _input.Dispose();");
            sb.AppendLine("    }");
            sb.AppendLine("}");

            WriteSafe("InputService.cs", sb.ToString());
        }

        // ======================================================
        // Utils
        // ======================================================
        private static void WriteSafe(string relativePath, string content) {
            var fullPath = Path.Combine(_rootFolder, relativePath);

            try {
                File.WriteAllText(fullPath, content, Encoding.UTF8);
            }
            catch (IOException ioEx) {
                Debug.LogError($"Ошибка записи файла {fullPath}: {ioEx.Message}");
            }
            catch (System.Exception ex) {
                Debug.LogError($"Неожиданная ошибка записи файла {fullPath}: {ex.Message}");
            }
        }

        private static string ToSafeIdentifier(string raw) {
            if (string.IsNullOrWhiteSpace(raw)) return "_";

            var sb = new StringBuilder();
            var capitalizeNext = true;

            foreach (var c in raw) {
                if (char.IsLetterOrDigit(c)) {
                    sb.Append(capitalizeNext ? char.ToUpperInvariant(c) : c);
                    capitalizeNext = false;
                }
                else {
                    capitalizeNext = true;
                }
            }

            if (sb.Length == 0) sb.Append('_');
            if (!char.IsLetter(sb[0]) && sb[0] != '_') sb.Insert(0, '_');

            return sb.ToString();
        }
    }
}
#endif
using System.Windows.Input;
using System.Collections.Generic;

namespace Woofy.Other
{
    public static class KeyboardManager
    {
        private class KeyBinding
        {
            public ConditionalInvoker Condition { get; private set; }
            public MethodInvoker ExecuteMethod { get; private set; }
            public IEnumerable<Key> Keys { get; private set; }

            public KeyBinding(ConditionalInvoker condition, MethodInvoker executeMethod, params Key[] keys)
            {
                Condition = condition;
                ExecuteMethod = executeMethod;
                Keys = keys;
            }
        }

        private static List<KeyBinding> KeyBindings = new List<KeyBinding>();

        public static void RegisterKeyBinding(ConditionalInvoker condition, MethodInvoker executeMethod, params Key[] keys)
        {
            KeyBindings.Add(new KeyBinding(condition, executeMethod, keys));
        }

        public static void CheckKeyBindings(Key key)
        {
            foreach (KeyBinding binding in KeyBindings)
            {
                if (!BindingIsMatch(key, binding))
                    continue;

                binding.ExecuteMethod();
                return;
            }
        }

        private static bool BindingIsMatch(Key key, KeyBinding binding)
        {
            bool isMatch = false;
            foreach (Key bindingKey in binding.Keys)
            {
                if (bindingKey != key)
                    continue;

                isMatch = true;
                break;
            }

            if (!isMatch)
                return false;

            if (!binding.Condition())
                return false;

            return true;
        }
    }
}

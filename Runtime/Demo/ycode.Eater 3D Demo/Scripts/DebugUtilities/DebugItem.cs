using System;
using TMPro;
using UnityEngine;

namespace ycode.DebugUtilities
{
    public class DebugItem : MonoBehaviour
    {
        public Func<string> GetLabel;
        public Func<string> GetValue;

        public TextMeshProUGUI Label;
        public TextMeshProUGUI Value;

        void Update()
        {
            Label.text = GetLabel?.Invoke();
            Value.text = GetValue?.Invoke();
        }
    }
}

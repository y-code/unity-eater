using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbstractITextDisplay : MonoBehaviour
{
    public string Text { get => _getText?.Invoke(); set => _setText?.Invoke(value); }

    private Func<string> _getText;
    private Action<string> _setText;

    private void Start()
    {
        var text = GetComponent<Text>();
        if (text != null)
        {
            _getText = () => text.text;
            _setText = value => text.text = value;
        }
        else
        {
            var textMesh = GetComponent<TextMesh>();
            if (textMesh != null)
            {
                _getText = () => textMesh.text;
                _setText = value => textMesh.text = value;
            }
            else
            {
                var textMeshPro = GetComponent<TextMeshPro>();
                if (textMeshPro != null)
                {
                    _getText = () => textMeshPro.text;
                    _setText = value => textMeshPro.text = value;
                }
                else
                {
                    var textMeshProGui = GetComponent<TextMeshProUGUI>();
                    if (textMeshProGui != null)
                    {
                        _getText = () => textMeshProGui.text;
                        _setText = value => textMeshProGui.text = value;
                    }
                }
            }
        }
    }
}

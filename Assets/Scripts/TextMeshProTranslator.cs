using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextMeshProTranslator : MonoBehaviour {
    [Header("Settings")] [SerializeField] private string translateKey;

    private TextMeshProUGUI _textMeshPro;

    private void Awake() {
        this._textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable() {
        this.Translate();
    }

    public void Translate() {
        this._textMeshPro.text = LocalizationManager.Instance.Translate(this.translateKey);
    }

    public void SetText(string value) {
        this._textMeshPro.text = value;
    }
}
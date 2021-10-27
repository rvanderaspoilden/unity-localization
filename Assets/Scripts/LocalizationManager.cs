using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class LocalizationManager : MonoBehaviour {
    [Header("Settings")] [SerializeField] private Language defaultLang;

    private Language _currentLang;

    private Dictionary<string, string> _translations = new Dictionary<string, string>();
    private Dictionary<string, string> _defaultTranslations = new Dictionary<string, string>();

    public static LocalizationManager Instance;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
        } else {
            Instance = this;
            this._defaultTranslations = this.LoadTranslations(this.defaultLang);
        }
    }

    private void Start() {
        this.SetLang(this.defaultLang);
    }

    public void SetLang(Language language) {
        this._currentLang = language;
        this._translations = this.LoadTranslations(language);

        foreach (TextMeshProTranslator textMeshProTranslator in FindObjectsOfType<TextMeshProTranslator>()) {
            textMeshProTranslator.Translate();
        }
    }

    public string Translate(string key) {
        if (this._translations.ContainsKey(key)) {
            return this._translations[key];
        } else if (this._defaultTranslations.ContainsKey(key)) {
            return this._defaultTranslations[key];
        }

        return key;
    }

    public Language CurrentLang => _currentLang;

    private Dictionary<string, string> LoadTranslations(Language language) {
        TextAsset file = Resources.Load<TextAsset>($"Localization/{language.ToString()}");

        if (!file) {
            throw new Exception($"[Localization] Cannot find translation file for lang '{language.ToString()}'");
        }

        Debug.Log($"<color=green>[Localization]</color> Translations file '{language.ToString()}' has been loaded");

        return JsonConvert.DeserializeObject<Dictionary<string, string>>(file.text);
    }
}

public enum Language {
    FR,
    EN
}
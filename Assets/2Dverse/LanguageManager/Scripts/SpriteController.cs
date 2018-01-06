using System;
using System.Collections;
using System.Collections.Generic;
using SmartLocalization;
using UnityEngine;
using UnityEngine.UI;

public class SpriteController : MonoBehaviour {
    private Image image;
    public String Key;

    private void Awake () {
        image = GetComponent<Image>();
    }

    private void Start () {
        ChangeSprite(LanguageManager.Instance);
        LanguageManager.Instance.OnChangeLanguage += ChangeSprite;
    }

    private void ChangeSprite (LanguageManager languageManager) {
        Texture2D texture = languageManager.GetTexture(Key) as Texture2D;
        Rect rec = new Rect(0, 0, texture.width, texture.height);
        image.sprite = Sprite.Create(texture, rec, Vector2.one, 100);
    }
    
    private void OnDestroy () {
        if(LanguageManager.HasInstance)
            LanguageManager.Instance.OnChangeLanguage -= ChangeSprite;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmartLocalization;

public class RandomText : TextController {
    public int Quantity;

    private void OnEnable () {
        string key = String.Concat(Key, (int) UnityEngine.Random.Range(0f, Quantity));
        text.text = LanguageManager.Instance.GetTextValue(key);
    }

    private new void Start () { }
}
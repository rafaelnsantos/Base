using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class SwitchLanguage : MonoBehaviour {

    public void Switch () {
        LanguageSettings.Instance.SwitchLanguage();
    }
}

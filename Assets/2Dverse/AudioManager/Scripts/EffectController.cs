using UnityEngine;
using UnityEngine.UI;

public class EffectController : MonoBehaviour {
    public Sprite On, Off;
    private Image image;

    private void Awake () {
        image = GetComponent<Image>();
    }

    private void Start () {
        ChangeSprite();

        AudioSettings.Instance.OnEffectChange += ChangeSprite;
    }

    private void ChangeSprite () {
        image.sprite = AudioSettings.Instance.EffectOn ? On : Off;
    }

    private void OnDestroy () {
        if (AudioSettings.Instance)
            AudioSettings.Instance.OnEffectChange -= ChangeSprite;
    }

    public void SwitchEffect () {
        AudioSettings.Instance.SwitchEffect();
    }
}
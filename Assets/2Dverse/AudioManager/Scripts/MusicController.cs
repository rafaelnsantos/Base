using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour {
    public Sprite On, Off;
    private Image image;

    private void Awake () {
        image = GetComponent<Image>();
    }

    private void Start () {
        ChangeSprite();

        AudioSettings.Instance.OnMusicChange += ChangeSprite;
    }

    private void ChangeSprite () {
        image.sprite = AudioSettings.Instance.MusicOn ? On : Off;
    }

    private void OnDestroy () {
        AudioSettings.Instance.OnMusicChange -= ChangeSprite;
    }

    public void SwitchMusic () {
        AudioSettings.Instance.SwitchMusic();
    }
}
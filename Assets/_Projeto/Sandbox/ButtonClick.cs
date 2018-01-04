using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour {
    public AudioClip Audio;

    private Button button;

    public void Awake () {
        button = GetComponent<Button>();
    }

    private void Start () {
        button.onClick.AddListener(PlayClick);
    }

    private void PlayClick () {
        AudioSettings.Instance.PlayEffect(Audio);
    }
}
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour {
    public AudioClip Audio;

    private Button button;

    private void Start () {
        GetComponent<Button>().onClick.AddListener(PlayClick);
    }

    private void PlayClick () {
        AudioSettings.Instance.PlayEffect(Audio);
    }
}
using SmartLocalization;
using UnityEngine;
using UnityEngine.UI;

public class SpriteController : MonoBehaviour {

	private Image image;
	public string Key;

	private LanguageManager languageManager;

	private void Awake () {
		image = GetComponent<Image>();
		languageManager = LanguageManager.Instance;
	}

	private void Start () {
		ChangeSprite(languageManager);
		languageManager.OnChangeLanguage += ChangeSprite;
	}

	private void ChangeSprite (LanguageManager languageMan) {
		Texture2D texture = languageMan.GetTexture(Key) as Texture2D;
		Rect rec = new Rect(0, 0, texture.width, texture.height);
		image.sprite = Sprite.Create(texture, rec, Vector2.one, 100);
	}

	private void OnDestroy () {
		languageManager.OnChangeLanguage -= ChangeSprite;
	}

}
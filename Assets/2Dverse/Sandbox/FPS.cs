using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour {
	
	public float Frequency = 1.0f;
	public Text fps;

	private void Awake () {
		DontDestroyOnLoad(this);
	}

	void Start () {
		StartCoroutine(CountFps());
	}

	private IEnumerator CountFps () {
		for (;;) {
			// Capture frame-per-second
			int lastFrameCount = Time.frameCount;
			float lastTime = Time.realtimeSinceStartup;
			yield return new WaitForSeconds(Frequency);

			float timeSpan = Time.realtimeSinceStartup - lastTime;
			int frameCount = Time.frameCount - lastFrameCount;

			// Display it
			int framesPerSec = Mathf.RoundToInt(frameCount / timeSpan);
			fps.text = framesPerSec.ToString();
		}
	}

}
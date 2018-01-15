using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour {

	public Text fpsText;
	public  float updateInterval = 0.5F;
 
	private float accum   = 0; // FPS accumulated over the interval
	private int   frames  = 0; // Frames drawn over the interval
	private float timeleft; // Left time for current interval
 
	private void Awake () {
		DontDestroyOnLoad(this);
	}
	

	void Start()
	{
		timeleft = updateInterval;  
	}
 
	void Update()
	{
		timeleft -= Time.deltaTime;
		accum += Time.timeScale/Time.deltaTime;
		++frames;
 
		// Interval ended - update GUI text and start new interval
		if( timeleft <= 0.0 )
		{
			// display two fractional digits (f2 format)
			float fps = accum/frames;
			string format = $"{fps:F2} FPS";
			fpsText.text = format;
			timeleft = updateInterval;
			accum = 0.0F;
			frames = 0;
		}
	}
}
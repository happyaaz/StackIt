using UnityEngine;
using System.Collections;

public class Background_Sound : MonoBehaviour {

	public static bool muteMusicButtonBool = false;
	public static string muteMusicButton;
	public static bool muteEffectsButtonBool = false;
	public static string muteEffectsButton;
	public static bool muteMusicBool = false;

	// volume
	float vol = 0.0f;
	private static Background_Sound instance = null;
	public static Background_Sound Instance {
		get { return instance; }
	}
	void Awake() {
		//check if this is the original themesongobj and if not, destroy it
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
		audio.enabled = true;
		muteMusicButtonBool = false;
		muteMusicButton = "MuteMusicOffButton";
		muteEffectsButtonBool = false;
		muteEffectsButton = "MuteEffectsOffButton";
	}
	
	void Start() {
		vol = 0.0f; // volume from start
	}

	void FixedUpdate(){ // volume out in the game
		if(vol < 0.05f)
		{
			vol = vol + 0.01f;
			audio.volume = vol; 
		}
		if (muteMusicBool == false)
		{
			audio.enabled = true;
		} else if (muteMusicBool == true)
			audio.enabled = false;
	}
}

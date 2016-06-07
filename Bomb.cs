using UnityEngine;
using System.Collections;
using System;

public class Bomb : MonoBehaviour {

	public AudioClip landingSound;
	public AudioSource tntSoundObj;
	public LineScore lS;

	public GameObject explosionHolder;

	// Use this for initialization
	void Start () {
		lS = GameObject.Find ("Platform").GetComponent <LineScore> ();
		tntSoundObj = GameObject.Find ("TNT_SoundObj").GetComponent <AudioSource> ();
	}
	

	public void BlowTheBombUp () {
		//  play the sound
		//  send the positions of the bomb (because we destroy pbjects with the same Z and X coordinates)
		if (GUIMain.soundEffectMuteBool == false)
			tntSoundObj.PlayOneShot (landingSound);
		lS.BlowTheBombUp (transform.position.x, transform.position.z, 
		                  (System.Math.Round (transform.position.y, MidpointRounding.AwayFromZero)).ToString ());
		Instantiate (explosionHolder, transform.position, explosionHolder.transform.rotation);
		Destroy (transform.root.gameObject);
	}
}

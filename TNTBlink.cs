using UnityEngine;
using System.Collections;

public class TNTBlink : MonoBehaviour {

	float counter = 0;
//	Renderer [] TNT;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		counter += Time.deltaTime;
//		if (counter >= 1) {
//			if (gameObject.transform.localScale == new Vector3 (0.99f,0.99f,0.99f)){
//				gameObject.transform.localScale -= new Vector3 (0.3f,0.3f,0.3f);
//			} else {
//				gameObject.transform.localScale += new Vector3 (0.3f,0.3f,0.3f);
//			}
//			counter = 0;
//		}	
//	}

		counter += Time.deltaTime;
		if (counter >= 0.30f) {
			//			TNT = gameObject.GetComponentsInChildren<Renderer>();
			//			foreach (Renderer Bomb in TNT){
			if (gameObject.renderer.enabled){
				gameObject.renderer.enabled = false;
			}else {
				gameObject.renderer.enabled = true;
			} 
			counter = 0;	
		}
//		counter += Time.deltaTime;
//		if (counter >= 0.15f) {
////			TNT = gameObject.GetComponentsInChildren<Renderer>();
////			foreach (Renderer Bomb in TNT){
//				if (gameObject.renderer.enabled){
//					gameObject.renderer.enabled = false;
//				}else {
//					gameObject.renderer.enabled = true;
//				} 
//			counter = 0;	
//		}
	}
}

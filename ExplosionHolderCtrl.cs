using UnityEngine;
using System.Collections;

public class ExplosionHolderCtrl : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		Destroy (this.gameObject, 5.0f);		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

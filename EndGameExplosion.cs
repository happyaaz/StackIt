using UnityEngine;
using System.Collections;

public class EndGameExplosion : MonoBehaviour {
	
	ControlRigidbodies [] MoreScriptsToDisable;
	public GameObject RBorder,LBorder,FBorder, BBorder;
	
	// Use this for initialization
//	void OnGUI () {
//		if (GUI.Button (new Rect (Screen.width/2,Screen.height/2,100,100),"BOOM")){
//			Endstate ();
//		}
//	}
	
	public void Endstate () {
		RBorder.collider.enabled = false;
		LBorder.collider.enabled = false;
		FBorder.collider.enabled = false;
		BBorder.collider.enabled = false;
		MoreScriptsToDisable = FindObjectsOfType <ControlRigidbodies> ();	
		foreach (ControlRigidbodies ctrlRgdB in MoreScriptsToDisable) {
			ctrlRgdB.enabled = true;
			ctrlRgdB.currentState = ControlRigidbodies.PossibleStates.NoControl;	
		}
	}
}

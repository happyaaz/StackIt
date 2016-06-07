using UnityEngine;
using System.Collections;
using System;


public class BioHazardMeltdown : MonoBehaviour {

	public LineScore lS;


	void Start () {
		lS = GameObject.Find ("Platform").GetComponent <LineScore> ();
	}
	
	public void MeltEverythingDown () {
		lS.MeltEverythingDown (transform.position.x, transform.position.z, 
		                       (int) (System.Math.Round (transform.position.y, MidpointRounding.AwayFromZero)));
	}
}

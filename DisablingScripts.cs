using UnityEngine;
using System.Collections;

//  just disable all the scripts for one of the shapes when needed
public class DisablingScripts : MonoBehaviour {

	SendShapeToManager sstm;
	ActualRotation ar;
	MovingController mc;
	ControlRigidbodies cr;
	CubeController [] cc;
	DrawRaysCube [] drc;
	GameObject child;


	void Start () {
		sstm = GetComponent <SendShapeToManager> ();
		ar = GetComponent <ActualRotation> ();
		mc = GetComponent <MovingController> ();
		cr = GetComponent <ControlRigidbodies> ();
		cc = GetComponentsInChildren <CubeController> ();

		foreach (Transform tr in transform)
		{
			child = tr.gameObject;
		}

		drc = child.GetComponentsInChildren <DrawRaysCube> () ;
	}


	public void DisableEverything () {
		sstm.enabled = false;
		ar.enabled = false;
		mc.enabled = false;
		cr.enabled = false;

		foreach (CubeController cubeCon in cc)
		{
			cubeCon.enabled = false;
		}

		foreach (DrawRaysCube drawRCube in drc)
		{
			drawRCube.enabled = false;
		}
	}
}

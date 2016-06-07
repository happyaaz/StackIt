using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ActualRotation : MonoBehaviour {

	private Vector3 rotateDirection;

	private int frameCount = 0, frameLimit = 5;
	public List <GameObject> children = new List <GameObject> ();
	public bool rotating = false;
	MovingController mc;


	void Start () {
		mc = GetComponent <MovingController> ();
	}


	public void RotatePiece(Vector3 _dir) {
		rotateDirection = _dir;
		rotating = true; 
		//rotating = true;
		StartCoroutine (RotateTheShape ());
	}




	//  prevent from going outside
	//  in short, call this function while moving the shape by one meter in some direction until it is completely inside the grid
	public void CheckIfItIsOutside () {

		//  collect all the cubes the shapes consist of.
		Transform t = transform.GetChild (0);
		GameObject parent = t.gameObject;
		
		foreach (Transform go in parent.transform) {
			children.Add (go.gameObject);
		}

		//  neg x
		var outXNeg = from go in children where go.transform.position.x < ChangeableVariables.outXNeg select go;

		//  if there's smth
		//  RECURSION
		if (outXNeg.Any ())
		{
			transform.position += Vector3.right;
			children.Clear ();
			CheckIfItIsOutside ();
			return;
		}

		//  pos x
		var outXPos = from go in children where go.transform.position.x > ChangeableVariables.outXPos select go;
		if (outXPos.Any ())
		{
			transform.position += Vector3.left;
			children.Clear ();
			CheckIfItIsOutside ();
			return;
		}

		// neg z
		var outZNeg = from go in children where go.transform.position.z < ChangeableVariables.outZNeg select go;
		//Debug.Log (outZNeg.Count ());
		if (outZNeg.Any ())
		{
			transform.position += Vector3.forward;
			children.Clear ();
			CheckIfItIsOutside ();
			return;
		
		}

		// pos z
		var outZPos = from go in children where go.transform.position.z > ChangeableVariables.outZPos select go;
		if (outZPos.Any ())
		{
			transform.position += Vector3.back;
			children.Clear ();
			CheckIfItIsOutside ();
			return;
		}


	}


	//  since I don't want to use update
	IEnumerator RotateTheShape () {
		//  while possible rotate be 18 degrees


		//  how to rotate????

		frameCount = 0;
		//  after we are done with it check if there are some shapes outside
		CheckIfItIsOutside ();
		mc.currentState = MovingController.StatesOfTheShape.Idle;
		//  stop rotating
		mc.isPossibleToRotate = false;
		mc.readyToRotateWhileMoving = false;

			mc.SpawnGhostShape ();

	}


	void ClampAngle (float angle, float min, float max) {

		if (angle < 90 || angle > 270)
		{
			if (angle > 180)
				angle -= 360;
			if (max > 180)
				max -= 360;
			if (min > 180)
				min -= 360;
		}
		angle = Mathf.Clamp (angle, min, max);
	}

}

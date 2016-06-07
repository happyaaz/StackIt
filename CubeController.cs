using UnityEngine;
using System.Collections;
using System;


public class CubeController : MonoBehaviour {

	private GameObject root;
	private ControlRigidbodies cr;
	private Vector3 collidedObject;
	// raycast
	private RaycastHit hit;
	public bool smthBelow = false;


	public GameObject lineScore;
	LineScore lc;
	SpawnCubes spc;

	MovingController mC;


	void Start () {

		spc = GameObject.FindGameObjectWithTag ("Spawner").GetComponent<SpawnCubes> ();
		lineScore = GameObject.Find ("Platform");
		lc = lineScore.GetComponent <LineScore> ();
		//  to change the states later
		root = transform.root.gameObject;
		cr = root.GetComponent <ControlRigidbodies> ();
		mC = root.GetComponent <MovingController> ();
	}


	void Update () {
		//  since the parent (transform) has its own rigidbody we should check only children's possible collisions
		//  we check it until the collision happens
		//  if smth is below - no need to use raycast because we already know it

		if (this.transform.gameObject.name.Contains("block") && cr.currentState == ControlRigidbodies.PossibleStates.TurnOff 
		    																							&& smthBelow == false) 
		{
			//  check if smth is below
			Ray ray = new Ray (transform.position, Vector3.down);
			if (Physics.Raycast (ray, 1, 1 << 8)) 
			{
				//  if smth has this layer and below us
				//  set needed strings
				smthBelow = true;
				//  that will cause to run the function in all cubes in order to
				//  set Gravity to true and isKinematic to false to  be able to fall
				//Debug.Log ("HEY = " + transform.root.transform.position);
				cr.changeGravity = true;
				mC.smthBelow = true;
				//  make the parent fall
				transform.parent.rigidbody.useGravity = true;
				transform.parent.rigidbody.isKinematic = false;


			}
			//Debug.DrawRay (ray.origin, ray.direction, Color.red, 1);
		}
	}


	void OnCollisionEnter (Collision other) {

		//  second part - because the next cube will collide with the one that is lower,
		//  so once we change the state - we don't need to indicate collisions
		//  turnOff - to call it only these times we need it
		if (smthBelow == true && cr.currentState == ControlRigidbodies.PossibleStates.TurnOff)
		{
			//collidedObject = other.transform.position;
			//  Freeze just to make sure
			rigidbody.constraints = RigidbodyConstraints.FreezeAll;
			//  change the state
			if (cr.currentState == ControlRigidbodies.PossibleStates.TurnOff)
			{
				cr.currentState = ControlRigidbodies.PossibleStates.TurnOn;
			}
		}
	}

	public void FreezeY () {

		//  for all the cubes in the shape
		if (this.gameObject.name.Contains("block"))
		{

//			if (this.gameObject.transform.position.x > ChangeableVariables.outXPos || this.gameObject.transform.position.x < ChangeableVariables.outXNeg ||
//			    this.gameObject.transform.position.z > ChangeableVariables.outZPos || this.gameObject.transform.position.z < ChangeableVariables.outZNeg ) 
//			{
//				Destroy (this.gameObject);
//				return;
//			}
			//  because for some weird reasons not all the cubes are frozen
			//  we need to manually adjust their Y positions
			//Debug.Log ("Prev =" + transform.position);
			Vector3 newPos = new Vector3 (transform.position.x, 
			                              (float)System.Math.Round (transform.position.y, MidpointRounding.AwayFromZero), transform.position.z);
			transform.position = newPos;
			string newTag = string.Empty;

			//  we don't want the bomb to count as one of the cubes
			if (this.transform.gameObject.name.Contains ("1_"))
			{
				newTag = "Bomb";
			}
			else if (this.transform.gameObject.name.Contains ("S_block"))
			{
				newTag = "Biohazard";
			}
			else
			{
				//  tag every cube (we start from the FIRST floor)
				newTag = (System.Math.Round (transform.position.y, MidpointRounding.AwayFromZero)).ToString ();
			}
			transform.gameObject.tag = newTag;
		}
	//	else if (transform.name.Contains ("Shape"))
	//	{
	//		StartCoroutine (PickNewShape ());
	//	}

		//  It is grounded, so set this layer to every cube (and the parent as well)
		this.transform.gameObject.layer = LayerMask.NameToLayer ("Grounded");

		//  cr.currentState = ControlRigidbodies.PossibleStates.DontTouch;
		rigidbody.constraints = RigidbodyConstraints.FreezeAll;
	}

	//  function to turn on the Gravity
	//  the shape with the layer is one meter below - so that means that the new shape
	//  will fall down automatically
	public void StopMovingLetsFall () {
		this.rigidbody.useGravity = true;
		this.rigidbody.isKinematic = false;
	}

	public void MoveTheCubes () {
		lc.CheckFloors ();
		//  Change the state to save processor's resources
		cr.currentState = ControlRigidbodies.PossibleStates.DontTouch;

	}

	//  because we need a mesh to stop casting shadows
	public void StopCastingShadows () {
		MeshRenderer mR = new MeshRenderer ();
		try
		{
			mR = this.GetComponent <MeshRenderer> ();
		}
		catch
		{

		}

		if (mR != null)
		{
			mR.castShadows = false;
		}
	}


}

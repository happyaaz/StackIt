using UnityEngine;
using System.Collections;


//  check if we can rotate or move
public class DrawRaysCube : MonoBehaviour {

	private float checkBorders = 0.6f;

	void Start () {

	}


	public bool DrawRaysRotateInside () {
		//  we should not use CheckSphere to check if we can move one of the shapes
		Vector3 rayM = new Vector3 (transform.position.x, transform.position.y - 0.5f, transform.position.z);

		Ray rayLeftM = new Ray (rayM, transform.TransformDirection (Vector3.back));
		Ray rayRightM = new Ray (rayM, transform.TransformDirection (Vector3.forward));
		Ray rayForwardM = new Ray (rayM, transform.TransformDirection (Vector3.right));
		Ray rayBackM = new Ray (rayM, transform.TransformDirection (Vector3.left));

		
		if(!(Physics.Raycast (rayLeftM, checkBorders, 1 << 8))
		   && !(Physics.Raycast (rayRightM, checkBorders, 1 << 8))
		   && !(Physics.Raycast (rayForwardM, checkBorders, 1 << 8))
		   && !(Physics.Raycast (rayBackM, checkBorders, 1 << 8)))
		{
	//		Debug.DrawRay (rayLeftM.origin, rayLeftM.direction * 10, Color.red, 1);
	//		Debug.DrawRay (rayRightM.origin, rayRightM.direction * 10, Color.red, 1);
	//		Debug.DrawRay (rayForwardM.origin, rayForwardM.direction * 10, Color.red, 1);
	//		Debug.DrawRay (rayBackM.origin, rayBackM.direction * 10, Color.red, 1);
			return true;
		}
		else
		{
			return false;
		}
	}

	//  if we can rotate vertically
	public bool RotateVertically () {
		float checkBordersV = 0.6f;
		/*if (transform.root.gameObject.name.Contains ("Shark"))
		{
			checkBordersV = 2.1f;
		}
		*/
		Vector3 rayM = new Vector3 (transform.position.x, transform.position.y - 0.5f, transform.position.z);
		
		Ray rayUpM = new Ray (rayM, Vector3.up);
		Ray rayDownM = new Ray (rayM, Vector3.down);
		
		if(!(Physics.Raycast (rayUpM, checkBordersV, 1 << 8))
		   && !(Physics.Raycast (rayDownM, checkBordersV, 1 << 8))) 
		{
			return true;
		}
		else
		{
			return false;
		}
	}


	//  move a shape.
	//  either the shape is clear or goes outside the borders
	public bool checkSphere (Vector3 dir) {

		if (Physics.CheckSphere (transform.position + dir / 2, 0.5f, 1 << 10))
		{
			return false;
		}
		else
		{
			return true;
		}

		/*
		Ray rayM = new Ray (transform.position + dir / 2, dir);
		if (Physics.Raycast (transform.position + dir / 2, dir, 0.5f, 1 << 10))
		{
			Debug.DrawRay (rayM.origin, rayM.direction * 10, Color.red, 1);
			return false;
		}
		else
		{
			Debug.DrawRay (rayM.origin, rayM.direction * 10, Color.red, 1);
			return true;
		}

		*/
	}
}

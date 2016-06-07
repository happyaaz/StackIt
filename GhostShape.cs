using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class GhostShape : MonoBehaviour {

	public List <GameObject> children = new List <GameObject> ();
	public List <float> positionsOfLanded = new List <float> ();
	float moveBy = 0;
	float closestDistance = 0;
	float diff = 0;
	public float theLowestCube = 0;


	GameObject tntChild;


	// Use this for initialization
	void Start () {

		if (! (this.name.Contains ("TNT")))
		{
			Transform t = transform.GetChild (0);
			GameObject parent = t.gameObject;
			
			foreach (Transform go in parent.transform) {
				children.Add (go.gameObject);
			}
			
			LowestCube ();


		}
		else
		{
			tntChild = this.gameObject;
			children.Add (tntChild);
			RaycastHit hit;
			
			
			Ray rayD = new Ray (tntChild.transform.position, Vector3.down);
			
			if (Physics.Raycast (rayD, out hit, 15, 1 << 8))
			{
				if (hit.transform.tag != "Platform")
				{
					tntChild.transform.position = hit.transform.gameObject.transform.position + Vector3.up;
				}
				else
				{
					tntChild.transform.position = new Vector3 (
						this.transform.position.x,
						hit.transform.gameObject.transform.position.y + 0.5f,
						this.transform.position.z);
				}
			}
		}

		StartCoroutine (JustToMakeSure ());

	}
	
	IEnumerator JustToMakeSure () {

		yield return new WaitForSeconds (0.5f);
		MakingSure ();
		yield return new WaitForSeconds (1f);
		MakingSure ();
		yield return new WaitForSeconds (1f);
		MakingSure ();
	}


	void MakingSure () {

		if (this.name.Contains ("WC"))
		{
			CustomWCSituation ();
		}
		else
		{
			CustomSituation ();
		}
	}

	public void LowestCube () {
		children = children.OrderBy (item => item.transform.position.y).ToList ();
		//Debug.Log (children [0].transform.position.y + ", " + children [0].name);
		closestDistance = children [0].transform.position.y;
		theLowestCube = closestDistance;
		ClosestDistance ();
	}


	public void ClosestDistance () {

		RaycastHit hit;

		foreach (GameObject go in children)
		{
			Ray rayD = new Ray (go.transform.position + Vector3.down, Vector3.down);

			if (Physics.Raycast (rayD, out hit, 15, 1 << 8))
			{
				if (hit.transform.name == "PlatformGrid")
				{
					positionsOfLanded.Add (1);
					//Debug.Log ((hit.transform.position.y - 0.5f) + ", " + hit.transform.name);
				}
				else
				{
					//Debug.Log (hit.transform.position.y + ", " + hit.transform.name);
					int pos = Convert.ToInt32 (hit.transform.tag);
					pos ++;
					positionsOfLanded.Add ( pos);
				}
			}
		}


		positionsOfLanded = positionsOfLanded.OrderByDescending (item => item).ToList ();
	//	Debug.Log ("Highest cube = " + positionsOfLanded [0]);
		moveBy = positionsOfLanded [0];
		//int diff = 0;
		diff = closestDistance - moveBy;

	//	Debug.Log ("diff = " + diff);
		PlacingGhostShape ();

	}



	public void PlacingGhostShape () {

		this.transform.position = new Vector3 (
			this.transform.position.x,
			this.transform.position.y - diff,
			this.transform.position.z);
		// --         -> situation when we have to move WC by 1 lower  
		//  -
		//  so if Block1 and Block3 have nothing under them (1 meter)
		//  move it by 1 lower
		
		if (this.name.Contains ("WC"))
		{
//			Debug.Log ("Do it");
			CustomWCSituation ();
		}
		else if (this.name.Contains ("Sofa"))
		{
			CustomSituation ();
			CustomSituation ();
			//CustomSofaSituation ();
		}
		// ---         -> situation when we have to move CAR by 1 lower  
		//  -
		//  so all the blocks have nothing under them (1 meter)
		//  move it by 1 lower FridgeFreezer
		else 
		{
			//			Debug.Log ("Do it");
			CustomSituation ();
		}


	}


	void CustomSofaSituation () {

		RaycastHit hit;
		foreach (GameObject go in children) 
		{

			if (go.name == "L_block1")
			{
				Ray rayD = new Ray (go.transform.position, Vector3.down);
				if (! (Physics.Raycast (rayD, out hit, 0.7f, 1 << 8)))
				{
					this.transform.position += Vector3.down;
				}
			}
		}
	}


	void CustomWCSituation () {

		RaycastHit hit;

		int count = 0;
		bool block2SmthIsBelow = false;

		foreach (GameObject go in children) 
		{
			Ray rayD = new Ray (go.transform.position, Vector3.down);
			if (go.name == "3_block1" || go.name == "3_block3")
			{

				if (! (Physics.Raycast (rayD, out hit, 0.7f, 1 << 8)))
				{
					count ++;
				}
			}
			else
			{
				if (Physics.Raycast (rayD, out hit, 0.7f, 1 << 8))
				{
					block2SmthIsBelow = true;
				}
			}
		}

		if (count == 2 && block2SmthIsBelow == false)
		{
			this.transform.position += Vector3.down;
		}

	}


	void CustomSituation () {
		
		RaycastHit hit;
		
		int count = 0;
		
		foreach (GameObject go in children) 
		{
			Ray rayD = new Ray (go.transform.position, Vector3.down);

			if (! (Physics.Raycast (rayD, out hit, 0.7f, 1 << 8)))
			{
				count ++;
			}
			
		}
		
		if (count == children.Count)
		{
			this.transform.position += Vector3.down;
		}
		
	}

}

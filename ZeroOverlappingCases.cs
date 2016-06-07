using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;


public class ZeroOverlappingCases : MonoBehaviour {

	public List <GameObject> children = new List <GameObject> ();



	void Start () {
		foreach (Transform tr in transform)
		{
			children.Add (tr.gameObject);
		}

	}

	public void CheckIfTagIsZero () {
		foreach (GameObject go in children)
		{
			if (go.tag == "0")
			{
				foreach (GameObject go1 in children)
				{
					Destroy (go);
				}
			}
		}
	}

	//  overlapping
	public void CheckIfCubesAreIdentical () {
		for (int i = 0; i < children.Count - 1; i ++)
		{
			GameObject go = children [i];
			List <GameObject> withoutGo = new List <GameObject> ();

			for (int j = 0; j < children.Count; j ++)
			{
				if (children [j] != go)
				{
					withoutGo.Add (children [j]);
				}
			}

			for (int j = 0; j < withoutGo.Count; j ++)
			{
				if (go.transform.position.x == withoutGo [j].transform.position.x && 
				    go.transform.position.z == withoutGo [j].transform.position.z &&
				    go.transform.position.y == withoutGo [j].transform.position.y )
				{
					Destroy (go);
					children = withoutGo;
				}
			}

		}
	}
}

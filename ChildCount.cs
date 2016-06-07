using UnityEngine;
using System.Collections;

public class ChildCount : MonoBehaviour {

	public int numberOfChildren;
	public bool parentWasDeleted = false;


	void Start () {
		numberOfChildren = transform.childCount;
	}


	void Update () {
		//  when the shape has no children destroy it
		numberOfChildren = transform.childCount;
		if (numberOfChildren == 0 && parentWasDeleted == false)
		{
			Destroy (transform.root.gameObject);
			parentWasDeleted = true;
		}
	}

}

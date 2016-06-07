using UnityEngine;
using System.Collections.Generic;

public class ShapesManager : MonoBehaviour {

	public List <GameObject> activeShapes = new List<GameObject> ();

	void Start () {
	
	}

	public void AddNewShape (GameObject newShape) {
		activeShapes.Add (newShape);
	}

	public void deleteEmptyShapes () {
		for (int i = 0; i < activeShapes.Count; i ++)
		{
			Transform child = activeShapes [i].transform.GetChild (0);
			if (child.childCount == 0)
			{
				Destroy (activeShapes [i]);
				activeShapes.Remove (activeShapes [i]);
			}
		}
	}
}

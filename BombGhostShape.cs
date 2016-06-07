using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class BombGhostShape : MonoBehaviour {

	public GameObject ghostShape;
	GameObject realGhostShape;

	// Use this for initialization
	void Start () {

	}

	
	public void SpawnGhost () {
//		Debug.Log ("I was called");
		try
		{
			RaycastHit hit;
			Ray rayD = new Ray (transform.position, Vector3.down);
			if (Physics.Raycast (rayD, out hit, 20, 1 << 8))
			{
				Vector3 pos = new Vector3 ();
					GameObject root = transform.root.gameObject;
					pos = new Vector3 (
						this.transform.position.x,
						hit.transform.position.y + 0.5f,
						this.transform.position.z);

				realGhostShape = Instantiate (ghostShape, pos, transform.rotation) as GameObject;
			}
		}
		catch
		{

		}

	}
}

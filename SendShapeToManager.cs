using UnityEngine;
using System.Collections;

public class SendShapeToManager : MonoBehaviour {

	private GameObject shapeMan;
	ShapesManager sM;

	// Use this for initialization
	void Start () {
		shapeMan = GameObject.FindGameObjectWithTag ("ShapesManager");
		sM = (ShapesManager) shapeMan.GetComponent (typeof (ShapesManager));
		sM.AddNewShape (this.gameObject);
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DynamicPositioning : MonoBehaviour {


	private GameObject spawner;
	public GameObject mainCamera;
	public GameObject platform;
	public LineScore lS;
	public cameraController cC;
	public int numberOfFloorsWithDetail = 0;
	public List <int> values = new List <int> ();
	private bool reachedPosition = true;
	public Vector3 newTarPos; 
	float multiplier = 0.5f;

	// Use this for initialization
	void Start () {
		values.Add (numberOfFloorsWithDetail);
		spawner = GameObject.FindGameObjectWithTag ("Spawner");
	}

	void Update () {

		//  when needed change camera's positions dinamically
		if (reachedPosition == false)
		{
			if (Vector3.Distance (Camera.main.transform.position, cC.CamPos [cC.CamPosCounter]) != 0)
			{
				//  move higher within one second
				Camera.main.transform.position = Vector3.MoveTowards (Camera.main.transform.position, 
			                                                    cC.CamPos [cC.CamPosCounter],
				                                                Time.deltaTime * numberOfFloorsWithDetail);

				cC.CamTarget = Vector3.MoveTowards (cC.CamTarget, newTarPos, Time.deltaTime * numberOfFloorsWithDetail);

				cC.ChangeCamera ();
			}
			else
			{
				reachedPosition = true;
			}
		}
	}


	public void DynamicallyChangeCameraAndSpawner () {

		//  we change camera's positions only in case if the previous number of floors with details is different
		if (values [values.Count - 1] != numberOfFloorsWithDetail)
		{

			if (numberOfFloorsWithDetail < 6)
			{
				multiplier = 0.7f;
			}
			else
			{
				multiplier = 1f;
			}
			//  because we can move camera again (one floor is cleared and then the second one, too, plus very fast)
			reachedPosition = true;
			//  add this value to the list
			values.Add (numberOfFloorsWithDetail);
			spawner.transform.position = new Vector3 (spawner.transform.position.x,
				                                          4 + numberOfFloorsWithDetail,
				                                          spawner.transform.position.z);
			//  new positions
			for (int i = 0; i < cC.CamPos.Length; i ++)
			{
				cC.CamPos [i] = new Vector3 (cC.CamPos [i].x, 
				                             8 + numberOfFloorsWithDetail * multiplier, 
				                             cC.CamPos [i].z);
					
			}
			//  new target
			newTarPos = new Vector3 (cC.CamTarget.x, 5 + numberOfFloorsWithDetail / 1.7f, cC.CamTarget.z);

			reachedPosition = false;
		}
	}

}

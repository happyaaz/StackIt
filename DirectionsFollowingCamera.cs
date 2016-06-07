using UnityEngine;
using System.Collections;

public class DirectionsFollowingCamera : MonoBehaviour {

	public Vector3 dir1 = Vector3.right;
	public Vector3 dir2 = Vector3.forward;
	public Vector3 dir3 = Vector3.left;
	public Vector3 dir4 = Vector3.back;


	public Vector3 rot3 = Vector3.left;
	public Vector3 rot4 = Vector3.right;

	//the automatic downwards movement speed
	public float speedOfFalling;
	//current difficulty level
	private float curDifficulty;
	//variable to access the ScoreController script
	ScoreController sc;

	public GameObject spawnCubesObj;
	public SpawnCubes spawnCubesScript;


	// Use this for initialization
	void Start () {
		//LineScore.changeDiff += DifficultyCurve;

		Time.timeScale = 1;
		sc = GameObject.Find ("Platform").GetComponent <ScoreController> ();
		speedOfFalling = 5;	
	}

	public void ChangeDirections (Vector3 dir1L, Vector3 dir2L, Vector3 dir3L, Vector3 dir4L) {
		dir1 = dir1L;
		dir2 = dir2L;
		dir3 = dir3L;
		dir4 = dir4L;
	}

	public void ChangeRotations (Vector3 rot3L, Vector3 rot4L) {
		rot3 = rot4L;
		rot4 = rot4L;
	}

	//  each two floors decrease time
	/*
	public void DifficultyCurve ()
	{

		if (sc.FloorsCleared % 2 == 0 && sc.FloorsCleared > 0) 
		{
			spawnCubesScript.timeToDecreaseTimer ++;
			speedOfFalling -= 0.1f;
		}
	}
	*/
}

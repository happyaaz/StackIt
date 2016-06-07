using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;


public class SpawnCubes : MonoBehaviour {
	public GUISkin Tetris1;
	public GameObject [] Cubes;
	public static GameObject tempcube1;
	private ScoreController sc;
	public int tempnum;
	public int shapesToUse = 1;
	public string nameOfNextShape;
	private GUIMain gm;
	private bool newShapeToBeDisplayed = false;
	public List <int> shapesToDisplay = new List<int> ();
	public static int timer = 20;
	public int timeToDecreaseTimer = 0;

	public GameObject shapesManager;
	public ShapesManager shapeMan;

	public GameObject lineScore;
	public LineScore lC;

	public GameObject dynamicPositioning;
	public DynamicPositioning dP;

	public GameObject tutorial;
	public TutorialLevel tL;

	public GameObject bioHazardLiquidHolder;

	List <TutorialLevel.TutorialStates> statesList = new List <TutorialLevel.TutorialStates> ();

	int numberOfTheState = 0;
	//  switching
	int index = 0;

	public delegate void Switching ();
	public static event Switching switchMovements;

	public static int totalTime = 0;
	
	public bool barrelsAvailable = false;


	bool bombWasSpawnedIn3by3 = false;
	int clearedFloorLastTime = 0;

	DirectionsFollowingCamera dfc;



	// Use this for initialization
	void Start () {

		if (ChangeableVariables.levelWeAreIn.Contains ("5x5") || StartFunction.freePlaySubLevels3appearB == true) 
		{
			barrelsAvailable = true;
		}
		else
		{
		}
		dfc = GameObject.Find ("DirectionsConnectedWithCamera").GetComponent <DirectionsFollowingCamera> ();
		//  total time we spend on accomplishing a level
		totalTime = 0;

		if (ChangeableVariables.levelWeAreIn == "3x3_2")
		{
			statesList.Add (TutorialLevel.TutorialStates.Accelerometer);
			statesList.Add (TutorialLevel.TutorialStates.Switch);
		}

		StartCoroutine (Timer ());

		for (int i = 1; i < 7; i ++)
		{
			shapesToDisplay.Add (i);
		}

		gm = Camera.main.GetComponent<GUIMain> ();
		sc = GameObject.Find ("Platform").GetComponent<ScoreController> ();
		tempnum = UnityEngine.Random.Range (ChangeableVariables.spawnFrom, ChangeableVariables.spawnTo);
		if (ChangeableVariables.levelWeAreIn == "3x3_3")
		{
			ChangeableVariables.spawnFrom = 0;
		}
		PickShape ();
	}


	void OnGUI () {
	}


	public void SwitchToBarrels () {
		Destroy (tempcube1);
		tempcube1 = Instantiate(Cubes[10],transform.position,transform.rotation) as GameObject;
		Instantiate (bioHazardLiquidHolder, transform.position, transform.rotation);
		//  switch current and next shapes
		SwapToBarrels ();
		
		nameOfNextShape = Cubes [tempnum].name;
		gm.DisplayImage ();
		
		//  change directions
		if (switchMovements != null)
		{
			switchMovements ();
		}
	}

	void SwapToBarrels () {
		int temp = tempnum;
		tempnum = index;
		index = temp;
	}

	public void SwitchShapes () {
		Destroy (GameObject.FindGameObjectWithTag ("GhostShape"));
		//var index = Array.FindIndex (Cubes, row => row.name.Contains (tempcube1.name.Substring (0, 7)));
///		Debug.Log ("Index = " + index);
		//  destroy a shape, spawn a new one instead
		Destroy (tempcube1);
		tempcube1 = Instantiate(Cubes[tempnum],transform.position,transform.rotation) as GameObject;
		//  switch current and next shapes
		Swap ();

		nameOfNextShape = Cubes [tempnum].name;
		gm.DisplayImage ();

		//  change directions
		if (switchMovements != null)
		{
			switchMovements ();
		}
	}



	void Swap () {
		int temp = tempnum;
		tempnum = index;
		index = temp;
	}


	IEnumerator Timer () {

			while (true)
			{
				
				yield return new WaitForSeconds (1);
				if (GUIMain.PauseIsOpen == false)
				{
					//  count total time
					totalTime ++;
					//Debug.Log (totalTime);
					if ((ChangeableVariables.levelWeAreIn == "3x3_1" && shapeMan.activeShapes.Count > 1) 
				    || ChangeableVariables.levelWeAreIn != "3x3_1")
					{
						if (lC.cantSwitchWhenVictoryOrLose == false)
						{
							timer --;
						}
					}
					//  space was hit for a current shape
					if ((timer <= 0) && shapeMan.activeShapes.Count > 0)
					{
						MovingController mC = shapeMan.activeShapes [shapeMan.activeShapes.Count - 1].GetComponent <MovingController> ();
						mC.SpaceWasHit ();
					}
				}
			}


	}

	


	public void PickShape () {
		if (ChangeableVariables.levelWeAreIn == "3x3_3")
		{
			ChangeableVariables.spawnFrom = 1;
		}
		//  tutorial level
		/*
		if (ChangeableVariables.levelWeAreIn == "3x3_1" && numberOfTheState < 4)
		{
			tL.currentState = statesList [numberOfTheState];
			tL.CurrentTexture ();
			tL.NewPositionsOfTheVectors ();
			numberOfTheState ++;
		}
		*/

		//  introduce accelerometer
		if (ChangeableVariables.levelWeAreIn == "3x3_2" && numberOfTheState < 2 && 
		    	PlayerPrefs.GetString ("level3by3_3enabled") == "false") {
			tL.currentState = statesList [numberOfTheState];
			tL.CurrentTexture ();
			tL.NewPositionsOfTheVectors ();
			numberOfTheState ++;
		}

		dP.numberOfFloorsWithDetail = lC.CountElementsInLists ();
		dP.DynamicallyChangeCameraAndSpawner ();


		if (lC.floor10Count.Count > 0)
		{
			lC.GameOver ();
			return;
		}
		//  standard time before a shape falls down
		if (ChangeableVariables.levelWeAreIn == "3x3_1" || ChangeableVariables.levelWeAreIn == "3x3_2" || ChangeableVariables.levelWeAreIn == "3x3_3")
		{
			timer = 30;
		}
		else
		{
			timer = 20;
		}

		if (sc.FloorsCleared % 2 == 0 && sc.FloorsCleared > 0 && clearedFloorLastTime != (int) sc.FloorsCleared && sc.FloorsCleared < 27) 
		{
			clearedFloorLastTime = (int) sc.FloorsCleared;
			timeToDecreaseTimer ++;
//			Debug.Log ("Decreased it");

		}
		//dfc.DifficultyCurve ();
		//  and decrease it

		DecreaseTimer (timeToDecreaseTimer);
		if (newShapeToBeDisplayed == true)
		{
			CheckIfItIsNewShape ();
		}

		tempcube1 = Instantiate(Cubes[tempnum],transform.position,transform.rotation) as GameObject;

		if (lC.floorClearedThisDrop) 
		{
			lC.floorClearedThisDrop = false;
			lC.floorClearedLastDrop = true;
			sc.FloorComboMultiplyer++;
		} 
		else if (!lC.floorClearedThisDrop && lC.floorClearedLastDrop) 
		{
			lC.floorClearedLastDrop = false;
			sc.FloorComboMultiplyer = 1;
		}

		sc.ScoreForDrop = 10;
		tempcube1.tag = "Active";
		index = tempnum;
		gm.Dropping = false;


		GettingRandomNumber ();

		//  3x3 level - spawn the bomb only once.
		if (ChangeableVariables.levelWeAreIn == "3x3_3" && bombWasSpawnedIn3by3 == false)
		{

			if (lC.floor1Count.Count > 4 || lC.floor2Count.Count > 4  || lC.floor3Count.Count > 4  
			    || lC.floor4Count.Count > 4  || lC.floor5Count.Count > 4 )
			{
//				Debug.Log ("Did it");
				tempnum = 0;
				ChangeableVariables.spawnFrom = 1;
//				Debug.Log (ChangeableVariables.spawnFrom);
				bombWasSpawnedIn3by3 = true;
			}
		}

		nameOfNextShape = Cubes [tempnum].name;
		gm.DisplayImage ();
		/*
		if (switchMovements != null)
		{
			switchMovements ();
		}
		*/
	}


	void GettingRandomNumber () {
		tempnum = UnityEngine.Random.Range (ChangeableVariables.spawnFrom, ChangeableVariables.spawnTo);
		if (tempnum == 0 && ChangeableVariables.levelWeAreIn.Contains ("4"))
		{
			int chance = UnityEngine.Random.Range (0, 2);
			if (chance == 0)
			{
				tempnum = 0;
			}
			else
			{
				GettingRandomNumber ();
			}
		}
		if (tempnum == 0 && ChangeableVariables.levelWeAreIn == "3x3_1")
		{
			GettingRandomNumber ();
		}
	}


	public void DecreaseTimer (int decreaseBy) {
		if (timer > 7)
			timer -= decreaseBy;
		else
			timer = 7;

		gm.maxTime = timer;
	}

	void CheckIfItIsNewShape () {
		if (shapesToDisplay.Exists (item => item == tempnum))
		{
			//Debug.Log ("Wow-wow-wow, it is a new shape!!!");
			shapesToDisplay.Remove (tempnum);
			newShapeToBeDisplayed = false;
		}
	}


	//  Resizing Script
	public static void AutoResize(int screenWidth, int screenHeight)
	{
		Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
	}

}

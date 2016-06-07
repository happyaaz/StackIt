using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Reflection;
using System;


public class LineScore : MonoBehaviour {
	
	public List <GameObject> floor1Count = new List<GameObject> ();
	public List <GameObject> floor2Count = new List<GameObject> ();
	public List <GameObject> floor3Count = new List<GameObject> ();
	public List <GameObject> floor4Count = new List<GameObject> ();
	public List <GameObject> floor5Count = new List<GameObject> ();
	public List <GameObject> floor6Count = new List<GameObject> ();
	public List <GameObject> floor7Count = new List<GameObject> ();
	public List <GameObject> floor8Count = new List<GameObject> ();
	public List <GameObject> floor9Count = new List<GameObject> ();
	public List <GameObject> floor10Count = new List<GameObject> ();
	public List <GameObject> floor11Count = new List<GameObject> ();
	public List <GameObject> floor12Count = new List<GameObject> ();
	public List <GameObject> floor13Count = new List<GameObject> ();
	
	public bool floorClearedThisDrop = false;
	public bool floorClearedLastDrop = false;
	public int NumOfFloorsDestroyedThisDrop = 0;
	
	public AudioClip Clicking;
	
	public GameObject shapeMan;
	public ShapesManager sM;
	public int sizeOfFloor;
	
	public bool gameOverBool = false;
	public bool victoryBool = false;
	public bool victoryText = false;
	public bool showGoalsGUI = true;
	
	//themesong
	public AudioSource music;
	private GameObject themeSongObj;

	//Variables for the crusher
	public AudioClip crushingSound;
	private int newCrusherYPos;
	private GameObject crushingSmoke1, crushingSmoke2, pistonRHorizontal, pistonLHorizontal, pistonRVertical, pistonLVertical;

	//Clearing floor particles and position variable
	public GameObject clearingFloorPartHolder;
	private Vector3 tempPos = new Vector3(0,0,0);

	//endgame explosion particles
	public GameObject platformPileExplHolder;
	public GameObject crusherExplosionHolder;

	ScoreController sc;
	EndGameExplosion ege;
	HighScore hsc;
	
	
	
	private int theHighestFloor = 13;
	
	//Game over song object
	public GameObject gameOverSoundObject;
	//Game Over Explosion
	private AudioSource tntSoundObj;
	public AudioClip gameOverExplosion;
	
	//Victory song object
	public GameObject victorySongObj;
	
	//victory GUI picture
	public Texture2D victoryTex;
	//game over GUI picture
	public Texture2D gameOverTex;
	
	//show victory text
	private bool showVictoryText = false;
	//show game over text
	private bool showGameOverText = false;
	
	//victory particles
	public GameObject victoryParticlesHolder;
	
	public delegate void ChangeDifficulty ();
	public static event ChangeDifficulty changeDiff;
	
	//  for highscore
	public GameObject highScore;
	public HighScore hS;
	
	public GameObject dynamicPositioning;
	public DynamicPositioning dP;
	
	
	
	public static int floorsAtTheSameTime = 0;
	
	//  level goals
	public delegate void fiveByFive ();
	public static event fiveByFive level31;
	public static event fiveByFive level32;
	public static event fiveByFive level33;
	
	public delegate void fourBeFour ();
	public static event fourBeFour level21;
	public static event fourBeFour level22;
	public static event fourBeFour level23;
	
	public delegate void threeByThree ();
	public static event threeByThree level11;
	public static event threeByThree level12;
	public static event threeByThree level13;
	
	//variables for the crusher to keep the same hight as the cleared floors when you clear many floors at once
	private int yPosIncrementer = 0;
	private int tempYPos = 0;
	//  meltdown
	public List <GameObject> objectsToCheck = new List <GameObject> ();
	public List <GameObject> possibleChildrenToMove = new List <GameObject> ();
	public List <GameObject> childrenInObjectsTocheck = new List<GameObject> ();
	public int indexMelt = 0;
	
	public List <GameObject> ancestors = new List<GameObject> ();
	
	
	public List <GameObject> temp1 = new List<GameObject> ();
	public List <GameObject> temp2 = new List<GameObject> ();
	public List <GameObject> temp3 = new List<GameObject> ();
	public List <GameObject> temp4 = new List<GameObject> ();
	public List <GameObject> temp5 = new List<GameObject> ();
	public List <GameObject> temp6 = new List<GameObject> ();
	public List <GameObject> temp7 = new List<GameObject> ();
	public List <GameObject> temp8 = new List<GameObject> ();
	public List <GameObject> temp9 = new List<GameObject> ();
	public List <GameObject> temp10 = new List<GameObject> ();
	public List <GameObject> temp11 = new List<GameObject> ();
	public List <GameObject> temp12 = new List<GameObject> ();
	
	private List <List <GameObject>> storage = new List<List<GameObject>> ();
	
	private bool threeByThreeThirdLevelEnabled = false;
	public bool cantSwitchWhenVictoryOrLose = false;
	//private Rect levelsGUIPos = new Rect (185, 960, 350, 100);

	public GameObject activeObject;
	string levelToReplay;

	
	void Start () {
		//Debug.Log ("ChangeableVariables.levelWeAreIn = " + ChangeableVariables.levelWeAreIn);
		storage.Add (temp1);
		storage.Add (temp2);
		storage.Add (temp3);
		storage.Add (temp4);
		storage.Add (temp5);
		storage.Add (temp6);
		storage.Add (temp7);
		storage.Add (temp8);
		storage.Add (temp9);
		storage.Add (temp10);
		storage.Add (temp11);
		storage.Add (temp12);
		
		if (ChangeableVariables.levelWeAreIn == "3x3_3")
		{
			threeByThreeThirdLevelEnabled = true;
		}
		sizeOfFloor = ChangeableVariables.squaresToFill;
		
		levelToReplay = ChangeableVariables.levelWeAreIn;
		//		if (3 % 3 == 0)
		//		{
		//			Debug.Log ("True");
		//		}

		themeSongObj = GameObject.Find ("Themesong");
	//	music = GameObject.Find ("Themesong").GetComponent<AudioSource> ();
		ege = GameObject.Find ("ExplosionThing").GetComponent<EndGameExplosion> ();
		sc = GameObject.Find ("Platform").GetComponent<ScoreController> ();
		hsc = GameObject.Find ("HighScore").GetComponent<HighScore> ();
		crushingSmoke1 = GameObject.FindGameObjectWithTag("CrushingSmoke1Tag");
		crushingSmoke2 = GameObject.FindGameObjectWithTag("CrushingSmoke2Tag");
		pistonRHorizontal = GameObject.FindGameObjectWithTag("PistonRHorizontalTag");
		pistonLHorizontal = GameObject.FindGameObjectWithTag("PistonLHorizontalTag");
		pistonRVertical = GameObject.FindGameObjectWithTag("PistonRVerticalTag");
		pistonLVertical = GameObject.FindGameObjectWithTag("PistonLVerticalTag");
		tntSoundObj = GameObject.Find ("TNT_SoundObj").GetComponent <AudioSource> ();

		if (ChangeableVariables.levelWeAreIn == "3x3_2")
		{
			PlayerPrefs.SetString ("CameraMovementUnlocked", "true");
			PlayerPrefs.Save ();
		}
	}
	
	void Update() {
		//  just in case if this value is set to 1 accidentally
		if(gameOverBool != true && Time.timeScale == 0)
		{
			Time.timeScale = 1;
		}
	}
	
	//  coung how many floors have at least one detail on them
	public int CountElementsInLists () {
		
		int numberOfFloorsWithDetails = 0;
		
		if (floor1Count.Count > 0)
		{
			numberOfFloorsWithDetails ++;
		}
		if (floor2Count.Count > 0)
		{
			numberOfFloorsWithDetails ++;
		}
		if (floor3Count.Count > 0)
		{
			numberOfFloorsWithDetails ++;
		}
		if (floor4Count.Count > 0)
		{
			numberOfFloorsWithDetails ++;
		}
		if (floor5Count.Count > 0)
		{
			numberOfFloorsWithDetails ++;
		}
		if (floor6Count.Count > 0)
		{
			numberOfFloorsWithDetails ++;
		}
		if (floor7Count.Count > 0)
		{
			numberOfFloorsWithDetails ++;
		}
		if (floor8Count.Count > 0)
		{
			numberOfFloorsWithDetails ++;
		}
		if (floor9Count.Count > 0)
		{
			numberOfFloorsWithDetails ++;
		}
		if (floor10Count.Count > 0)
		{
			numberOfFloorsWithDetails ++;
		}
		return numberOfFloorsWithDetails;
	}
	
	
	void OnGUI () {	
		
		AutoResize (720, 1280);
		GUI.skin = StartFunction.currentTheme_GUISkin;

		//game over screen
		if (gameOverBool == true)
		{	
			StartFunction.FreePlay = false;
			StartFunction.freePlayGUIBool = false;
			StartFunction.freePlaySubLevels3appearB = false;
			GUI.depth = 0;
			showGameOverText = false;
			themeSongObj.audio.enabled = false;
			if(GUIMain.gameOverSongMuteBool == false)
			{
				gameOverSoundObject.SetActive (true);
			}
			Time.timeScale = 0;
			
			GUI.Box (new Rect (110, 140, 500, 1000), "", "GameOVictBox");
			GUI.Label(new Rect(-30, 100, (victoryTex.width), (victoryTex.height)), "", "GameOverLabel");
			if (GUI.Button(new Rect (185, 480, 350, 100), "REPLAY", "OptionsMenuButton"))		
			{	
				if (GUIMain.soundEffectMuteBool == false)
				{
					audio.PlayOneShot(Clicking, 0.05f);
				}
				themeSongObj.audio.enabled = true;
				ChangeableVariables.levelWeAreIn = levelToReplay;
				Application.LoadLevel(1);
			//	Time.timeScale = 1;
			}
			if (GUI.Button(new Rect (185, 800, 350, 100), "LEVELS", "OptionsMenuButton"))		
			{	
				if (GUIMain.soundEffectMuteBool == false)
				{
					audio.PlayOneShot(Clicking, 0.05f);
				}
				ChangeableVariables.enabled5by5goals = false;
				ChangeableVariables.enabled4by4goals = false;
				ChangeableVariables.enabled3by3goals = false;
				StartFunction.freePlayGUIBool = false;
				StartFunction.openChoosingLevels = true;
				StartFunction.camPosWLogoBool = false;
				themeSongObj.audio.enabled = true;
				Application.LoadLevel(0);
				//Time.timeScale = 1;
			}
			if (hsc.isPossibleToDisplayResults == true)
			{
				//  displaying highscore
				GUI.Label (new Rect (225, 320, 170, 800), hsc.displayNames, "GameOVictHscLabel");
				GUI.Label (new Rect (405, 320, 170, 800), hsc.displayScore, "GameOVictHscLabel");
			}
		}
		//victoryBool
		//AutoResize (900,1600);
		//GUI.skin = TetrisMain;
		//victory screen
		if (victoryBool == true)
		{			
			GUI.depth = 0;
			showVictoryText = false;
			showGoalsGUI = false;
			themeSongObj.audio.enabled = false;
			if(GUIMain.victorySongMuteBool == false)
			{
				victorySongObj.SetActive (true);
			}
			Time.timeScale = 0;
			GUI.Box (new Rect (110, 140, 500, 1000), "", "GameOVictBox");
			if (ChangeableVariables.levelWeAreIn != "5x5_3")
			{
				GUI.Label(new Rect(-30, 100, (victoryTex.width), (victoryTex.height)), "", "VictoryLabel");

				if (GUI.Button (new Rect (185, 400, 350, 100), "NEXT LEVEL", "OptionsMenuButton"))
				{
					if (GUIMain.soundEffectMuteBool == false)
					{
						audio.PlayOneShot (Clicking, 0.05f);
					}
					victoryParticlesHolder.SetActive (false);
					themeSongObj.audio.enabled = true;
					Application.LoadLevel (1);
					//Time.timeScale = 1;
				}
				if (GUI.Button(new Rect (185, 630, 350, 100), "REPLAY", "OptionsMenuButton"))		
				{	
					if (GUIMain.soundEffectMuteBool == false)
					{
						audio.PlayOneShot(Clicking, 0.05f);
					}
					themeSongObj.audio.enabled = true;
					ChangeableVariables.levelWeAreIn = levelToReplay;
					Application.LoadLevel(1);
				}
				if (GUI.Button(new Rect (185, 860, 350, 100), "LEVELS", "OptionsMenuButton"))		
				{	
					if (GUIMain.soundEffectMuteBool == false)
					{
						audio.PlayOneShot(Clicking, 0.05f);
					}
					victoryParticlesHolder.SetActive (false);
					ChangeableVariables.enabled5by5goals = false;
					ChangeableVariables.enabled4by4goals = false;
					ChangeableVariables.enabled3by3goals = false;
					StartFunction.freePlayGUIBool = false;
					StartFunction.openChoosingLevels = true;
					StartFunction.camPosWLogoBool = false;
					themeSongObj.audio.enabled = true;
					Application.LoadLevel (0);
					//Time.timeScale = 1;
				}
			}
			else
			{
				if (GUI.Button(new Rect (240, 890, 280, 150), "LEVELS", "OptionsMenuButton"))		
				{	
					if (GUIMain.soundEffectMuteBool == false)
					{
						audio.PlayOneShot(Clicking, 0.05f);
					}
					victoryParticlesHolder.SetActive (false);
					ChangeableVariables.enabled5by5goals = false;
					ChangeableVariables.enabled4by4goals = false;
					ChangeableVariables.enabled3by3goals = false;
					StartFunction.freePlayGUIBool = false;
					StartFunction.openChoosingLevels = true;
					StartFunction.camPosWLogoBool = false;
					themeSongObj.audio.enabled = true;
					Application.LoadLevel (0);
					//Time.timeScale = 1;
				}
			}
			if (hsc.isPossibleToDisplayResults == true)
			{
				//  displaying highscore
				GUI.Label (new Rect (225, 360, 170, 800), hsc.displayNames, "GameOVictHscLabel");
				GUI.Label (new Rect (405, 360, 170, 800), hsc.displayScore, "GameOVictHscLabel");
			}
		}
		if(showVictoryText == true)
			GUI.Label(new Rect(-30, 250, (victoryTex.width), (victoryTex.height)), "", "VictoryLabel");

		if(showGameOverText == true)
			GUI.Label(new Rect(-30, 250, (victoryTex.width), (victoryTex.height)), "", "GameOverLabel");
	}
	
	void MovePistons (int yPos) {
		if (StartFunction.junkyardTheme == false)
		{
			tempPos = new Vector3(0, yPos, 0);
			Instantiate (clearingFloorPartHolder, tempPos, clearingFloorPartHolder.transform.rotation);
		}
		if (StartFunction.junkyardTheme == true) {
			tempYPos = yPos - yPosIncrementer;
			if (yPos > 0) {
				pistonRVertical.transform.Translate (0, tempYPos, 0);
				pistonLVertical.transform.Translate (0, tempYPos, 0);
				//yield return new WaitForSeconds(0.2f);
				//DoStuffAfterDestroyingAFloor(yPos, listWithObjectsToDestroy, floorToMove);
			}
			
			yPosIncrementer ++;
		}
		floorClearedThisDrop = true;
		sc.MultipleFloorCombo = yPosIncrementer;
		NumOfFloorsDestroyedThisDrop++;
		sc.FloorCleared ();
		
	}
	
	
	void DestroyObjectsInLists (List <GameObject> listWithObjectsToDestroy) {
		
		for (int i = 0; i < listWithObjectsToDestroy.Count; i++)
		{
			Destroy(listWithObjectsToDestroy[i]);
		}
		
		if (listWithObjectsToDestroy.Count != 0)
		{
			listWithObjectsToDestroy.Clear ();
		}
	}
	
	
	void MoveDownFloorsAbove (int startFloor) {
		//  we need to move every floor that is higher than the destroyed one
		if (startFloor != 12)
		{
			for (int i = startFloor; i < theHighestFloor; i ++)
			{
				//  pass numbers of the floors to be moved
				MoveCubesAfterDestroyingTheFloor (i);
			}
		}
		else
		{
			MoveCubesAfterDestroyingTheFloor (startFloor);
		}
	}
	
	
	IEnumerator CrushersBusiness (int yPos) {
		if (StartFunction.junkyardTheme == true) 
		{
			//activating the crusher crushing
			//moving the Tetris shapes down needs to wait a bit to let the crusher do its thing
			yield return new WaitForSeconds (0.3f);
			pistonRVertical.transform.Translate (0, -tempYPos, 0);
			pistonLVertical.transform.Translate (0, -tempYPos, 0);
		}
	}
	
	
	void DoStuffAfterDestroyingAFloor (int yPos, int floorToMove) {
		//StartCoroutine (MovePistons (yPos));
		//DestroyObjectsInLists (listWithObjectsToDestroy);
		if (StartFunction.junkyardTheme == true) 
			StartCoroutine (CrushersBusiness (yPos));
		MoveDownFloorsAbove (floorToMove);
		
		//  fire an event when we clear one floor - 3x3 / first level
		Level1in3by3IsAccomplished ();
		
		//  fire an event when we clear two floors at the same time - 3x3 / third level
		floorsAtTheSameTime ++;
		if (floorsAtTheSameTime == 2) 
		{
			Level3in3by3IsAccomplished ();
		}

		//changeDiff ();
		
	}
	
	
	public void LevelIsAccomplished () {
		Destroy (GameObject.FindGameObjectWithTag ("GhostShape"));
		showGoalsGUI = false;
		Destroy (SpawnCubes.tempcube1);
		StartCoroutine (WaitLevelIsAccomplished ());
	}
	
	
	private IEnumerator WaitLevelIsAccomplished () {
		cantSwitchWhenVictoryOrLose = true;
		TutorialLevel.showGuiText = false;
		showVictoryText = true;
		victoryParticlesHolder.SetActive (true);
		yield return new WaitForSeconds (2);
		
		if (victoryBool != true)
		{
			victoryBool = true;
			Time.timeScale = 0;
			hS.SetNewResult ();
		}
		
	}
	
	public void Level3in3by3IsAccomplished () { 
		if (ChangeableVariables.levelWeAreIn == "3x3_3" && threeByThreeThirdLevelEnabled == true)
		{
			ChangeableVariables.levelWeAreIn = "4x4_1";
			ChangeableVariables.SizeIsFour ();
			ChangeableVariables.spawnFrom = 0;
			ChangeableVariables.spawnTo = 5;
			StartFunction.curLevelInt = 1;
			StartFunction.levelGoalsInt = 10000;
			StartFunction.levelGoalsString = string.Empty;
			ChangeableVariables.enabled3by3goals = false;
			ChangeableVariables.enabled4by4goals = true;
			ChangeableVariables.enabled5by5goals = false;
			
			if (PlayerPrefs.GetString ("level4by4_1enabled") == "false" && PlayerPrefs.GetString ("level3by3_FreeEnabled") == "false")
			{
				PlayerPrefs.SetString ("level4by4_1enabled", "true");
				PlayerPrefs.SetString ("level3by3_FreeEnabled", "true");
				PlayerPrefs.Save ();
			}
			LevelIsAccomplished ();
		}
	}
	
	
	public void Level1in3by3IsAccomplished () {
		if (ChangeableVariables.levelWeAreIn == "3x3_1")
		{
			
			ChangeableVariables.levelWeAreIn = "3x3_2";
//			Debug.Log ("ChangeableVariables.levelWeAreIn = " + ChangeableVariables.levelWeAreIn);
			ChangeableVariables.SizeIsThree ();
			ChangeableVariables.spawnFrom = 1;
			ChangeableVariables.spawnTo = 3;
			StartFunction.curLevelInt = 2;
			StartFunction.levelGoalsInt = 5;			
			ChangeableVariables.enabled3by3goals = true;
			ChangeableVariables.enabled4by4goals = false;
			ChangeableVariables.enabled5by5goals = false;
			
			if (PlayerPrefs.GetString ("level3by3_2enabled") == "false")
			{
				PlayerPrefs.SetString ("level3by3_2enabled", "true");
				PlayerPrefs.Save ();
			}
			LevelIsAccomplished ();
			PlayerPrefs.SetString ("FinidhedFirstTutorial", "true");
			PlayerPrefs.Save ();

		}
	}
	
	public void Level2in3by3IsAccomplished () {
		//  fire an event when we clear one floor - 3x3 / second level
//		Debug.Log ("3x3_2");
		if (ChangeableVariables.levelWeAreIn == "3x3_2")
		{
			if (PlayerPrefs.GetString ("level3by3_3enabled") == "false")
			{
				PlayerPrefs.SetString ("level3by3_3enabled", "true");
				PlayerPrefs.Save ();
			}
			LevelIsAccomplished ();
			ChangeableVariables.levelWeAreIn = "3x3_3";
			ChangeableVariables.SizeIsThree ();
			ChangeableVariables.spawnFrom = 1;
			ChangeableVariables.spawnTo = 3;
			StartFunction.curLevelInt = 3;
			StartFunction.levelGoalsString = "Clear 2 at the\nsame time";
			
			ChangeableVariables.enabled3by3goals = true;
			ChangeableVariables.enabled4by4goals = false;
			ChangeableVariables.enabled5by5goals = false;
		}
	}
	
	
	public void Level1in4by4IsAccomplished () {
		//  fire an event when we clear one floor - 3x3 / second level
		if (ChangeableVariables.levelWeAreIn == "4x4_1")
		{
			ChangeableVariables.levelWeAreIn = "4x4_2";
			ChangeableVariables.SizeIsFour ();
			ChangeableVariables.spawnFrom = 0;
			ChangeableVariables.spawnTo = 6;
			StartFunction.curLevelInt = 2;
			StartFunction.levelGoalsInt = 20000;
			ChangeableVariables.enabled3by3goals = false;
			ChangeableVariables.enabled4by4goals = true;
			ChangeableVariables.enabled5by5goals = false;
			
			if (PlayerPrefs.GetString ("level4by4_2enabled") == "false")
			{
				PlayerPrefs.SetString ("level4by4_2enabled", "true");
				PlayerPrefs.Save ();
			}
			LevelIsAccomplished ();
		}
	}
	
	
	public void Level2in4by4IsAccomplished () {
		//  fire an event when we clear one floor - 3x3 / second level
		if (ChangeableVariables.levelWeAreIn == "4x4_2")
		{
			ChangeableVariables.levelWeAreIn = "4x4_3";
			ChangeableVariables.SizeIsFour ();
			ChangeableVariables.spawnFrom = 0;
			ChangeableVariables.spawnTo = 7;
			StartFunction.curLevelInt = 3;
			StartFunction.levelGoalsInt = 30000;
			ChangeableVariables.enabled3by3goals = false;
			ChangeableVariables.enabled4by4goals = true;
			ChangeableVariables.enabled5by5goals = false;
			
			if (PlayerPrefs.GetString ("level4by4_3enabled") == "false")
			{
				PlayerPrefs.SetString ("level4by4_3enabled", "true");
				PlayerPrefs.Save ();
			}
			LevelIsAccomplished ();
		}
	}
	
	
	public void Level3in4by4IsAccomplished () {
		//  fire an event when we clear one floor - 3x3 / second level
		if (ChangeableVariables.levelWeAreIn == "4x4_3")
		{
			ChangeableVariables.levelWeAreIn = "5x5_1";
			ChangeableVariables.SizeIsFive ();
			ChangeableVariables.spawnFrom = 0;
			ChangeableVariables.spawnTo = 9;
			StartFunction.curLevelInt = 1;
			StartFunction.levelGoalsInt = 10000;
			ChangeableVariables.enabled3by3goals = false;
			ChangeableVariables.enabled4by4goals = false;
			ChangeableVariables.enabled5by5goals = true;
			
			if (PlayerPrefs.GetString ("level5by5_1enabled") == "false" && PlayerPrefs.GetString ("level4by4_FreeEnabled") == "false")
			{
				PlayerPrefs.SetString ("level5by5_1enabled", "true");
				PlayerPrefs.SetString ("level4by4_FreeEnabled", "true");
				PlayerPrefs.Save ();
			}
			LevelIsAccomplished ();
		}
	}
	
	
	public void Level1in5by5IsAccomplished () {
		//  fire an event when we clear one floor - 3x3 / second level
		if (ChangeableVariables.levelWeAreIn == "5x5_1")
		{
			ChangeableVariables.levelWeAreIn = "5x5_2";
			ChangeableVariables.SizeIsFive ();
			ChangeableVariables.spawnFrom = 0;
			ChangeableVariables.spawnTo = 10;
			StartFunction.curLevelInt = 2;
			StartFunction.levelGoalsInt = 20000;
			ChangeableVariables.enabled3by3goals = false;
			ChangeableVariables.enabled4by4goals = false;
			ChangeableVariables.enabled5by5goals = true;
			
			if (PlayerPrefs.GetString ("level5by5_2enabled") == "false")
			{
				PlayerPrefs.SetString ("level5by5_2enabled", "true");
				PlayerPrefs.Save ();
			}
		//	Debug.Log ("Called it");
			LevelIsAccomplished ();
		}
	}
	
	
	public void Level2in5by5IsAccomplished () {
		//  fire an event when we clear one floor - 3x3 / second level
		if (ChangeableVariables.levelWeAreIn == "5x5_2")
		{
			ChangeableVariables.levelWeAreIn = "5x5_3";
			ChangeableVariables.SizeIsFive ();
			ChangeableVariables.spawnFrom = 0;
			ChangeableVariables.spawnTo = 11;
			StartFunction.curLevelInt = 3;
			StartFunction.levelGoalsInt = 30000;
			ChangeableVariables.enabled3by3goals = false;
			ChangeableVariables.enabled4by4goals = false;
			ChangeableVariables.enabled5by5goals = true;
			
			if (PlayerPrefs.GetString ("level5by5_3enabled") == "false")
			{
				PlayerPrefs.SetString ("level5by5_3enabled", "true");
				PlayerPrefs.Save ();
			}
			LevelIsAccomplished ();
		}
	}
	
	
	public void Level3in5by5IsAccomplished () {
		//  fire an event when we clear one floor - 3x3 / second level
		if (ChangeableVariables.levelWeAreIn == "5x5_3")
		{
			if (PlayerPrefs.GetString ("level5by5_FreeEnabled") == "false")
			{
				PlayerPrefs.SetString ("level5by5_FreeEnabled", "true");
				PlayerPrefs.Save ();
			}
			LevelIsAccomplished ();
		}
	}
	
	
	public void GameOver () {
		StartCoroutine (WaitGameOver ());
	}
	
	
	private IEnumerator WaitGameOver () {
		cantSwitchWhenVictoryOrLose = true;
		TutorialLevel.showGuiText = false;
		if (GUIMain.soundEffectMuteBool == false)
			tntSoundObj.PlayOneShot (gameOverExplosion);
		showGameOverText = true;
		if(StartFunction.junkyardTheme == false)
			Instantiate (platformPileExplHolder, platformPileExplHolder.transform.position, platformPileExplHolder.transform.rotation);
		else if(StartFunction.junkyardTheme == true)
			Instantiate (crusherExplosionHolder, crusherExplosionHolder.transform.position, crusherExplosionHolder.transform.rotation);
		ege.Endstate ();
		yield return new WaitForSeconds (2);
		if (gameOverBool != true)
		{
			Destroy (SpawnCubes.tempcube1);
			gameOverBool = true;
			Time.timeScale = 0;
			hS.SetNewResult ();
		}
	}
	
	
	
	//  to move the floors down
	IEnumerator CheckToMove () {
		
		if (floor11Count.Count >= 1)
		{
			GameOver ();
		}
		
		if (floor12Count.Count >= 1)
		{
			GameOver ();
		}
		if (floor13Count.Count >= 1)
		{
			GameOver ();
		}
		
		//  if the floor is full
		if (floor1Count.Count >= sizeOfFloor)
		{	
			MovePistons (0);
			DestroyObjectsInLists (floor1Count);
			if (StartFunction.junkyardTheme == true)
				StartCoroutine(PistonHorizontalActive ());
			yield return new WaitForSeconds(0.4f);
			DoStuffAfterDestroyingAFloor (0, 2);
		}
		
		if (floor2Count.Count >= sizeOfFloor)
		{
			MovePistons (1);
			yield return new WaitForSeconds(0.2f);
			DestroyObjectsInLists (floor2Count);
			if (StartFunction.junkyardTheme == true)
				StartCoroutine(PistonHorizontalActive ());
			yield return new WaitForSeconds(0.4f);
			DoStuffAfterDestroyingAFloor (1, 3);
		}
		
		if (floor3Count.Count >= sizeOfFloor)
		{
			MovePistons (2);
			yield return new WaitForSeconds(0.2f);
			DestroyObjectsInLists (floor3Count);
			if (StartFunction.junkyardTheme == true)
				StartCoroutine(PistonHorizontalActive ());
			yield return new WaitForSeconds(0.4f);
			DoStuffAfterDestroyingAFloor (2, 4);
		}
		
		if (floor4Count.Count >= sizeOfFloor)
		{
			MovePistons (3);
			yield return new WaitForSeconds(0.2f);
			DestroyObjectsInLists (floor4Count);
			if (StartFunction.junkyardTheme == true)
				StartCoroutine(PistonHorizontalActive ());
			yield return new WaitForSeconds(0.4f);
			DoStuffAfterDestroyingAFloor (3, 5);
		}
		
		if (floor5Count.Count >= sizeOfFloor)
		{
			MovePistons (4);
			DestroyObjectsInLists (floor5Count);
			yield return new WaitForSeconds(0.2f);
			if (StartFunction.junkyardTheme == true)
				StartCoroutine(PistonHorizontalActive ());
			yield return new WaitForSeconds(0.4f);
			DoStuffAfterDestroyingAFloor (4, 6);
		}
		
		if (floor6Count.Count >= sizeOfFloor)
		{
			MovePistons (5);
			DestroyObjectsInLists (floor6Count);
			yield return new WaitForSeconds(0.2f);
			if (StartFunction.junkyardTheme == true)
				StartCoroutine(PistonHorizontalActive ());
			yield return new WaitForSeconds(0.4f);
			DoStuffAfterDestroyingAFloor (5, 7);
		}
		
		if (floor7Count.Count >= sizeOfFloor)
		{
			MovePistons (6);
			DestroyObjectsInLists (floor7Count);
			yield return new WaitForSeconds(0.2f);
			if (StartFunction.junkyardTheme == true)
				StartCoroutine(PistonHorizontalActive ());
			yield return new WaitForSeconds(0.4f);
			DoStuffAfterDestroyingAFloor (6, 8);
		}
		
		if (floor8Count.Count >= sizeOfFloor)
		{
			MovePistons (7);
			DestroyObjectsInLists (floor8Count);
			yield return new WaitForSeconds(0.2f);
			if (StartFunction.junkyardTheme == true)
				StartCoroutine(PistonHorizontalActive ());
			yield return new WaitForSeconds(0.4f);
			DoStuffAfterDestroyingAFloor (7, 9);
		}
		
		if (floor9Count.Count >= sizeOfFloor)
		{
			MovePistons (8);
			DestroyObjectsInLists (floor9Count);
			yield return new WaitForSeconds(0.2f);
			if (StartFunction.junkyardTheme == true)
				StartCoroutine(PistonHorizontalActive ());
			yield return new WaitForSeconds(0.4f);
			DoStuffAfterDestroyingAFloor (8, 10);
		}
		
		if (floor10Count.Count >= sizeOfFloor)
		{
			MovePistons (9);
			DestroyObjectsInLists (floor10Count);
			yield return new WaitForSeconds(0.2f);
			if (StartFunction.junkyardTheme == true)
				StartCoroutine(PistonHorizontalActive ());
			yield return new WaitForSeconds(0.4f);
			DoStuffAfterDestroyingAFloor (9, 11);
		}
		
		dP.numberOfFloorsWithDetail = CountElementsInLists ();
		dP.DynamicallyChangeCameraAndSpawner ();
		
		//to reset the incrementer when we are done crushing multiple floors at once
		//the function runs through everything in order and when it is done it checks again for floors to clear.
		yPosIncrementer = 0;
	}
	
	
	void CheckTags () {
		//  fill the lists with the details with appropriate tags
		floor1Count = GameObject.FindGameObjectsWithTag ("1").ToList ();
		floor2Count = GameObject.FindGameObjectsWithTag ("2").ToList ();
		floor3Count = GameObject.FindGameObjectsWithTag ("3").ToList ();
		floor4Count = GameObject.FindGameObjectsWithTag ("4").ToList ();
		floor5Count = GameObject.FindGameObjectsWithTag ("5").ToList ();
		floor6Count = GameObject.FindGameObjectsWithTag ("6").ToList ();
		floor7Count = GameObject.FindGameObjectsWithTag ("7").ToList ();
		floor8Count = GameObject.FindGameObjectsWithTag ("8").ToList ();
		floor9Count = GameObject.FindGameObjectsWithTag ("9").ToList ();
		floor10Count = GameObject.FindGameObjectsWithTag ("10").ToList ();
		floor11Count = GameObject.FindGameObjectsWithTag ("11").ToList ();
	}
	
	
	public void CheckFloors () {
		//  if there are some problems - don't use these two functions - just paste code from them here.
		floorsAtTheSameTime = 0;
		CheckTags ();
		StartCoroutine (CheckToMove ());
	}
	
	
	//  Since I didn't want to say to the functions directly which floors should be moved
	//  we need to get a variable having just its name
	void MoveCubesAfterDestroyingTheFloor (int floor) {
		//  just get this variable
		List <GameObject> listToMove = this.GetType().GetField("floor" + floor +"Count").GetValue (this) as List <GameObject>;
		//  if the floor is not empty
		if (listToMove.Count != 0)
		{
			foreach (GameObject go in listToMove)
			{
				//  move a cube down
				if (go != null)
				{
					go.transform.position += Vector3.down;
					//  change its tag respectively
					int prevTag = int.Parse (go.tag);
					int newTag = prevTag - 1;
					go.tag = "" + newTag;
				}
			}
		}
//		//  put it here
//		activeObject.transform.position = new Vector3 (activeObject.transform.position.x,
//		                                               activeObject.transform.position.y - listToMove.Count,
//		                                               activeObject.transform.position.z);
//		activeObject = null;
	}
	
	
	//To move the pistons towards the middle of the platform and out while emitting some smoke particles
	IEnumerator PistonHorizontalActive ()
	{
		if (StartFunction.junkyardTheme == true) {
			pistonRHorizontal.transform.Translate (2.0f, 0, 0);
			pistonLHorizontal.transform.Translate (-2.0f, 0, 0);
			
			CrushingSmokeActive ();
			
			yield return new WaitForSeconds (0.2f);
			
			pistonRHorizontal.transform.Translate (-2.0f, 0, 0);
			pistonLHorizontal.transform.Translate (2.0f, 0, 0);
		}
	}
	
	
	//  to emit the smoke particles
	void CrushingSmokeActive ()
	{
		crushingSmoke1.particleSystem.Play ();
		crushingSmoke2.particleSystem.Play ();
		if (GUIMain.soundEffectMuteBool == false)
			audio.PlayOneShot(crushingSound, 1);
		StartCoroutine (MusicPause ());
		
	}
	
	
	//  Volume on themesong down when cleared floor
	IEnumerator MusicPause ()
	{
		try
		{
			music.audio.volume = 0.0f;
		}
		catch
		{

		}

		yield return new WaitForSeconds(2.5f);

		try
		{
			music.audio.volume = 0.1f;
		}
		catch
		{

		}
	}
	
	
	//  Resizing Script
	public static void AutoResize(int screenWidth, int screenHeight)
	{
		Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
	}
	
	
	public void BlowTheBombUp (float x , float z, string tag) {
		
		List <GameObject> destroySomeDetails = new List<GameObject> ();
		
		//  declare two variables I will use later
		float zPos = 0, xPos = 0;
		
		//  find objects on the same floor where the bomb is
		destroySomeDetails = GameObject.FindGameObjectsWithTag (tag).ToList ();
		
		foreach (GameObject go in destroySomeDetails) 
		{
			//Debug.Log (Math.Round(go.transform.position.z, 1));
			//  to destroy on x axis and z axis
			
			//  if Z positions are the same
			if ((float) Math.Round(go.transform.position.z, 1, MidpointRounding.ToEven) == 
			    (float) Math.Round (z, 1, MidpointRounding.ToEven)
			    && !(go.name.Contains ("1_")))
			{
				zPos = (float) Math.Round (go.transform.position.z, 1, MidpointRounding.ToEven);
				xPos = (float) Math.Round (go.transform.position.x, 1, MidpointRounding.ToEven);
				//  Destroy this object
				Destroy (go);
				//  Move all the objects that are higher with the same X and Z positions as destroyed one used to have
				int tagToStartWith = int.Parse (tag) + 1;
				for (int i = tagToStartWith; i < 11; i ++)
				{
					MoveCubesAfterBlowingUp (i, xPos, zPos);
				}
			}
			
			//  if X positions are the same
			if ( (float) Math.Round(go.transform.position.x, 1, MidpointRounding.ToEven) == 
			    (float) Math.Round (x, 1, MidpointRounding.ToEven)
			    && !(go.name.Contains ("1_")))
			{
				//  the same
				zPos = (float) Math.Round (go.transform.position.z, 1, MidpointRounding.ToEven);
				xPos = (float) Math.Round (go.transform.position.x, 1, MidpointRounding.ToEven);
				Destroy (go);
				int tagToStartWith = int.Parse (tag) + 1;
				for (int i = tagToStartWith; i < 11; i ++)
				{
					MoveCubesAfterBlowingUp (i, xPos, zPos);
				}
			}
			dP.numberOfFloorsWithDetail = CountElementsInLists ();
			dP.DynamicallyChangeCameraAndSpawner ();
		}
	}
	
	
	void MoveCubesAfterBlowingUp (int floor, float x, float z) {
		List <GameObject> listToMove = new List<GameObject> ();
		listToMove = GameObject.FindGameObjectsWithTag ("" + floor).ToList ();
		//  if the floor is not empty
		if (listToMove.Count != 0)
		{
			foreach (GameObject go in listToMove)
			{
				//Debug.Log ("Z = " + (float) Math.Round(go.transform.position.z, 1, MidpointRounding.ToEven) + " ^__^,  " + z);
				//Debug.Log ("X = " + (float) Math.Round(go.transform.position.x, 1, MidpointRounding.ToEven) + " ^__^,  " + x);
				
				if (((float) Math.Round(go.transform.position.z, 1, MidpointRounding.ToEven)) == z
				    && (float) Math.Round(go.transform.position.x, 1, MidpointRounding.ToEven) == x)
				{
					//  move a cube down
					go.transform.position += Vector3.down;
					//  change its tag respectively
					string newTag = (System.Math.Round (go.transform.position.y, MidpointRounding.AwayFromZero)).ToString ();
					
					go.transform.gameObject.tag = newTag;
				}
			}
		}
	}
	
	
	
	public void MeltEverythingDown (float x , float z, int lastFloor) {
		
		//objectsToCheck.Clear ();
		//possibleChildrenToMove.Clear ();
		//indexMelt = 0;
		
		List <GameObject> destroySomeDetails = new List<GameObject> ();
		
		float zPos = (float) Math.Round (z, 1, MidpointRounding.ToEven);
		float xPos = (float) Math.Round (x, 1, MidpointRounding.ToEven);
		
		
		for (int i = 1; i < lastFloor; i ++)
		{
			List <GameObject> theSameXandZpos = GameObject.FindGameObjectsWithTag ("" + i)
				.Select (go => go)
					.Where ( t => (float) Math.Round (t.transform.position.z, 1, MidpointRounding.ToEven) == zPos
					        && (float) Math.Round (t.transform.position.x, 1, MidpointRounding.ToEven) == xPos)
					.ToList ();
			
			if (theSameXandZpos.Count != 0)
			{
				foreach (GameObject go in theSameXandZpos)
				{
					GameObject parent = go.transform.root.gameObject;
					
					
					
					//  we need to know which objects we are gonna move if needed
					//  because when we move some cubes down, we need to know only those cubes
					//  which parent has at least one child deleted because of the barrels
					
					if (! (objectsToCheck.Exists (gO => gO == parent)))
					{
						objectsToCheck.Add (parent);
					}
					Destroy (go);
				}
			}
			
		}
		
		//MoveCubesAfterMeltingDown (lastFloor);
	}
	
	
	public void MoveCubesAfterMeltingDown () {
		
		//  we have to wait a bit because it takes some time to destroy a shape
		StartCoroutine (Meh ());
		//objectsToCheck.Clear ();
	}
	
	
	IEnumerator Meh () {
		yield return new WaitForSeconds (0.1f);
		
		List <GameObject> temp = new List<GameObject> ();
		
		foreach (GameObject go in objectsToCheck)
		{
			//  There can be some shapes that weren't destroyed
			//  since some of the shapes may be destroyed
			//  we need to check it
			if (go != null)
			{
				temp.Add (go);
			}
		}
		
		//  without nulls
		objectsToCheck = temp;
		
		
		for (int i = 0; i < objectsToCheck.Count; i ++)
		{
			GameObject go = objectsToCheck [i];
			GameObject child = go.transform.GetChild (0).gameObject;
			
			foreach (Transform t in child.transform)
			{
				//  everything except for the first floor
				//  add children of objects that were partially destroyed by the barrels
				if (t.gameObject.tag != "1")
				{
					storage [i].Add (t.gameObject);
				}
			}
			
			//  for each of them we want to check if there's something higher
			if (storage [i].Count > 0)
			{
				CheckIfWorks (i, 0, storage [i].Count);
			}
			
		}
		
		
		foreach (List <GameObject> listTemp in storage)
		{
			if (listTemp.Count != 0)
			{
				CheckToMoveDown (listTemp);
			}
		}
	}
	
	
	void CheckIfWorks (int indexOfTemp, int startIndex, int endIndex) {
		
		for (int j = startIndex; j < endIndex; j ++)
		{
			GameObject go = storage [indexOfTemp] [j];
			
			float zP = (float) Math.Round (go.transform.position.z, 1, MidpointRounding.ToEven);
			float xP = (float) Math.Round (go.transform.position.x, 1, MidpointRounding.ToEven);
//			Debug.Log (go.name + ": x = " + xP + ", z =" + zP);
			
			int tag = Convert.ToInt32 (go.tag) + 1;
			
			var check = GameObject.FindGameObjectsWithTag ("" + tag)
				.Select (gameO => gameO)
					.Where (t => 
					        (float) Math.Round (t.transform.position.x, 1, MidpointRounding.ToEven) == xP && 
					        (float) Math.Round (t.transform.position.z, 1, MidpointRounding.ToEven) == zP)
					.FirstOrDefault ();
			
			//  if we found smth that is higher
			if (check != null)
			{
//				Debug.Log ("Found!!! " + check.name + ", xp = " + check.transform.position.z + ", zp = " + check.transform.position.z + ", tag = " + check.tag);
				
				//check.transform.position += new Vector3 (0.5f, 0, 0.5f);
				if (! (objectsToCheck.Exists (gameO => gameO == check.transform.root.gameObject))) 
				{
					//  if it is not an object with the same paren
					if (check.transform.root.gameObject != go.transform.root.gameObject)
					{
//						Debug.Log ("I am here");
						//  get a parent of the found cube
						GameObject parent = check.transform.parent.gameObject;
						
						foreach (Transform t in parent.transform)
						{
							//Debug.Log (storage [indexOfTemp].Exists (gameO => gameO == t.gameObject));
							//  NO DUPLICATES!!!
							//  Because we find all these objects at least three times
							
							if (t.tag != "1")
							{
								if (! (storage [indexOfTemp].Exists (gameO => gameO == t.gameObject)))
								{
//									Debug.Log ("And there!!!");
									storage [indexOfTemp].Add (t.gameObject);
								}
							}
						}
					}
				}
			}
		}
		
		if (endIndex < storage [indexOfTemp].Count)
		{
			CheckIfWorks (indexOfTemp, endIndex, storage [indexOfTemp].Count);
		}
		
	}
	
	
	
	
	
	void CheckToMoveDown (List <GameObject> listToCheck) {
		
		//  we need to move everything by the distance from the closest to the ground cube
		var checkList = listToCheck.OrderBy (pos => pos.transform.position.y).ToList ();
		
		GameObject lowest = checkList [0];
//		Debug.Log ("lowest = " + lowest.name + ", " + lowest.transform.position.y);
		int multiply = 0;
		Ray rayD = new Ray (lowest.transform.position, Vector3.down);
		
		RaycastHit hit;
		if (Physics.Raycast (rayD, out hit , 15, 1 << 8))
		{
			if (hit.distance > 1)
			{
				multiply = (int) Mathf.Floor (hit.distance);
//				Debug.Log ("Multiply = " + multiply);
				
				listToCheck = listToCheck.OrderBy (pos => pos.transform.position.y).ToList ();
				
				for (int i = 0; i < listToCheck.Count; i ++)
				{
					
					Ray raySecond = new Ray (listToCheck [i].transform.position, Vector3.down);
					
					RaycastHit hitSecond;
					if (Physics.Raycast (raySecond, out hitSecond , 15, 1 << 8))
					{
						Vector3 decrease = new Vector3 ();
						if ((int) Mathf.Floor (hitSecond.distance) >= multiply)
						{
							decrease = new Vector3 (0, - multiply, 0);
						}
						else
						{
							decrease = new Vector3 (0, - (int) Mathf.Floor (hitSecond.distance), 0);
						}
//						Debug.Log (decrease);
						listToCheck [i].transform.position += decrease;
					}
				}
			}
		}
		
		// tags
		
		foreach (GameObject go in listToCheck)
		{
			string newTag = (System.Math.Round (go.transform.position.y, MidpointRounding.AwayFromZero)).ToString ();
			
			go.transform.gameObject.tag = newTag;
		}
		
		
		//  check new tags. don't forget
		CheckTags ();
		
	}
	
	
	public void Default () {
		objectsToCheck.Clear ();
		possibleChildrenToMove.Clear ();
		foreach (List <GameObject> listTemp in storage)
		{
			listTemp.Clear ();
		}
		indexMelt = 0;
	}
	
}

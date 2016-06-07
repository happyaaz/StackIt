using UnityEngine;
using System.Collections;

public class GUIMain : MonoBehaviour {
		
	// Toogle on or off the mute toogle
	public bool muteMusicToggle = false;
	public bool muteEffectsToggle = false;
	
	// volume
	float vol = 0.0f;

	//dropped shape pop up score GUI
	private float TimerToShowScore = 0;
	
	//Checking if window is open
	public static bool PauseIsOpen = false;
	public bool ShowControls;
	//Pre level window for goals and introducing new things
	public bool preLevelWOpen = false;
	private string preLevelWGUI = "";

	public static bool floorGoal = false;
	
	//The Window Sizes
	private Rect OptionsWRect = new Rect (110, 140, 500, 1000);
	//public Rect	windowRect1 = new Rect (100, 200, 300, 600);
	private Rect ControlsWRect = new Rect (110, 140, 500, 1000);
	//private Rect PreLevelWRect = new Rect (0,300,720,800);
	private Rect PreLevelWRect = new Rect (75,120,558,1024);
	
	//String names for the "Next shape" feature that get translated into GUI styles
	//Junkyard
	public string One_Shape_TNT_Pref = "One_Shape_TNT_Pref";
	public string Two_Shape_BottleBox_Pref = "Two_Shape_BottleBox_Pref";
	public string Three_Shape_WC_Pref = "Three_Shape_WC_Pref";
	public string I_Shape_CanShark_Pref = "I_Shape_CanShark_Pref";
	public string L_Shape_Sofa_Pref = "L_Shape_Sofa_Pref";
	public string T_Shape_Car_Pref = "T_Shape_Car_Pref";
	public string Z_Shape_Microwave_Pref = "Z_Shape_MicroWave_Pref";
	public string O_Shape_TV_Pref = "O_Shape_TV_Pref";
	public string S_Shape_Barrels_Pref = "S_Shape_Barrels_Pref";
	public string N_Shape_FridgeFreezer_Pref = "N_Shape_FridgeFreezer_Pref";
	public string Y_Shape_Boxes_Pref = "Y_Shape_Boxes_Pref";
	//Modern and Tron
	public string One_Shape_TNTTron_Pref = "One_Shape_TNT_Pref";
	public string Two_Shape_BottleBoxTron_Pref = "Two_Shape_BottleBox_Pref";
	public string Three_Shape_WCTron_Pref = "Three_Shape_WC_Pref";
	public string I_Shape_CanSharkTron_Pref = "I_Shape_CanShark_Pref";
	public string L_Shape_SofaTron_Pref = "L_Shape_Sofa_Pref";
	public string T_Shape_CarTron_Pref = "T_Shape_Car_Pref";
	public string Z_Shape_MicrowaveTron_Pref = "Z_Shape_MicroWave_Pref";
	public string O_Shape_TVTron_Pref = "O_Shape_TV_Pref";
	public string S_Shape_BarrelsTron_Pref = "S_Shape_Barrels_Pref";
	public string N_Shape_FridgeFreezerTron_Pref = "N_Shape_FridgeFreezer_Pref";
	public string Y_Shape_BoxesTron_Pref = "Y_Shape_Boxes_Pref";
	//Original
	public string One_Shape_TNTOrgTet_Pref = "One_Shape_TNT_Pref";
	public string Two_Shape_BottleBoxOrgTet_Pref = "Two_Shape_BottleBox_Pref";
	public string Three_Shape_WCOrgTet_Pref = "Three_Shape_WC_Pref";
	public string I_Shape_CanSharkOrgTet_Pref = "I_Shape_CanShark_Pref";
	public string L_Shape_SofaOrgTet_Pref = "L_Shape_Sofa_Pref";
	public string T_Shape_CarOrgTet_Pref = "T_Shape_Car_Pref";
	public string Z_Shape_MicrowaveOrgTet_Pref = "Z_Shape_MicroWave_Pref";
	public string O_Shape_TVOrgTet_Pref = "O_Shape_TV_Pref";
	public string S_Shape_BarrelsOrgTet_Pref = "S_Shape_Barrels_Pref";
	public string N_Shape_FridgeFreezerOrgTet_Pref = "N_Shape_FridgeFreezer_Pref";
	public string Y_Shape_BoxesOrgTet_Pref = "Y_Shape_Boxes_Pref";
	
	//variable used to connect name of Shape to name of Style
	private string NextShape = "";
	
	//The Skin we are using
	public GUISkin junkyard_GUISkin;

	ScoreController sc;
	SpawnCubes spc;
	HighScore hsc;
	LineScore ls;
	public bool Dropping = false;
	
	//Sounds
	public AudioClip Clicking;
	private GameObject themeSongObj;
	public static bool gameOverSongMuteBool = false;
	public static bool victorySongMuteBool = false;
	public static bool soundEffectMuteBool = false;
	
	//timerbar
	public float timerBarLength;
	public float maxTime;

	//Score default color variable
	private Color scoreDefaultColor;

	//Controll-picture
	public Texture2D ControllerTex;
	
	void Awake()
	{
		preLevelWOpen = true;
	}
	
	void Start() {
		//vol = 0.0f; // volume from start

		timerBarLength = 170;
		
		maxTime = 20;
		if (ChangeableVariables.levelWeAreIn == "3x3_1" || ChangeableVariables.levelWeAreIn == "3x3_2" || ChangeableVariables.levelWeAreIn == "3x3_3") 
		{
			maxTime = 30;
		} else if (ChangeableVariables.levelWeAreIn == "4x4_1" || ChangeableVariables.levelWeAreIn == "4x4_2" || ChangeableVariables.levelWeAreIn == "4x4_3") 
		{
			maxTime = 20;
		}else if (ChangeableVariables.levelWeAreIn == "5x5_1" || ChangeableVariables.levelWeAreIn == "5x5_2" || ChangeableVariables.levelWeAreIn == "5x5_3") 
		{
			maxTime = 20;
		}else if (ChangeableVariables.levelWeAreIn == "3x3_Free" || ChangeableVariables.levelWeAreIn == "4x4_Free" || ChangeableVariables.levelWeAreIn == "5x5_Free") 
		{
			maxTime = 20;
		}
						
		scoreDefaultColor = StartFunction.currentTheme_GUISkin.FindStyle ("ScoreLabel").normal.textColor;

		sc = GameObject.Find ("Platform").GetComponent<ScoreController> ();
		spc = GameObject.FindGameObjectWithTag ("Spawner").GetComponent<SpawnCubes> ();
		hsc = GameObject.Find ("HighScore").GetComponent<HighScore> ();
		ls = GameObject.Find ("Platform").GetComponent<LineScore> ();
		themeSongObj = GameObject.Find ("Themesong");
		DisplayImage ();
	}

	// Use this for initialization
	void OnGUI () {
		
		AutoResize (720, 1280);
		GUI.skin = StartFunction.currentTheme_GUISkin;
		
		if (preLevelWOpen == true)
		{
			//A custom style for each Pre Level Window
			if(ChangeableVariables.levelWeAreIn == "3x3_1")
			{
				preLevelWGUI = "PreLvLW_3x3_1";
			}else if(ChangeableVariables.levelWeAreIn == "3x3_2")
			{
				preLevelWGUI = "PreLvLW_3x3_2";
			}else if(ChangeableVariables.levelWeAreIn == "3x3_3")
			{
				preLevelWGUI = "PreLvLW_3x3_3";
			}else if(ChangeableVariables.levelWeAreIn == "4x4_1")
			{
				preLevelWGUI = "PreLvLW_4x4_1";
			}else if(ChangeableVariables.levelWeAreIn == "4x4_2")
			{
				preLevelWGUI = "PreLvLW_4x4_2";
			}else if(ChangeableVariables.levelWeAreIn == "4x4_3")
			{
				preLevelWGUI = "PreLvLW_4x4_3";
			}else if(ChangeableVariables.levelWeAreIn == "5x5_1")
			{
				preLevelWGUI = "PreLvLW_5x5_1";
			}else if(ChangeableVariables.levelWeAreIn == "5x5_2")
			{
				preLevelWGUI = "PreLvLW_5x5_2";
			}else if(ChangeableVariables.levelWeAreIn == "5x5_3")
			{
				preLevelWGUI = "PreLvLW_5x5_3";
			}else if(ChangeableVariables.levelWeAreIn == "3x3_Free")
			{
				preLevelWGUI = "PreLvLW_3x3_Free";
			}else if(ChangeableVariables.levelWeAreIn == "4x4_Free")
			{
				preLevelWGUI = "PreLvLW_4x4_Free";
			}else if(ChangeableVariables.levelWeAreIn == "5x5_Free")
			{
				preLevelWGUI = "PreLvLW_5x5_Free";
			}

			//PreLevelWRect = GUI.Window (0, PreLevelWRect, PreLevelW, "", "OptionsMenuWindow");
			PreLevelWRect = GUI.Window (0, PreLevelWRect, PreLevelW, "", preLevelWGUI);
			Time.timeScale = 0;
		}
		
		//free play	goals
		//3x3
		if (ChangeableVariables.enabled3by3goals == true && StartFunction.freePlayGUIBool == true && ls.showGoalsGUI == true) 
		{
			GUI.Label (new Rect (10, 210, 350, 75), sc.Score.ToString ("0"), "ScoreLabel");
			GUI.Label (new Rect (10, 170, 350, 75), "Floors: "+ sc.FloorsCleared.ToString(), "ScoreLabel");
		}
		//4x4
		if (StartFunction.freePlayGUIBool == true && ChangeableVariables.enabled4by4goals == true && ls.showGoalsGUI == true) 
		{
			GUI.Label (new Rect (10, 210, 350, 75), sc.Score.ToString ("0"), "ScoreLabel");
			GUI.Label (new Rect (10, 170, 350, 75), "Floors: "+ sc.FloorsCleared.ToString(), "ScoreLabel");
		}
		//5x5
		if (StartFunction.freePlayGUIBool == true && ChangeableVariables.enabled5by5goals == true && ls.showGoalsGUI == true) 
		{
			GUI.Label (new Rect (10, 210, 350, 75), sc.Score.ToString ("0"), "ScoreLabel");
			GUI.Label (new Rect (10, 170, 350, 75), "Floors: "+ sc.FloorsCleared.ToString(), "ScoreLabel");
		}
		
		//normal levels goals
		//3x3
		if (ChangeableVariables.enabled3by3goals == true && StartFunction.freePlayGUIBool == false 
		    && ChangeableVariables.levelWeAreIn != "3x3_3" && ls.showGoalsGUI == true) 
		{
			GUI.Label (new Rect (10, 170, 350, 75), "Floors: "+ sc.FloorsCleared.ToString() + "/" + StartFunction.levelGoalsInt, "ScoreLabel");
		}
		//3x3 - 3
		if (StartFunction.freePlayGUIBool == false && ChangeableVariables.enabled3by3goals == true 
		    && ChangeableVariables.levelWeAreIn == "3x3_3" && ls.showGoalsGUI == true) 
		{
			GUI.Label (new Rect (10, 170, 850, 375), StartFunction.levelGoalsString, "ScoreLabel");
		}
		//4x4
		if (ChangeableVariables.enabled4by4goals == true && StartFunction.freePlayGUIBool == false 
		    && ls.showGoalsGUI == true) 
		{
			GUI.Label (new Rect (10, 170, 350, 75), sc.Score.ToString ("0") + "/" + StartFunction.levelGoalsInt, "ScoreLabel");
		}
		//5x5
		if (ChangeableVariables.enabled5by5goals == true && StartFunction.freePlayGUIBool == false 
		    && ls.showGoalsGUI == true) 
		{
			GUI.Label (new Rect (10, 170, 350, 75), sc.Score.ToString ("0") + "/" + StartFunction.levelGoalsInt, "ScoreLabel");
		}		
		
		//timerBar
//		GUI.Label (new Rect (10, 10, 150, 70), "Timer", "InGameLabel");
		GUI.Box (new Rect (10, 50, 296, 125), "", "TimerBox");
		GUI.Box (new Rect (110 + (timerBarLength * SpawnCubes.timer / maxTime), 73, 30, 80), "", "TimerIndicatorBox");
		GUI.Label (new Rect (32, 80, 80, 70), "" + SpawnCubes.timer, "CountdownLabel");	
				
		//next shape background and switch button
		GUI.Box (new Rect (430, 0, 300, 300), "", "NextShapeBox");
		if (GUI.Button (new Rect (475, 50, 200, 200), "", NextShape) && ls.cantSwitchWhenVictoryOrLose == false 
		    && preLevelWOpen == false && !Dropping && PlayerPrefs.GetString ("level3by3_2enabled") == "true")
		{
			
			TutorialLevel.showGuiText = false;
			spc.SwitchShapes();
		}

		//biohazard barrels button
		/*
		if (spc.barrelsAvailable == true)
		{
			if (GUI.Button (new Rect (560, 1120, 150, 150), "Barrels", "OptionsButton"))
			{
				spc.SwitchToBarrels ();
			}
		}
		*/
		//options menu cog
		if (GUI.Button(new Rect (10, 1120, 150, 150),"", "OptionsButton"))
		{
			if (soundEffectMuteBool == false)
				audio.PlayOneShot (Clicking,100);
			PauseIsOpen = !PauseIsOpen;
			if (PauseIsOpen == true)
				Time.timeScale = 0;
			else if (PauseIsOpen == false)
				Time.timeScale = 1;
		}

		if (sc.ShowScore && !ChangeableVariables.levelWeAreIn.Contains ("3x3")) 
		{
			TimerToShowScore += Time.deltaTime;
			StartFunction.currentTheme_GUISkin.FindStyle ("ScoreLabel").normal.textColor = Color.black;
			GUI.Label (new Rect (235, 800, 250,130),"+"+sc.ScoreToAdd.ToString ("0"), "ScorePupupLabel");
			if (TimerToShowScore >= 0.25)
				StartFunction.currentTheme_GUISkin.FindStyle ("ScoreLabel").normal.textColor = scoreDefaultColor;
			if (TimerToShowScore >= 0.5)
				StartFunction.currentTheme_GUISkin.FindStyle ("ScoreLabel").normal.textColor = Color.green;
			if (TimerToShowScore >= 0.75)
				StartFunction.currentTheme_GUISkin.FindStyle ("ScoreLabel").normal.textColor = scoreDefaultColor;
			if (TimerToShowScore >= 2)
			{
				sc.ShowScore = false;
				TimerToShowScore = 0;
			}
		}
		else if (sc.ShowScore && ChangeableVariables.levelWeAreIn.Contains ("_Free"))
		{
			TimerToShowScore += Time.deltaTime;
			StartFunction.currentTheme_GUISkin.FindStyle ("ScoreLabel").normal.textColor = Color.black;
			GUI.Label (new Rect (235, 800, 250,130),"+"+sc.ScoreToAdd.ToString ("0"), "ScorePupupLabel");
			if (TimerToShowScore >= 0.25)
				StartFunction.currentTheme_GUISkin.FindStyle ("ScoreLabel").normal.textColor = scoreDefaultColor;
			if (TimerToShowScore >= 0.5)
				StartFunction.currentTheme_GUISkin.FindStyle ("ScoreLabel").normal.textColor = Color.green;
			if (TimerToShowScore >= 0.75)
				StartFunction.currentTheme_GUISkin.FindStyle ("ScoreLabel").normal.textColor = scoreDefaultColor;
			if (TimerToShowScore >= 2)
			{
				sc.ShowScore = false;
				TimerToShowScore = 0;
			}
		}


		if (PauseIsOpen == true)
		{
			OptionsWRect = GUI.Window (0, OptionsWRect, Options, "", "OptionsMenuWindow");
		}
		
		if (ShowControls ==true)
		{
			ControlsWRect = GUI.Window (2, ControlsWRect, Controls, "", "OptionsMenuWindow");
		}
		
		if (hsc.isPossibleToDisplayResults == true)
		{
			//  displaying highscore
			GUI.Label (new Rect (225, 340, 370, 600), hsc.displayNames, "GameOVictHscLabel");
			GUI.Label (new Rect (405, 340, 170, 800), hsc.displayScore, "GameOVictHscLabel");
		}
		
		
	}
	
	void PreLevelW (int windowID0)
	{
		if (GUI.Button (new Rect (98, 850, 370, 100), "", "OptionsMenuButton"))
		{
			preLevelWOpen = false;
			Time.timeScale = 1;
			ls.cantSwitchWhenVictoryOrLose = false;
		}
		GUI.Box (new Rect (200, 860, 182, 80), "", "PlayLabel");
	}
	
	
	//  display an image of a new shape if possible
	public void DisplayImage () {
		try 
		{
			NextShape = this.GetType().GetField(spc.nameOfNextShape).GetValue (this) as string;
		}
		catch
		{
			
		}
	}
	
	
	void Options (int windowID0) 
	{	
		if (GUI.Button (new Rect (75, 80, 350, 100), "NEW GAME", "OptionsMenuButton"))
		{
			PauseIsOpen = false; 
			if (soundEffectMuteBool == false)
				audio.PlayOneShot (Clicking, 100);
			Time.timeScale = 1;
			Application.LoadLevel (1);
			
		}
		
		else if (GUI.Button (new Rect (75, 223, 350, 100), "MAIN MENU", "OptionsMenuButton"))
		{
			if (soundEffectMuteBool == false)
				audio.PlayOneShot (Clicking, 100);
			PauseIsOpen = false;
			Time.timeScale = 1;
			ChangeableVariables.enabled5by5goals = false;
			ChangeableVariables.enabled4by4goals = false;
			ChangeableVariables.enabled3by3goals = false;
			StartFunction.freePlayGUIBool = false;
			StartFunction.openChoosingLevels = false;
			StartFunction.openStartMenu = true;
			StartFunction.camPosWLogoBool = true;
			Application.LoadLevel (0);
		}
		
		else if (GUI.Button (new Rect (75, 366, 350, 100), "LEVELS", "OptionsMenuButton"))
		{
			if (soundEffectMuteBool == false)
				audio.PlayOneShot (Clicking, 100);
			PauseIsOpen = false;
			Time.timeScale = 1;
			ChangeableVariables.enabled5by5goals = false;
			ChangeableVariables.enabled4by4goals = false;
			ChangeableVariables.enabled3by3goals = false;
			StartFunction.freePlayGUIBool = false;
			StartFunction.openChoosingLevels = true;
			StartFunction.camPosWLogoBool = false;
			Application.LoadLevel (0);
			
		}
		
		else if (GUI.Button (new Rect (75, 510, 350, 100), "CONTROLS", "OptionsMenuButton"))
		{
			ShowControls = true;
			if (soundEffectMuteBool == false)
				audio.PlayOneShot (Clicking, 100);	
		}
		//button to mute the music
		else if (GUI.Button (new Rect (45, 730, 200, 200), "", Background_Sound.muteMusicButton))
		{
			Background_Sound.muteMusicButtonBool = !Background_Sound.muteMusicButtonBool;
			Background_Sound.muteMusicBool = !Background_Sound.muteMusicBool;
			if(Background_Sound.muteMusicButtonBool == false)
				Background_Sound.muteMusicButton = "MuteMusicOffButton";
			else if(Background_Sound.muteMusicButtonBool == true)
				Background_Sound.muteMusicButton = "MuteMusicOnButton";
			themeSongObj.audio.enabled = !themeSongObj.audio.enabled;
			gameOverSongMuteBool = !gameOverSongMuteBool;
			victorySongMuteBool = !victorySongMuteBool;
			if (soundEffectMuteBool == false)
				audio.PlayOneShot (Clicking, 100);
		}
		//button to mute the sound effects
		else if (GUI.Button (new Rect (245, 730, 200, 200), "", Background_Sound.muteEffectsButton))
		{
			Background_Sound.muteEffectsButtonBool = !Background_Sound.muteEffectsButtonBool;
			if (soundEffectMuteBool == false)
				audio.PlayOneShot (Clicking, 100);
			soundEffectMuteBool = !soundEffectMuteBool;
			if(Background_Sound.muteEffectsButtonBool == false)
				Background_Sound.muteEffectsButton = "MuteEffectsOffButton";
			else if(Background_Sound.muteEffectsButtonBool == true)
				Background_Sound.muteEffectsButton = "MuteEffectsOnButton";
		}
		
		GUI.Label (new Rect (180, 680, 300, 150), "MUTE:", "InGameLabel");
	}
	
	void Controls (int windowID1) {
		if(TutorialLevel.mobileTutorial == true)
			GUI.Label(new Rect(50, -10, 400, 900), "","ControllerPic");
		//	GUI.Label(new Rect (100, 100, 440,600),"Controls:\nMove Object: Drag Shape\nRotate Object: Swipe\nRotate Camera: Tilt the Phone\nDrop Object Down: Double Tap", "InGameLabel");
		else if(TutorialLevel.mobileTutorial == false)
			GUI.Label(new Rect(50, -10, 400, 900), "","ControllerPic");
		//	GUI.Label(new Rect (100, 100, 440,600),"Controls:\nMove Object: Arrows\nRotate Object: A & D, W & S\nRotate Camera: Q & R\nDrop Object Down: Space", "InGameLabel");
		
		if (GUI.Button (new Rect (75, 850, 350, 100), "BACK", "OptionsMenuButton"))
		{
			if (soundEffectMuteBool == false)
				audio.PlayOneShot (Clicking, 100);
			ShowControls = false;
		}
	}
	
	
	
	
	
	//  Resizing Script
	public static void AutoResize(int screenWidth, int screenHeight)
	{
		Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
	}
	
}

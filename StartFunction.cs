using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Collections;

public class StartFunction : MonoBehaviour {


	//Loading texture
	private bool loadingImageOn = false;

	//sound
	private GameObject themeSongObj;


	public string gameVersionNr;

	private Vector3 camPosMenus;
	public static bool camPosWLogoBool = true;
	private Vector3 camPosWLogo = new Vector3 (0, 0, 0);
	private Vector3 camPosNoLogo = new Vector3 (5, 0, 0);

	private bool displayHighScore = false;
	public string score3, score4, score5 = string.Empty;
	private List <int> highScoreList = new List <int> ();

	public Rect HighScoreWRect = new Rect (0, 0, 720, 1280);

	//Theme selection preview
	private string themeSelectStyle = "";

	//Theme GUI Skins
	public GUISkin junkyard_GUISkin;
	public GUISkin modern_GUISkin;
	public GUISkin tronBlack_GUISkin;
	public GUISkin tronGrid_GUISkin;
	public GUISkin light_GUISkin;
	public GUISkin OriginalTetris_GUISkin;
	//Variable to use when we change the GUI Skins for each theme
	public static GUISkin currentTheme_GUISkin;

	//Toggles for themes
	private bool junkyardThemeToggle = false;
	private bool originalThemeToggle = false;
	private bool modernThemeToggle = true;
	private bool lightToggle = false;
	private bool tronBlackToggle = false;
	private bool tronGridToggle = false;

	//the goals of each level
	public static int levelGoalsInt;

	//levels GUI
	public static string levelGoalsString;
	public static bool freePlayGUIBool = false;

	//The current level string for use in the Pre level window
	public static int curLevelInt;

	public bool EasyLevels;
	public bool MediumLevels;
	public bool HardLevels;
	public static bool FreePlay;

	//opening menus
	public static bool openStartMenu = true;
	public static bool openChoosingLevels = false;
	public bool openChoosingThemes = false;

	//activating themes
	public static bool junkyardTheme = false;
	public static bool originalTheme = false;
	public static bool modernTheme = true;
	public static bool lightTheme = false;
	public static bool tronBlackTheme = false;
	public static bool tronGridTheme = false;
	private bool settingsWindow = false;
	public bool creditsWindow = false;
	private bool resetPrompt = false;

	//button clicking sound
	public AudioClip Clicking;

	private Vector3 threeByThreeFinalDest = new Vector3 (60, 100);
	private Vector3 fourByFourFinalDest = new Vector3 (60, 300);
	private Vector3 fiveByFiveFinalDest = new Vector3 (60, 500);
	private Vector3 freePlayFinalDest = new Vector3 (60, 700);

	private Vector3 threeByThreeStartDest = new Vector3 (295, 100);
	private Vector3 fourByFourStartDest = new Vector3 (295, 300);
	private Vector3 fiveByFiveStartDest = new Vector3 (295, 500);
	private Vector3 freePlayStartDest = new Vector3 (295, 700);

	private Vector3 threeByThreeStandardDest = new Vector3 (295, 100);
	private Vector3 fourByFourStandardDest = new Vector3 (295, 300);
	private Vector3 fiveByFiveStandardDest = new Vector3 (295, 500);
	private Vector3 freePlayStandardDest = new Vector3 (295, 700);

	private bool guiIsMovingThree = false;
	private bool guiIsMovingFour = false;
	private bool guiIsMovingFive = false;
	private bool guiIsMovingfFreePlay = false;

	private float distance = 230;

	//  smooth appearence of 3x3 sublevels
	//  smooth animation, only for two buttons
	private float thirdButtonSpeed = 500, secondButtonSpeed = 700;
	//  when to appear
	private float subLevels3appearFl = 217;
	private float subLevels2appearFl = 139;
	private float subLevels1appearFl = 61;
	//  three be three

	private Vector2 threeByThreeSubLevels2appearFinalPos = new Vector2 (360, 100);
	private Vector2 threeByThreeSubLevels3appearFinalPos = new Vector2 (510, 100);

	private Vector2 threeByThreeSubLevels2appearStartPos = new Vector2 (139, 100);
	private Vector2 threeByThreeSubLevels3appearStartPos = new Vector2 (217, 100);

	private Vector2 threeByThreeSubLevels2appearStandardPos = new Vector2 (139, 100);
	private Vector2 threeByThreeSubLevels3appearStandardPos = new Vector2 (217, 100);

	//  indicate that they can appear
	private bool threeByThreeSubLevels3appearB = false;
	private bool threeByThreeSubLevels2appearB = false;
	private bool threeByThreeSubLevels1appearB = false;

	//  four by four
	private Vector2 fourByFourSubLevels2appearFinalPos = new Vector2 (360, 300);
	private Vector2 fourByFourSubLevels3appearFinalPos = new Vector2 (510, 300);
	
	private Vector2 fourByFourSubLevels2appearStartPos = new Vector2 (139, 300);
	private Vector2 fourByFourSubLevels3appearStartPos = new Vector2 (217, 300);
	
	private Vector2 fourByFourSubLevels2appearStandardPos = new Vector2 (139, 300);
	private Vector2 fourByFourSubLevels3appearStandardPos = new Vector2 (217, 300);

	//  indicate that they can appear
	private bool fourByFourSubLevels3appearB = false;
	private bool fourByFourSubLevels2appearB = false;
	private bool fourByFourSubLevels1appearB = false;

	//  five by five
	private Vector2 fiveByFiveSubLevels2appearFinalPos = new Vector2 (360, 500);
	private Vector2 fiveByFiveSubLevels3appearFinalPos = new Vector2 (510, 500);
	
	private Vector2 fiveByFiveSubLevels2appearStartPos = new Vector2 (139, 500);
	private Vector2 fiveByFiveSubLevels3appearStartPos = new Vector2 (217, 500);
	
	private Vector2 fiveByFiveSubLevels2appearStandardPos = new Vector2 (139, 500);
	private Vector2 fiveByFiveSubLevels3appearStandardPos = new Vector2 (217, 500);
	
	//  indicate that they can appear
	private bool fiveByFiveSubLevels3appearB = false;
	private bool fiveByFiveSubLevels2appearB = false;
	private bool fiveByFiveSubLevels1appearB = false;



	//  free play
	private Vector2 freePlaySubLevels2appearFinalPos = new Vector2 (360, 700);
	private Vector2 freePlaySubLevels3appearFinalPos = new Vector2 (510, 700);
	
	private Vector2 freePlaySubLevels2appearStartPos = new Vector2 (139, 700);
	private Vector2 freePlaySubLevels3appearStartPos = new Vector2 (217, 700);
	
	private Vector2 freePlaySubLevels2appearStandardPos = new Vector2 (139, 700);
	private Vector2 freePlaySubLevels3appearStandardPos = new Vector2 (217, 700);
	
	//  indicate that they can appear
	public static bool freePlaySubLevels3appearB = false;
	private bool freePlaySubLevels2appearB = false;
	private bool freePlaySubLevels1appearB = false;
	public Texture2D HighSc;



	void Start () {

	
		themeSongObj = GameObject.Find ("Themesong");

		Time.timeScale = 1;

		//These are the starting unlockables
		if (PlayerPrefs.GetString ("level3by3_2enabled") == "false" || !PlayerPrefs.HasKey ("level3by3_2enabled"))
		{
			PlayerPrefs.SetString ("level3by3_1enabled", "true");
			PlayerPrefs.SetString ("FinidhedFirstTutorial", "false");
			PlayerPrefs.SetString ("CameraMovementUnlocked", "false");
			//  change them to "false" when needed
			PlayerPrefs.SetString ("level3by3_2enabled", "false");
			PlayerPrefs.SetString ("level3by3_3enabled", "false");
			
			PlayerPrefs.SetString ("level4by4_1enabled", "false");
			PlayerPrefs.SetString ("level4by4_2enabled", "false");
			PlayerPrefs.SetString ("level4by4_3enabled", "false");
			
			PlayerPrefs.SetString ("level5by5_1enabled", "false");
			PlayerPrefs.SetString ("level5by5_2enabled", "false");
			PlayerPrefs.SetString ("level5by5_3enabled", "false");
			
			PlayerPrefs.SetString ("level3by3_FreeEnabled", "false");
			PlayerPrefs.SetString ("level4by4_FreeEnabled", "false");
			PlayerPrefs.SetString ("level5by5_FreeEnabled", "false");
			
			PlayerPrefs.Save ();
		}
		modernTheme = true;
		junkyardTheme = false;
		originalTheme = false;
		lightTheme = false;
		tronBlackTheme = false;
		tronGridTheme = false;
		//all other toggles to false
		junkyardThemeToggle = false;
		originalThemeToggle = false;
		lightToggle = false;
		tronBlackToggle = false;
		tronGridToggle = false;
		
		currentTheme_GUISkin = modern_GUISkin;
		themeSelectStyle = "ThemeSelectModern";

		transform.position = camPosWLogo;

		if (camPosWLogoBool == true)
		{
			transform.position = camPosWLogo;
		} else if (camPosWLogoBool == false)
			transform.position = camPosNoLogo;

	}

	void Update () {
		//  to the start position
		if (guiIsMovingThree == true)
		{
			if (Vector2.Distance (threeByThreeStandardDest, threeByThreeFinalDest) != 0)
			{
				threeByThreeStandardDest = Vector2.MoveTowards (threeByThreeStandardDest, threeByThreeFinalDest, Time.deltaTime * distance);

				//  move sublevels
				//  detect if we can spawn them
			}	
			if (threeByThreeStandardDest.x < subLevels3appearFl && threeByThreeSubLevels3appearB == false)
				{
					threeByThreeSubLevels3appearB = true;
				}
				else if (threeByThreeStandardDest.x < subLevels2appearFl && threeByThreeSubLevels2appearB == false)
				{
					threeByThreeSubLevels2appearB = true;
				}
				else if (threeByThreeStandardDest.x < subLevels1appearFl && threeByThreeSubLevels1appearB == false)
				{
					threeByThreeSubLevels1appearB = true;
				}

				//  actual spawning of sublevels
				if (threeByThreeSubLevels3appearB == true && 
				    Vector2.Distance (threeByThreeSubLevels3appearStandardPos, threeByThreeSubLevels3appearFinalPos) != 0)
				{
					threeByThreeSubLevels3appearStandardPos = Vector2.MoveTowards (threeByThreeSubLevels3appearStandardPos,
					                                                               threeByThreeSubLevels3appearFinalPos,
					                                                               Time.deltaTime * thirdButtonSpeed);
				}

				if (threeByThreeSubLevels2appearB == true && 
				    Vector2.Distance (threeByThreeSubLevels2appearStandardPos, threeByThreeSubLevels2appearFinalPos) != 0)
				{
					threeByThreeSubLevels2appearStandardPos = Vector2.MoveTowards (threeByThreeSubLevels2appearStandardPos,
					                                                               threeByThreeSubLevels2appearFinalPos,
					                                                               Time.deltaTime * secondButtonSpeed);
				}


			
		}
		else if (guiIsMovingFour == true)
		{
			if (Vector2.Distance (fourByFourStandardDest, fourByFourFinalDest) != 0)
			{
				fourByFourStandardDest = Vector2.MoveTowards (fourByFourStandardDest, fourByFourFinalDest, Time.deltaTime * distance);
			}

			//  move sublevels
			//  detect if we can spawn them
			if (fourByFourStandardDest.x < subLevels3appearFl && fourByFourSubLevels3appearB == false)
			{
				fourByFourSubLevels3appearB = true;
			}
			else if (fourByFourStandardDest.x < subLevels2appearFl && fourByFourSubLevels2appearB == false)
			{
				fourByFourSubLevels2appearB = true;
			}
			else if (fourByFourStandardDest.x < subLevels1appearFl && fourByFourSubLevels1appearB == false)
			{
				fourByFourSubLevels1appearB = true;
			}
			
			//  actual spawning of sublevels

			if (fourByFourSubLevels3appearB == true && 
			    Vector2.Distance (fourByFourSubLevels3appearStandardPos, fourByFourSubLevels3appearFinalPos) != 0)
			{
				fourByFourSubLevels3appearStandardPos = Vector2.MoveTowards (fourByFourSubLevels3appearStandardPos,
				                                                             fourByFourSubLevels3appearFinalPos,
				                                                               Time.deltaTime * thirdButtonSpeed);
			}
			
			if (fourByFourSubLevels2appearB == true && 
			    Vector2.Distance (fourByFourSubLevels2appearStandardPos, fourByFourSubLevels2appearFinalPos) != 0)
			{
				fourByFourSubLevels2appearStandardPos = Vector2.MoveTowards (fourByFourSubLevels2appearStandardPos,
				                                                             fourByFourSubLevels2appearFinalPos,
				                                                               Time.deltaTime * secondButtonSpeed);
			}


		}
		else if (guiIsMovingFive == true)
		{
			if (Vector2.Distance (fiveByFiveStandardDest, fiveByFiveFinalDest) != 0)
			{
				fiveByFiveStandardDest = Vector2.MoveTowards (fiveByFiveStandardDest, fiveByFiveFinalDest, Time.deltaTime * distance);
			}

			//  move sublevels
			//  detect if we can spawn them
			if (fiveByFiveStandardDest.x < subLevels3appearFl && fiveByFiveSubLevels3appearB == false)
			{
				fiveByFiveSubLevels3appearB = true;
			}
			else if (fiveByFiveStandardDest.x < subLevels2appearFl && fiveByFiveSubLevels2appearB == false)
			{
				fiveByFiveSubLevels2appearB = true;
			}
			else if (fiveByFiveStandardDest.x < subLevels1appearFl && fiveByFiveSubLevels1appearB == false)
			{
				fiveByFiveSubLevels1appearB = true;
			}
			
			//  actual spawning of sublevels
			
			if (fiveByFiveSubLevels3appearB == true && 
			    Vector2.Distance (fiveByFiveSubLevels3appearStandardPos, fiveByFiveSubLevels3appearFinalPos) != 0)
			{
				fiveByFiveSubLevels3appearStandardPos = Vector2.MoveTowards (fiveByFiveSubLevels3appearStandardPos,
				                                                             fiveByFiveSubLevels3appearFinalPos,
				                                                             Time.deltaTime * thirdButtonSpeed);
			}
			
			if (fiveByFiveSubLevels2appearB == true && 
			    Vector2.Distance (fiveByFiveSubLevels2appearStandardPos, fiveByFiveSubLevels2appearFinalPos) != 0)
			{
				fiveByFiveSubLevels2appearStandardPos = Vector2.MoveTowards (fiveByFiveSubLevels2appearStandardPos,
				                                                             fiveByFiveSubLevels2appearFinalPos,
				                                                             Time.deltaTime * secondButtonSpeed);
			}

		}
		else if (guiIsMovingfFreePlay == true)
		{
			if (Vector2.Distance (freePlayStandardDest, freePlayFinalDest) != 0)
			{
				freePlayStandardDest = Vector2.MoveTowards (freePlayStandardDest, freePlayFinalDest, Time.deltaTime * distance);
			}
			
			//  move sublevels
			//  detect if we can spawn them
			if (freePlayStandardDest.x < subLevels3appearFl && freePlaySubLevels3appearB == false)
			{
				freePlaySubLevels3appearB = true;
			}
			else if (freePlayStandardDest.x < subLevels2appearFl && freePlaySubLevels2appearB == false)
			{
				freePlaySubLevels2appearB = true;
			}
			else if (freePlayStandardDest.x < subLevels1appearFl && freePlaySubLevels1appearB == false)
			{
				freePlaySubLevels1appearB = true;
			}
			
			//  actual spawning of sublevels
			
			if (freePlaySubLevels3appearB == true && 
			    Vector2.Distance (freePlaySubLevels3appearStandardPos, freePlaySubLevels3appearFinalPos) != 0)
			{
				freePlaySubLevels3appearStandardPos = Vector2.MoveTowards (freePlaySubLevels3appearStandardPos,
				                                                           freePlaySubLevels3appearFinalPos,
				                                                             Time.deltaTime * thirdButtonSpeed);
			}
			
			if (freePlaySubLevels2appearB == true && 
			    Vector2.Distance (freePlaySubLevels2appearStandardPos, freePlaySubLevels2appearFinalPos) != 0)
			{
				freePlaySubLevels2appearStandardPos = Vector2.MoveTowards (freePlaySubLevels2appearStandardPos,
				                                                           freePlaySubLevels2appearFinalPos,
				                                                             Time.deltaTime * secondButtonSpeed);
			}

		}



		//  Back to the middle
		if (guiIsMovingThree == false && (guiIsMovingFour == true || guiIsMovingFive == true || guiIsMovingfFreePlay == true))
		{
			if (Vector2.Distance (threeByThreeStandardDest, threeByThreeStartDest) != 0)
			{
				threeByThreeStandardDest = Vector2.MoveTowards (threeByThreeStandardDest, threeByThreeStartDest, Time.deltaTime * distance);
			}

			if (threeByThreeStandardDest.x > subLevels3appearFl && threeByThreeSubLevels3appearB == true)
			{
				threeByThreeSubLevels3appearB = false;
				ThreeByThreeToDefault ();
			}
			else if (threeByThreeStandardDest.x > subLevels2appearFl && threeByThreeSubLevels2appearB == true)
			{
				threeByThreeSubLevels2appearB = false;
			}
			else if (threeByThreeStandardDest.x > subLevels1appearFl && threeByThreeSubLevels1appearB == true)
			{
				threeByThreeSubLevels1appearB = false;
			}



		}
		if ((guiIsMovingThree == true || guiIsMovingFive == true || guiIsMovingfFreePlay == true) && guiIsMovingFour == false)
		{
			if (Vector2.Distance (fourByFourStandardDest, fourByFourStartDest) != 0)
			{
				fourByFourStandardDest = Vector2.MoveTowards (fourByFourStandardDest, fourByFourStartDest, Time.deltaTime * distance);
			}

			if (fourByFourStandardDest.x > subLevels3appearFl && fourByFourSubLevels3appearB == true)
			{
				fourByFourSubLevels3appearB = false;
				FourByFourToDefault ();
			}
			else if (fourByFourStandardDest.x > subLevels2appearFl && fourByFourSubLevels2appearB == true)
			{
				fourByFourSubLevels2appearB = false;
			}
			else if (fourByFourStandardDest.x > subLevels1appearFl && fourByFourSubLevels1appearB == true)
			{
				fourByFourSubLevels1appearB = false;
			}



		}
		if ((guiIsMovingThree == true || guiIsMovingFour == true || guiIsMovingfFreePlay == true) && guiIsMovingFive == false )
		{
			if (Vector2.Distance (fiveByFiveStandardDest, fiveByFiveStartDest) != 0)
			{
				fiveByFiveStandardDest = Vector2.MoveTowards (fiveByFiveStandardDest, fiveByFiveStartDest, Time.deltaTime * distance);
			}

			if (fiveByFiveStandardDest.x > subLevels3appearFl && fiveByFiveSubLevels3appearB == true)
			{
				fiveByFiveSubLevels3appearB = false;
				FiveByFiveToDefault ();
			}
			else if (fiveByFiveStandardDest.x > subLevels2appearFl && fiveByFiveSubLevels2appearB == true)
			{
				fiveByFiveSubLevels2appearB = false;
			}
			else if (fiveByFiveStandardDest.x > subLevels1appearFl && fiveByFiveSubLevels1appearB == true)
			{
				fiveByFiveSubLevels1appearB = false;
			}


		}
		//  free play
		if ((guiIsMovingThree == true || guiIsMovingFour == true || guiIsMovingFive == true) && guiIsMovingfFreePlay == false ) 
		{
			if (Vector2.Distance (freePlayStandardDest, freePlayStartDest) != 0)
			{
				freePlayStandardDest = Vector2.MoveTowards (freePlayStandardDest, freePlayStartDest, Time.deltaTime * distance);
			}
			
			if (freePlayStandardDest.x > subLevels3appearFl && freePlaySubLevels3appearB == true)
			{
				freePlaySubLevels3appearB = false;
				FreePlayToDefault ();
			}
			else if (freePlayStandardDest.x > subLevels2appearFl && freePlaySubLevels2appearB == true)
			{
				freePlaySubLevels2appearB = false;
			}
			else if (freePlayStandardDest.x > subLevels1appearFl && freePlaySubLevels1appearB == true)
			{
				freePlaySubLevels1appearB = false;
			}
		}

		//  if smth is false = just move it to the middle
	}



	void OnGUI () {

		AutoResize (720, 1280);	
		GUI.skin = junkyard_GUISkin;

		//HighScoreWRect = GUI.Window (2, HighScoreWRect, Controls, "", "HighscoreWindow");

		//GUI.Label (2, HighScoreWRect, Controls, "", "HighscoreWindow");
		if (openChoosingLevels == false && openStartMenu == true)
		{
			//opens up the levels menu

			if (GUI.Button (new Rect (210, 700, 300, 100), "PLAY", "LongButton"))
			{
				transform.position =  camPosNoLogo;
				if (GUIMain.soundEffectMuteBool == false)
				{
					audio.PlayOneShot(Clicking, 100);
				}
				if(originalTheme == true)
					currentTheme_GUISkin = OriginalTetris_GUISkin;
				openChoosingLevels = true;
				openStartMenu = false;
			}
			//opens up the themes menu
			//			if (GUI.Button (new Rect (210, 820, 300, 100), "THEMES", "LevelMenuButton"))
//			{
//				transform.position = camPosNoLogo;
//				if (GUIMain.soundEffectMuteBool == false)
//				{
//					audio.PlayOneShot(Clicking, 100);
//				}
//				openChoosingThemes = true;
//				openStartMenu = false;
			//			}

			//Reset button to reset the previous progress in the game, opens a prompt to prevent accidental pushes
			if (GUI.Button (new Rect (210, 850, 300, 100), "SETTINGS", "LongButton"))
			{
				if (GUIMain.soundEffectMuteBool == false)
				{
					audio.PlayOneShot(Clicking, 100);
				}
				openStartMenu = false;
				settingsWindow = true;
//				resetPrompt = true;
			}
			//Quit button to quit the application, opens a prompt to prevent accidental pushes
			if (GUI.Button (new Rect (210, 1000, 300, 100), "CREDITS", "LongButton"))
			{
				GUI.Label (new Rect(110, 775, 500, 75), "Helgi Bergmann" + System.Environment.NewLine + "Niels Jonsson" + System.Environment.NewLine + "Gabriel Pettersen" + System.Environment.NewLine + "Michael Selven" + System.Environment.NewLine + "Christine Stene" + System.Environment.NewLine + "Andrey Vlasov", "Credits_Label");

				if (GUIMain.soundEffectMuteBool == false)
				{
					audio.PlayOneShot(Clicking, 100);
				}
				openStartMenu = false;
				creditsWindow = true;
			}
//			GUI.Label(new Rect (200,1130, 550,50), "StackIt! Version: " + gameVersionNr.ToString (), "BuildVersionLabel");
		}

		if(settingsWindow == true)
		{
			if (GUI.Button (new Rect (210, 690, 300, 100), "RESET", "LongButton"))
			{
				settingsWindow = false;
				resetPrompt = true;
			}
			if (GUI.Button (new Rect (160, 830, 200, 200), "", Background_Sound.muteMusicButton))
			{
				Background_Sound.muteMusicButtonBool = !Background_Sound.muteMusicButtonBool;
				Background_Sound.muteMusicBool = !Background_Sound.muteMusicBool;
				if(Background_Sound.muteMusicButtonBool == false)
					Background_Sound.muteMusicButton = "MuteMusicOffButton";
				else if(Background_Sound.muteMusicButtonBool == true)
					Background_Sound.muteMusicButton = "MuteMusicOnButton";
				themeSongObj.audio.enabled = !themeSongObj.audio.enabled;
				GUIMain.gameOverSongMuteBool = !GUIMain.gameOverSongMuteBool;
				GUIMain.victorySongMuteBool = !GUIMain.victorySongMuteBool;
				if (GUIMain.soundEffectMuteBool == false)
					audio.PlayOneShot (Clicking, 100);
			}
			//button to mute the sound effects
			else if (GUI.Button (new Rect (360, 830, 200, 200), "", Background_Sound.muteEffectsButton))
			{
				Background_Sound.muteEffectsButtonBool = !Background_Sound.muteEffectsButtonBool;
				if (GUIMain.soundEffectMuteBool == false)
					audio.PlayOneShot (Clicking, 100);
				GUIMain.soundEffectMuteBool = !GUIMain.soundEffectMuteBool;
				if(Background_Sound.muteEffectsButtonBool == false)
					Background_Sound.muteEffectsButton = "MuteEffectsOffButton";
				else if(Background_Sound.muteEffectsButtonBool == true)
					Background_Sound.muteEffectsButton = "MuteEffectsOnButton";
			}
			
//			GUI.Label (new Rect (280, 880, 300, 150), "MUTE:", "InGameLabel");

			else if (GUI.Button (new Rect (210, 1075, 300, 75), "BACK", "LongButton"))
			{
				if (GUIMain.soundEffectMuteBool == false)
				{
					audio.PlayOneShot(Clicking, 100);
				}
				settingsWindow = false;
				openStartMenu = true;
			}
		}

		if (creditsWindow == true) 
		{

			GUI.Label (new Rect(110, 775, 500, 75), "Helgi Bergmann" + System.Environment.NewLine + "Niels Jonsson" + System.Environment.NewLine + "Gabriel Pettersen" + System.Environment.NewLine + "Michael Selven" + System.Environment.NewLine + "Christine Stene" + System.Environment.NewLine + "Andrey Vlasov", "Credits_Label");

			if (GUI.Button (new Rect (210, 1075, 300, 75), "BACK", "LongButton"))
			{
				if (GUIMain.soundEffectMuteBool == false)
				{
					audio.PlayOneShot(Clicking, 100);
				}
				openStartMenu = true;
				creditsWindow = false;
			}
		}
		//to reset the progress in the game
		if (resetPrompt == true) 
		{
			GUI.Label (new Rect (150,650,450,250), "Are you sure you want to Reset the progress?", "PromptLabel");
			if(GUI.Button (new Rect (150,900,150,150),"Yes","LongButton"))
			{
				ResetProgress ();			
				openStartMenu = true;
				resetPrompt = false;
			}			
			if(GUI.Button (new Rect (400,900,150,150),"No","LongButton"))
			{
				openStartMenu = true;
				resetPrompt = false;
			}			
		}
		/*
		if (openChoosingThemes == true)
		{
			//background
			GUI.Box (new Rect( 260, 100, 200, 200), "", themeSelectStyle);
			
			//Original theme
			originalThemeToggle = GUI.Toggle (new Rect (185, 320, 350, 100), originalThemeToggle, "ORIGINAL", "ThemeMenuToggle");
			if (originalThemeToggle)
			{
				originalTheme = true;
				junkyardTheme = false;
				modernTheme = false;
				lightTheme = false;
				tronBlackTheme = false;
				tronGridTheme = false;
				//all other toggles to false
				junkyardThemeToggle = false;
				modernThemeToggle = false;
				lightToggle = false;
				tronBlackToggle = false;
				tronGridToggle = false;
				
				currentTheme_GUISkin = OriginalTetris_GUISkin;
				themeSelectStyle = "ThemeSelectOriginal";
			}
			
			//Light theme
			lightToggle = GUI.Toggle (new Rect (185, 470, 350, 100), lightToggle, "Light", "ThemeMenuToggle");
			if (lightToggle)
			{
				lightTheme = true;
				junkyardTheme = false;
				originalTheme = false;
				modernTheme = false;
				tronBlackTheme = false;
				tronGridTheme = false;
				//all other toggles to false
				junkyardThemeToggle = false;
				originalThemeToggle = false;
				modernThemeToggle = false;
				tronBlackToggle = false;
				tronGridToggle = false;
				
				currentTheme_GUISkin = light_GUISkin;
				themeSelectStyle = "ThemeSelectLight";
			}
			
			//Modern theme

				modernThemeToggle = GUI.Toggle (new Rect (185, 620, 350, 100), modernThemeToggle, "MODERN", "ThemeMenuToggle");
				if (modernThemeToggle)
				{
					modernTheme = true;
					junkyardTheme = false;
					originalTheme = false;
					lightTheme = false;
					tronBlackTheme = false;
					tronGridTheme = false;
					//all other toggles to false
					junkyardThemeToggle = false;
					originalThemeToggle = false;
					lightToggle = false;
					tronBlackToggle = false;
					tronGridToggle = false;
					
					currentTheme_GUISkin = modern_GUISkin;
					themeSelectStyle = "ThemeSelectModern";
				}

			
			//Tron theme
			if(PlayerPrefs.GetString ("level5by5_1enabled") == "true")
			{
				tronBlackToggle = GUI.Toggle (new Rect (185, 770, 350, 100), tronBlackToggle, "TRON", "ThemeMenuToggle");
				if (tronBlackToggle)
				{
					tronBlackTheme = true;
					lightTheme = false;
					junkyardTheme = false;
					originalTheme = false;
					modernTheme = false;
					tronGridTheme = false;
					//all other toggles to false
					junkyardThemeToggle = false;
					originalThemeToggle = false;
					modernThemeToggle = false;
					lightToggle = false;
					tronGridToggle = false;
					
					currentTheme_GUISkin = tronBlack_GUISkin;
					themeSelectStyle = "ThemeSelectTron";
				}
			}
			else if(PlayerPrefs.GetString ("level5by5_1enabled") == "false")
				GUI.Box (new Rect (185, 770, 350, 100), "???", "ThemeMenuLockedBox");
			*/
			//Junkyard theme
			/*
			if (PlayerPrefs.GetString ("level5by5_FreeEnabled") == "true")
			{
				junkyardThemeToggle = GUI.Toggle (new Rect (185, 920, 350, 100), junkyardThemeToggle, "JUNKYARD", "ThemeMenuToggle");
				if (junkyardThemeToggle)
				{
					junkyardTheme = true;
					originalTheme = false;
					modernTheme = false;
					lightTheme = false;
					tronBlackTheme = false;
					tronGridTheme = false;
					//all other toggles to false
					originalThemeToggle = false;
					modernThemeToggle = false;
					lightToggle = false;
					tronBlackToggle = false;
					tronGridToggle = false;
					
					currentTheme_GUISkin = junkyard_GUISkin;
					themeSelectStyle = "T_Shape_Car_Pref";
				}
			}
			else if (PlayerPrefs.GetString ("level5by5_FreeEnabled") == "false")
				GUI.Box (new Rect (185, 920, 350, 100), "???", "ThemeMenuLockedBox");

			//Back to Main menu
			if (GUI.Button (new Rect (185, 1070, 350, 100), "BACK", "LevelMenuButton"))
			{
				transform.position = camPosWLogo;
				if (GUIMain.soundEffectMuteBool == false)
				{
					audio.PlayOneShot(Clicking, 100);
				}
				openChoosingThemes = false;
				openStartMenu = true;
			}
		}
*/
		if (openChoosingLevels == true)
		{
			//Platform button for 3x3 that opens up to the 3x3 levels
			if (GUI.Button (new Rect (threeByThreeStandardDest.x, threeByThreeStandardDest.y, 150, 150), "3x3", "LevelMenuButton")) 
			{
				if (GUIMain.soundEffectMuteBool == false)
				{
					audio.PlayOneShot(Clicking, 100);
				}
				EasyLevels = true;
				//MediumLevels = false;
				//HardLevels = false;
				FreePlay = false;
				
				guiIsMovingThree = true;
				guiIsMovingFour = false;
				guiIsMovingFive = false;
				guiIsMovingfFreePlay = false;
				//FourByFourToDefault ();
				//FiveByFiveToDefault ();
			}
			//Platform button for 4x4 that opens up to the 4x4 levels
			if (GUI.Button (new Rect (fourByFourStandardDest.x, fourByFourStandardDest.y, 150, 150), "4x4", "LevelMenuButton") && PlayerPrefs.GetString ("level4by4_1enabled") == "true") 
			{
				if (GUIMain.soundEffectMuteBool == false)
				{
					audio.PlayOneShot(Clicking, 100);
				}
				//EasyLevels = false;
				MediumLevels = true;
				//HardLevels = false;
				FreePlay = false;
				
				guiIsMovingThree = false;
				guiIsMovingFour = true;
				guiIsMovingFive = false;
				guiIsMovingfFreePlay = false;
				//ThreeByThreeToDefault ();
				//FiveByFiveToDefault ();
			}
			if (PlayerPrefs.GetString ("level4by4_1enabled") == "false")
				GUI.Box (new Rect (fourByFourStandardDest.x, fourByFourStandardDest.y, 150, 150), "4x4", "LevelMenuLockedBox");
			//Platform button for 5x5 that opens up to the 5x5 levels
			if (GUI.Button (new Rect (fiveByFiveStandardDest.x, fiveByFiveStandardDest.y, 150, 150), "5x5", "LevelMenuButton") && PlayerPrefs.GetString ("level5by5_1enabled") == "true")
			{
				if (GUIMain.soundEffectMuteBool == false)
				{
					audio.PlayOneShot(Clicking, 100);
				}
				//EasyLevels = false;
				//MediumLevels = false;
				HardLevels = true;
				FreePlay = false;
				
				guiIsMovingThree = false;
				guiIsMovingFour = false;
				guiIsMovingFive = true;
				guiIsMovingfFreePlay = false;
				//ThreeByThreeToDefault ();
				//FourByFourToDefault ();
			}
			if (PlayerPrefs.GetString ("level5by5_1enabled") == "false")
				GUI.Box (new Rect (fiveByFiveStandardDest.x, fiveByFiveStandardDest.y, 150, 150), "5x5", "LevelMenuLockedBox");
			
			//Free play button that opens up to the Free play levels for all platform sizes
			if (GUI.Button (new Rect (freePlayStandardDest.x, freePlayStandardDest.y, 150, 150), "", "InfiniteButton") && PlayerPrefs.GetString ("level3by3_FreeEnabled") == "true")
			{
				if (GUIMain.soundEffectMuteBool == false)
				{
					audio.PlayOneShot(Clicking, 100);
				}
				//EasyLevels = false;
				//MediumLevels = false;
				//HardLevels = false;
				FreePlay = true;
				
				guiIsMovingfFreePlay = true;
				guiIsMovingThree = false;
				guiIsMovingFour = false;
				guiIsMovingFive = false;
			}
			if (PlayerPrefs.GetString ("level3by3_FreeEnabled") == "false")
				GUI.Box (new Rect (freePlayStandardDest.x, freePlayStandardDest.y, 150, 150), "", "InfiniteLockedButton");
			
			
			if (FreePlay == true) 
			{
				if (freePlaySubLevels1appearB == true)
				{
					if (GUI.Button (new Rect (210, 700, 150, 150), "3x3", "LevelMenuButton") && PlayerPrefs.GetString ("level3by3_FreeEnabled") == "true" && freePlaySubLevels1appearB == true) 
					{
						if (GUIMain.soundEffectMuteBool == false)
						{
							audio.PlayOneShot(Clicking, 100);
						}
						ChangeableVariables.levelWeAreIn = "3x3_Free";
						ChangeableVariables.SizeIsThree ();
						freePlayGUIBool = true;
						LoadingNewLevel ();
					}
					if (PlayerPrefs.GetString ("level3by3_FreeEnabled") == "false")
						GUI.Box (new Rect (210, 700, 150, 150), "3x3", "LevelMenuLockedBox");
				}
				if (freePlaySubLevels2appearB == true)
				{
					if (GUI.Button (new Rect (freePlaySubLevels2appearStandardPos.x, freePlaySubLevels2appearStandardPos.y, 
					                          150, 150), "4x4", "LevelMenuButton") && PlayerPrefs.GetString ("level4by4_FreeEnabled") == "true" && freePlaySubLevels1appearB == true)
					{
						if (GUIMain.soundEffectMuteBool == false)
						{
							audio.PlayOneShot(Clicking, 100);
						}
						ChangeableVariables.levelWeAreIn = "4x4_Free";
						ChangeableVariables.SizeIsFour ();
						freePlayGUIBool = true;
						LoadingNewLevel ();
					}
					if (PlayerPrefs.GetString ("level4by4_FreeEnabled") == "false")
						GUI.Box (new Rect (freePlaySubLevels2appearStandardPos.x, freePlaySubLevels2appearStandardPos.y, 
						                   150, 150), "4x4", "LevelMenuLockedBox");
				}
				if (freePlaySubLevels3appearB == true)
				{
					if (GUI.Button (new Rect (freePlaySubLevels3appearStandardPos.x, freePlaySubLevels3appearStandardPos.y, 
					                          150, 150), "5x5", "LevelMenuButton") && PlayerPrefs.GetString ("level5by5_FreeEnabled") == "true" && freePlaySubLevels1appearB == true)
					{
						if (GUIMain.soundEffectMuteBool == false)
						{
							audio.PlayOneShot(Clicking, 100);
						}
						ChangeableVariables.levelWeAreIn = "5x5_Free";
						ChangeableVariables.SizeIsFive ();
						freePlayGUIBool = true;
						LoadingNewLevel ();
					}
					if (PlayerPrefs.GetString ("level4by4_FreeEnabled") == "false")
						GUI.Box (new Rect (freePlaySubLevels3appearStandardPos.x, freePlaySubLevels3appearStandardPos.y, 
						                   150, 150), "5x5", "LevelMenuLockedBox");
				}
			}
			
			// 3x3
			if (EasyLevels == true) 
			{ 
				
				if (threeByThreeSubLevels1appearB == true)
				{
					if (GUI.Button (new Rect (210, 100, 
					                          150, 150), "1", "LevelMenuButton") && PlayerPrefs.GetString ("level3by3_1enabled") == "true"
					    && threeByThreeSubLevels1appearB == true) 
					{
						if (GUIMain.soundEffectMuteBool == false)
						{
							audio.PlayOneShot(Clicking, 100);
						}
						ChangeableVariables.levelWeAreIn = "3x3_1";
						ChangeableVariables.SizeIsThree ();
						ChangeableVariables.spawnFrom = 1;
						ChangeableVariables.spawnTo = 2;
						levelGoalsInt = 1;
						curLevelInt = 1;
						LoadingNewLevel ();
					} 
				}
				if (threeByThreeSubLevels2appearB == true)
				{
					if (GUI.Button (new Rect (threeByThreeSubLevels2appearStandardPos.x, threeByThreeSubLevels2appearStandardPos.y, 
					                          150, 150), "2", "LevelMenuButton") && PlayerPrefs.GetString ("level3by3_2enabled") == "true"
					    && threeByThreeSubLevels1appearB == true) 
					{
						if (GUIMain.soundEffectMuteBool == false)
						{
							audio.PlayOneShot(Clicking, 100);
						}
						ChangeableVariables.levelWeAreIn = "3x3_2";
						ChangeableVariables.SizeIsThree ();
						ChangeableVariables.spawnFrom = 1;
						ChangeableVariables.spawnTo = 3;
						levelGoalsInt = 5;
						curLevelInt = 2;
						LoadingNewLevel ();
					}
					if (PlayerPrefs.GetString ("level3by3_2enabled") == "false")
					{
						GUI.Box (new Rect (threeByThreeSubLevels2appearStandardPos.x, threeByThreeSubLevels2appearStandardPos.y, 
						                   150, 150), "2", "LevelMenuLockedBox");
					}
					
				}
				if (threeByThreeSubLevels3appearB == true)
				{
					if (GUI.Button (new Rect (threeByThreeSubLevels3appearStandardPos.x, threeByThreeSubLevels3appearStandardPos.y, 
					                          150, 150), "3", "LevelMenuButton") && PlayerPrefs.GetString ("level3by3_3enabled") == "true"
					    && threeByThreeSubLevels1appearB == true) 
					{
						if (GUIMain.soundEffectMuteBool == false)
						{
							audio.PlayOneShot(Clicking, 100);
						}
						ChangeableVariables.levelWeAreIn = "3x3_3";
						ChangeableVariables.SizeIsThree ();
						ChangeableVariables.spawnFrom = 1;
						ChangeableVariables.spawnTo = 3;
						levelGoalsString = "Clear 2 at the\nsame time";
						curLevelInt = 3;
						//LineScore.floorsAtTheSameTime;
						LoadingNewLevel ();
					}
					if(PlayerPrefs.GetString ("level3by3_3enabled") == "false")
						GUI.Box (new Rect (threeByThreeSubLevels3appearStandardPos.x, threeByThreeSubLevels3appearStandardPos.y, 
						                   150, 150), "3", "LevelMenuLockedBox");
				}
			}
			
			//  4x4
			if(MediumLevels == true)
			{
				if (fourByFourSubLevels1appearB == true)
				{
					if (GUI.Button (new Rect (210, 300, 
					                          150, 150), "1", "LevelMenuButton") && PlayerPrefs.GetString ("level4by4_1enabled") == "true"
					    && fourByFourSubLevels1appearB == true) 
					{
						if (GUIMain.soundEffectMuteBool == false)
						{
							audio.PlayOneShot(Clicking, 100);
						}
						ChangeableVariables.levelWeAreIn = "4x4_1";
						ChangeableVariables.SizeIsFour ();
						ChangeableVariables.spawnFrom = 0;
						ChangeableVariables.spawnTo = 5;
						levelGoalsInt = 10000;
						curLevelInt = 1;
						LoadingNewLevel ();
					}
					if(PlayerPrefs.GetString ("level4by4_1enabled") == "false")
						GUI.Box (new Rect (200, 300, 150, 150), "1", "LevelMenuLockedBox");
				}
				if (fourByFourSubLevels2appearB == true)
				{
					if (GUI.Button (new Rect (fourByFourSubLevels2appearStandardPos.x, fourByFourSubLevels2appearStandardPos.y, 
					                          150, 150), "2", "LevelMenuButton") && PlayerPrefs.GetString ("level4by4_2enabled") == "true"
					    && fourByFourSubLevels1appearB == true) 
					{
						if (GUIMain.soundEffectMuteBool == false)
						{
							audio.PlayOneShot(Clicking, 100);
						}
						ChangeableVariables.levelWeAreIn = "4x4_2";
						ChangeableVariables.SizeIsFour ();
						ChangeableVariables.spawnFrom = 0;
						ChangeableVariables.spawnTo = 6;
						levelGoalsInt = 20000;
						curLevelInt = 2;
						LoadingNewLevel ();
					}
					if(PlayerPrefs.GetString ("level4by4_2enabled") == "false")
						GUI.Box (new Rect (fourByFourSubLevels2appearStandardPos.x, fourByFourSubLevels2appearStandardPos.y, 
						                   150, 150), "2", "LevelMenuLockedBox");
				}
				if (fourByFourSubLevels3appearB == true)
				{
					if (GUI.Button (new Rect (fourByFourSubLevels3appearStandardPos.x, fourByFourSubLevels3appearStandardPos.y, 
					                          150, 150), "3", "LevelMenuButton") && PlayerPrefs.GetString ("level4by4_3enabled") == "true"
					    && fourByFourSubLevels1appearB == true) 
					{
						if (GUIMain.soundEffectMuteBool == false)
						{
							audio.PlayOneShot(Clicking, 100);
						}
						ChangeableVariables.levelWeAreIn = "4x4_3";
						ChangeableVariables.SizeIsFour ();
						ChangeableVariables.spawnFrom = 0;
						ChangeableVariables.spawnTo = 7;
						levelGoalsInt = 30000;
						curLevelInt = 3;
						LoadingNewLevel ();
					}
					if(PlayerPrefs.GetString ("level4by4_3enabled") == "false")
						GUI.Box (new Rect (fourByFourSubLevels3appearStandardPos.x, fourByFourSubLevels3appearStandardPos.y, 
						                   150, 150), "3", "LevelMenuLockedBox");
				}
			}
			
			//  5x5
			if (HardLevels == true) 
			{
				
				if (fiveByFiveSubLevels1appearB == true)
				{
					if (GUI.Button (new Rect (210, 500, 
					                          150, 150), "1", "LevelMenuButton") && PlayerPrefs.GetString ("level5by5_1enabled") == "true"
					    && fiveByFiveSubLevels1appearB == true) 
					{
						if (GUIMain.soundEffectMuteBool == false)
						{
							audio.PlayOneShot(Clicking, 100);
						}
						ChangeableVariables.levelWeAreIn = "5x5_1";
						ChangeableVariables.SizeIsFive ();
						ChangeableVariables.spawnFrom = 0;
						ChangeableVariables.spawnTo = 9;
						levelGoalsInt = 10000;
						curLevelInt = 1;
						LoadingNewLevel ();
					}
					if(PlayerPrefs.GetString ("level5by5_1enabled") == "false")
						GUI.Box (new Rect (200, 500, 150, 150), "1", "LevelMenuLockedBox");
				}
				
				if (fiveByFiveSubLevels2appearB == true)
				{
					if (GUI.Button (new Rect (fiveByFiveSubLevels2appearStandardPos.x, fiveByFiveSubLevels2appearStandardPos.y, 
					                          150, 150), "2", "LevelMenuButton") && PlayerPrefs.GetString ("level5by5_2enabled") == "true"
					    && fiveByFiveSubLevels1appearB == true)
					{
						if (GUIMain.soundEffectMuteBool == false)
						{
							audio.PlayOneShot(Clicking, 100);
						}
						ChangeableVariables.levelWeAreIn = "5x5_2";
						ChangeableVariables.SizeIsFive ();
						ChangeableVariables.spawnFrom = 0;
						ChangeableVariables.spawnTo = 10;
						levelGoalsInt = 20000;
						curLevelInt = 2;
						LoadingNewLevel ();
					}
					if(PlayerPrefs.GetString ("level5by5_2enabled") == "false")
						GUI.Box (new Rect (fiveByFiveSubLevels2appearStandardPos.x, fiveByFiveSubLevels2appearStandardPos.y, 
						                   150, 150), "2", "LevelMenuLockedBox");
				}
				
				if (fiveByFiveSubLevels3appearB == true)
				{
					if (GUI.Button (new Rect (fiveByFiveSubLevels3appearStandardPos.x, fiveByFiveSubLevels3appearStandardPos.y, 
					                          150, 150), "3", "LevelMenuButton") && PlayerPrefs.GetString ("level5by5_3enabled") == "true"
					    && fiveByFiveSubLevels1appearB == true)
					{
						if (GUIMain.soundEffectMuteBool == false)
						{
							audio.PlayOneShot(Clicking, 100);
						}
						ChangeableVariables.levelWeAreIn = "5x5_3";
						ChangeableVariables.SizeIsFive ();
						ChangeableVariables.spawnFrom = 0;
						ChangeableVariables.spawnTo = 11;
						levelGoalsInt = 30000;
						curLevelInt = 3;
						LoadingNewLevel ();
					}
					if(PlayerPrefs.GetString ("level5by5_3enabled") == "false")
						GUI.Box (new Rect (fiveByFiveSubLevels3appearStandardPos.x, fiveByFiveSubLevels3appearStandardPos.y, 
						                   150, 150), "3", "LevelMenuLockedBox");
				}
			}

			//  highscore
			if (GUI.Button (new Rect (185, 890, 350, 100), "HIGHSCORE", "LongButton"))
			{
				if (GUIMain.soundEffectMuteBool == false)
				{
					audio.PlayOneShot(Clicking, 100);
				}
				//PlayerPrefs.DeleteAll ();
				//PlayerPrefs.Save ();
				ChangeableVariables.FillTheNames ();  //  "you, you, ..."

				//  displaying the results
				if (PlayerPrefs.HasKey ("3x3"))
				{
					score3 = GetResults ("3x3");
				}
				else
				{
					score3 = FillTheResults ();
				}

				if (PlayerPrefs.HasKey ("4x4"))
				{
					score4 = GetResults ("4x4");
				}
				else
				{
					score4 = FillTheResults ();
				}

				if (PlayerPrefs.HasKey ("5x5"))
				{
					score5 = GetResults ("5x5");
				}
				else
				{
					score5 = FillTheResults ();
				}

				displayHighScore = true;
			}


			if (displayHighScore == true)
			{

				HighScoreWRect = GUI.Window (2, HighScoreWRect, Controls, "", "HighscoreWindow");


			}
			if (GUI.Button (new Rect (160,1040, 400, 100), "MAIN MENU", "LongButton"))
			{
				transform.position = camPosWLogo;
				if (GUIMain.soundEffectMuteBool == false)
				{
					audio.PlayOneShot(Clicking, 100);
				}
				openChoosingLevels = false;
				openStartMenu = true;
			}
		}
		if(loadingImageOn == true)
		{
			GUI.Box (new Rect (20, 1150, 120, 120), "", "LoadingImage_Box");
			GUI.Label (new Rect (520, 1200, 200, 120), "Loading...", "LoadingImage_Label");
		}
	}



	private void LoadingNewLevel () {

		StartCoroutine (ActualLoading ());
	}


	IEnumerator ActualLoading () {
	
		yield return null;
		loadingImageOn = true;
		Application.LoadLevel (1);

	}


	private void ThreeByThreeToDefault () {
		EasyLevels = false;

		threeByThreeSubLevels2appearStandardPos = new Vector2 (129, 100);
		threeByThreeSubLevels3appearStandardPos = new Vector2 (207, 100);
	}



	private void FourByFourToDefault () {
		MediumLevels = false;
		fourByFourSubLevels2appearStandardPos = new Vector2 (129, 300);
		fourByFourSubLevels3appearStandardPos = new Vector2 (207, 300);
		/*
		fourByFourSubLevels3appearB = false;
		fourByFourSubLevels2appearB = false;
		fourByFourSubLevels1appearB = false;
		*/
	}


	private void FiveByFiveToDefault () {
		HardLevels = false;
		fiveByFiveSubLevels2appearStandardPos = new Vector2 (129, 500);
		fiveByFiveSubLevels3appearStandardPos = new Vector2 (207, 500);
		/*
		fiveByFiveSubLevels3appearB = false;
		fiveByFiveSubLevels2appearB = false;
		fiveByFiveSubLevels1appearB = false;
		*/
	}

	private void FreePlayToDefault () {
		//FreePlay = false;
		freePlaySubLevels2appearStandardPos = new Vector2 (129, 700);
		freePlaySubLevels3appearStandardPos = new Vector2 (207, 700);
	}


	public static void AutoResize(int screenWidth, int screenHeight)
	{
		Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
	}


	void Controls (int windowID1) 
	{
		//GUI.skin.font = highScoreFont;
		GUI.Label(new Rect(105, 50, (HighSc.width/2), (HighSc.height)), "", "HighSc");
		//  displaying highscore
		GUI.Label (new Rect (120, 325, 170, 800), "3x3", "HighscorePFSLabel");
		GUI.Label (new Rect (320, 275, 170, 800), ChangeableVariables.filledNamesForHighscore, "HighscoreLabel");
		GUI.Label (new Rect (520, 275, 170, 800), score3, "HighscoreLabel");

		GUI.Label (new Rect (120, 600, 170, 800), "4x4", "HighscorePFSLabel");
		GUI.Label (new Rect (320, 550, 170, 800), ChangeableVariables.filledNamesForHighscore, "HighscoreLabel");
		GUI.Label (new Rect (520, 550, 170, 800), score4, "HighscoreLabel");

		GUI.Label (new Rect (120, 875, 170, 800), "5x5", "HighscorePFSLabel");
		GUI.Label (new Rect (320, 825, 170, 800), ChangeableVariables.filledNamesForHighscore, "HighscoreLabel");
		GUI.Label (new Rect (520, 825, 170, 800), score5, "HighscoreLabel");

		if (GUI.Button (new Rect (210, 1075, 300, 75), "BACK", "LongButton"))
		{
			if (GUIMain.soundEffectMuteBool == false)
			{
				audio.PlayOneShot(Clicking, 100);
			}
			displayHighScore = false;
		}
	}


	string GetResults (string getCorrectList) {
		highScoreList = GetUnserializedList (PlayerPrefs.GetString (getCorrectList));
		TakeTenBestResults ();
		System.Text.StringBuilder resultToDisplayScore = new System.Text.StringBuilder();

		foreach (int kvp in highScoreList)
		{
			//Debug.Log (kvp.Key.Substring (0, kvp.Key.Length - 6) + ", " + kvp.Value.ToString ("0"));
			
			resultToDisplayScore.Append (kvp).Append (Environment.NewLine);
		}

		return resultToDisplayScore.ToString ();
	}


	void TakeTenBestResults () {
		//  take 10 highest results
		var temp = from entry in highScoreList orderby entry descending select entry;
		var temp10 = temp.Take (5);
		highScoreList = temp10.ToList ();
	}


	string GetSerializedList (List <int> d)
	{
		// Build up each line one-by-one and then trim the end
		System.Text.StringBuilder builder = new System.Text.StringBuilder();
		
		foreach (int pair in d)
		{
			builder.Append(pair).Append(":");
		}
		string result = builder.ToString();
		// Remove the final delimiter
		result = result.TrimEnd(':');
		//Debug.Log (result);
		return result;
	}

	List <int> GetUnserializedList (string serialisedDict) {
		
		List <int> d = new List<int> ();
		
		//  As input, Split takes an array of chars 
		//  that indicate which characters are to be used as delimiters.
		string [] tokens = serialisedDict.Split(new char [] {':'});
		
		// Walk through each item
		for (int i = 0; i < tokens.Length; i ++)
		{
			string freq = tokens[i];
			
			int count = int.Parse (freq);
			// Fill the value in the sorted dictionary	
			d.Add(count);
		}	
		return d;
	}



	//  in case there are no data

	private string FillTheResults () {
		System.Text.StringBuilder score = new System.Text.StringBuilder ();
		
		for (int i = 0; i < 5; i ++)
		{
			if (i != 6)
				score.Append ("0").Append (Environment.NewLine);
			else
				score.Append ("0").Append (Environment.NewLine);
		}
		
		return score.ToString ();
	}
	//Function that resets all progress in the game
	void ResetProgress ()
	{
		PlayerPrefs.DeleteAll ();
		PlayerPrefs.SetString ("level3by3_1enabled", "true");
		//  change them to "false" when needed
		PlayerPrefs.SetString ("FinidhedFirstTutorial", "false");
		PlayerPrefs.SetString ("CameraMovementUnlocked", "false");
		PlayerPrefs.SetString ("level3by3_2enabled", "false");
		PlayerPrefs.SetString ("level3by3_3enabled", "false");
		
		PlayerPrefs.SetString ("level4by4_1enabled", "false");
		PlayerPrefs.SetString ("level4by4_2enabled", "false");
		PlayerPrefs.SetString ("level4by4_3enabled", "false");
		
		PlayerPrefs.SetString ("level5by5_1enabled", "false");
		PlayerPrefs.SetString ("level5by5_2enabled", "false");
		PlayerPrefs.SetString ("level5by5_3enabled", "false");
		
		PlayerPrefs.SetString ("level3by3_FreeEnabled", "false");
		PlayerPrefs.SetString ("level4by4_FreeEnabled", "false");
		PlayerPrefs.SetString ("level5by5_FreeEnabled", "false");
		
		PlayerPrefs.Save ();
	}
}

using UnityEngine;
using System.Collections;

public class TutorialLevel : MonoBehaviour {


	public Texture hand, hand2;
	public Texture handWithCircles, handWithCircles2;
	public Texture phone, phone2;
	public Texture emptyTexture;
	public Texture handFlipped, handFlipped2;

	public Texture currentTexture;

	public int numberOfTimesAnimationPlayed = 0;

	public enum TutorialStates {
		//  enums with indexes
		DoubleTap = 0,
		Drag = 1,
		RotateHorizontally = 2,
		RotateVertically = 3,
		Accelerometer = 4,
		Switch = 5,
		None = 6
	}

	public TutorialStates currentState;

	public Vector2 startPos = Vector2.zero;
	public Vector2 endPos = Vector2.zero;
	public float speed = 0;
	public float sizesOfTutorialButtons = 150;
	private string tutorialText = string.Empty;
	private Rect tutorialTextPos = new Rect (50, 550, 600, 760);

	//bool for showing tutorial text and controls text depending on device
	public static bool mobileTutorial = true;

	public static bool showGuiText = true;
	public static string cornerText = string.Empty;

	GameObject shapesManager;
	ShapesManager sM;
	StartFunction sF;
	private string firstLevelAccomplished = string.Empty;

	GUIStyle locked = new GUIStyle ();


	void Start () {

		locked.normal.textColor = Color.black;

		MovingController.changingState += None;
		firstLevelAccomplished = PlayerPrefs.GetString ("FinidhedFirstTutorial");
	}


	void OnGUI () {

		AutoResize (720, 1280);
		GUI.skin = StartFunction.currentTheme_GUISkin;
		//  display a texture
		if (showGuiText == true) 
		{
			if (!(ChangeableVariables.levelWeAreIn == "3x3_1" && firstLevelAccomplished == "true"))
			{
				GUI.DrawTexture (new Rect (startPos.x, startPos.y, sizesOfTutorialButtons, sizesOfTutorialButtons), currentTexture);
				GUI.Label (tutorialTextPos, tutorialText, "TutorialLabel");
			}
		}
		//GUI.Label (new Rect (400, 1000, 320, 280), cornerText, locked);

	}


	void Update () {
		//  move the start position of the texture
		if (currentState != TutorialStates.None) 
		{
			startPos = Vector2.MoveTowards (startPos, endPos, speed * 0.5f * Time.deltaTime);
			if (Vector2.Distance (startPos, endPos) == 0)
			{
				if (currentState == TutorialStates.RotateHorizontally)
				{
					startPos = new Vector2 (Screen.width / 2 - Screen.width / 2.61f, Screen.height / 2 - Screen.width / 9.6f);
				}
				else if (currentState == TutorialStates.DoubleTap)
				{
					startPos = new Vector2 (Screen.width / 2 - Screen.width / 9.6f, Screen.height / 2);
				} 
				else if (currentState == TutorialStates.Drag)
				{
					startPos = new Vector2 (500, 450);
				}
				else if (currentState == TutorialStates.RotateVertically)
				{
					startPos = new Vector2 (Screen.width / 2 - Screen.width / 9.6f, Screen.width / 3.6f);
				}
				else if (currentState == TutorialStates.Switch)
				{
					startPos = new Vector2 (Screen.width / 2 + Screen.width / 6.5f, Screen.height / 3);
				}
				else if (currentState == TutorialStates.Accelerometer)
				{
					startPos = new Vector2 (Screen.width / 2 - Screen.width / 28.8f, Screen.height / 2);
				}
				numberOfTimesAnimationPlayed++;
			}
			/*
			if (numberOfTimesAnimationPlayed >= 3)
			{
				showGuiText = false;
				numberOfTimesAnimationPlayed = 0;
			}
			*/
//			Debug.Log (showGuiText);
		}
	}


	public void CurrentTexture () {

		if (currentState == TutorialStates.Drag) {
			if (StartFunction.junkyardTheme){
				currentTexture = handWithCircles;
			} else {
				currentTexture = handWithCircles2;
			}
		} else if (currentState == TutorialStates.RotateHorizontally || 
			currentState == TutorialStates.RotateVertically || currentState == TutorialStates.DoubleTap) {
			if (StartFunction.junkyardTheme){
				currentTexture = hand;
			} else {
				currentTexture = hand2;
			}
		} else if (currentState == TutorialStates.Accelerometer) {
			if (StartFunction.junkyardTheme){
				currentTexture = phone;
			} else {
				currentTexture = phone2;
			}

		} else if (currentState == TutorialStates.Switch) {
			if (StartFunction.junkyardTheme){
				currentTexture = handFlipped;
			} else {
				currentTexture = handFlipped2;
			}	
		}
	}

	public void NewPositionsOfTheVectors () {

		if (currentState == TutorialStates.DoubleTap) 
		{
			//  75
			startPos = new Vector2 (500, 450);
			endPos = new Vector2 (500, 457);
			speed = Vector2.Distance (startPos, endPos) / 2;
			StartCoroutine (SwapDoubleTapTexture ());
			sizesOfTutorialButtons = Screen.width / 4.8f;
			if(mobileTutorial == true)
				tutorialText = "Tap twice to make a shape fall down";
			else if(mobileTutorial == false)
				tutorialText = "Push SPACE to make the shape fall down";
			showGuiText = true;
		}
		else if (currentState == TutorialStates.Drag) 
		{
			//  500
			startPos = new Vector2 (Screen.width / 1.44f, Screen.width / 1.44f);
			//  100
			endPos = new Vector2 (Screen.width / 7.2f, Screen.width / 7.2f);
			speed = Vector2.Distance (startPos, endPos) / 2f;
			//  150
			sizesOfTutorialButtons = Screen.width / 4.8f * 1.75f;
			ChangeableVariables.spawnTo = 3;
			if(mobileTutorial == true)
				tutorialText = "Drag the shape to move it";
			else if(mobileTutorial == false)
				tutorialText = "Use ARROW Keys to move it";
			showGuiText = true;
		}
		else if (currentState == TutorialStates.RotateHorizontally) 
		{
			//  275, 75
			startPos = new Vector2 (Screen.width / 2 - Screen.width / 2.61f, Screen.height / 2 - Screen.width / 9.6f);
			endPos = new Vector2 (Screen.width / 2 + Screen.width / 2.61f, Screen.height / 2 - Screen.width / 9.6f);
			speed = Vector2.Distance (startPos, endPos) / 2f;
			//  150
			sizesOfTutorialButtons = Screen.width / 4.8f;
			if(mobileTutorial == true)
				tutorialText = "Swipe horizontally to make a shape rotate horizontally";
			else if(mobileTutorial == false)
				tutorialText = "Use A and D to make a shape rotate horizontally";
			showGuiText = true;
		}
		else if (currentState == TutorialStates.RotateVertically) 
		{
			//  75, 200, 1000
			startPos = new Vector2 (Screen.width / 2 - Screen.width / 9.6f, Screen.width / 3.6f);
			endPos = new Vector2 (Screen.width / 2 - Screen.width / 9.6f, Screen.width / 0.72f);
			speed = Vector2.Distance (startPos, endPos) / 1.2f;
			//  150
			sizesOfTutorialButtons = Screen.width / 4.8f;
			if(mobileTutorial == true)
				tutorialText = "Swipe vertically to make a shape rotate vertically";
			else if(mobileTutorial == false)
				tutorialText = "Use W and S to make a shape rotate vertically";
			showGuiText = true;
		}
		else if (currentState == TutorialStates.Accelerometer) 
		{
			//  25
			startPos = new Vector2 (Screen.width / 2 - Screen.width / 28.8f, Screen.height / 2);
			endPos = new Vector2 (Screen.width / 2 + Screen.width / 28.8f, Screen.height / 2);
			speed = Vector2.Distance (startPos, endPos) / 1.2f;
			sizesOfTutorialButtons = Screen.width / 4.8f * 1.75f;
			if(mobileTutorial == true)
				tutorialText = "Tilt your phone to change the camera's position";
			else if(mobileTutorial == false)
				tutorialText = "Tilt your phone to change the camera's position";
			showGuiText = true;
		}
		else if (currentState == TutorialStates.Switch){
			startPos = new Vector2 (Screen.width / 2 + Screen.width / 6.5f, Screen.height / 3);
			endPos = new Vector2 (Screen.width / 2 + Screen.width / 6f, Screen.height / 3);
			speed = Vector2.Distance (startPos, endPos) / 2;
			sizesOfTutorialButtons = Screen.width / 4.8f * 1.75f;
			if(mobileTutorial == true)
				tutorialText = "Tap the next shape image to swap between shapes";
			else if(mobileTutorial == false)
				tutorialText = "Tap the next shape image to swap between shapes";
			showGuiText = true;
		}
		cornerText = tutorialText;
//		Debug.Log ("cornerText = " + cornerText);
	}


	IEnumerator SwapDoubleTapTexture () {
		while (currentState == TutorialStates.DoubleTap)
		{
			yield return new WaitForSeconds (0.6f);
			SwapTextures ();
		}
	}


	void SwapTextures () {
		Texture temp = currentTexture;
		currentTexture = emptyTexture;
		emptyTexture = temp;
	}

	void None () {
		currentState = TutorialStates.None;
		MovingController.changingState -= None;
	}

	public static void AutoResize(int screenWidth, int screenHeight)
	{
		Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
	}

	void OnDestroy () {
		MovingController.changingState -= None;
		
	}
}

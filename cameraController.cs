using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {
	
//	private Quaternion CamRot = Quaternion.Euler (60,45,0);
	//public float distFromBot = 50;
	//public float distFromRight = 50;
	//public float boxHeight = 30;
	//public float boxWidth = 30;
	bool isRotating = false;
//	private float startTime;
//	private float journeyLength;
	public float RotTime = 0.1f;

	[HideInInspector]
	public int CamPosCounter = 0;

	private float distBetCamAndAim;
	private GameObject shapeToChangeDirections;
	MovingController mC;
	
	public Vector3 [] CamPos;

	public Vector3 CamTarget;
	Vector3 cameraRotation;
	public AudioClip CameraLeft;
	public AudioClip CameraRight;

	float boundary = 10;
	int widthOfScreen, heightOfScreen;

	bool canRotateWithAccelerometer = true;

	// Use this for initialization
	void Start () {

		widthOfScreen = Screen.width;
		heightOfScreen = Screen.height;
		
		//  get new coordinates of possible camera's positions
		CamPos [0] = new Vector3 (- ChangeableVariables.CameraXZPos, CamPos [0].y, - ChangeableVariables.CameraXZPos);
		CamPos [1] = new Vector3 (- ChangeableVariables.CameraXZPos + 0.5f, CamPos [1].y, ChangeableVariables.CameraXZPos - 0.5f);
		CamPos [2] = new Vector3 (ChangeableVariables.CameraXZPos, CamPos [2].y, ChangeableVariables.CameraXZPos);
		CamPos [3] = new Vector3 (ChangeableVariables.CameraXZPos + 0.5f, CamPos [3].y, - ChangeableVariables.CameraXZPos - 0.5f);
		//  set the initial position
		transform.position = CamPos [CamPosCounter];

		CamTarget = ChangeableVariables.CamTargetForLevels;

		//  look at the rarget position
		Quaternion cameraRotationQ = Quaternion.LookRotation (CamTarget - transform.position);
		cameraRotation = new Vector3 (cameraRotationQ.eulerAngles.x, cameraRotationQ.eulerAngles.y, cameraRotationQ.eulerAngles.z);
		transform.eulerAngles = cameraRotation;

	}


	//  dinamically change camera's positions
	public void ChangeCamera () {
		Quaternion cameraRotationQ = Quaternion.LookRotation (CamTarget - transform.position);
		cameraRotation = new Vector3 (cameraRotationQ.eulerAngles.x, cameraRotationQ.eulerAngles.y, cameraRotationQ.eulerAngles.z);
		transform.eulerAngles = cameraRotation;
	}


	void ECam () {
		if (GUIMain.soundEffectMuteBool == false)
			audio.PlayOneShot(CameraRight,0.7f);
		mC.SwapDirectionsRight();
		
		if (CamPosCounter >= 1)
		{
			CamPosCounter--;
			mC.PositionTwo ();
		} 
		else 
		{
			CamPosCounter = 3;
			mC.PositionThree ();
		}
		
		DependsOnCameraPos ();
		
		isRotating = true;
		distBetCamAndAim = Vector3.Distance (transform.position, CamPos[CamPosCounter]);

		
		TutorialLevel.showGuiText = false;
	}

	void QCam (){
		if (GUIMain.soundEffectMuteBool == false)
			audio.PlayOneShot(CameraLeft, 0.7f);
		//  change controls
		mC.SwapDirectionsLeft ();
		
		if (CamPosCounter <= 2)
		{
			CamPosCounter++;
			mC.PositionOne ();
		} 
		else 
		{
			CamPosCounter = 0;
		}
		DependsOnCameraPos ();
		
		isRotating = true;
		distBetCamAndAim = Vector3.Distance (transform.position, CamPos[CamPosCounter]);
		
		TutorialLevel.showGuiText = false;
	}

	void Update ()
	{
#if UNITY_EDITOR
			

		if (Input.GetKeyDown (KeyCode.E) && isRotating == false && PlayerPrefs.GetString ("CameraMovementUnlocked") == "true")
		{
			ECam ();
		}
		if (Input.GetKeyDown (KeyCode.Q) && isRotating == false && PlayerPrefs.GetString ("CameraMovementUnlocked") == "true")
		{
			QCam ();
		}
		
		if (isRotating == true)
		{
			MoveCamera ();
		}
#endif
		if (Input.acceleration.x > -0.3f && Input.acceleration.x < 0.3f && Time.timeScale == 1)
		{
			if (canRotateWithAccelerometer == false)
			{
				canRotateWithAccelerometer = true;
			}
		}
		else if (Input.acceleration.x > 0.4f && isRotating == false && canRotateWithAccelerometer == true 
		         && GUIMain.PauseIsOpen == false && PlayerPrefs.GetString ("level3by3_2enabled") == "true")
		{
			//  because we need to return the phone in the initial position to be able to rotate the camera again
			canRotateWithAccelerometer = false;
			ECam ();
		}
		else if (Input.acceleration.x < -0.4f && isRotating == false && canRotateWithAccelerometer == true
		         && GUIMain.PauseIsOpen == false && PlayerPrefs.GetString ("level3by3_2enabled") == "true")
		{
			canRotateWithAccelerometer = false;
			QCam ();
		}
		if (isRotating == true)
		{
			MoveCamera ();
		}
		if (mC != null)
		{
			DependsOnCameraPos ();
		}
	}


	// Update is called once per frame
	/*void OnGUI () {
		if (GUI.Button (new Rect(Screen.width-(2 * distFromRight), Screen.height - (distFromBot), boxWidth,boxHeight),"Right")
		    && isRotating == false)
		{
			mC.SwapDirectionsRight();
			mC.SwapDistanceToBorder ();

			if (CamPosCounter >= 1)
			{
				CamPosCounter--;
			} 
			else 
			{
				CamPosCounter = 3;
			}
			isRotating = true;
			distBetCamAndAim = Vector3.Distance (transform.position, CamPos[CamPosCounter]);
		}
		if (GUI.Button (new Rect (distFromRight, Screen.height - (distFromBot), boxWidth, boxHeight), "Left")
		    && isRotating == false) 
		{
			mC.SwapDirectionsLeft ();
			mC.SwapDistanceToBorder ();
			if (CamPosCounter <= 2)
			{
				CamPosCounter++;
			} 
			else 
			{
				CamPosCounter = 0;
			}

			isRotating = true;
			distBetCamAndAim = Vector3.Distance (transform.position, CamPos[CamPosCounter]);
		}
	}
*/

	void MoveCamera () {
		//  while changing the positions change camera's angle
		if (Vector3.Distance (transform.position, CamPos[CamPosCounter]) != 0)
		{
			transform.position = Vector3.MoveTowards(transform.position, CamPos[CamPosCounter],
			                                         distBetCamAndAim * Time.deltaTime);
			
			Quaternion cameraRotationQ = Quaternion.LookRotation (CamTarget - transform.position);
			cameraRotation = new Vector3 (cameraRotationQ.eulerAngles.x, cameraRotationQ.eulerAngles.y, cameraRotationQ.eulerAngles.z);
			transform.eulerAngles = cameraRotation;
		}
		else
		{
			isRotating = false;
		}
	}


	public void DependsOnCameraPos () {
		if (CamPosCounter == 0) {
			mC.PositionZero ();
		}
		else if (CamPosCounter == 1) {
			mC.PositionOne ();
		}
		else if (CamPosCounter == 2) {
			mC.PositionTwo ();
		}
		else if (CamPosCounter == 3) {
			mC.PositionThree ();
		}
	}


	//  we need to know which shape's directions we need to change
	public void GetTheShapeToChangeItsDirections (GameObject shape) {
		shapeToChangeDirections = shape;
//		Debug.Log (shapeToChangeDirections.name);
		mC = (MovingController) shapeToChangeDirections.GetComponent (typeof (MovingController));
	}
}

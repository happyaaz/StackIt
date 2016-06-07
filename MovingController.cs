using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Linq;


public class MovingController : MonoBehaviour {

	private GameObject root;
	private ControlRigidbodies cr;


	[HideInInspector]
	public Vector3 dir1, dir2, dir3, dir4, rot1, rot2, rot3, rot4;


	cameraController camCon;

	DrawRaysCube [] drc;

	private ActualRotation actualRot;
	[HideInInspector]

	//our current automatic falling down speed
	private float curSpeed;

	GameObject directions;
	DirectionsFollowingCamera dfc;
	ScoreController sc;
	LineScore lS;

	//sounds
	public AudioClip Swosh;
	public AudioClip MoveBump;
	public AudioClip CantMove;

	public LayerMask mask;

	private bool hitSpace = false;

	string nameOfTheClickedShape = string.Empty;


	public enum StatesOfTheShape 
	{
		Idle,  //  Idle
		Rotate,
		Drop,
		Drag,  //  Move in the space
	}
	public StatesOfTheShape currentState;

	//  mobile
	Vector3 startPos = Vector3.zero;
	Vector3 endPos = Vector3.zero;
	float angleBetweenVectors;
	Touch newTouch;
	private Vector2 newClick = new Vector2(0,0);
	public bool readyToRotateWhileMoving = false;
	public bool isPossibleToRotate = false;
	float deltaPos;
	bool endMovement = false;
	float distanceOfSwipe;
	float delay;

	public Texture ringOne;
	public Texture ringTwo;

	private Event drawingCircles;
	private bool stopDrawing = false;
	private Vector2 curTouchPos;
	private Vector2 sizesOfCircle;
	private float angleOfRotationForGUI = 0;
	Vector2 pivotPoint = Vector2.zero;
	private bool allowRotateTexture;
	private bool avoidChangingPositionWhileRotating = true;
	//  optimising movements
	string direction = string.Empty;
	bool iGotDirection = false;
	bool weAreAtTheAngle = false;

	//  new gameplay
	public int timer;
	public bool smthBelow = false;


	public delegate void ChangeState ();
	public static event ChangeState changingState;

	GUIMain guiMain;

	//  advanced tutorial
	List <TutorialLevel.TutorialStates> statesList = new List <TutorialLevel.TutorialStates> ();
	int numberOfTheState = 0;
	public bool weAccomplishedTutorial = false;

	GameObject shapesManager;
	ShapesManager sM;

	GameObject tutorial;
	TutorialLevel tL;

	//  ghost shape
	public GameObject ghostShape;
	public GameObject realGhostShape;
	GameObject childTNT;
	public bool Dropping;

	bool sofaHigher = false;

	//Web gestures
	private bool one_click = false;
	private bool timer_running;
	private float timer_for_double_click;	
	//this is how long in seconds to allow for a double click
	private float doubleClickDelay = 0.3f;
	BombGhostShape bgs;

	public bool spaceWasHitBool = false;


	void Start () {

		guiMain = Camera.main.GetComponent <GUIMain> ();


		shapesManager = GameObject.Find ("ShapesManager");
		sM = shapesManager.GetComponent <ShapesManager> ();
		tutorial = GameObject.Find ("Tutorial");
		tL = tutorial.GetComponent <TutorialLevel> ();

		//  just for the first shape
		if (ChangeableVariables.levelWeAreIn == "3x3_1" && sM.activeShapes.Count < 1 && 
		    PlayerPrefs.GetString ("FinidhedFirstTutorial") == "false")
		{
			statesList.Add (TutorialLevel.TutorialStates.RotateHorizontally);
			statesList.Add (TutorialLevel.TutorialStates.RotateVertically);
			statesList.Add (TutorialLevel.TutorialStates.Drag);
			statesList.Add (TutorialLevel.TutorialStates.DoubleTap);

			tL.currentState = statesList [numberOfTheState];
			tL.CurrentTexture ();
			tL.NewPositionsOfTheVectors ();
			numberOfTheState ++;
			weAccomplishedTutorial = false;
		}
		else
		{
			weAccomplishedTutorial = true;
		}


		SwapTexture ();


		currentState = StatesOfTheShape.Idle;
		sc = GameObject.Find ("Platform").GetComponent <ScoreController> ();
		lS = GameObject.Find ("Platform").GetComponent <LineScore> ();

		lS.activeObject = this.gameObject;

		actualRot = transform.GetComponentInChildren <ActualRotation> ();
		drc = transform.GetComponentsInChildren <DrawRaysCube> ();

		directions = GameObject.FindGameObjectWithTag ("Directions");
		dfc = (DirectionsFollowingCamera) directions.GetComponent (typeof (DirectionsFollowingCamera));

		curSpeed = dfc.speedOfFalling;

		//  get directions
		//  each time we spawn a new shape we need to know
		//  which directions we used for the last one
		dir1 = dfc.dir1;
		dir2 = dfc.dir2;
		dir3 = dfc.dir3;
		dir4 = dfc.dir4;

		rot1 = Vector3.down;
		rot2 = Vector3.up;


		camCon = (cameraController) Camera.main.gameObject.GetComponent (typeof (cameraController));
		camCon.GetTheShapeToChangeItsDirections (this.gameObject);
		camCon.DependsOnCameraPos ();
		//  custom positions - most of details don't land in the center
		if (transform.gameObject.name.Contains ("Sofa")) 
		{
			transform.position = new Vector3 (ChangeableVariables.LShapeX, transform.position.y + 1.5f, ChangeableVariables.LShapeZ);
			SpawnGhostShape ();
		}
		else if (transform.gameObject.name.Contains ("CanShark")) 
		{
			transform.position = new Vector3 (ChangeableVariables.IShapeX, transform.position.y + 2.5f, ChangeableVariables.IShapeZ);
			SpawnGhostShape ();
		}
		else if (transform.gameObject.name.Contains ("TV"))
		{
			transform.position = new Vector3 (ChangeableVariables.OShapeX, transform.position.y + 0.5f, ChangeableVariables.OShapeZ);
			SpawnGhostShape ();
		}
		else if (transform.gameObject.name.Contains ("Car"))
		{
			transform.position = new Vector3 (ChangeableVariables.TShapeX, transform.position.y + 1.5f, ChangeableVariables.TShapeZ);
			SpawnGhostShape ();
		}
		else if (transform.gameObject.name.Contains ("Microwave"))
		{
			transform.position = new Vector3 (ChangeableVariables.ZShapeX, transform.position.y + 1, ChangeableVariables.ZShapeZ);
			SpawnGhostShape ();
		}
		else if (transform.gameObject.name.Contains ("Barrels"))
		{
			transform.position = new Vector3 (ChangeableVariables.SShapeX, transform.position.y + 0.5f, ChangeableVariables.SShapeZ);
			SpawnGhostShape ();
		}
		else if (transform.gameObject.name.Contains ("Boxes"))
		{
			transform.position = new Vector3 (ChangeableVariables.YShapeX, transform.position.y + 0.5f, ChangeableVariables.YShapeZ);
			SpawnGhostShape ();
		}
		else if (transform.gameObject.name.Contains ("Fridge"))
		{
			transform.position = new Vector3 (ChangeableVariables.NShapeX, transform.position.y + 0.5f, ChangeableVariables.NShapeZ);
			SpawnGhostShape ();
		}
		else if (transform.gameObject.name.Contains ("BottleBox"))
		{
			transform.position = new Vector3 (ChangeableVariables.TwoShapeX, transform.position.y + 0.5f, ChangeableVariables.TwoShapeZ);
			SpawnGhostShape ();
		}
		else if (transform.gameObject.name.Contains ("WC"))
		{
			transform.position = new Vector3 (ChangeableVariables.ThreeShapeX, transform.position.y + 0.5f, ChangeableVariables.ThreeShapeZ);
			SpawnGhostShape ();
		}
		else if (transform.gameObject.name.Contains ("TNT"))
		{
			transform.position = new Vector3 (ChangeableVariables.OneShapeX, transform.position.y, ChangeableVariables.OneShapeZ);
			bgs = GetComponentInChildren <BombGhostShape> (); 
			SpawnGhostShape ();
		}
		root = transform.root.gameObject;
		cr = (ControlRigidbodies) root.GetComponent (typeof (ControlRigidbodies));
	}


	public void SpawnGhostShape () {
		if (! (transform.gameObject.name.Contains ("TNT")))
		{
			realGhostShape = Instantiate (ghostShape, transform.position, transform.rotation) as GameObject;
		}
		else
		{
			bgs.SpawnGhost ();
			//Debug.DrawRay (transform.position - Vector3.left / 2 - Vector3.back / 2, Vector3.down * 20, Color.red, 1);
		}
	}


	public void DestroyGhostShape () {

		Destroy (GameObject.FindGameObjectWithTag ("GhostShape"));
		
	}

	void OnGUI () {

		/*
		GUI.Label (new Rect (10, 300, 200, 20), "SwipeDis = " + distanceOfSwipe);
		GUI.Label (new Rect (10, 320, 200, 20), "Direction = " + direction);
		GUI.Label (new Rect (10, 340, 200, 20), "StartPos = " + startPos);
		Event e = Event.current;
		Vector2 pivotPoint = new Vector2 (e.mousePosition.x - 50, e.mousePosition.y - 50);
		GUIUtility.RotateAroundPivot (angleOfRotationForGUI, pivotPoint);
		GUI.DrawTexture (new Rect (e.mousePosition.x - 50, e.mousePosition.y - 50, 100, 100), ringOne);
		*/

		if (nameOfTheClickedShape != string.Empty && stopDrawing != true)
		{
			//  rotate the texture only once
			if (avoidChangingPositionWhileRotating == false)
			{
				pivotPoint = new Vector2 (curTouchPos.x, curTouchPos.y);
				GUIUtility.RotateAroundPivot (angleOfRotationForGUI, pivotPoint);
			}

			GUI.DrawTexture (new Rect (curTouchPos.x - sizesOfCircle.x / 2, curTouchPos.y - sizesOfCircle.y / 2, sizesOfCircle.x, sizesOfCircle.y), ringOne);
		}

	//	GUI.Label (new Rect (10, 300, 300, 20), "mousePosition = " + e.mousePosition);

		//GUI.DrawTexture (new Rect (e.mousePosition.x - 45 / 2, e.mousePosition.y - 45 / 2, 45, 45), ringOne);
	//	GUI.Label (new Rect (10, 350, 300, 20), "Dist = " + distanceOfSwipe);
	}

	void AvoidSmthWhileROtatingTexture () {
		avoidChangingPositionWhileRotating = true;
	}

	// Update is called once per frame
	void Update () {

#if UNITY_EDITOR
		if (cr.currentState == ControlRigidbodies.PossibleStates.TurnOff && Time.timeScale == 1)
		{
			if (Input.GetKeyDown (KeyCode.Space) && hitSpace == false && currentState == StatesOfTheShape.Idle
			    && (tL.currentState == TutorialLevel.TutorialStates.DoubleTap ||
			    weAccomplishedTutorial == true))
			{
				SpaceWasHit ();
			}
			//  we MOVE the object DOWN and can move it to different directions in the case if
			//  nothing is below it
			if (smthBelow == false)
			{
				if (currentState == StatesOfTheShape.Drop)
				{
					transform.position = Vector3.MoveTowards (transform.position, 
				                                          new Vector3 (transform.position.x, transform.position.y - 1, transform.position.z), 
				                                          Time.deltaTime / 0.15f);
					guiMain.Dropping = true;
				}
				//  rotation
				if (Input.GetKeyDown (KeyCode.D) && currentState == StatesOfTheShape.Idle)
				{
					RotateIfPossible (rot1);
				}
				else if (Input.GetKeyDown (KeyCode.A) && currentState == StatesOfTheShape.Idle)
				{
					RotateIfPossible (rot2);
				}
				else if (Input.GetKeyDown (KeyCode.W) && currentState == StatesOfTheShape.Idle)
				{
					RotateIfPossibleVertically (rot3);
				}
				else if (Input.GetKeyDown (KeyCode.S) && currentState == StatesOfTheShape.Idle)
				{
					RotateIfPossibleVertically (rot4);
				}

				//  movements
				if (Input.GetKeyDown (KeyCode.DownArrow) && currentState == StatesOfTheShape.Idle)
				{
					MoveIfPossible (dir3);
					angleOfRotationForGUI = 270;
				}
				else if (Input.GetKeyDown (KeyCode.UpArrow) && currentState == StatesOfTheShape.Idle)
				{
					MoveIfPossible (dir1);
					angleOfRotationForGUI = 90;
				}
				else if (Input.GetKeyDown (KeyCode.LeftArrow) && currentState == StatesOfTheShape.Idle)
				{
					MoveIfPossible (dir2);
					angleOfRotationForGUI = 0;
					
				}
				else if (Input.GetKeyDown (KeyCode.RightArrow) && currentState == StatesOfTheShape.Idle)
				{
					MoveIfPossible (dir4);
					angleOfRotationForGUI = 180;
				}
				
			}
		}
#endif
		//  we can do smth with one of the shapes only in this case
		if (cr.currentState == ControlRigidbodies.PossibleStates.TurnOff && Time.timeScale == 1)
		{
			
			//  we MOVE the object DOWN and can move it to different directions in the case if
			//  nothing is below it
			if (smthBelow == false)
			{
				if (currentState == StatesOfTheShape.Drop)
				{
					transform.position = Vector3.MoveTowards (transform.position, 
					                                          new Vector3 (transform.position.x, transform.position.y - 1, transform.position.z), 
					                                          Time.deltaTime / curSpeed);
				}


				foreach (Touch touch in Input.touches)
				{
					//  double tap
					if (touch.tapCount == 2 && Time.timeScale != 0 && (tL.currentState == TutorialLevel.TutorialStates.DoubleTap ||
					    weAccomplishedTutorial == true))
					{
						//  stop drawing circles (because they will appear anyways)
						stopDrawing = true;
						if (ChangeableVariables.levelWeAreIn == "3x3_1")
						{
							//  fire an event
							if (changingState != null)
							{
								changingState ();


							}
							//TutorialLevel.currentState = TutorialLevel.TutorialStates.None;
						}
						//  make the shape fall

							SpaceWasHit ();

					}
				}

				//  we move and rotate the shapes with only ONE finger
				if (Input.touchCount == 1) {
					
					newTouch = Input.GetTouch (0);

					Ray ray = Camera.main.ScreenPointToRay(newTouch.position);
					RaycastHit hit;
					

					if (newTouch.phase == TouchPhase.Began && currentState == StatesOfTheShape.Idle)
					{
						//  each time we need to update the standard distance of swipe
						distanceOfSwipe = Screen.width / 4;  // 180

						if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 9)) 
						{
							//  now we can show the divided texture
							SwapTexture ();
							nameOfTheClickedShape = hit.transform.root.gameObject.name;

							//  update it
							endMovement = false;
							stopDrawing = false;
							//  nice feature to match the GUI with finger's position
							drawingCircles = Event.current;
							sizesOfCircle = new Vector2 (Screen.width / 7.2f, Screen.width / 7.2f);
							//  prevent a shape from moving when we click on it (kind of a double tap)
							delay = Time.time;
							//  each time set the pivot point to zero (possible to delete)
							pivotPoint = Vector2.zero;

							curTouchPos = new Vector2 (drawingCircles.mousePosition.x, drawingCircles.mousePosition.y);
							nameOfTheClickedShape = hit.transform.root.gameObject.name;
							allowRotateTexture = false;

							//  optimizing movements
							iGotDirection = false;
							direction = string.Empty;
							weAreAtTheAngle = false;
						}
						else
						{
							//  else we need to rotate a shape
							nameOfTheClickedShape = string.Empty;
						}
						startPos = newTouch.position;

					}

					else if (newTouch.phase == TouchPhase.Moved && currentState == StatesOfTheShape.Idle
					         && nameOfTheClickedShape != string.Empty)
					{
						endPos = newTouch.position;
						//  to indicate the pivot point of the circle to draw
						//  ???
						drawingCircles = Event.current;
						curTouchPos = new Vector2 (drawingCircles.mousePosition.x, drawingCircles.mousePosition.y);

						//  optimizing movements
						if (iGotDirection == false && ChangeableVariables.levelWeAreIn.Contains ("5"))
						{
							GetToKnowDir ();
						}

						//  change texture
						if (Vector3.Distance (startPos, endPos) > Screen.width / 55 && allowRotateTexture == false)
						{
							//  call it all only once
							allowRotateTexture = true;
							//  exchange the textures
							SwapTexture ();
							//  rotate the texture only once
							avoidChangingPositionWhileRotating = false;
							//  calculate how we should rotate the texture
						}
						//  constantle rotate the texture
						RotateTexture ();

						//  stop drawing the circles after a  certain distance
						/*
						if (Vector3.Distance (startPos, endPos) > Screen.width / 10f)
						{
							stopDrawing = true;
						}
						*/
						//  
						if (Vector3.Distance (startPos, endPos) > distanceOfSwipe)
						{
							CalculatingAreasOfMovements ();
						}
					}
					//  if we don't move a finger
					else if (newTouch.phase == TouchPhase.Stationary && currentState == StatesOfTheShape.Idle 
					         && startPos != Vector3.zero)
					{
						distanceOfSwipe = Screen.width / 4;  // 180
					}
					//  if we stop moving a finger
					else if (newTouch.phase == TouchPhase.Ended && currentState == StatesOfTheShape.Idle 
						         && startPos != Vector3.zero)
					{
						//  if we don't hold down the shape
						if (nameOfTheClickedShape == string.Empty)
						{
							FindingAngle ();
						}
						//  if we hold down the shape and we can move it plus it is not a double tap 
						//  end of movement:
						//  to prevent moving the shape after we stopped moving the finger
						else if (Time.time - delay > 0.2f && endMovement == false && Vector3.Distance (startPos, endPos) > distanceOfSwipe)
						{
							endMovement = true;
							CalculatingAreasOfMovements ();
						}

						//  in case we still hold down the shape when it should fall down
						allowRotateTexture = true;
						stopDrawing = true;
						nameOfTheClickedShape = string.Empty;
						distanceOfSwipe = Screen.width / 4; 
					}

					//  increase sizes of the circle
					//  if we holding down the shape and we still draw the circle
					if (nameOfTheClickedShape != string.Empty && stopDrawing != true)
					{
						if (sizesOfCircle.x < Screen.width / 3.2f && sizesOfCircle.y < Screen.width / 3.2f)
						{
							sizesOfCircle = new Vector2 (sizesOfCircle.x + Screen.width / 15.6f, 
							                             sizesOfCircle.y + Screen.width / 15.6f);
						}
					}
				}
			}
		}
	}

	//  it get only the first direction
	//  somehow I need to figure out all the points and positions
	//  to optimize everything
	//  and it depends on camera's position
	//  and since we decrease the distance of a swipe we need to change it in a diff.funct
	//  I think something like if (direction == empty) - standard
	//  else - custom
	void GetToKnowDir () {

		iGotDirection = true;
		Vector3 vector = endPos - startPos;
		float angleBetweenVectors = Mathf.Atan2 (vector.y, vector.x) * Mathf.Rad2Deg;
		
		if (angleBetweenVectors > 0 && angleBetweenVectors < 90)
		{
			direction = "dir1";
		}
		else if (angleBetweenVectors > 90 && angleBetweenVectors < 180) 
		{
			direction = "dir2";
		}
		else if (angleBetweenVectors < -90 && angleBetweenVectors > -180)
		{
			direction = "dir3";
		}
		else if (angleBetweenVectors > -90 && angleBetweenVectors < 0)
		{
			direction = "dir4";
		}

		if (camCon.CamPosCounter == 0)
		{
			switch (direction)
			{
				case ("dir2"):
					//  far side
					if (startPos.x > Screen.width / 1.8f && startPos.x < Screen.width / 1.44f)  //  400, 500
					{
						distanceOfSwipe = Screen.width / 7.2f; // 100
					}
					else if (startPos.x > Screen.width / 1.44f && startPos.x < Screen.width)  //  500, 700 ? 720
					{
						distanceOfSwipe = Screen.width / 5.53f; //  130
					}
					//  close side
					else if (startPos.x > Screen.width / 3.6f && startPos.x < Screen.width / 2.88f)  //  200, 250
					{
						distanceOfSwipe = Screen.width / 5.53f;  //  130
						weAreAtTheAngle = true;
					}
					else if (startPos.x > Screen.width / 7.2f && startPos.x < Screen.width / 3.6f)  //  100, 200
					{
						distanceOfSwipe = Screen.width / 7.2f;  // 100
						weAreAtTheAngle = true;
					}
					break;
				case ("dir4"):
					if (startPos.x > Screen.width / 1.8f && startPos.x < Screen.width / 1.44f)
					{
						distanceOfSwipe = Screen.width / 5.53f;  //  130
					}
					else if (startPos.x > Screen.width / 1.44f && startPos.x < Screen.width)
					{
						distanceOfSwipe = Screen.width / 7.2f;  //  100
					}
					else if (startPos.x > Screen.width / 3.6f && startPos.x < Screen.width / 2.88f)
					{
						distanceOfSwipe = Screen.width / 7.2f;  //  100
						weAreAtTheAngle = true;
					}
					else if (startPos.x > Screen.width / 7.2f && startPos.x < Screen.width / 3.6f)
					{
						distanceOfSwipe = Screen.width / 5.53f;  // 130
						weAreAtTheAngle = true;
					}
					break;

					default:
						break;
			}
		}
	}



	void SwapTexture ()
	{
		Texture temp = ringOne;
		ringOne = ringTwo;
		ringTwo = temp;
	}


	void RotateTexture () {
		//  calculate the angle when moving a finger
		Vector3 vector = endPos - startPos;
		float angleBetweenVectors = Mathf.Atan2 (vector.y, vector.x) * Mathf.Rad2Deg;
		
		if (angleBetweenVectors > 0 && angleBetweenVectors < 90)
		{
			angleOfRotationForGUI = 90;
		}
		else if (angleBetweenVectors > 90 && angleBetweenVectors < 180) 
		{
			angleOfRotationForGUI = 0;
		}
		else if (angleBetweenVectors < -90 && angleBetweenVectors > -180)
		{
			angleOfRotationForGUI = 270;
		}
		else if (angleBetweenVectors > -90 && angleBetweenVectors < 0)
		{
			angleOfRotationForGUI = 180;
		}

	}


	public void SpaceWasHit () {
		//  check if the shape is outside the borders
		//actualRot.CheckIfItIsOutside ();
		if (guiMain.preLevelWOpen == false && spaceWasHitBool == false)
		{
			if (lS.cantSwitchWhenVictoryOrLose == false)
			{
				spaceWasHitBool = true;
				this.tag = "Stuck";
				DestroyGhostShape ();
				curSpeed = 0.05f;
				sc.DropShape ();
				hitSpace = true;
				currentState = StatesOfTheShape.Drop;
				//  stop drawing the circles
				stopDrawing = true;
				nameOfTheClickedShape = string.Empty;
				TutorialLevel.cornerText = string.Empty;

				//  adv tutorial
				if (weAccomplishedTutorial == false)
				{
					weAccomplishedTutorial = true;
				}
			}
		}
	}


	void CalculatingAreasOfMovements () {

		if (nameOfTheClickedShape.Contains ("Ghost"))
		{
			return;
		}

		//  small distances, decrease distances
		//  if 5x5
		if (weAreAtTheAngle == false)
		{
			if (distanceOfSwipe > Screen.width / 7.2f) // 100
			{
				distanceOfSwipe -= Screen.width / 13.1f;  // 55
			}
			else if (distanceOfSwipe < Screen.width / 7.2f && distanceOfSwipe > Screen.width / 14.4f)  //  100, 50
			{
				distanceOfSwipe -= Screen.width / 28.8f;  // 25
			}
			else
			{
				distanceOfSwipe = Screen.width / 4;  // 180
			}
		}
		//  4x4, 3x3
		else
		{
			if (distanceOfSwipe > Screen.width / 7.2f)  // 100
			{
				distanceOfSwipe -= Screen.width / 13.1f;  //  55
			}
			else if (distanceOfSwipe == Screen.width / 7.2f)  //  100
			{
				distanceOfSwipe -= Screen.width / 72;  // 10
			}
			else
			{
				distanceOfSwipe = Screen.width / 4;  // 180
			}
		}
		endMovement = true; 
		Vector3 vector = endPos - startPos;
		angleBetweenVectors = Mathf.Atan2 (vector.y, vector.x) * Mathf.Rad2Deg;
		startPos = Vector3.zero;
		
		if (angleBetweenVectors > 0 && angleBetweenVectors < 90)
		{
			MoveIfPossible (dir1);
		}
		else if (angleBetweenVectors > 90 && angleBetweenVectors < 180) 
		{
			MoveIfPossible (dir2);
		}
		else if (angleBetweenVectors < -90 && angleBetweenVectors > -180)
		{
			MoveIfPossible (dir3);
		}
		else if (angleBetweenVectors > -90 && angleBetweenVectors < 0)
		{
			MoveIfPossible (dir4);
		}
		startPos = endPos;
	}

	//  rotate
	void FindingAngle () {
		
		//endPos = Input.mousePosition;
		endPos = newTouch.position;
		/*
		if ( startPos.y > ((Screen.height / 100) * 80) && endPos.y < ((Screen.height / 100) * 20))
		{
			currentState = StatesOfTheShape.Drop;
			return;
		}
		*/
		//  to prevent single taps
		if (Vector3.Distance (startPos, endPos) < 150)
		{
			return;
		}
		Vector3 vector = endPos - startPos;
		angleBetweenVectors = Mathf.Atan2 (vector.y, vector.x) * Mathf.Rad2Deg;
		startPos = Vector3.zero;

		if (angleBetweenVectors > -40 && angleBetweenVectors < 40)
		{
			RotateIfPossible (rot1);
		}
		else if ((angleBetweenVectors < -140 && angleBetweenVectors > -180) || (angleBetweenVectors > 140 && angleBetweenVectors < 180)) 
		{
			RotateIfPossible (rot2);
		}
		else if (angleBetweenVectors > 40 && angleBetweenVectors < 140)
		{
			RotateIfPossibleVertically (rot3);
		}
		else if (angleBetweenVectors > -140 && angleBetweenVectors < -40)
		{
			RotateIfPossibleVertically (rot4);
		}
	}
	


	void MoveIfPossible (Vector3 direction)
	{
		if (tL.currentState == TutorialLevel.TutorialStates.Drag ||
		    weAccomplishedTutorial == true)
		{
			bool canMove = true;
			//  for each child we call this function
			foreach (DrawRaysCube drawRays in drc)
			{
				//  if it doesn't fullfil the requirements
				//  we cannot rotate and stop executing function
				canMove = drawRays.checkSphere (direction);
				if (canMove == false)
				{
					if (GUIMain.soundEffectMuteBool == false)
						audio.PlayOneShot (CantMove,1f);
					break;
				}
			}
			
			if (canMove) 
			{
				transform.position += direction;
				if (GUIMain.soundEffectMuteBool == false)
				{
					audio.PlayOneShot(MoveBump,1f);
				}
				if (tL.currentState == TutorialLevel.TutorialStates.Drag &&  PlayerPrefs.GetString ("FinidhedFirstTutorial") == "false")
				{
					tL.currentState = statesList [numberOfTheState];
					tL.CurrentTexture ();
					tL.NewPositionsOfTheVectors ();
					numberOfTheState ++;
				}
				DestroyGhostShape ();
				SpawnGhostShape ();
			}
		}
	}



	void RotateIfPossible (Vector3 direction) {
		//  we can rotate by defaul
		if (guiMain.preLevelWOpen == false)
		{
			if ((tL.currentState == TutorialLevel.TutorialStates.RotateHorizontally) ||
			    weAccomplishedTutorial == true && lS.cantSwitchWhenVictoryOrLose == false)
			{
				bool canRotate = true;

				//  for each child we call this function
				foreach (DrawRaysCube drawRays in drc)
				{
					//  if it doesn't fullfil the requirements
					//  we cannot rotate and stop executing function
					canRotate = drawRays.DrawRaysRotateInside ();
					if (canRotate == false)
					{
						if (GUIMain.soundEffectMuteBool == false)
							audio.PlayOneShot (CantMove,0.01f);
						break;
					}
				}
				
				if (canRotate) 
				{
					DestroyGhostShape ();
					currentState = StatesOfTheShape.Rotate;
					//  rotate in a certain direction direction
					actualRot.RotatePiece (direction);
					if (GUIMain.soundEffectMuteBool == false)
					{
						audio.PlayOneShot (Swosh,1f);
					}

					//  show the tutorial text only once
					if (tL.currentState == TutorialLevel.TutorialStates.RotateHorizontally 
					    && PlayerPrefs.GetString ("FinidhedFirstTutorial") == "false")
					{
						tL.currentState = statesList [numberOfTheState];
						tL.CurrentTexture ();
						tL.NewPositionsOfTheVectors ();
						numberOfTheState ++;
					}
					/*
					if (transform.gameObject.name.Contains ("Sofa") && sofaHigher == true) 
					{
						transform.position += Vector3.down * 1.5f;
						sofaHigher = false;
					}
					*/
				}
			}
		}
	}


	void RotateIfPossibleVertically (Vector3 direction) {

		if (guiMain.preLevelWOpen == false)
		{
			if (tL.currentState == TutorialLevel.TutorialStates.RotateVertically ||
			    weAccomplishedTutorial == true  && lS.cantSwitchWhenVictoryOrLose == false)
			{
				bool canRotate = true;
				//  for each child we call this function
				foreach (DrawRaysCube drawRays in drc)
				{
					//  if it doesn't fullfil the requirements
					//  we cannot rotate and stop executing function
					canRotate = drawRays.RotateVertically ();
					if (canRotate == false)
					{
						if (GUIMain.soundEffectMuteBool == false)
							audio.PlayOneShot (CantMove,0.01f);
						break;
					}
				}
				
				if (canRotate) 
				{
					/*
					if (transform.gameObject.name.Contains ("Sofa") && sofaHigher == false) 
					{
						transform.position += Vector3.up * 1.5f;
						sofaHigher = true;
					}
					else if (transform.gameObject.name.Contains ("Sofa") && sofaHigher == true) 
					{
						transform.position += Vector3.down * 1.5f;
						sofaHigher = false;
					}
					*/
					DestroyGhostShape ();
					currentState = StatesOfTheShape.Rotate;
					//  rotate in a certain direction direction
					actualRot.RotatePiece (direction);

					if (GUIMain.soundEffectMuteBool == false)
					{
						audio.PlayOneShot (Swosh,1f);
					}

					if (tL.currentState == TutorialLevel.TutorialStates.RotateVertically &&
					    PlayerPrefs.GetString ("FinidhedFirstTutorial") == "false")
					{
						tL.currentState = statesList [numberOfTheState];
						tL.CurrentTexture ();
						tL.NewPositionsOfTheVectors ();
						numberOfTheState ++;
					}
				}
			}
		}
	}

	//  connected to the camera
	public void SwapDirectionsRight () {
		Vector3 temp = dir1;
		dir1 = dir2;
		dir2 = dir3;
		dir3 = dir4;
		dir4 = temp;
	}

	public void SwapDirectionsLeft () {
		Vector3 temp = dir4;
		dir4 = dir3;
		dir3 = dir2;
		dir2 = dir1;
		dir1 = temp;
	}


	public void PositionZero () {
		rot3 = Vector3.right;
		rot4 = Vector3.left;
	}


	public void PositionOne () {
		rot3 = Vector3.back;
		rot4 = Vector3.forward;
	}


	public void PositionTwo () {
		rot3 = Vector3.left;
		rot4 = Vector3.right;
	}


	public void PositionThree () {
		rot3 = Vector3.forward;
		rot4 = Vector3.back;
	}
}

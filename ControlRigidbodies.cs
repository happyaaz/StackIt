using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class ControlRigidbodies : MonoBehaviour {

	public enum PossibleStates {
		TurnOn,
		TurnOff,
		DontTouch,
		NoControl//  grounded
	}
	public PossibleStates currentState;

	CubeController [] cC;
	MovingController mC;

	//  automatically spawn new shape
	private GameObject spawner;
	private SpawnCubes sC;

	GameObject directions;
	DirectionsFollowingCamera dfc;

	//sound
	public AudioClip landingSound;


	public bool changeGravity = false;
	DisablingScripts dS;

	ZeroOverlappingCases zOc;

	ScoreController scCtrl; 
	
	TutorialLevel tL;

	public GameObject bioHazardLiquidHolder;

	void Start () {

		if (ChangeableVariables.levelWeAreIn == "3x3_1" || ChangeableVariables.levelWeAreIn == "3x3_2")
		{
			tL = GameObject.Find ("Tutorial").GetComponent <TutorialLevel> ();
		}

		//  fire an event to change shapes directions (how we control the shape) - "switch feature"
		//  because we can rotate the camera before we decide to switch shapes
		//  and because the new shape each time takes the directions that used to be
		//  we need to change them
		SpawnCubes.switchMovements += ChangeDirections;

		scCtrl = GameObject.Find ("Platform").GetComponent <ScoreController> ();

		dS = transform.GetComponent <DisablingScripts> ();

		zOc = transform.GetComponentInChildren <ZeroOverlappingCases> ();

		currentState = PossibleStates.TurnOff;
		cC = GetComponentsInChildren <CubeController> ();

		spawner = GameObject.FindGameObjectWithTag ("Spawner");
		sC = (SpawnCubes) spawner.GetComponent (typeof (SpawnCubes));
		mC = GetComponent <MovingController> ();

		directions = GameObject.FindGameObjectWithTag ("Directions");
		dfc = (DirectionsFollowingCamera) directions.GetComponent (typeof (DirectionsFollowingCamera));
	}
	
	// Update is called once per frame
	void Update () {
		if (currentState == PossibleStates.NoControl) {
			Rigidbody [] AllRgdB = GetComponentsInChildren <Rigidbody> ();
			foreach (Rigidbody eachRgdB in AllRgdB) {
				eachRgdB.constraints = RigidbodyConstraints.None;
				eachRgdB.useGravity = true;
				eachRgdB.mass = 7;
				eachRgdB.isKinematic = false;
				//				if (!exploded){

//				eachRgdB.AddExplosionForce (100, new Vector3 (0,7,0), 30, 3);
				eachRgdB.AddExplosionForce (100, new Vector3 (1,4,1), 30, 3);
				eachRgdB.AddExplosionForce (100, new Vector3 (-1,4,-1), 30, 3);
				eachRgdB.AddExplosionForce (100, new Vector3 (-1,4,1), 30, 3);
				eachRgdB.AddExplosionForce (100, new Vector3 (1,4,-1), 30, 3);
				//					eachRgdB.AddExplosionForce (100, new Vector3 (0.5f,0,0.5f), 30,3);
				//					exploded = true;
				//				}
			}
		}
		if (currentState == PossibleStates.TurnOn) {
			//  quite an important function
			//  after you are done with the last cube in the shape
			//  you can do whatever you want
			CubeController last = cC.Last ();
			foreach (CubeController cubeCon in cC)
			{
				if (tL != null)
				{
					if (tL.currentState != TutorialLevel.TutorialStates.None)
					{
						tL.currentState = TutorialLevel.TutorialStates.None;
					}
				}
				if (cubeCon.Equals (last)) 
				{
					TutorialLevel.showGuiText = false;
					cubeCon.FreezeY ();
					//  if there are some bugs with weird positioning - just comment out these two lines
					//  cubes are overlapping
					zOc.CheckIfCubesAreIdentical ();
					//  cubes have zero position
					zOc.CheckIfTagIsZero ();
					//  stop casting shadowas
					cubeCon.StopCastingShadows ();
					//  if it is a bomb, blow it up.
					Bomb blowItUp = GetComponentInChildren <Bomb> ();
					BioHazardMeltdown [] meltEerythingDown = GetComponentsInChildren <BioHazardMeltdown> ();

					if (blowItUp != null)
					{
						blowItUp.BlowTheBombUp ();
						//  change directions
						dfc.ChangeDirections (mC.dir1, mC.dir2, mC.dir3, mC.dir4);
						//  disable almost all the scripts for this shape
						dS.DisableEverything ();
						//  spawn a new shape
						sC.PickShape ();
					}
					else if (meltEerythingDown.Length != 0)
					{
						dfc.ChangeDirections (mC.dir1, mC.dir2, mC.dir3, mC.dir4);
						//  disable almost all the scripts for this shape
						dS.DisableEverything ();
						int lastFloor = (int) System.Math.Round (transform.position.y, MidpointRounding.AwayFromZero);
						Instantiate (bioHazardLiquidHolder, transform.position, transform.rotation);

						StartCoroutine (WaitForBarrelsParticles (meltEerythingDown));
					}
					else
					{
						//  check if we fill some floors
						cubeCon.MoveTheCubes ();
						dfc.ChangeDirections (mC.dir1, mC.dir2, mC.dir3, mC.dir4);
						scCtrl.CheckIfAccomplishedTheLevel ();
						if (GUIMain.soundEffectMuteBool == false)
							audio.PlayOneShot(landingSound, 0.1f);
						dS.DisableEverything ();
						//  change the directions
						sC.PickShape ();
					}
				}
				else 
				{
					//  stop casting shadows (for each individual cube)
					cubeCon.StopCastingShadows ();
					//  stop moving one of the shapes by turning off the rigidbodies
					cubeCon.FreezeY ();
				}

			}
		}

		//  if we can fall down, turn on the rigidbodies
		if (changeGravity == true && currentState != PossibleStates.DontTouch)
		{
			foreach (CubeController cubeCon in cC)
			{
				if (cubeCon != null)
				{
					cubeCon.StopMovingLetsFall ();
				}
			}
		}

		if (this.transform.position.y < -1)
		{
			sC.PickShape ();
			Destroy (this);
		}

	}


	private IEnumerator WaitForBarrelsParticles (BioHazardMeltdown [] meltEerythingDown) {

		yield return new WaitForSeconds (1);
		LineScore lS = GameObject.Find ("Platform").GetComponent <LineScore> ();
		lS.Default ();
		for (int i = 0; i < 3; i ++)
		{
			meltEerythingDown [i].MeltEverythingDown ();
		}

		//instantiate the Bio hazard liquid particles

		Destroy (this.gameObject);
		
		lS.MoveCubesAfterMeltingDown ();
		//  spawn a new shape
		sC.PickShape ();

	}


	//  change directions
	void ChangeDirections () {
		dfc.ChangeDirections (mC.dir1, mC.dir2, mC.dir3, mC.dir4);
		SpawnCubes.switchMovements -= ChangeDirections;
	}

}

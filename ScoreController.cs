using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {

	public float Score = 0, FloorsCleared = 0, ScoreForFloor = 1000, ScoreForDrop = 10, FloorComboMultiplyer = 0, MultipleFloorCombo = 0;


	public float ScoreToAdd = 0;
	public bool ShowScore = false;
//	public float TimerToShowScore = 0;
	
	public AudioClip dropScore;
	public AudioClip FloorScore;

	private LineScore lS;


	void Start () {
		lS = GameObject.Find ("Platform").GetComponent <LineScore> ();
	}
	

	void Update () {
		if (ChangeableVariables.levelWeAreIn == "3x3_1" || ChangeableVariables.levelWeAreIn == "3x3_2" || ChangeableVariables.levelWeAreIn == "3x3_3") 
		{
			ScoreForDrop -= Time.deltaTime/3;
		} else
			ScoreForDrop -= Time.deltaTime/2;
	}



	public void FloorCleared () {
		FloorsCleared++;
		if (GUIMain.soundEffectMuteBool == false)
			audio.PlayOneShot (FloorScore, 0.5f);
//		FloorComboMultiplyer++;
		ScoreToAdd = ScoreForFloor * (FloorComboMultiplyer+MultipleFloorCombo);
		Score += ScoreToAdd;
		ShowScore = true;

		//  to fire the events when having a 4x4 / 5x5 grids
		CheckIfAccomplishedTheLevel ();

		//  to fire an event - 3x3 / second level is accomplished
		if (FloorsCleared == 5 && ChangeableVariables.levelWeAreIn == "3x3_2")
		{
			lS.Level2in3by3IsAccomplished ();
		}

		//Debug.Log (Score);
	}


	public void DropShape (){
		int roundedDropScore = (int)Mathf.Round (ScoreForDrop) * 10;
		ScoreToAdd = roundedDropScore;
		Score += ScoreToAdd;
		ShowScore = true;
		if (GUIMain.soundEffectMuteBool == false)
			audio.PlayOneShot (dropScore, 0.05f);
	}


	public void CheckIfAccomplishedTheLevel () {
		if (Score >= 10000 && Score < 20000)
		{
			if (ChangeableVariables.levelWeAreIn == "4x4_1")
			{
				lS.Level1in4by4IsAccomplished ();
			}
			if (ChangeableVariables.levelWeAreIn == "5x5_1")
			{
				lS.Level1in5by5IsAccomplished ();
			}
		}
		else if (Score >= 20000 && Score < 30000)
		{
			if (ChangeableVariables.levelWeAreIn == "4x4_2")
			{
				lS.Level2in4by4IsAccomplished ();
			}
			if (ChangeableVariables.levelWeAreIn == "5x5_2")
			{
				lS.Level2in5by5IsAccomplished ();
			}
		}
		else if (Score >= 30000)
		{
			if (ChangeableVariables.levelWeAreIn == "4x4_3")
			{
				lS.Level3in4by4IsAccomplished ();
			}
			if (ChangeableVariables.levelWeAreIn == "5x5_3")
			{
				lS.Level3in5by5IsAccomplished ();
			}
		}
	}

}

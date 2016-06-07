using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;


public class HighScore : MonoBehaviour {

	public List <int> highScoreList = new List<int> ();				// declare the highScoreList
	string serialisedDict = string.Empty;

	public GameObject highScore;
	public ScoreController sC;

	private string getCorrectList = ChangeableVariables.GetHighscoreList;


	public string displayNames = ChangeableVariables.filledNamesForHighscore;
	public string displayScore = string.Empty;
	private StringBuilder resultToDisplayScore = new StringBuilder ();
	public bool isPossibleToDisplayResults = false;




	void Start () {

		//  default results
		if (PlayerPrefs.HasKey (getCorrectList) == false)
		{
			AddExistingScore ();
			PlayerPrefs.SetString (getCorrectList, GetSerializedList (highScoreList));
			PlayerPrefs.Save ();
		}
		else
		{
			highScoreList = GetUnserializedList (PlayerPrefs.GetString (getCorrectList));
			TakeTenBestResults ();
		}
	}



	public void SetNewResult () {

		//  if it is a completely new result
		if (!(CheckIfValuesAndKeysMatch ()))
		{
			int newResult = (int) Mathf.RoundToInt (sC.Score);
			//  add to the list
			highScoreList.Add (newResult);
			//  take ten best results
			TakeTenBestResults ();
			//  save new new list without forgetting to serialize it
			PlayerPrefs.SetString (getCorrectList, GetSerializedList (highScoreList));
			PlayerPrefs.Save ();
		}

		//  show new result by saving everything in one string
		foreach (int kvp in highScoreList)
		{
			resultToDisplayScore.Append (kvp).Append (Environment.NewLine);
		}

		displayScore = resultToDisplayScore.ToString ();
		if (ChangeableVariables.levelWeAreIn.Contains ("_Free"))
		{
			isPossibleToDisplayResults = true;
		}
	}


	bool CheckIfValuesAndKeysMatch () {
		//  check if there's smth with the same result
		bool containsThatKey = highScoreList.Any (item => item == sC.Score);
		return containsThatKey;
	}


	void TakeTenBestResults () {
		//  take 10 highest results
		var temp = from entry in highScoreList orderby entry descending select entry;
		var temp10 = temp.Take (10);
		//  save as a list
		highScoreList = temp10.ToList ();
	}

	void AddExistingScore () {

		for (int i = 0; i < 12; i ++)
		{
			//highScoreList.Add (UnityEngine.Random.Range (0, 20000));
			highScoreList.Add (0);
		}
	}


	string GetSerializedList (List <int> d) {

		// Build up each line one-by-one and then trim the end
		System.Text.StringBuilder builder = new System.Text.StringBuilder();
		
		foreach (int pair in d)
		{
			builder.Append(pair).Append(":");
		}
		string result = builder.ToString();
		// Remove the final delimiter
		result = result.TrimEnd(':');
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

}

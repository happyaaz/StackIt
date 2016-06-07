using UnityEngine;
using System.Collections;
using System.Text;
using System;


//  Changing values within the scripts
public static class ChangeableVariables {
	

	public static int squaresToFill;
	public static int spawnFrom, spawnTo;
	public static float outXNeg, outXPos, outZNeg, outZPos;
	public static int CameraXZPos;
	public static Vector3 CamTargetForLevels;
	public static float LShapeX, LShapeZ,
		IShapeX,IShapeZ,
		OShapeX,OShapeZ,
		TShapeX, TShapeZ,
		ZShapeX,ZShapeZ,
		SShapeX,SShapeZ,
		YShapeX,YShapeZ,
		NShapeX,NShapeZ,
		OneShapeX,OneShapeZ,
		TwoShapeX,TwoShapeZ,
		ThreeShapeX,ThreeShapeZ;

	public static Vector3 topCornerAPos, topCornerBPos, topCornerCPos, topCornerDPos;
	public static Vector3 topCornerARot, topCornerBRot, topCornerCRot, topCornerDRot;

	public static Vector2 materialTiling;

	//Positioning of Objects
	public static Vector3 BorderAPos;
	public static Vector3 BorderBPos;
	public static Vector3 BorderCPos;
	public static Vector3 BorderDPos;
	public static Vector3 PlatformPos;
	public static Vector3 PlatformScale;
	public static Vector3 SpawnerPos;
	public static Vector3 CrusherAPos;
	public static Vector3 CrusherBPos;

	public static int SizeOfGrid;

	public static string GetHighscoreList = string.Empty;
	public static string filledNamesForHighscore;
	
	//activating level goals
	public static bool enabled3by3goals = false;
	public static bool enabled4by4goals = false;
	public static bool enabled5by5goals = false;

	public static string levelWeAreIn = string.Empty;

	//preLevelWindow
	public static string platformSizeString;

	public static void SizeIsFive () {

		squaresToFill = 25;
		spawnFrom = 0;
		spawnTo = 10;
		//  borders
		outXNeg = -2.1f;
		outXPos = 3.1f;
		outZNeg = -3.1f;
		outZPos = 2.1f;
		//  possible camera's positions
		CameraXZPos = 6;
		CamTargetForLevels = new Vector3 (0.5f, 5, -0.5f);

		//  initial positions for the objects to be spawned from
		LShapeX = 1; 
		LShapeZ = -1;
		IShapeX= 0;
		IShapeZ = -1;
		OShapeX = 0;
		OShapeZ = -1;
		TShapeX = 0;
		TShapeZ = -1;
		ZShapeX = 0;
		ZShapeZ = -1;
		SShapeX = 0;
		SShapeZ = 0;
		YShapeX = 0;
		YShapeZ = 0;
		NShapeX = 0;
		NShapeZ = 0;
		OneShapeX = 0;
		OneShapeZ = -1;
		TwoShapeX = 1;
		TwoShapeZ = 0;
		ThreeShapeX = 0;
		ThreeShapeZ = 0; 

		materialTiling = new Vector2 (5, 5);
		
		// Positioning of diff objects on the scene
		BorderAPos = new Vector3 (0.5f, -3f, 2.6f);
		BorderBPos = new Vector3 (0.5f, -3, -3.6f);
		BorderCPos = new Vector3 (3.6f, -3, -0.5f);
		BorderDPos = new Vector3 (-2.6f, -3, -0.5f);

		PlatformPos = new Vector3 (0.5f, 0, -0.5f);
		PlatformScale = new Vector3 (5, 1, 5);
		SpawnerPos = new Vector3 (0, 5, 0);

		CrusherAPos = new Vector3 (3.2f, 0, -0.5f);
		CrusherBPos = new Vector3 (-2.2f ,0, -0.5f);

		topCornerAPos = new Vector3 (-1.5f, 10, 1.5f);
		topCornerBPos = new Vector3 (-1.5f, 10, -2.5f);
		topCornerCPos = new Vector3 (2.5f, 10, 1.5f);
		topCornerDPos = new Vector3 (2.5f, 10, -2.5f);
//		topCornerARot = new Quaternion (0, 180, 0);
//		topCornerBRot = new Quaternion (0, 90, 0);
//		topCornerCRot = new Quaternion (0, 270, 0);
//		topCornerDRot = new Quaternion (0, 0, 0);

		//  highscore for each level depenging on the size of the grid
		GetHighscoreList = "5x5";
		FillTheNames ();

		enabled5by5goals = true;
		platformSizeString = "5x5";
	}


	public static void SizeIsFour () {
		//Changing values within the scripts
		squaresToFill = 16;
		spawnFrom = 0;
		spawnTo = 7;

		outXNeg = -1.1f;
		outXPos = 2.1f;
		outZNeg = -2.1f;
		outZPos = 1.1f;

		CameraXZPos = 5;  //  don't forget to change it
		CamTargetForLevels = new Vector3 (0.5f, 5, -0.5f);

		LShapeX = 0.5f; 
		LShapeZ = -0.5f;
		IShapeX= 0.5f;
		IShapeZ = -0.5f;
		OShapeX = 0.5f;
		OShapeZ = -0.5f;
		TShapeX = 0.5f;
		TShapeZ = -0.5f;
		ZShapeX = 0.5f;
		ZShapeZ = -0.5f;
		SShapeX = 0.5f;
		SShapeZ = -0.5f;
		YShapeX = 0.5f;
		YShapeZ = -0.5f;
		NShapeX = 0.5f;
		NShapeZ = -0.5f;
		OneShapeX = 0.5f;
		OneShapeZ = -0.5f;
		TwoShapeX = 0.5f;
		TwoShapeZ = 0.5f;
		ThreeShapeX = 0.5f;
		ThreeShapeZ = 0.5f; 

		materialTiling = new Vector2 (4, 4);

		//Positioning of Objects
		BorderAPos = new Vector3 (0.5f, -3f, 2.1f);
		BorderBPos = new Vector3 (0.5f, -3, -3.1f);
		BorderCPos = new Vector3 (3.1f, -3, -0.5f);
		BorderDPos = new Vector3 (-2.1f, -3, -0.5f);

		PlatformPos = new Vector3 (0.5f ,0, -0.5f);
		PlatformScale = new Vector3 (4, 1, 4);
		SpawnerPos = new Vector3 (0.5f, 5, -0.5f);

		CrusherAPos = new Vector3 (2.6f, 0, -0.5f);
		CrusherBPos = new Vector3 (-1.6f, 0, -0.5f);

		topCornerAPos = new Vector3 (-1f, 10, 1f);
		topCornerBPos = new Vector3 (-1f, 10, -2f);
		topCornerCPos = new Vector3 (2f, 10, 1f);
		topCornerDPos = new Vector3 (2f, 10, -2f);
//		topCornerARot = new Vector3 (0, 180, 0);
//		topCornerBRot = new Vector3 (0, 90, 0);
//		topCornerCRot = new Vector3 (0, 270, 0);
//		topCornerDRot = new Vector3 (0, 0, 0);

		GetHighscoreList = "4x4";
		FillTheNames ();
		//PlayerPrefs.DeleteAll ();
		//PlayerPrefs.Save ();
		enabled4by4goals = true;
		platformSizeString = "4x4";
	}


	public static void SizeIsThree () {
		//Changing values within the scripts
		squaresToFill = 9;
		spawnFrom = 0;
		spawnTo = 3;
		
		outXNeg = -0.6f;
		outXPos = 1.6f;
		outZNeg = -1.6f;
		outZPos = 0.6f;
		
		CameraXZPos = 4;  //  don't forget to change it
		CamTargetForLevels = new Vector3 (0.5f, 5, -0.5f);

		LShapeX = 0; 
		LShapeZ = 0;
		IShapeX= 0;
		IShapeZ = 0;
		OShapeX = 0;
		OShapeZ = 0;
		TShapeX = 0;
		TShapeZ = 0;
		ZShapeX = 0;
		ZShapeZ = 0;
		SShapeX = 0;
		SShapeZ = 0;
		YShapeX = 0;
		YShapeZ = 0;
		NShapeX = 0;
		NShapeZ = 0;
		OneShapeX = 0;
		OneShapeZ = -1;
		TwoShapeX = 1;
		TwoShapeZ = 0;
		ThreeShapeX = 0;
		ThreeShapeZ = 0; 
		
		materialTiling = new Vector2 (3, 3);
		
		//Positioning of Objects
		BorderAPos = new Vector3 (0.5f, -3f, 1.6f);
		BorderBPos = new Vector3 (0.5f, -3, -2.6f);
		BorderCPos = new Vector3 (2.6f, -3, -0.5f);
		BorderDPos = new Vector3 (-1.6f, -3, -0.5f);

		PlatformPos = new Vector3 (0.5f ,0, -0.5f);
		PlatformScale = new Vector3 (3, 1, 3);
		SpawnerPos = new Vector3 (0.5f, 5, -0.5f);

		CrusherAPos = new Vector3 (2.0f, 0, -0.5f);
		CrusherBPos = new Vector3 (-1.0f, 0, -0.5f);

		topCornerAPos = new Vector3 (-0.5f, 10, 0.5f);
		topCornerBPos = new Vector3 (-0.5f, 10, -1.5f);
		topCornerCPos = new Vector3 (1.5f, 10, 0.5f);
		topCornerDPos = new Vector3 (1.5f, 10, -1.5f);
//		topCornerARot = new Vector3 (0, 180, 0);
//		topCornerBRot = new Vector3 (0, 90, 0);
//		topCornerCRot = new Vector3 (0, 270, 0);
//		topCornerDRot = new Vector3 (0, 0, 0);

		GetHighscoreList = "3x3";
		FillTheNames ();
		//PlayerPrefs.DeleteAll ();
		//PlayerPrefs.Save ();
		enabled3by3goals = true;
		platformSizeString = "3x3";
	}


	public static void FillTheNames () {
		StringBuilder names = new StringBuilder ();

		for (int i = 0; i < 5; i ++)
		{
			if (i != 6)
				names.Append ("" + (i + 1) + ")   You :").Append (Environment.NewLine);
			else
				names.Append ("" + (i + 1) + ") You :").Append (Environment.NewLine);
		}

		filledNamesForHighscore = names.ToString ();
	}

}

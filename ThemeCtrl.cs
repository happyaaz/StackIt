using UnityEngine;
using System.Collections;

public class ThemeCtrl : MonoBehaviour {
	
	
	public GameObject spawnerJunkyard;
	public GameObject spawnerOrgTetris;
	public GameObject spawnerModern;
	public GameObject spawnerLight;
	public GameObject spawnerTronBlack;
	public GameObject spawnerTronGrid;
	public GameObject backGround;
	public Camera mainCamera;
	public GameObject tronBBackground;
	public GameObject tronWBackground;
	public GameObject modernBackground;
	public GameObject originalBackground;
	public GameObject platformGrid;
	public Texture TronBlackGrid;
	public Texture LightGrid;

	public GUISkin junkyard_GUISkin;
	
	// Use this for initialization
	
	void Awake () {
		if(StartFunction.modernTheme == true && gameObject.name != "SpawnerModern")
		{
			backGround.SetActive(false);
			modernBackground.SetActive(true);
			spawnerModern.SetActive (true);
			gameObject.SetActive (false);
		} 
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

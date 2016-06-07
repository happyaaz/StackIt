using UnityEngine;
using System.Collections;

//  move all the object on the scene how we nned it
public class MoveStuff : MonoBehaviour {

	public GameObject CrusherLeft,CrusherRight,BorderA,BorderB,BorderC,BorderD,Platform,PlatformGrid;
	public GameObject topCornerA, topCornerB, topCornerC, topCornerD;
	public Texture whiteTopCorner;


	void Start () {

		CrusherLeft.transform.position = ChangeableVariables.CrusherAPos;
		CrusherRight.transform.position = ChangeableVariables.CrusherBPos;
		BorderA.transform.position = ChangeableVariables.BorderAPos;
		BorderB.transform.position = ChangeableVariables.BorderBPos;
		BorderC.transform.position = ChangeableVariables.BorderCPos;
		BorderD.transform.position = ChangeableVariables.BorderDPos;
		Platform.transform.position = ChangeableVariables.PlatformPos;
		Platform.transform.localScale = ChangeableVariables.PlatformScale;
		PlatformGrid.renderer.material.mainTextureScale = ChangeableVariables.materialTiling;

		topCornerA.transform.position = ChangeableVariables.topCornerAPos;
		topCornerB.transform.position = ChangeableVariables.topCornerBPos;
		topCornerC.transform.position = ChangeableVariables.topCornerCPos;
		topCornerD.transform.position = ChangeableVariables.topCornerDPos;

//		topCornerA.transform.rotation = ChangeableVariables.topCornerARot;
//		topCornerB.transform.rotation = ChangeableVariables.topCornerBRot;
//		topCornerC.transform.rotation = ChangeableVariables.topCornerCRot;
//		topCornerD.transform.rotation = ChangeableVariables.topCornerDRot;

		if (StartFunction.tronBlackTheme==true || StartFunction.tronGridTheme==true){ 
			topCornerA.renderer.material.mainTexture = whiteTopCorner;
			topCornerB.renderer.material.mainTexture = whiteTopCorner;
			topCornerC.renderer.material.mainTexture = whiteTopCorner;
			topCornerD.renderer.material.mainTexture = whiteTopCorner;
		}
	}
}

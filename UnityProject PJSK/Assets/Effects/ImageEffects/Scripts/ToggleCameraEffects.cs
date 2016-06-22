using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class ToggleCameraEffects : MonoBehaviour {

    Tonemapping tonemapping;
    ColorCorrectionCurves colorCorrect;
    GlobalFog globalFog;


	// Use this for initialization
	void Start () {

        tonemapping = GetComponent<Tonemapping>();
        colorCorrect = GetComponent<ColorCorrectionCurves>();
        globalFog = GetComponent<GlobalFog>();
	}
	
	// Update is called once per frame
	public void EnterForest(bool enter)
    {
        tonemapping.enabled = enter;
        colorCorrect.enabled = enter;
        globalFog.enabled = enter;
    }
}


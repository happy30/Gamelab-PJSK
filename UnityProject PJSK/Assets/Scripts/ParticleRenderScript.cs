using UnityEngine;
using System.Collections;

public class ParticleRenderScript : MonoBehaviour {

	void Start () {
        GetComponent<Renderer>().material.renderQueue = 2800;
    }

}

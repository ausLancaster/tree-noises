using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terrain;

public class CharacterPlacer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        INoiseProvider noiseProvider = new SeussNoise0();
        transform.position = new Vector3(0, noiseProvider.GetValue(0, 0), 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terrain;

public class CharacterPlacer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        float height = 0.2f;
        if (height < 0.2f) height = 0.2f;
        transform.position = new Vector3(0, 0.98f + height, 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

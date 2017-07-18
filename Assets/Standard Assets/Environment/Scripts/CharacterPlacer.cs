using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terrain;

public class CharacterPlacer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.position = new Vector3(0, 0.98f + HeightMap.SeussLike(0, 0, 1 / 40.0f, 40.0f, 2.0f, 1 / 4.0f), 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

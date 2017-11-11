using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terrain;

public class CharacterPlacer : MonoBehaviour {

    public TerrainChunkGenerator tcg;
    private float relativeHeight = 0.98f;

	// Use this for initialization
	void Start () {
        Place();
    }
	
	// Update is called once per frame
	void LateUpdate () {
        if (Input.GetButtonDown("Generate"))
        {
            Place();
        }

    }

    private void Place()
    {
        transform.position = new Vector3(0, tcg.GetHeight(0, 0) + relativeHeight, 0);
    }
}
